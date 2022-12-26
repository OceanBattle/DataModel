using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OceanBattle.DataModel.Game.Abstractions
{
    public class Weapon : BaseModel
    {
        public int DamageRadius { get; set; }
        public int Damage { get; set; }
    }
}
