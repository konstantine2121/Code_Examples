using System;
using System.Data.Common;
using System.Runtime.CompilerServices;

namespace Maze_Example
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");
        }
    }

    public class Game : IDisposable
    {
        public void Start()
        {

        }

        public void Exit()
        {
            Dispose();
        }

        public void Dispose()
        {
            
        }
    }

    public static class PointExtensions
    {
        public static void MoveCursor(this Vector point)
        {
            Console.SetCursorPosition(point.X, point.Y);
        }
    }
}
