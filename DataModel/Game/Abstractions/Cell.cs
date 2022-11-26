using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OceanBattle.DataModel.Game.Abstractions
{
    public abstract class Cell
    {
        public bool IsPopulated { get; internal set; }
        public bool IsHit { get; private set; }

        public virtual void Hit(int damage)
        {
            IsHit = damage > 0;
        }
    }
}
