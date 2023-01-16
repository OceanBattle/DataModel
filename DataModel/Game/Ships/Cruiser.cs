using Newtonsoft.Json;
using OceanBattle.DataModel.Game.Abstractions;
using OceanBattle.DataModel.Game.EnviromentElements;

namespace OceanBattle.DataModel.Game.Ships
{
    public class Cruiser : Warship
    {
        [JsonConstructor]
        public Cruiser(
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

        public Cruiser(int armour)
            : base(1, 5, armour)
        {
        }
    }
}
