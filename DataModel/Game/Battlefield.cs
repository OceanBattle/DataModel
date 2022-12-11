using OceanBattle.DataModel.Game.Abstractions;
using OceanBattle.DataModel.Game.EnviromentElements;

namespace OceanBattle.DataModel.Game
{
    public class Battlefield : IBattlefield
    {
        private List<Ship> _ships = new List<Ship>();
        public IEnumerable<Ship> Ships => _ships.AsEnumerable();

        public IEnumerable<Ship> AnonimizedShips => _ships
            .Where(ship => ship.IsDestroyed)
            .AsEnumerable();

        public User? Owner { get; set; }

        public Cell[][] Grid { get; private set; }

        public Cell[][] AnonimizedGrid => AnonimizeGrid();

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

        public bool PlaceShip(int x, int y, Ship ship)
        {
            int[][] transformation =
            {
                new int[] { 1, 0 },
                new int[] { 0, 1 }
            };

            switch (ship.Orientation)
            {
                case Orientation.West:
                    transformation[0] = new int[] { 0, -1 };
                    transformation[1] = new int[] { 1,  0 };
                    break;
                case Orientation.South:
                    transformation[0] = new int[] { -1,  0 };
                    transformation[1] = new int[] {  0, -1 };
                    break;
                case Orientation.East:
                    transformation[0] = new int[] {  0, 1 };
                    transformation[1] = new int[] { -1, 0 };
                    break;
                default:
                    break;
            }

            for (int i = 0; i < ship.Width; i++)
                for (int j = 0; j < ship.Length; j++)
                {
                    (int x, int y) vector =
                        (i * transformation[0][0] + j * transformation[0][1] + x,
                         i * transformation[1][0] + j * transformation[1][1] + y);

                    if (vector.x >= Grid.Length ||
                        vector.x < 0 ||
                        vector.y >= Grid[0].Length ||
                        vector.y < 0)
                        return false;

                    Cell cell = Grid[vector.x][vector.y];

                    if (cell is null || cell.IsPopulated)
                        return false;

                    Grid[vector.x][vector.y] = ship.Cells[i][j];
                }

            _ships.Add(ship);

            return true;
        }

        public bool Hit(int x, int y, Weapon weapon)
        {
            if (x < 0 || x >= Grid.Length ||
                y < 0 || y >= Grid[x].Length)
                return false;

            Random rng = new Random(Guid.NewGuid().GetHashCode());

            List<(int x, int y)> cells = new List<(int x, int y)>();

            for (int i = 0; i <= weapon.DamageRadius; i++)
                for (int j = 0; j * j + i * i <= weapon.DamageRadius * weapon.DamageRadius; j++)
                {
                    if (x + i >= 0 && x + i < Grid.Length &&
                        y + j >= 0 && x + j < Grid[x].Length)
                        cells.Add((x + i, y + j));

                    if (j != 0 && 
                        x + i >= 0 && x + i < Grid.Length &&
                        y - j >= 0 && x - j < Grid[x].Length)
                        cells.Add((x + i, y - j));
                    
                    if (i != 0 && 
                        x - i >= 0 && x - i < Grid.Length &&
                        y + j >= 0 && x + j < Grid[x].Length)
                        cells.Add((x - i, y + j));

                    if (i != 0 && j != 0 && 
                        x - i >= 0 && x - i < Grid.Length &&
                        y - j >= 0 && x - j < Grid[x].Length)
                        cells.Add((x - i, y - j));
                }

            int[] randomNumbers = new int[1 + cells.Count / 2];
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
                int i = cells[number].x;
                int j = cells[number].y;

                double distance = Math.Sqrt(j * j + i * i);

                if (distance < 1)
                    distance = 1;

                int maxDamage = (int)(weapon.Damage / distance);
                int damage = rng.Next((int)Math.Floor(maxDamage * 0.75), maxDamage);

                Grid[cells[number].x][cells[number].y].Hit(damage);
            }

            return true;
        }

        private Cell[][] AnonimizeGrid()
        {
            Cell[][] cells = new Cell[Grid.Length][];

            for (int i = 0; i < cells.Length; i++)
            {
                cells[i] = new Cell[Grid[i].Length];

                for (int j = 0; j < cells[i].Length; j++)
                {
                    Cell cell = Grid[i][j];

                    if (cell is Water || cell is Land || (cell is Armour && cell.IsHit))
                        cells[i][j] = cell;
                    else
                        cells[i][j] = new Water();
                }
            }
                
            return cells;
        }
    }
}
