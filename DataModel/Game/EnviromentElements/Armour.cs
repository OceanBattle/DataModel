using OceanBattle.DataModel.Game.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OceanBattle.DataModel.Game.EnviromentElements
{
    public class Armour : Cell
    {
        public int MaxHP { get; private set; }

        public int HP { get; private set; }

        public Armour(int hp)        {
            MaxHP = hp;
            HP = hp;
            IsPopulated = true;
        }

        public override void Hit(int damage)
        {
            if (HP > damage)
                HP -= damage;
            else
                HP = 0;

            base.Hit(damage);
        }
    }
}
