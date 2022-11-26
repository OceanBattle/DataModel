using OceanBattle.DataModel.Game.Abstractions;
using OceanBattle.DataModel.Game.EnviromentElements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OceanBattle.DataModel.Game
{
    public class Battlefield
    {
        private List<Ship> _ships = new List<Ship>();
        public IEnumerable<Ship> Ships => _ships.AsEnumerable();

        public Cell[][] Grid { get; private set; }

        public Battlefield(int m, int n)
        {
            Grid = new Cell[m][];

            for (int i = 0; i < m; i++)
            {
                Grid[i] = new Cell[n];

                for (int j = 0; j < n; j++)
                    Grid[i][j] = new Water();
            }
        }

        public bool PlaceShip(int m, int n, Ship ship)
        {
            for (int i = 0; i < ship.Width; i++)
                for (int j = 0; j < ship.Length; j++)
                {
                    Cell cell = Grid[i + m][j + n];

                    if (cell.IsPopulated)
                        return false;

                    Grid[i + m][j + n] = ship.Cells[i][j];
                }

            return true;
        }

        public void Hit(int m, int n, Weapon weapon)
        {
            Random rng = new Random(Guid.NewGuid().GetHashCode());

            List<(int m, int n)> cells = new List<(int m, int n)>();

            for (int i = 0; i <= weapon.DamageRadius; i++)
                for (int j = 0; j * j + i * i <= weapon.DamageRadius * weapon.DamageRadius; j++)
                {
                    cells.Add((m + i, n + j));

                    if (i != 0)
                        cells.Add((m + i, n - j));
                    
                    if (j != 0)
                        cells.Add((m - i, n + j));

                    if (i != 0 && j != 0)
                        cells.Add((m - i, n - j));
                }

            int[] randomNumbers = new int[cells.Count / 2];
            randomNumbers[0] = 0;

            int count = 1;

            for (int i = 1; i < cells.Count && count < randomNumbers.Length; i++)
            {
                int toFind = randomNumbers.Length - count;
                int remaining = cells.Count - i;

                if (rng.Next(0, remaining) < toFind)
                    randomNumbers[count++] = i;
            }

            foreach (int number in randomNumbers)
            {
                int i = cells[number].m;
                int j = cells[number].n;

                int maxDamage = (int)(weapon.Damage / Math.Sqrt(j * j + i * i));
                int damage = rng.Next((int)Math.Floor(maxDamage * 0.75), maxDamage);

                Grid[cells[number].m][cells[number].n].Hit(damage);
            }
        }
    }
}
