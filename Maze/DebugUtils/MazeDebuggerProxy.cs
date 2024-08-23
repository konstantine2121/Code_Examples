using System.Text;

namespace Maze_Example.DebugUtils
{
    internal class MazeDebuggerProxy
    {
        public MazeDebuggerProxy(Maze maze)
        {
            Maze = maze;
        }

        public Maze Maze { get; }

        public string Walls
        {
            get
            {
                var sb = new StringBuilder();
                char empty = ' ';
                char wall = '#';

                for (int row = 0; row < Maze.Height; row++)
                {
                    for (int column = 0; column < Maze.Width; column++)
                    {
                        var point = new Vector(column, row);
                        var ch = Maze.HasWall(point) ? wall : empty;
                        sb.Append(ch);
                    }
                    sb.AppendLine();
                }

                return sb.ToString();
            }
        }
    }
}
