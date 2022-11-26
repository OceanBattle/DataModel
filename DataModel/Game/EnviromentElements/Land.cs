using OceanBattle.DataModel.Game.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OceanBattle.DataModel.Game.EnviromentElements
{
    public class Land : Cell
    {
        public Land() 
        {
            IsPopulated = true;
        }
    }
}
