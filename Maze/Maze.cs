namespace Maze_Example
{
    public class Maze : IDisposable
    {
        #region Fields

        private bool[,] _walls;
        private Vector[] _exits;
        private readonly Vector _playerStart;

        #endregion Fields

        #region Ctor

        ///<summary>
        /// Карта лабиринта загруженная через <see cref="MazeLoader"/>
        ///</summary>
        public Maze(int width, int height, bool[,] walls, Vector playerStart, Vector[] exits)
        {
            ValidateArgs(width, height, walls,playerStart, exits);

            Width = width;
            Height = height;
            _walls = walls;
            _playerStart = playerStart;
            _exits = exits;
        }

        private static void ValidateArgs(int width, int height, bool[,] walls, Vector playerStart, Vector[] exits)
        {
            if (width <= 0)
                throw new ArgumentOutOfRangeException(nameof(width));

            if (height <= 0)
                throw new ArgumentOutOfRangeException(nameof(height));

            if (walls == null)
                throw new ArgumentNullException(nameof(walls));

            if (exits == null)
                throw new ArgumentNullException(nameof(exits));

            if (width < walls.GetLength(1))
                throw new InvalidOperationException($"{nameof(width)} != {nameof(walls)} num of columns");

            if (height < walls.GetLength(0))
                throw new InvalidOperationException($"{nameof(height)} != {nameof(walls)} num of rows");

            if (!exits.Any())
            {
                throw new InvalidOperationException($"{exits} must contains at least one element");
            }

            if (playerStart.OutOfBound(width, height))
            {
                throw new ArgumentOutOfRangeException($"{playerStart} out of bounds");
            }

            if (exits.Any(exit => exit.OutOfBound(width, height)))
            {
                throw new ArgumentOutOfRangeException("exit out of bounds");
            }
        }

        #endregion Ctor

        #region Properties

        public int Width { get; }

        public int Height { get; }

        public Vector PlayerStart => _playerStart;

        #endregion Properties

        #region PublicMethods

        public bool HasWall(Vector point)
        {
            return _walls[point.Y, point.X];
        }

        public bool IsExit(Vector point)
        {
            return _exits.Any(exit => exit == point);
        }

        public bool OutOfBound(Vector vector)
        {
            return vector.OutOfBound(Width, Height);
        }

        public void Dispose()
        {
            _walls = null;
            _exits = null;
        }

        #endregion PublicMethods
    }
}
