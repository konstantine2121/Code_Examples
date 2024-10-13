namespace Input_Examples
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.InputEncoding = System.Text.Encoding.UTF8;

            Console.WriteLine("Демонстрация ввода/вывода данных");

            PlayerCreator creator = new PlayerCreator();
            PlayerPrinter printer = new PlayerPrinter();
            List<Player> players = new List<Player>();

            int numberOfPlayers = 2;

            for(int i=0; i < numberOfPlayers; i++)
            {
                players.Add(creator.Create());
                Console.WriteLine();
            }

            Console.WriteLine(PlayerPrinter.TableFormat, "Имя", "Уровень");
            Console.WriteLine(PlayerPrinter.TableFormat, "---", "---");

            for (int i = 0; i < players.Count; i++)
            {
                printer.Print(players[i]);
            }
        }

        class Player
        {
            public string Name;
            public int Level;
        }

        class PlayerPrinter
        {
            public const string TableFormat = "| {0, -20} | {1, 10} |";

            /// <exception cref="ArgumentNullException"></exception>
            public void Print(Player player)
            {
                if (player == null)
                {
                    //Console.WriteLine($"{nameof(player)} не может быть null");
                    throw new ArgumentNullException(nameof(player));
                }

                string message = string.Format(TableFormat, player.Name, player.Level);
                Console.WriteLine(message);

                //Console.WriteLine(TableFormat, player.Name, player.Level);
                //под капотом вызывает string.Format(TableFormat, player.Name, player.Level);

            }
        }

        class PlayerCreator
        {
            public Player Create()
            {
                string name = InputName();
                int level = InputLevel();

                Player player = new Player(); 
                //new Player(){ Name = name, Level = level }; //Инициализаторы (новая версия языка)
                player.Name = name;
                player.Level = level;

                return player;
            }

            private string InputName()
            {
                bool correct = false;
                string input = string.Empty;

                while (!correct) 
                {
                    Console.Write("Введите имя: ");
                    input = Console.ReadLine();

                    if (string.IsNullOrWhiteSpace(input))
                    {
                        Console.WriteLine("Имя должно влючать в себя значимые символы.");
                    }
                    else 
                    {
                        correct = true;
                    }
                }
                
                return input;
            }

            private int InputLevel()
            {
                bool correct = false;
                string input = string.Empty;
                int level = 0;

                while (!correct)
                {
                    Console.Write("Введите уровень: ");
                    input = Console.ReadLine();

                    if (!int.TryParse(input, out level))
                    {
                        Console.WriteLine("Неверно указан уровень (ожидается целое число)");
                    }
                    else
                    {
                        correct = true;
                    }
                }

                return level;
            }
        }
    }
}
