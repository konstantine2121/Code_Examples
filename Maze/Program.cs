namespace Maze_Example
{
    internal class Program
    {
        static void Main(string[] args)
        {
            using (var game = new Game())
            {
                game.Start();
            }
        }
    }
}
