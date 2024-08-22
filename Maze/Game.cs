namespace Maze_Example
{
    public class Game : IDisposable
    {
        private Maze _maze;
        private Player _player;
        private List<string> _levels;

        public void Start()
        {
            //load map / 1st map
            //run game cycle

        }

        public void Load() 
        {

        }

        public void SwitchToNextMap()
        {

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
        }

        public void Exit()
        {
            Dispose();
            Environment.Exit(0);
        }

        public void Dispose()
        {
            _maze.Dispose();
            _maze = null;
            _player = null;
        }
    }
}
