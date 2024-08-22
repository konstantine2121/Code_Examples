namespace Maze_Example
{
    public class Player
    {
        public Player(Vector startPosition)
        {
            Position = startPosition;
        }

        public Vector Position { get; private set; }

        public void TryMove(Vector direction, Maze maze)
        {
            if (maze == null)
            {
                throw new ArgumentNullException(nameof(maze));
            }

            if (!direction.IsValidDirection())
            {
                throw new InvalidOperationException($"invalid {nameof( direction)} value");
            }

            var nextPosition = Position + direction;
            var canMove = !maze.HasWall(nextPosition);

            if (canMove) 
            {
                Position = nextPosition;
            }
        }
    }
}
