using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Automov.Exceptions
{
    internal class ElementNotFoundException : Exception
    {
        public ElementNotFoundException(string? message) : base(message)
        {

        }
    }
}
