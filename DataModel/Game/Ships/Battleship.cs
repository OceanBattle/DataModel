using OceanBattle.DataModel.Game.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OceanBattle.DataModel.Game.Ships
{
    public class Battleship : Warship
    {
        public Battleship(int armour)
            : base(2, 5, armour)
        {
        }
    }
}
