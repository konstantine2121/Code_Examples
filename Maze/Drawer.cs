using System.Net.WebSockets;
using System.Text;

namespace Maze_Example
{
    internal class Drawer
    {
        private const string Empty = " ";
        private const string Wall = "█";
        private const string Exit = "░";
        private const string Player = "@";

        private Vector lastPlayerPosition;

        public void ClearScene()
        {
            Console.Clear();
        }

        public void ClearPlayer()
        {
            lastPlayerPosition.SetCursor();
            Console.Write(Empty);
        }

        public void Draw(Maze maze, Player player) 
        {
            ClearPlayer();
            DrawWalls(maze);
            DrawExits(maze);
            DrawPlayer(player);
            maze.MoveCursorUnderMaze();
        }

        private void DrawWalls(Maze maze)
        {
            Console.SetCursorPosition(0, 0);

            var sb = new StringBuilder();
            
            for (int row = 0; row < maze.Height; row++)
            {
                for (int column = 0; column < maze.Width; column++)
                {
                    var point = new Vector(column, row);
                    var ch = maze.HasWall(point) ? Wall : Empty;
                    sb.Append(ch);
                }
                sb.AppendLine();
            }

            Console.WriteLine(sb.ToString());
        }

        private void DrawExits(Maze maze)
        {
            foreach(var exit in maze.Exits)
            {
                exit.SetCursor();
                Console.Write(Exit);
            }
        }

        private void DrawPlayer(Player player)
        {
            player.Position.SetCursor();
            Console.Write(Player);
            lastPlayerPosition = player.Position;
        }
    }
}
