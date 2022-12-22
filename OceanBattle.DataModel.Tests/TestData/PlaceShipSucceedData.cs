using OceanBattle.DataModel.Game;
using OceanBattle.DataModel.Game.Ships;
using System.Collections;

namespace OceanBattle.DataModel.Tests.TestData
{
    internal class PlaceShipSucceedData : IEnumerable<object[]>
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
                { typeof(Frigate),    1 },
                { typeof(Corvette),   5 },
            }
        };

        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[]
            {
                5, 5, 
                level,
                new Frigate(100), 
                (5, 5), 
                (5, 6),
                (5, 7)
            };

            yield return new object[]
            {
                5, 5,
                level,
                new Frigate(100) { Orientation = Orientation.West },                
                (5, 5),
                (4, 5), 
                (3, 5)
            };

            yield return new object[]
            {
                5, 5,
                level,
                new Frigate(100) { Orientation = Orientation.South },                
                (5, 5),
                (5, 4),
                (5, 3)
            };

            yield return new object[]
            {
                5, 5,
                level,
                new Frigate(100) { Orientation = Orientation.East },                
                (5, 5),
                (6, 5),
                (7, 5)
            };

            yield return new object[]
            {
                5, 7,
                level,
                new Battleship(100),                
                (5, 7),  (6, 7),
                (5, 8),  (6, 8),
                (5, 9),  (6, 9),
                (5, 10), (6, 10),
                (5, 11), (6, 11)
            };

            yield return new object[]
            {
                5, 7,
                level,
                new Battleship(100) { Orientation = Orientation.West },                
                (5, 7), (5, 8),
                (4, 7), (4, 8),
                (3, 7), (3, 8),
                (2, 7), (2, 8),
                (1, 7), (1, 8)
            };

            yield return new object[]
            {
                5, 7,
                level,
                new Battleship(100) { Orientation = Orientation.South },                
                (5, 7), (4, 7),
                (5, 6), (4, 6),
                (5, 5), (4, 5),
                (5, 4), (4, 4),
                (5, 3), (4, 3)
            };

            yield return new object[]
            {
                5, 7,
                level,
                new Battleship(100) { Orientation = Orientation.East },                
                (5, 7), (5, 6),
                (6, 7), (6, 6),
                (7, 7), (7, 6),
                (8, 7), (8, 6),
                (9, 7), (9, 6)
            };
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();     
    }
}
