using OceanBattle.DataModel.Game;
using OceanBattle.DataModel.Game.Abstractions;
using OceanBattle.DataModel.Game.EnviromentElements;
using OceanBattle.DataModel.Game.Ships;
using OceanBattle.DataModel.Tests.TestData;
using System.Reactive.Linq;

namespace OceanBattle.DataModel.Tests
{
    public class BattlefieldTests
    {
        [Theory]
        [ClassData(typeof(PlaceShipSucceedData))]
        public void PlaceShip_ShouldSucceed(
            int x,
            int y,
            Level level,
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
                    if (!cells.Any(c => c.x == i && c.y == j))
                        Assert.False(battlefield.Grid[i][j].IsPopulated);
        }

        [Theory]
        [ClassData(typeof(PlaceShipFailData))]
        public void PlaceShip_ShouldFail(
            int x,
            int y,
            Level level,
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
            Level level,
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

                    int i = element.x - x;
                    int j = element.y - y;

                    double distance = Math.Sqrt(i * i + j * j) + 1;

                    int maxDamage = (int)(weapon.Damage / distance);

                    if (cell is Armour armour)
                        Assert.True(armour.HP < armour.MaxHP && armour.HP >= armour.MaxHP - maxDamage);
                }
            }

            for (int i = 0; i < level.BattlefieldSize; i++)
                for (int j = 0; j < level.BattlefieldSize; j++)
                    if (!cells.Any(c => c.x == i || c.y == j))
                        Assert.False(battlefield.Grid[i][j].IsHit);

            Assert.True(hitCount == 1 + cells.Length / 2);
        }

        [Fact]
        public void GotHit_ShouldEmitOnCompleted()
        {
            // Arrange
            Level level = new Level
            {
                BattlefieldSize = 4,
                AvailableTypes = new Dictionary<Type, int>
                {
                    { typeof(Corvette), 1 },
                }
            };

            Weapon weapon = new Weapon
            {
                DamageRadius = 0,
                Damage = 200
            };

            Battlefield battlefield = new Battlefield(level);
            Corvette corvette = new Corvette(100);
            
            battlefield.PlaceShip(1, 1, corvette);
            
            bool completed = false;

            battlefield.GotHit.Subscribe((arg) => { }, () => completed = true);

            // Act
            battlefield.Hit(1, 1, weapon);
            battlefield.Hit(1, 2, weapon);

            // Assert
            Assert.True(completed);
        }

        [Theory]
        [ClassData(typeof(HitSucceedData))]
        public void GotHit_ShouldEmitOnNext(
            int x,
            int y,
            Level level,
            Weapon weapon,
            params (int x, int y)[] cells)
        {
            // Arrange
            Battlefield battlefield = new Battlefield(level);
            Battleship battleship = new Battleship(400);
            
            battlefield.PlaceShip(15, 15, battleship);

            (int x, int y) actual = (0, 0);
            battlefield.GotHit.Subscribe(x => actual = x);

            // Act
            
            battlefield.Hit(x, y, weapon);

            // Assert
            Assert.Equal(x, actual.x);
            Assert.Equal(y, actual.y);
        }

        [Theory]
        [ClassData(typeof(HitFailData))]
        public void Hit_ShouldFail(
            int x,
            int y,
            Level level,
            int hp,
            Dictionary<Ship, (int x, int y)> dataSets)
        {
            // Arrange
            Battlefield battlefield = CreatePopulatedBattlefield(level, hp, dataSets);
            Weapon weapon = new Weapon { Damage = hp, DamageRadius = 3 };

            battlefield.Hit(18, 18, weapon);

            // Act
            bool actual = battlefield.Hit(x, y, weapon);

            // Assert
            Assert.False(actual);
        }

        [Theory]
        [ClassData(typeof(AnonimizedSucceedData))]
        public void AnonimizedShips_ShouldSucceed(
            Level level,
            int hp,
            Dictionary<Ship, (int x, int y)> dataSets)
        {
            // Arrange 
            Battlefield battlefield =
                CreatePopulatedBattlefield(level, hp, dataSets);

            // Act 
            IEnumerable<Ship> actual = battlefield.AnonimizedShips.ToList();

            // Assert
            Assert.DoesNotContain(actual, ship => !ship.IsDestroyed);
            Assert.True(actual.Count() == 3);
        }

        [Theory]
        [ClassData(typeof(AnonimizedSucceedData))]
        public void AnonimizedGrid_ShouldSucceed(
            Level level,
            int hp,
            Dictionary<Ship, (int x, int y)> dataSets)
        {
            // Arrange
            Battlefield battlefield =
                CreatePopulatedBattlefield(level, hp, dataSets);

            // Act
            Cell[][] actual = battlefield.AnonimizedGrid;

            // Assert              
            Assert.DoesNotContain(
                actual.SelectMany(cells => cells),
                cell => cell is Armour armour && !armour.IsHit);
        }


        [Theory]
        [ClassData(typeof(HitSucceedData))]
        public void CanBeHit_ShouldBeTrue(
            int x,
            int y,
            Level level,
            params object[] prams)
        {
            // Arrange
            Battlefield battlefield = new Battlefield(level);
            Battleship battleship = new Battleship(400);
            battlefield.PlaceShip(15, 15, battleship);

            // Act
            bool actual = battlefield.CanBeHit(x, y);

            // Assert
            Assert.True(actual);
        }

        [Theory]
        [ClassData(typeof(HitFailData))]
        public void CanBeHit_ShouldBeFalse(
            int x,
            int y,
            Level level,
            int hp,
            Dictionary<Ship, (int x, int y)> dataSets)
        {
            // Arrange
            Battlefield battlefield = CreatePopulatedBattlefield(level, hp, dataSets);
            Weapon weapon = new Weapon { Damage = hp, DamageRadius = 3 };

            battlefield.Hit(18, 18, weapon);

            // Act
            bool actual = battlefield.CanBeHit(x, y);

            // Assert
            Assert.False(actual);
        }

        [Theory]
        [ClassData(typeof(PlaceShipSucceedData))]
        public void CanPlaceShip_ShouldBeTrue(
            int x,
            int y,
            Level level,
            Ship ship,
            params (int x, int y)[] cells)
        {
            // Arrange
            Battlefield battlefield = new Battlefield(level);

            // Act
            bool actual = battlefield.CanPlaceShip(x, y, ship);

            // Assert
            Assert.True(actual);
        }

        [Theory]
        [ClassData(typeof(PlaceShipFailData))]
        public void CanPlaceShip_ShouldBeFalse(
            int x,
            int y,
            Level level,
            Ship ship)
        {
            // Arrange
            Battlefield battlefield = new Battlefield(level);
            battlefield.PlaceShip(19, 16, new Destroyer(100));

            // Act
            bool actual = battlefield.CanPlaceShip(x, y, ship);

            // Assert
            Assert.False(actual);
        }

        [Fact]
        public void IsReady_ShouldBeTrue()
        {
            // Arrange
            Battlefield battlefield = IsReady_Arrange();

            // battlefield needs owner to be ready
            battlefield.Owner = new User();

            bool notified = false;
            bool status = false;
            battlefield.StatusChanged.Subscribe(s => 
            { 
                notified = true; 
                status = s; 
            });

            // Act 
            battlefield.IsReady = true;
            bool actual = battlefield.IsReady;

            // Assert
            Assert.True(actual);
            Assert.True(notified); 
            Assert.True(status);
        }

        [Fact]
        public void IsReady_ShouldBeFalse_NoOwner()
        {
            // Arrange
            Battlefield battlefield = IsReady_Arrange();

            bool notified = false;
            battlefield.StatusChanged.Subscribe(s => notified = true);

            // Act 
            battlefield.IsReady = true;
            bool actual = battlefield.IsReady;

            // Assert
            Assert.False(actual);
            Assert.False(notified);
        }

        [Fact]
        public void IsReady_ShouldBeFalse_NotAllShipsPlaced() 
        {
            // Arrange
            Level level = new Level
            {
                AvailableTypes = new Dictionary<Type, int>
                {
                    { typeof(Corvette), 2 },
                    { typeof(Frigate), 1 },
                },
                BattlefieldSize = 8
            };

            Dictionary<Ship, (int x, int y)> dataSet = new Dictionary<Ship, (int x, int y)>
            {
                { new Corvette(100), (0, 0) },
                { new Corvette(100), (4, 0) }
            };

            Battlefield battlefield = CreatePopulatedBattlefield(level, 0, dataSet);
            battlefield.Owner = new User();

            bool notified = false;
            battlefield.StatusChanged.Subscribe(s => notified = true);

            // Act
            battlefield.IsReady = true;
            bool actual = battlefield.IsReady;

            // Assert
            Assert.False(actual);  
            Assert.False(notified);
        }

        #region private helpers

        private Battlefield IsReady_Arrange()
        {
            // simple level for test
            Level level = new Level
            {
                AvailableTypes = new Dictionary<Type, int>
                {
                    { typeof(Corvette), 2 },
                    { typeof(Frigate), 1 },
                },
                BattlefieldSize = 8
            };

            // ships that fill level requirements, it needs to satisfy level to be ready
            Dictionary<Ship, (int x, int y)> dataSet = new Dictionary<Ship, (int x, int y)>
            {
                { new Corvette(100), (0, 0) },
                { new Corvette(100), (4, 0) },
                { new Frigate(100), (1, 4) }
            };

            // creates populated battlefield, it needs to be for it being ready
            Battlefield battlefield = CreatePopulatedBattlefield(level, 0, dataSet);    

            return battlefield;
        }

        private Battlefield CreatePopulatedBattlefield(
            Level level,
            int hp,
            Dictionary<Ship, (int x, int y)> dataSets)
        {
            Battlefield battlefield = new Battlefield(level);

            foreach (var dataSet in dataSets)
            {
                battlefield.PlaceShip(dataSet.Value.x, dataSet.Value.y, dataSet.Key);

                dataSet.Key.Cells.ToList()
                                 .ForEach(cells => cells.ToList().ForEach(cell => cell.Hit(hp)));
            }

            return battlefield;
        }

        #endregion
    }
}