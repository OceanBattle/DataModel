using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OceanBattle.DataModel.Game.Abstractions
{
    public abstract class Warship : Ship
    {        
        public IEnumerable<Weapon>? Weapons { get; set; }

        public Warship(
            int width,
            int length,
            int armour)
            : base(
                  width, 
                  length, 
                  armour) 
        {
        }
    }
}
