using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Automov
{
    public interface ISegment
    {

    }
    public class Segment : ISegment
    {
        public string? Id { get; set; }
        public string? Value { get; set; }
        public InputType InputType { get; set; }
    }
    public enum InputType
    {
        Textbox,
        Radiobutton,
        Checkbox,
        Select
    }
}
