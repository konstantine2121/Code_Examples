using System;
using System.Globalization;

namespace Maze_Example
{
    public static partial class Input
    {
        #region NotUsing

        /// <summary>
        /// Установка InvariantCulture позволяет вводить '.' как десятичный разделитель вне зависимости от региона. <br/>
        /// Для СНГ по умолчанию разделителем является символ ','
        /// </summary>
        private static CultureInfo CultureInfo { get; } = CultureInfo.InvariantCulture;

        private static NumberStyles NumberStyle { get; } = NumberStyles.Float;

        public static double ReadValue(string message)
        {
            const string error = "Ошибка при вводе числа.";

            double value = double.NaN;
            bool correctValue = false;

            do
            {
                Console.Write(message);
                string input = Console.ReadLine();
                correctValue = double.TryParse(input, NumberStyle, CultureInfo, out value);

                if (!correctValue)
                {
                    Console.WriteLine(error);
                }

            } while (!correctValue);

            return value;
        }

        #endregion NotUsing

        public static ConsoleKey ReadKey()
        {
            return Console.ReadKey().Key;
        }
    }
}
