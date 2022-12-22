using OceanBattle.DataModel.Game;
using OceanBattle.DataModel.Game.Abstractions;
using OceanBattle.DataModel.Game.Ships;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OceanBattle.DataModel.Tests.TestData
{
    internal class HitSucceedData : IEnumerable<object[]>
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

        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[]
            {
                0, 0,
                level,
                new Weapon { Damage = 100, DamageRadius = 0 },
                (0, 0)
            };

            yield return new object[]
            {
                5, 5,
                level,
                new Weapon { Damage = 100, DamageRadius = 1 },
                (5, 4),
                (4, 5),
                (5, 5),
                (6, 5),
                (5, 6)
            };

            yield return new object[]
            {
                1, 1,
                level,
                new Weapon { Damage = 100, DamageRadius = 2 }, 
                (0, 0),
                (1, 0),
                (2, 0),
                (0, 1),
                (1, 1),
                (2, 1),
                (3, 1),
                (0, 2),
                (1, 2),
                (2, 2),
                (1, 3)
            };

            yield return new object[]
            {
                18, 18,
                level,
                new Weapon { Damage = 100, DamageRadius = 3 },
                (18, 15),
                (16, 16),
                (17, 16),
                (18, 16),
                (19, 16),
                (16, 17),
                (17, 17),
                (18, 17),
                (19, 17),
                (15, 18),
                (16, 18),
                (17, 18),
                (18, 18),
                (19, 18),
                (16, 19),
                (17, 19),
                (18, 19),
                (19, 19),
            };
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
