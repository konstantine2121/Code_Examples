using System;
using System.Data.Common;
using System.Runtime.CompilerServices;

namespace Maze
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

    public class Maze
    {
        private readonly bool[,] _walls;
        private readonly Point _exit;

        public Maze(int width, int height) 
        {
            if (width <= 0)
                throw new ArgumentOutOfRangeException(nameof(width));

            if (height <= 0)
                throw new ArgumentOutOfRangeException(nameof(height));

            Width = width;
            Height = height;

            _walls = new bool[Height, Width];
        }

        public Maze(string pathToMap)
        {
            
        }

        public int Width { get; }
        public int Height { get; }

        public bool HasWall(Point point)
        {
            return _walls[point.Y, point.X];
        }

        public bool IsExit(Point point)
        {
            return _exit == point;
        }
    }

    public struct Point
    {
        public Point(int x, int y) : this()
        {
            X = x;
            Y = y;
        }

        public int X { get; set; }

        public int Y { get; set; }

        public static Point operator + (Point point1, Point point2)
        {
            return new Point(
                point1.X + point2.X,
                point1.Y + point2.Y);
        }

        public static Point operator - (Point point1, Point point2)
        {
            return new Point(
                point1.X - point2.X,
                point1.Y - point2.Y);
        }

        public static bool operator == (Point point1, Point point2)
        {
            return (point1.X == point2.X) && 
                   (point1.Y == point2.Y);
        }

        public static bool operator !=(Point point1, Point point2)
        {
            return !(point1 == point2);
        }

        public override bool Equals(object obj)
        {
            return obj is Point point && point == this;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(X, Y);
        }
    }

    public static class PointExtensions
    {
        public static void MoveCursor(this Point point)
        {
            Console.SetCursorPosition(point.X, point.Y);
        }
    }
}
