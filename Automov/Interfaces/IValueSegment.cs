using Automov.Enums;

namespace Automov.Interfaces
{
    public interface IValueSegment : ISegment
    {
        string? Value { get; set; }
        InputType InputType { get; set; }
    }
}
