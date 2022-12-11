using OceanBattle.DataModel.Game.Abstractions;

namespace OceanBattle.DataModel.Game.Ships
{
    public class Corvette : Warship
    {
        public Corvette(int armour)
            : base(1, 2, armour)
        {
        }
    }
}
