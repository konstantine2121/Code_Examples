namespace Command_Line_Arguments_Examples
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.InputEncoding = System.Text.Encoding.UTF8;

            Console.ForegroundColor = ConsoleColor.Green;

            if (args.Length == 0)
            {
                Console.WriteLine("Привет Неизвестный пользователь!");
            }
            else if (args.Length == 1)
            {
                Console.WriteLine($"Привет {args[0]}");
            }
            else
            {
                string name = args[0];
                string stringAge = args[1];
                int age = 0;

                Console.WriteLine($"Привет {name}");

                // try
                // {
                //     age = int.Parse(stringAge);// Небезопасный (может выбросить исключение)
                // }
                // catch 
                // {
                //     age = -1;
                // }

                bool correctAge = int.TryParse(stringAge, out age);// Безопасный (в случае провала вернет false, age = 0)

                if (correctAge)
                {
                    Console.WriteLine($"{name}: полных лет {age}");
                }

                else
                {
                    Console.WriteLine("Возраст указан неверно!");
                }
            }


            Console.ReadLine();
        }
    }
}
