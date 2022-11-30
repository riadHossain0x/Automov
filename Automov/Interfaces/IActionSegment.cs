using Automov.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Automov.Interfaces
{
    public interface IActionSegment
    {
        string? SelectorText { get; set; }
        SelectorType SelectorType { get; set; }
    }
}
