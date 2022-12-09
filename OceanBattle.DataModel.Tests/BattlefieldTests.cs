using OceanBattle.DataModel.Game;
using OceanBattle.DataModel.Game.Abstractions;
using OceanBattle.DataModel.Game.EnviromentElements;
using OceanBattle.DataModel.Game.Ships;
using OceanBattle.DataModel.Tests.TestData;
using System.Drawing;

namespace OceanBattle.DataModel.Tests
{
    public class BattlefieldTests
    {
        [Theory]
        [ClassData(typeof(PlaceShipSucceedData))]
        public void PlaceShip_ShouldSucceed(
            int x, 
            int y, 
            Ship ship,
            params (int x, int y)[] cells)
        {
            // Arrange
            int dimension = 20;
            Battlefield battlefield = new Battlefield(dimension, dimension);

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
            int dimension = 20;
            Battlefield battlefield = new Battlefield(dimension, dimension);

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
            int dimensions = 20;
            Battlefield battlefield = new Battlefield(dimensions, dimensions);
            Battleship battleship = new Battleship(400);
            battlefield.PlaceShip(15, 18, battleship);

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
        public void hit_ShouldFail(int x, int y)
        {
            // Arrange
            int dimensions = 20;
            Battlefield battlefield = new Battlefield(dimensions, dimensions);
            Weapon weapon = new Weapon { Damage = 100, DamageRadius = 3 };

            // Act
            bool actual = battlefield.Hit(x, y, weapon);

            // Assert
            Assert.False(actual);
        }
    }
}