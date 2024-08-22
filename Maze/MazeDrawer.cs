using System.Net.WebSockets;

namespace Maze_Example
{
    internal class MazeDrawer
    {
        private const string Empty = " ";
        private const string Wall = "#";
        //private const string Wall = "█";
        //private const string Exit = "░";
        private const string Exit = "e";
        private const string Player = "P";
        //private const string Player = "☺";

        public void Clear()
        {
            Console.Clear();
        }

        public void Draw(Maze maze, Player player) 
        {
            Clear();
            DrawWalls(maze);
            DrawExits(maze);
            DrawPlayer(player);
        }

        private void DrawWalls(Maze maze)
        {
            Console.SetCursorPosition(0, 0);

            for (int y = 0; y < maze.Height; y++) 
            {
                for(int x = 0; x < maze.Width; x++) 
                {
                    var wall = maze.HasWall(new Vector(x, y));

                    Console.Write(wall ? Wall : Empty);
                }
            }
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
        }
    }
}
