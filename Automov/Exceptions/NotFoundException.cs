using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Automov.Logger;

namespace Automov.Exceptions
{
    public class NotFoundException : Exception
    {
        private readonly ILogger _logger;

        public NotFoundException(ILogger logger, string message) : base(message)
        {
            _logger = logger;
            _logger.Write(message, LogType.Error);
        }
    }
}
