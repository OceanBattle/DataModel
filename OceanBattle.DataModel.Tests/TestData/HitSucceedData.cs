using OceanBattle.DataModel.Game.Abstractions;
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
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[]
            {
                0, 0,
                new Weapon { Damage = 100, DamageRadius = 0 },
                (0, 0)
            };

            yield return new object[]
            {
                5, 5,
                new Weapon { Damage = 100, DamageRadius = 1 },
                (5, 5), (4, 5),
                (6, 5), (5, 4),
                (5, 6)
            };

            yield return new object[]
            {
                1, 1,
                new Weapon { Damage = 100, DamageRadius = 2 }, 
                (0, 0), (0, 1), 
                (0, 2), (1, 0), 
                (1, 1), (1, 2), 
                (1, 3), (2, 0), 
                (2, 1), (2, 2),
                (3, 1)
            };

            yield return new object[]
            {
                18, 18,
                new Weapon { Damage = 100, DamageRadius = 3 },
                (18, 14), (16, 15), (16, 17), (15, 18), 
                (17, 15), (17, 17), (16, 18), (17, 19),
                (18, 15), (18, 17), (17, 18), (18, 19),
                (19, 15), (19, 17), (18, 18), (19, 19),
                (19, 18), (16, 19),
            };
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
