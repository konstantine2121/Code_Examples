namespace Maze_Example
{
    public partial class Game
    {
        private class InputHandler : IDisposable
        {
            private const int MaxInputLength = 10;

            private Game _game;
            
            public InputHandler(Game game)
            {
                _game = game ?? throw new ArgumentNullException(nameof(game));
            }

            public bool HandleInput()
            {
                var input = GetInput();

                if (input.Any(ch => ch.Key.IsExitKey()) && HandleExit())
                {
                    return true;
                }

                HandleMovement(input);

                return false;
            }

            public static IEnumerable<ConsoleKeyInfo> GetInput()
            {
                var input = new HashSet<ConsoleKeyInfo>();
                var keyCount = 0;

                while (Console.KeyAvailable)
                {
                    var key = Console.ReadKey(true);

                    if (++keyCount > MaxInputLength)
                    {
                        continue;
                    }

                    input.Add(key);
                }

                return input;
            }

            private bool HandleExit()
            {
                _game._maze.MoveCursorUnderMaze();

                var exit = Input.ReadDialog("Вы хотите выйти? [Y,N]");
                if (exit)
                {
                    _game._running = false;
                    _game.Exit();
                }

                return true;
            }

            private void HandleMovement(IEnumerable<ConsoleKeyInfo> input)
            {
                var movementKeys = input.Select(ki => ki.Key)
                    .Where(key => key.IsMoveKey())
                    .ToArray();

                if (!movementKeys.Any())
                {
                    return;
                }

                var direction = Vector.Zero;

                foreach (var movement in movementKeys)
                {
                    switch (movement)
                    {
                        case InputConfig.Move.Up:
                            direction += Vector.Directions.Down;
                            break;

                        case InputConfig.Move.Down:
                            direction += Vector.Directions.Up;
                            break;

                        case InputConfig.Move.Left:
                            direction += Vector.Directions.Left;
                            break;

                        case InputConfig.Move.Right:
                            direction += Vector.Directions.Right;
                            break;
                    }
                }

                if (direction == Vector.Zero)
                {
                    return;
                }

                if (!direction.IsValidDirection())
                {
                    ResetRandomAxies(ref direction);
                }

                _game._player.TryMove(direction, _game._maze);
            }

            private void ResetRandomAxies(ref Vector direction)
            {
                var axies = Random.Shared.Next(0, 2);

                switch (axies)
                {
                    case 0:
                        direction.X = 0;
                        break;

                    case 1:
                        direction.Y = 0;
                        break;
                }
            }

            public void Dispose()
            {
                _game = null;
            }
        }
    }
}
