using OceanBattle.DataModel.Game.EnviromentElements;

namespace OceanBattle.DataModel.Game.Abstractions
{
    public abstract class Ship : BaseModel
    {
        public Orientation Orientation { get; set; }
        public int Armour { get; private set; }
        public int Width { get; private set; }
        public int Length { get; private set; }
        public Armour[][] Cells { get; private set; }

        public bool IsDestroyed 
            => !Cells.SelectMany(a => a)
                     .Any(c => c.HP != 0);

        public Ship(
            int width, 
            int length,
            int armour)
        {
            Width = width;
            Length = length;
            Armour = armour;

            Cells = new Armour[width][];

            for (int i = 0; i < width; i++)
            {
                Cells[i] = new Armour[length];

                for (int j = 0; j < length; j++)
                    Cells[i][j] = new Armour(armour);
            }
        }

        public string? UserId { get; set; }
        public User? User { get; set; }
    }
}
