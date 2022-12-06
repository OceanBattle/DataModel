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
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
