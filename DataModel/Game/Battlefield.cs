using OceanBattle.DataModel.Game.Abstractions;
using OceanBattle.DataModel.Game.EnviromentElements;
using System.Reactive.Linq;
using System.Reactive.Subjects;

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
            if (!IsAllowedByLevel(ship))
                return false;

            int[][] transformation = CreateTransformationMatrix(ship.Orientation);

            for (int i = 0; i < ship.Width; i++)
                for (int j = 0; j < ship.Length; j++)
                    if (!VerifyCell((i, j), (x, y), transformation, out (int x, int y) _))
                        return false;

            return true;
        }

        public bool PlaceShip(int x, int y, Ship ship)
        {
            if (!IsAllowedByLevel(ship))
                return false;

            int[][] transformation = CreateTransformationMatrix(ship.Orientation);

            for (int i = 0; i < ship.Width; i++)
                for (int j = 0; j < ship.Length; j++)
                {
                    if (!VerifyCell((i, j), (x, y), transformation, out (int x, int y) vector))
                        return false;

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
                int i = cells[number].x - x;
                int j = cells[number].y - y;

                double distance = Math.Sqrt(j * j + i * i) + 1;

                int maxDamage = (int)(weapon.Damage / distance);
                int damage = rng.Next((int)Math.Floor(maxDamage * 0.75), maxDamage);

                Grid[cells[number].x][cells[number].y].Hit(damage);
            }

            EmitHit(x, y);

            return true;
        }        

        #region private helpers

        private (int x, int y) Add((int x, int y) vector1, (int x, int y) vector2) 
            => (vector1.x + vector2.x, vector1.y + vector2.y);

        private (int x, int y) Multiply((int x, int y) vector, int[][] matrix)
        {
            if (matrix.Length != 2 || matrix[0].Length != 2)
                return vector;

            return new (
                matrix[0][0] * vector.x + matrix[0][1] * vector.y, 
                matrix[1][0] * vector.x + matrix[1][1] * vector.y);
        }

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

        private bool IsAllowedByLevel(Ship ship) 
            => Level.AvailableTypes!.TryGetValue(ship.GetType(), out int maxAmount) && 
               _ships.Where(s => s.GetType() == ship.GetType()).Count() < maxAmount;
        
        private bool IsInBounds((int x, int y) vector) 
            => vector.x < Grid.Length && 
               vector.x >= 0 && 
               vector.y < Grid[0].Length &&
               vector.y >= 0;

        private bool VerifyCell(
            (int i, int j) positionVector, 
            (int x, int y) shifVector,
            int[][] matrix,
            out (int x, int y) vector)
        {
            vector = Add(Multiply(positionVector, matrix), shifVector);

            if (!IsInBounds(vector))
                return false;

            Cell cell = Grid[vector.x][vector.y];

            if (cell is null || cell.IsPopulated)
                return false;

            return true;
        }

        #endregion
    }
}
