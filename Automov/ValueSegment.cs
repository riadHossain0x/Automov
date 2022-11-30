using Automov.Enums;
using Automov.Interfaces;

namespace Automov
{
    public class ValueSegment : IValueSegment
    {
        public string? SelectorText { get; set; }
        public string? Value { get; set; }
        public InputType InputType { get; set; }
        public SelectorType SelectorType { get; set; }
    }
}
