using OceanBattle.DataModel.Game.Abstractions;

namespace OceanBattle.DataModel.DTOs
{
    public class BattlefieldDto
    {
        public Cell[][]? Grid { get; set; }
        public IEnumerable<Ship>? Ships { get; set; }
    }
}
