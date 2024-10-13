namespace Input_Examples
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.InputEncoding = System.Text.Encoding.UTF8;

            Console.ForegroundColor = ConsoleColor.Green;

            if (args.Length > 0)
            {
                //Console.WriteLine("Привет " + args[0]); Старая
                Console.WriteLine($"Привет {args[0]}");// Интерполяция строк
            }
            else
            {
                Console.WriteLine("Привет Неизвестный пользователь!");
            }

            Console.ReadLine();
        }
    }
}
