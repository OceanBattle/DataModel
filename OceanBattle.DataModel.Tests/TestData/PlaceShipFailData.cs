using OceanBattle.DataModel.Game;
using OceanBattle.DataModel.Game.Ships;
using System.Collections;

namespace OceanBattle.DataModel.Tests.TestData
{
    internal class PlaceShipFailData : IEnumerable<object[]>
    {
        private const int dimensions = 20;
        private readonly Level succeedLevel = new Level
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

        private readonly Level failLevel = new Level
        {
            BattlefieldSize = dimensions,
            AvailableTypes = new Dictionary<Type, int>
            {
                { typeof(Cruiser), 2 },
                { typeof(Frigate), 0 }
            }
        };

        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[]
            {
                19, 18,
                succeedLevel,
                new Corvette(100)
            };

            yield return new object[]
            {
                5, -1, 
                succeedLevel,
                new Frigate(100)
            };

            yield return new object[]
            {
                -1, 6,
                succeedLevel,
                new Frigate(100)      
            };

            yield return new object[]
            {
                20, 5,
                succeedLevel,
                new Frigate(100)            
            };

            yield return new object[]
            {
                5, 20,
                succeedLevel,
                new Frigate(100)
            };

            yield return new object[]
            {
                10, 16,
                succeedLevel,
                new Battleship(100)
            };

            yield return new object[]
            {
                3, 3,
                succeedLevel,
                new Battleship(100) { Orientation = Orientation.West }
            };

            yield return new object[]
            {
                5, 2,
                succeedLevel,
                new Battleship(100) { Orientation = Orientation.South }
            };

            yield return new object[]
            {
                16, 7,
                succeedLevel,
                new Battleship(100) { Orientation = Orientation.East }
            };

            yield return new object[]
            {
                10, 10,
                failLevel,
                new Corvette(100)
            };

            yield return new object[]
            {
                10, 10,
                failLevel,
                new Frigate(100)
            };
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();     
    }
}
