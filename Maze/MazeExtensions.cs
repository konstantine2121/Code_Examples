using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace Maze_Example
{
    public static class MazeExtensions
    {
        public static void MoveCursorUnderMaze(this Maze maze)
        {
            Console.SetCursorPosition(0, maze.Height + 1);
        }
    }
}
