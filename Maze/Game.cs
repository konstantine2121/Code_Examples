namespace Maze_Example
{
    public partial class Game : IDisposable
    {
        private const string MapsDirectory = "Maps";
        private const string MapExtension = ".txt";
        
        private const int MillisecondsTimeout = 200;

        private MazeDrawer _drawer;
        private InputHandler _inputHandler;

        private Maze _maze;
        private Player _player;

        private bool _running = false;
        private string[] _levels;
        private int _levelIndex = -1;

        public Game()
        {
            _inputHandler = new InputHandler(this);
            _drawer = new MazeDrawer();
        }

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
            //sleep 0,2 sec (200ms)

            if (CheckExit())
            {
                SwitchToNextMap();
                return;
            }

            if (_inputHandler.HandleInput())
            {
                return;
            }

            Thread.Sleep(MillisecondsTimeout);
        }

        private bool CheckExit()
        {
            if (_maze.IsExit(_player.Position))
            {
                _maze.MoveCursorUnderMaze();

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
            _drawer = null;
            _inputHandler?.Dispose();
            _inputHandler = null;

            _maze?.Dispose();
            _maze = null;

            _player = null;
        }

        #region Helpers

        #endregion Helpers
    }
}
