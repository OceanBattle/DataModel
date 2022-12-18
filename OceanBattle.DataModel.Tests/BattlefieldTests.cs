using OceanBattle.DataModel.Game;
using OceanBattle.DataModel.Game.Abstractions;
using OceanBattle.DataModel.Game.EnviromentElements;
using OceanBattle.DataModel.Game.Ships;
using OceanBattle.DataModel.Tests.TestData;
using System.Drawing;
using System.Linq;
using System.Runtime.Serialization.Json;

namespace OceanBattle.DataModel.Tests
{
    public class BattlefieldTests
    {
        private const int dimensions = 20;
        private readonly Level level = new Level
        {
            BattlefieldSize = dimensions,
            AvailableTypes = new Dictionary<Type, int>
            {
                { typeof(Battleship), 5 },
                { typeof(Cruiser),    5 },
                { typeof(Destroyer),  5 },
                { typeof(Frigate),    5 },    
                { typeof(Corvette),   5 },
            }
        };

        [Theory]
        [ClassData(typeof(PlaceShipSucceedData))]
        public void PlaceShip_ShouldSucceed(
            int x,
            int y,
            Ship ship,
            params (int x, int y)[] cells)
        {
            // Arrange
            Battlefield battlefield = new Battlefield(level);

            // Act
            bool actual = battlefield.PlaceShip(x, y, ship);

            // Assert
            Assert.True(actual);
            Assert.Contains(ship, battlefield.Ships);

            foreach ((int m, int n) element in cells)
            {
                Cell cell = battlefield.Grid[element.m][element.n];
                Assert.True(cell.IsPopulated);
            }

            for (int i = 0; i < battlefield.Grid.Length; i++)
                for (int j = 0; j < battlefield.Grid[i].Length; j++)
                {
                    if (!cells.Any(c => c.x == i && c.y == j))
                        Assert.False(battlefield.Grid[i][j].IsPopulated);
                }
        }

        [Theory]
        [ClassData(typeof(PlaceShipFailData))]
        public void PlaceShip_ShouldFail(
            int x,
            int y,
            Ship ship)
        {
            // Arrange
            Battlefield battlefield = new Battlefield(level);

            battlefield.PlaceShip(19, 16, new Destroyer(100));

            // Act
            bool actual = battlefield.PlaceShip(x, y, ship);

            // Assert
            Assert.False(actual);
        }

        [Theory]
        [ClassData(typeof(HitSucceedData))]
        public void Hit_ShouldSucceed(
            int x,
            int y,
            Weapon weapon,
            params (int x, int y)[] cells)
        {
            // Arrange
            Battlefield battlefield = new Battlefield(level);
            Battleship battleship = new Battleship(400);
            battlefield.PlaceShip(15, 15, battleship);

            // Act
            bool actual = battlefield.Hit(x, y, weapon);

            // Assert
            Assert.True(actual);
            Assert.True(battlefield.Grid[x][y].IsHit);

            int hitCount = 0;

            foreach ((int x, int y) element in cells)
            {
                Cell cell = battlefield.Grid[element.x][element.y];

                if (cell.IsHit)
                {
                    hitCount++;

                    double distance = Math.Sqrt(x * x + y * y);

                    if (distance < 1)
                        distance = 1;

                    int maxDamage = (int)(weapon.Damage / distance);

                    if (cell is Armour armour)
                        Assert.True(armour.HP < armour.MaxHP && armour.HP >= armour.MaxHP - maxDamage);
                }
            }

            for (int i = 0; i < dimensions; i++)
                for (int j = 0; j < dimensions; j++)
                {
                    if (!cells.Any(c => c.x == i || c.y == j))
                        Assert.False(battlefield.Grid[i][j].IsHit);
                }

            Assert.True(hitCount == 1 + cells.Length / 2);
        }

        [Theory]
        [InlineData(22, 25)]
        [InlineData(-1, 10)]
        [InlineData(20, 19)]
        public void Hit_ShouldFail(int x, int y)
        {
            // Arrange
            Battlefield battlefield = new Battlefield(level);
            Weapon weapon = new Weapon { Damage = 100, DamageRadius = 3 };

            // Act
            bool actual = battlefield.Hit(x, y, weapon);

            // Assert
            Assert.False(actual);
        }

        [Fact]
        public void AnonimizedShips_ShouldSucceed()
        {
            // Arrange 
            Battlefield battlefield = new Battlefield(level);
            PopulateBattlefield(battlefield);

            // Act 
            IEnumerable<Ship> actual = battlefield.AnonimizedShips.ToList();

            // Assert
            Assert.DoesNotContain(actual, ship => !ship.IsDestroyed);
            Assert.True(actual.Count() == 3);
        }

        [Fact]
        public void AnonimizedGrid_ShouldSucceed()
        {
            // Arrange
            Battlefield battlefield = new Battlefield(level);
            PopulateBattlefield(battlefield);

            // Act
            Cell[][] actual = battlefield.AnonimizedGrid;

            // Assert              
            Assert.DoesNotContain(actual.SelectMany(cells => cells), cell => cell is Armour armour && !armour.IsHit);
        }

        private void PopulateBattlefield(Battlefield battlefield)
        {
            int hp = 100;
            Dictionary<Ship, (int x, int y)> dataSets = new Dictionary<Ship, (int x, int y)>
            {
                { new Battleship(2 * hp), (0, 0) },
                { new Destroyer(hp), (2, 10) },
                { new Frigate(2 * hp), (5, 3) },
                { new Frigate(hp), (6, 10) },
                { new Corvette(2 * hp), (8, 5) },
                { new Corvette(hp), (12, 13) }
            };

            dataSets.AsParallel().ForAll(dataSet =>
            {
                battlefield.PlaceShip(dataSet.Value.x, dataSet.Value.y, dataSet.Key);

                dataSet.Key.Cells
                .ToList()
                .ForEach(cells => cells.ToList().ForEach(cell => cell.Hit(hp)));
            });
        }
    }
}