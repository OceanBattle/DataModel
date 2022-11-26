using OceanBattle.DataModel.Game.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OceanBattle.DataModel.Game.Ships
{
    public class Corvette : Warship
    {
        public Corvette(int armour)
            : base(1, 2, armour)
        {
        }
    }
}
