using System.Globalization;

namespace Quadratic_Equation
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var solver = new QuadraticEquationSolver();
            solver.PrintInfo();
            solver.Solve();
        }
    }

    #region QuadraticEquation

    public class QuadraticEquationSolver
    {
        private readonly Printer printer = new Printer();
        private readonly QuadraticEquationCalculator calculator = new QuadraticEquationCalculator();

        public void PrintInfo()
        {
            printer.PrintInfo();
        }

        public void Solve()
        {
            printer.PrintWelcome();

            var a = Input.ReadValue("a = ");
            var b = Input.ReadValue("b = ");
            var c = Input.ReadValue("c = ");
            Console.WriteLine();
            printer.PrintInput(a, b, c);

            //Вынужденное повторное вычисление дискиминанта для более наглядного вывода данных.
            var discriminant = calculator.CalculateDiscriminant(a, b, c);

            var result = calculator.Calculate(a, b, c);

            printer.PrintResult(discriminant, result);
        }
    }

    public class Printer
    {
        public void PrintInfo()
        {
            Console.WriteLine(
                "Программа для решения квадратных уравнений вида:" + Environment.NewLine +
                "a*x^2 + b*x + c = 0");
            Console.WriteLine();
        }

        public void PrintWelcome()
        {
            Console.WriteLine("Введите исходные данные (a, b, c): ");
        }

        public void PrintInput(double a, double b, double c)
        {
            Console.WriteLine("Исходные данные:");
            Console.WriteLine($"a = {a}");
            Console.WriteLine($"b = {b}");
            Console.WriteLine($"c = {c}");
            Console.WriteLine();
        }

        public void PrintResult(double discriminant, QuadraticEquationResult result)
        {
            Console.WriteLine("Результат:");
            Console.WriteLine();
            PrintDiscriminant(discriminant);
            Console.WriteLine();

            ResultTypes resultType = result.ResultType;

            switch (resultType)
            {
                case ResultTypes.TwoRoots:
                    Console.WriteLine("(D > 0) -- уравнение имеет два корня:");
                    Console.WriteLine("x1 = (-b + √D) / (2*a)");
                    Console.WriteLine("x2 = (-b - √D) / (2*a)");
                    Console.WriteLine();
                    Console.WriteLine($"x1 = {result.X1}");
                    Console.WriteLine($"x2 = {result.X2}");

                    break;

                case ResultTypes.OneRoot:
                    Console.WriteLine("(D == 0) -- уравнение имеет один корень:");
                    Console.WriteLine("x = -b / (2*a)");
                    Console.WriteLine($"x = {result.X1}");

                    break;

                default:
                    Console.WriteLine("(D < 0) -- уравнение корней не имеет.");
                    break;
            }

            Console.WriteLine();
        }


        private void PrintDiscriminant(double discriminant)
        {
            Console.WriteLine("D = b^2 - 4*a*c");
            Console.WriteLine($"D = {discriminant}");            
        }
    }

    public class QuadraticEquationCalculator
    {
        public QuadraticEquationResult Calculate(double a, double b, double c)
        {
            ResultTypes resultType = ResultTypes.NoRoots;
            double discriminant = CalculateDiscriminant(a, b, c);

            if (discriminant > 0)
            {
                resultType = ResultTypes.TwoRoots;
            }
            else if (discriminant == 0)
            {
                resultType = ResultTypes.OneRoot;
            }
            else
            {
                resultType = ResultTypes.NoRoots;
            }

            switch (resultType)
            {
                case ResultTypes.TwoRoots:
                    var roots = CalculateTwoRoots(a, b, discriminant);
                    return new QuadraticEquationResult(resultType, roots.Item1, roots.Item2);

                case ResultTypes.OneRoot:
                    var root = CalculateOneRoot(a, b);
                    return new QuadraticEquationResult(resultType, root);

                default:
                    return new QuadraticEquationResult(resultType);
            }
        }

        public double CalculateDiscriminant(double a, double b, double c)
        {
            return Math.Pow(b, 2) - (4 * a * c);
        }

        private Tuple<double, double> CalculateTwoRoots(double a, double b, double discriminant)
        {
            double x1 = (-b + Math.Sqrt(discriminant)) / (2 * a);
            double x2 = (-b - Math.Sqrt(discriminant)) / (2 * a);

            return Tuple.Create(x1, x2);
        }

        private double CalculateOneRoot(double a, double b)
        {
            double x1 = (-b) / (2 * a);

            return x1;
        }
    }

    public struct QuadraticEquationResult
    {
        public QuadraticEquationResult(ResultTypes resultType, double? x1 = null, double? x2 = null)
        {
            ResultType = resultType;
            X1 = x1;
            X2 = x2;
        }

        public ResultTypes ResultType { get; } = ResultTypes.NoRoots;

        public double? X1 { get; } = null;

        public double? X2 { get; } = null;
    }

    public enum ResultTypes
    {
        NoRoots,
        OneRoot,
        TwoRoots
    }

    #endregion QuadraticEquation

    #region Helpers

    public static class Input
    {
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
    }

    #endregion Helpers
}
