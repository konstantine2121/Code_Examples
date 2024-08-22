namespace Maze_Example
{
    internal class Logger
    {
        const string LogFile = "error_log.txt";

        public static void Log(string message)
        {
            try
            {
                var time = DateTime.Now.ToString("[yyyy.MM.dd HH:mm:ss]");

                File.AppendAllText(LogFile, 
                    time + Environment.NewLine
                    + message + Environment.NewLine);
            }
            catch (Exception ex) 
            {
                Console.WriteLine("Не удалось записать в лог");
            }
        }

        public static void Log(Exception exception)
        {
            string message = GetFormattedException(exception);
            Log(message);
        }

        public static string GetFormattedException(Exception exception)
        {
            return string.Format("Message: {0}\nSource: {1}\nStackTrace: {2}",
                       exception.Message,
                       exception.Source,
                       exception.StackTrace);
        }
    }
}
