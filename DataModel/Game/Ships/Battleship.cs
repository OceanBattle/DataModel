using Newtonsoft.Json;
using OceanBattle.DataModel.Game.Abstractions;
using OceanBattle.DataModel.Game.EnviromentElements;

namespace OceanBattle.DataModel.Game.Ships
{
    public class Battleship : Warship
    {
        [JsonConstructor]
        public Battleship(
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

        public Battleship(int armour)
            : base(2, 5, armour)
        {
        }
    }
}
