using OceanBattle.DataModel.Game;
using OceanBattle.DataModel.Game.Ships;
using System.Collections;

namespace OceanBattle.DataModel.Tests.TestData
{
    internal class PlaceShipFailData : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[]
            {
                19, 18,
                new Corvette(100)
            };

            yield return new object[]
            {
                5, -1, 
                new Frigate(100)
            };

            yield return new object[]
            {
                -1, 6,
                new Frigate(100)      
            };

            yield return new object[]
            {
                20, 5,
                new Frigate(100)            
            };

            yield return new object[]
            {
                5, 20,
                new Frigate(100)
            };

            yield return new object[]
            {
                10, 16,
                new Battleship(100)
            };

            yield return new object[]
            {
                3, 3,
                new Battleship(100) { Orientation = Orientation.West }
            };

            yield return new object[]
            {
                5, 2,
                new Battleship(100) { Orientation = Orientation.South }
            };

            yield return new object[]
            {
                16, 7,
                new Battleship(100) { Orientation = Orientation.East }
            };
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();     
    }
}
