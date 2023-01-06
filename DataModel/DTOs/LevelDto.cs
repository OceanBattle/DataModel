using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OceanBattle.DataModel.DTOs
{
    public class LevelDto : BaseModel
    {
        public int BattlefieldSize { get; set; }
        public Dictionary<string, int>? AvailableTypes { get; set; }
    }
}
