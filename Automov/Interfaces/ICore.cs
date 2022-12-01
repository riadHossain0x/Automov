using Automov.Enums;
using OpenQA.Selenium;

namespace Automov.Interfaces
{
    public interface ICore
    {
        void CheckElementValue(IValueSegment valueSegment);
        void Navigate(string url);
        void SetElementValue(IWebElement webElement, InputType inputType, string? value);
        IWebElement GetWebElement(ISegment segment);
    }
}