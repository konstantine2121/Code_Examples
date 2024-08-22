namespace Maze_Example
{
    public static class VectorExtensions
    {
        public static void SetCursor(this Vector point)
        {
            Console.SetCursorPosition(point.X, point.Y);
        }

        public static bool OutOfBound(this Vector vector, int width, int height)
        {
            return (vector.X < 0 || vector.Y < 0)
                || (vector.X >= width || vector.Y >= height);
        }

        public static bool IsValidDirection(this Vector vector)
        {
            return Vector.Directions.IsValidDirection(vector);
        }
    }
}
