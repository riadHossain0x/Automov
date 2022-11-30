using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Automov.Logger
{
    public interface ILogger
    {
        void Write(string message, LogType type);
    }
}
