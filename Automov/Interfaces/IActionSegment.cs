using Automov.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Automov.Interfaces
{
    public interface IActionSegment : ISegment
    {
        IValueSegment Result { get; set; }
    }
}
