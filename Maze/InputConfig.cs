namespace Maze_Example
{
    using static Maze_Example.InputConfig;

    public static class InputConfig
    {
        public const ConsoleKey Exit = ConsoleKey.Q;

        public const ConsoleKey Yes = ConsoleKey.Y;
        public const ConsoleKey No = ConsoleKey.N;

        public static class Move
        {
            public const ConsoleKey Up = ConsoleKey.UpArrow;
            public const ConsoleKey Down = ConsoleKey.DownArrow;
            public const ConsoleKey Left = ConsoleKey.LeftArrow;
            public const ConsoleKey Right = ConsoleKey.RightArrow;

            public static IReadOnlyList<ConsoleKey> Array { get; } = new ConsoleKey[]
            {
                    Up, Down, Left, Right
            };

            public static bool IsMoveKey(ConsoleKey key)
            {
                return Array.Any(moveKey => moveKey == key);
            }
        }
    }

    public static class ConsoleKeyExtenstions
    {
        public static bool IsMoveKey(this ConsoleKey key)
        {
            return Move.IsMoveKey(key);
        }

        public static bool IsYesNoKey(this ConsoleKey key)
        {
            return Yes == key || No == key;
        }

        public static bool IsExitKey(this ConsoleKey key)
        {
            return Exit == key;
        }
    }
}
