using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Automov_Pilot.Logger;

namespace Automov_Pilot.Exceptions
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
