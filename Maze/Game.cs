namespace Maze_Example
{
    public class Game : IDisposable
    {
        private const string MapsDirectory = "Maps";
        private const string MapExtension = ".txt";
        private const int MillisecondsTimeout = 2000;
        private readonly MazeDrawer _drawer = new MazeDrawer();

        private Maze _maze;
        private Player _player;
        private bool _running = false;
        private string[] _levels;
        private int _levelIndex = -1;

        public void Start()
        {
            //load map / 1st map
            try
            {
                LoadMaps();
                SwitchToNextMap();
            }
            catch (Exception ex)
            {
                Console.WriteLine(Logger.GetFormattedException(ex));

                Logger.Log(ex);

                Exit();
            }

            //run game cycle
            _running = true;

            while (_running) 
            {
                try 
                {
                    DrawLevel();
                    PerformStep();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Непредвиденная ошибка");
                    var message = Logger.GetFormattedException(ex);

                    Console.WriteLine(message);
                    Logger.Log(message);
                }
            }
        }

        private void DrawLevel()
        {
            _drawer.Draw(_maze, _player);
        }

        /// <summary>
        /// Загрузка карт
        /// </summary>
        /// <exception cref="DirectoryNotFoundException"></exception>
        /// <exception cref="InvalidDataException"></exception>
        public void LoadMaps() 
        {
            var pathToMaps = Path.Combine(Directory.GetCurrentDirectory(), MapsDirectory);
            
            if (!Directory.Exists(pathToMaps))
            {
                throw new DirectoryNotFoundException($"Указанная папка не найдена '{pathToMaps}'. Проверьте путь в папке с картами");
            }

            var dirInfo = new DirectoryInfo(pathToMaps);
            var files = dirInfo.GetFiles();

            var maps = files.Where(file => file.Extension.Equals(MapExtension)).Select(file => file.FullName);
            
            if (!maps.Any()) 
            {
                throw new InvalidDataException($"Карты не найдены: '{pathToMaps}'");
            }

            _levels = maps.ToArray();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <exception cref="InvalidOperationException"></exception>
        public void SwitchToNextMap()
        {
            if (!_levels.Any())
            {
                throw new InvalidOperationException("No levels to swicth");
            }

            _levelIndex++;

            if (_levelIndex >= _levels.Length)
            {
                _levelIndex = 0;
            }

            _maze = MazeLoader.Load(_levels[_levelIndex]);
            _player = new Player(_maze.PlayerStart);
        }

        public void PerformStep()
        {
            //check playerPos
            //               \ if onExit -> show congratulations message, then wait input and load next map
            //read input
            //          \if hasMoveInput -> check if can move, then move
            //          \if hasExitInput (q) -> show exit_dialog, then wait input (y,n) then exit or continue (via return -- without sleep)
            //          \no input -> do nothing
            //
            //sleep 0,5 sec (500ms)

            if (CheckExit())
            {
                SwitchToNextMap();
                return;
            }

            if (HandleInput())
            {
                return;
            }

            Thread.Sleep(MillisecondsTimeout);
        }

        #region HandleInput

        private bool HandleInput()
        {
            if (!Console.KeyAvailable)
            {
                return false;
            }

            var key = Console.ReadKey(true).Key;
            
            if (HandleExit(key))
            {
                return true;
            }

            HandleMovement(key);

            while (Console.KeyAvailable)
            {
                Console.ReadKey(true); 
            }


            return false;
        }

        private bool HandleExit(ConsoleKey key)
        {
            if (key.IsExitKey())
            {
                MoveCursorUnderMaze();
                var exit = Input.ReadDialog("Вы хотите выйти? [Y,N]");
                if (exit)
                {
                    _running = false;
                    Exit();
                }

                return true;
            }

            return false;
        }

        private void HandleMovement(ConsoleKey key)
        {
            if (key.IsMoveKey())
            {
                var direction = new Vector();

                switch (key)
                {
                    case InputConfig.Move.Up:
                        direction = Vector.Directions.Down;
                        break;

                    case InputConfig.Move.Down:
                        direction = Vector.Directions.Up;
                        break;

                    case InputConfig.Move.Left:
                        direction = Vector.Directions.Left;
                        break;

                    case InputConfig.Move.Right:
                        direction = Vector.Directions.Right;
                        break;
                }

                _player.TryMove(direction, _maze);
            }
        }

        #endregion HandleInput

        private bool CheckExit()
        {
            if (_maze.IsExit(_player.Position))
            {
                MoveCursorUnderMaze();

                Console.WriteLine("Поздравляю! Вы прошли уровень!");
                Console.ReadKey();

                return true;
            }
            return false;
        }

        public void Exit()
        {
            Dispose();
            Environment.Exit(0);
            Console.ReadKey();
        }

        public void Dispose()
        {
            _maze?.Dispose();
            _maze = null;
            _player = null;
        }

        #region Helpers

        private void MoveCursorUnderMaze()
        {
            Console.SetCursorPosition(0, _maze.Height + 1);
        }

        #endregion Helpers
    }
}
