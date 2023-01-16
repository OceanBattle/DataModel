using Newtonsoft.Json;
using OceanBattle.DataModel.Game.Abstractions;
using OceanBattle.DataModel.Game.EnviromentElements;

namespace OceanBattle.DataModel.Game.Ships
{
    public class Corvette : Warship
    {
        [JsonConstructor]
        public Corvette(
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

        public Corvette(int armour)
            : base(1, 2, armour)
        {
        }
    }
}
