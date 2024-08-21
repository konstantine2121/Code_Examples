
namespace Maze_Example
{
    public static class ArrayExtensions
    {
        /// <summary>
        /// Получить высоту (количество строк)
        /// </summary>
        /// <typeparam name="T">любой тип данных</typeparam>
        /// <param name="array">двумерный массив</param>
        /// <returns></returns>
        public static int GetHeigth<T>(this T[,] array)
        {
            return array.GetLength(0);
        }

        /// <summary>
        /// Получить ширину (количество столбцов)
        /// </summary>
        /// <typeparam name="T">любой тип данных</typeparam>
        /// <param name="array">двумерный массив</param>
        /// <returns></returns>
        public static int GetWidth<T>(this T[,] array)
        {
            return array.GetLength(1);
        }
    }
}
