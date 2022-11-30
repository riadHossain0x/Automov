using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Automov_Pilot
{
    public interface ISegment
    {
        string? Text { get; set; }
        string? Value { get; set; }
        InputType InputType { get; set; }
        SelectorType SelectorType { get; set; }
    }
    public class Segment : ISegment
    {
        public string? Text { get; set; }
        public string? Value { get; set; }
        public InputType InputType { get; set; }
        public SelectorType SelectorType { get; set; }
    }

    public enum InputType
    {
        Textbox,
        Radiobutton,
        Checkbox,
        Dropdown
    }

    public enum SelectorType
    {
        Id,
        LinkText,
        Name,
        XPath,
        ClassName,
        PartialLinkText,
        TagName,
        CssSelector
    }
}
