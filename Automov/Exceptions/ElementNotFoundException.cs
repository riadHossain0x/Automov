using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Automov.Enums;
using Automov.Interfaces;

namespace Automov.Exceptions
{
    public class ElementNotFoundException : Exception
    {
        private readonly ILogger _logger;

        public ElementNotFoundException(ILogger logger, string message) : base(message)
        {
            _logger = logger;
            _logger.Write(message, LogType.Error);
        }
    }
}
