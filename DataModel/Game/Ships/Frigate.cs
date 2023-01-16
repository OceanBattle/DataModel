using Newtonsoft.Json;
using OceanBattle.DataModel.Game.Abstractions;
using OceanBattle.DataModel.Game.EnviromentElements;

namespace OceanBattle.DataModel.Game.Ships
{
    public class Frigate : Warship
    {
        [JsonConstructor]
        public Frigate(
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

        public Frigate(int armour) 
            : base(1, 3, armour)
        {
        }
    }
}
