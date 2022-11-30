using Automov.Enums;

namespace Automov.Interfaces
{
    public interface ILogger
    {
        void Write(string message, LogType type);
    }
}
