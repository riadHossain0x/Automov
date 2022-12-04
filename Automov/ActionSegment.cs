using Automov.Enums;
using Automov.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Automov
{
    public class ActionSegment : IActionSegment
    {
        public string? SelectorText { get; set; }
        public SelectorType SelectorType { get; set; }
        public bool IsMultiple { get; set; }
        public IValueSegment Result { get; set; } = null!;
    }
}
