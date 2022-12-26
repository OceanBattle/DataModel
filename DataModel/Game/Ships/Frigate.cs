using OceanBattle.DataModel.Game.Abstractions;

namespace OceanBattle.DataModel.Game.Ships
{
    public class Frigate : Warship
    {
        public Frigate(int armour) 
            : base(1, 3, armour)
        {
        }
    }
}
