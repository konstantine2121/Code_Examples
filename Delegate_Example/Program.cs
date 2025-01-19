using System.Net.NetworkInformation;

namespace Delegate_Example
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");

            //var example1 = new DelegateExample1();
            //example1.CallExample();
            //example1.CallExample2();
            //example1.CallExample3();

            //var example2 = new DelegateExample2();
            //example2.CallExample();

            //var callbackExample = new CallbackExample();
            //callbackExample.CallExample();

            LambdaExample lambdaExample = new LambdaExample();
            lambdaExample.CallExample();
            lambdaExample.CallExample();
        }
    }


    class DelegateExample1
    {
        //Пример объявления делегата

        public delegate double CalculateSquareDelegate(double sideA, double sideB);//В double конвертируются без потерь все базаовые типы данных

        //Подробнее https://learn.microsoft.com/ru-ru/dotnet/csharp/language-reference/builtin-types/numeric-conversions#implicit-numeric-conversions

        public delegate double CalculateSquareDelegate<T1, T2>(T1 sideA, T2 sideB);//Обобщения - не входят в рамки рассматриваемой темы        

        public double CalculateSquareSquare(double sideA, double sideB)
        {
            return sideA * sideB;
        }

        public double CalculateTriangleSquare(double sideA, double height)
        {
            return sideA * height / 2.0;
        }


        public double UseCalculateDelegate(CalculateSquareDelegate calculate)
        {
            double result1 = calculate(12, 32);         // Равнозначно
            double result2 = calculate.Invoke(12, 32);   // 

            // result1 == result2

            return result1;
        }

        public double UseCalculateDelegate(CalculateSquareDelegate calculate, double arg1, double arg2)
        {
            double result1 = calculate(arg1, arg2);         // Равнозначно
            double result2 = calculate.Invoke(arg1, arg2);   // 

            // result1 == result2

            return result1;
        }

        public double UseCalculateDelegateUnsafe1(Delegate calculate, double arg1, double arg2)
        {
            double result1 = (double)calculate.DynamicInvoke(new object[] { arg1, arg2 });//НЕ РЕКОМЕНДУЮ

            return result1;
        }

        public double UseCalculateDelegateUnsafe2(Delegate calculate, double arg1, double arg2)
        {
            double result1 = 0;

            try
            {
                result1 = (double)calculate.DynamicInvoke(new object[] { "sd212", arg2 });//НЕ РЕКОМЕНДУЮ
            }
            catch (Exception e)
            {
                Console.WriteLine("Ой");
                Console.WriteLine(e.Message);
                result1 = double.NaN;
            }

            return result1;
        }

        public void CallExample()
        {
            double res1 = UseCalculateDelegate(CalculateSquareSquare);

            Console.WriteLine(res1);
            double res2 = UseCalculateDelegate(CalculateTriangleSquare);
            Console.WriteLine(res2);
            double res3 = UseCalculateDelegate(CalculateTriangleSquare, 15, 30);
            Console.WriteLine(res3);

            double res4 = UseCalculateDelegateUnsafe1(CalculateTriangleSquare, 15, 30);
            Console.WriteLine(res4);

            double res5 = UseCalculateDelegateUnsafe2(CalculateTriangleSquare, 15, 30);
            Console.WriteLine(res5);
        }

        public void CallExample2()
        {
            var options = new string[] { "треугольник", "квадрат" };

            Console.WriteLine("Введите площадь какой фигуры посчитать посчитать({0})", string.Join(", ", options));
            string input = Console.ReadLine().ToLower();

            if (!options.Contains(input))
            {
                Console.WriteLine("Такой вариант не предусмотрен");
                return;
            }

            double res1 = 0;
            // Индуский код - так делать НЕЛЬЗЯ!!!
            if (input.Equals(options[1]))                                    //
            {                                                                //
                res1 = UseCalculateDelegate(CalculateSquareSquare);          //
            }                                                                //
                                                                             //
            if (input.Equals(options[0]))                                    //
            {                                                                //
                res1 = UseCalculateDelegate(CalculateTriangleSquare);        //
            }                                                                //
            Console.WriteLine(res1);                                         //
        }

        public void CallExample3()
        {
            var optionsMap = new Dictionary<string, CalculateSquareDelegate>
            {
                ["треугольник"] = CalculateTriangleSquare,
                ["квадрат"] = CalculateSquareSquare
            };

            //Мы сложили экземпляры делегатов в словарь

            Console.WriteLine("Введите площадь какой фигуры посчитать посчитать({0})", string.Join(", ", optionsMap.Keys));

            string input = Console.ReadLine().ToLower();

            if (!optionsMap.ContainsKey(input))
            {
                Console.WriteLine("Такой вариант не предусмотрен");
                return;
            }

            double res1 = 0;

            res1 = UseCalculateDelegate(optionsMap[input]);

            Console.WriteLine(res1);
        }
    }

    class DelegateExample2
    {
        public double CalculateSquareSquare(double sideA, double sideB)
        {
            return sideA * sideB;
        }

        public double CalculateTriangleSquare(double sideA, double height)
        {
            return sideA * height / 2.0;
        }


        public double UseCalculateDelegate(Func<double, double, double> calculate)
        {
            double result1 = calculate(12, 32);         // Равнозначно
            double result2 = calculate.Invoke(12, 32);   // 

            return result1;
        }

        /// <summary>
        /// Подсмотреть можно тут <see cref="System.Func{T1, T2, TResult}"/>
        /// </summary>
        /// <param name="calculate"></param>
        /// <param name="arg1"></param>
        /// <param name="arg2"></param>
        /// <returns></returns>
        public double UseCalculateDelegate(Func<double, double, double> calculate, double arg1, double arg2)
        //Func на самом деле это делегат вида
        //public delegate TResult Func<in T1, in T2, out TResult>(T1 arg1, T2 arg2);
        //Обычно программисты использую
        {
            double result1 = calculate(arg1, arg2);         // Равнозначно
            double result2 = calculate.Invoke(arg1, arg2);   // 

            return result1;
        }

        public void CallExample()
        {
            double res1 = UseCalculateDelegate(CalculateSquareSquare);

            Console.WriteLine(res1);
            double res2 = UseCalculateDelegate(CalculateTriangleSquare);
            Console.WriteLine(res2);
            double res3 = UseCalculateDelegate(CalculateTriangleSquare, 15, 30);
            Console.WriteLine(res3);
        }
    }

    class CallbackExample
    {
        public void CallExample()
        {
            var user = new CalculatorUser();
            user.Use();
        }

        #region Nested Classes

        public class Calculator
        {
            internal void Calculate(double a, double b, Action<double> callback)
            {
                var result = a * b;

                callback?.Invoke(result);// ?. проверка на null
                // Подробнее тут https://learn.microsoft.com/ru-ru/dotnet/csharp/language-reference/operators/member-access-operators#null-conditional-operators--and-
            }
        }

        public class CalculatorUser
        {
            private readonly Calculator _calculator = new Calculator();

            public void Use()
            {
                _calculator.Calculate(12, 13, PrintResult);
            }

            private void PrintResult(double result)
            {
                Console.WriteLine("Резальтат вычисления: "+result);
            }
        }

        #endregion Nested Classes

    }

    class LambdaExample
    {
        delegate int Incr(int v);

        public void CallExample()
        {
            //count => count + 2

            Incr incr = count => count + 2;            incr = count => count + 2;            incr = Count;
        }

        //count => count + 2
        public int Count(int count)
        {
            return count + 2;
        }
    }
}
