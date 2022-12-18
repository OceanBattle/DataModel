using OceanBattle.DataModel.Game.Abstractions;
using OceanBattle.DataModel.Game.EnviromentElements;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Security.Cryptography.X509Certificates;

namespace OceanBattle.DataModel.Game
{
    public class Battlefield : IBattlefield
    {
        private Subject<(int x, int y)> _gotHit = new();
        public IObservable<(int x, int y)> GotHit => _gotHit.AsObservable();

        private List<Ship> _ships = new();
        public IEnumerable<Ship> Ships => _ships.AsEnumerable();

        public IEnumerable<Ship> AnonimizedShips => _ships
            .Where(ship => ship.IsDestroyed)
            .AsEnumerable();

        public Level Level { get; private set; }

        public User? Owner { get; set; }

        private bool _isReady;
        public bool IsReady
        {
            get => _isReady;
            set => _isReady = 
                value && Owner is not null;            
        }

        public Cell[][] Grid { get; private set; }

        public Cell[][] AnonimizedGrid => AnonimizeGrid();

        public Battlefield(Level level)
        {
            Level = level;

            if (Level.AvailableTypes is null)
                Level.AvailableTypes = new Dictionary<Type, int>();

            Grid = new Cell[level.BattlefieldSize][];

            for (int i = 0; i < level.BattlefieldSize; i++)
            {
                Grid[i] = new Cell[level.BattlefieldSize];

                for (int j = 0; j < level.BattlefieldSize; j++)
                    Grid[i][j] = new Water();
            }
        }

        public bool CanPlaceShip(int x, int y, Ship ship)
        {
            if (!Level.AvailableTypes!.TryGetValue(ship.GetType(), out int maxAmount))
                return false;

            if (_ships.Where(s => s.GetType() == ship.GetType()).Count() >= maxAmount)
                return false;

            int[][] transformation = CreateTransformationMatrix(ship.Orientation);

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
                }

            return true;
        }

        public bool PlaceShip(int x, int y, Ship ship)
        {
            if (!CanPlaceShip(x, y, ship))
                return false;

            int[][] transformation = CreateTransformationMatrix(ship.Orientation);

            for (int i = 0; i < ship.Width; i++)
                for (int j = 0; j < ship.Length; j++)
                {
                    (int x, int y) vector =
                        (i * transformation[0][0] + j * transformation[0][1] + x,
                         i * transformation[1][0] + j * transformation[1][1] + y);

                    Grid[vector.x][vector.y] = ship.Cells[i][j];
                }

            _ships.Add(ship);

            return true;
        }

        public bool CanBeHit(int x, int y)
        {
            bool inRange = x >= 0 && x < Grid.Length && y >= 0 && y < Grid[x].Length;
            
            if (!inRange ||
                AnonimizedShips.Count() == _ships.Count) 
                return false;

            Cell cell = Grid[x][y];

            if (cell.IsHit && 
                (cell is Water || cell is Land || (cell is Armour armour && armour.HP == 0)))
                return false;

            return true;
        }     

        public bool Hit(int x, int y, Weapon weapon)
        {
            if (!CanBeHit(x, y)) 
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

            EmitHit(x, y);

            return true;
        }        

        #region private helpers

        private int[][] CreateTransformationMatrix(Orientation orientation)
        {
            int[][] transformation =
            {
                new int[] { 1, 0 },
                new int[] { 0, 1 }
            };

            switch (orientation)
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

            return transformation;
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

        private void EmitHit(int x, int y)
        {
            _gotHit.OnNext((x, y));

            if (AnonimizedShips.Count() == _ships.Count)
                _gotHit.OnCompleted();            
        }
        #endregion
    }
}
