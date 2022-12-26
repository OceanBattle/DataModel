using OceanBattle.DataModel.Game.Abstractions;

namespace OceanBattle.DataModel.Game.Ships
{
    public class Destroyer : Warship
    {
        public Destroyer(int armour)
            : base(1, 4, armour)
        {
        }
    }
}
