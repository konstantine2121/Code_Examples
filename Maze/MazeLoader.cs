
namespace Maze_Example
{
    public class MazeLoader
    {
        /// <summary>
        /// Легенда карты / Символьные обозначения
        /// </summary>
        public static class MapLegend
        {
            /// <summary>
            /// Пустая клетка
            /// </summary>
            public const char Space = ' ';

            /// <summary>
            /// Стена
            /// </summary>
            public const char Wall = '#';

            /// <summary>
            /// Выход
            /// </summary>
            public const char Exit = 'e';

            /// <summary>
            /// Стартовая точка для игрока
            /// </summary>
            public const char PlayerStart = 's';
        }

        public static Maze Load(string path)
        {
            if (!File.Exists(path))
            {
                throw new FileNotFoundException("Лабиринт не найден ", path);
            }

            var lines = File.ReadAllLines(path);

            char[,] map = GetMapFormattedContent(lines);

            var height = map.GetHeigth();
            var width = map.GetWidth();
            bool[,] walls = GetWalls(map);
            Vector playerStart = GetPlayerStart(map);
            Vector[] exits = GetExits(map);

            return new Maze(width, height, walls, playerStart, exits);
        }

        #region ConvertContent

        private static char[,] GetMapFormattedContent(string[] lines)
        {
            var width = lines.Max(line => line.Length);
            var height = lines.Length;

            var chars = new char[height, width];


            for (int row = 0; row < height; row++)
            {
                for (int column = 0; column < width; column++)
                {
                    var line = lines[row];

                    if (column < line.Length)
                    {
                        chars[row, column] = line[column];
                    }
                    else
                    {
                        chars[row, column] = MapLegend.Space;
                    }
                }
            }

            return chars;
        }

        #endregion ConvertContent

        #region InfoExtraction

        public static bool[,] GetWalls(char[,] map)
        {
            var height = map.GetHeigth();
            var width = map.GetWidth();

            var walls = new bool[height, width];

            for (int row = 0; row < height; row++)
            {
                for (int column = 0; column < width; column++)
                {
                    var destinationRow = InvertRowIndex(row, height);

                    walls[destinationRow, column] = map[row, column] == MapLegend.Wall;
                }
            }

            return walls;
        }

        private static Vector GetPlayerStart(char[,] map)
        {
            var height = map.GetHeigth();
            var width = map.GetWidth();

            for (int row = 0; row < height; row++)
            {
                for (int column = 0; column < width; column++)
                {
                    if (map[row, column] == MapLegend.PlayerStart)
                    {
                        var destinationRow = InvertRowIndex(row, height);

                        return new Vector(column, destinationRow);
                    }
                }
            }

            throw new InvalidDataException($"Player start char '{MapLegend.PlayerStart}' not found");
        }

        private static Vector[] GetExits(char[,] map)
        {
            var height = map.GetHeigth();
            var width = map.GetWidth();
            var exits = new List<Vector>();

            for (int row = 0; row < height; row++)
            {
                for (int column = 0; column < width; column++)
                {
                    if (map[row, column] == MapLegend.Exit)
                    {
                        var destinationRow = InvertRowIndex(row, height);

                        exits.Add(new Vector(column, destinationRow));
                    }
                }
            }

            if (!exits.Any())
                throw new InvalidDataException($"{nameof(exits)} must contains at least one element");

            return exits.ToArray();
        }

        #endregion InfoExtraction

        #region Helpers

        private static int InvertRowIndex(int row, int height)
        {
            var lastRowIndex = height - 1;

            return lastRowIndex - row;
        }

        #endregion Helpers
    }
}
