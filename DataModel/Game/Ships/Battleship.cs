using OceanBattle.DataModel.Game.Abstractions;

namespace OceanBattle.DataModel.Game.Ships
{
    public class Battleship : Warship
    {
        public Battleship(int armour)
            : base(2, 5, armour)
        {
        }
    }
}
