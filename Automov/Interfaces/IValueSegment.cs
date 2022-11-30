using Automov.Enums;

namespace Automov.Interfaces
{
    public interface IValueSegment
    {
        string? SelectorText { get; set; }
        string? Value { get; set; }
        InputType InputType { get; set; }
        SelectorType SelectorType { get; set; }
    }
}
