using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OceanBattle.DataModel.Game.EnviromentElements;

namespace OceanBattle.DataModel.Game.Abstractions
{
    public abstract class Ship
    {
        public Orientation Orientation { get; set; }
        public int Width { get; private set; }
        public int Length { get; private set; }
        public Armour[][] Cells { get; private set; }

        public Ship(
            int width, 
            int length,
            int armour)
        {
            Width = width;
            Length = length;

            Cells = new Armour[width][];

            for (int i = 0; i < width; i++)
            {
                Cells[i] = new Armour[length];

                for (int j = 0; j < length; j++)
                    Cells[i][j] = new Armour(armour);
            }
        }
    }
}
