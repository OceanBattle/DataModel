using Newtonsoft.Json;
using OceanBattle.DataModel.Game.Abstractions;
using OceanBattle.DataModel.Game.EnviromentElements;

namespace OceanBattle.DataModel.Game.Ships
{
    public class Destroyer : Warship
    {
        [JsonConstructor]
        public Destroyer(
            int width,
            int length,
            int armour,
            Armour[][] cells)
            : base(
                  width,
                  length,
                  armour,
                  cells)
        {
        }

        public Destroyer(int armour)
            : base(1, 4, armour)
        {
        }
    }
}
