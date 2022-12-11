using OceanBattle.DataModel.Game.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OceanBattle.DataModel.Game
{
    public class Level : BaseModel
    {
        public int BattlefieldSize { get; set; }
        public IEnumerable<(Type type, int maxAmount)>? AvailableTypes { get; set; }
    }
}
