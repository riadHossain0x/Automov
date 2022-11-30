using Automov.Enums;
using Automov.Interfaces;

namespace Automov.Loggers
{
    public class ConsoleLogger : ILogger
    {
        public void Write(string message, LogType type)
        {
            ConsoleColor color = ConsoleColor.Black;

            switch (type)
            {
                case LogType.info:
                    color = ConsoleColor.Green;
                    break;
                case LogType.Success:
                    color = ConsoleColor.Green;
                    break;
                case LogType.Warning:
                    color = ConsoleColor.Yellow;
                    break;
                case LogType.Error:
                    color = ConsoleColor.Red;
                    break;
                default:
                    break;
            }

            Console.ForegroundColor = color;
            Console.Write($"{type.ToString()}: ");
            Console.ResetColor();
            Console.WriteLine(message);
        }
    }
}
