using OceanBattle.DataModel.Game.Abstractions;

namespace OceanBattle.DataModel.Game.Ships
{
    public class Cruiser : Warship
    {
        public Cruiser(int armour)
            : base(1, 5, armour)
        {
        }
    }
}
