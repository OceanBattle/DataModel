using OceanBattle.DataModel.Game.Ships;
using OceanBattle.DataModel.Game;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using OceanBattle.DataModel.Game.Abstractions;

namespace OceanBattle.DataModel.Tests.TestData
{
    internal class HitFailData : IEnumerable<object[]>
    {
        private const int hp = 100;
        private Dictionary<Ship, (int x, int y)> dataSets => new Dictionary<Ship, (int x, int y)>
        {
            { new Battleship(2 * hp), (0, 0) },
            { new Destroyer(hp), (2, 10) },
            { new Frigate(2 * hp), (5, 3) },
            { new Frigate(hp), (6, 10) },
            { new Corvette(2 * hp), (8, 5) },
            { new Corvette(hp), (12, 13) }
        };

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

        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[] 
            { 
                22, 25, level, hp, dataSets
            };

            yield return new object[]
            {
                -1, 10, level, hp, dataSets
            };

            yield return new object[]
            {
                20, 19, level, hp, dataSets
            };

            yield return new object[]
            {
                12, 13, level, hp, dataSets
            };

            yield return new object[]
            {
                18, 18, level, hp, dataSets
            };
        }

        IEnumerator IEnumerable.GetEnumerator() 
            => GetEnumerator();
    }
}
