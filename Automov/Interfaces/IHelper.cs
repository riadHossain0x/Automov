using OpenQA.Selenium;

namespace Automov.Interfaces
{
    public interface IHelper
    {
        void CheckElementValue(IValueSegment valueSegment);
        void Navigate(string url);
        void SetElementValue(IWebElement webElement, IValueSegment segment);
        IWebElement GetWebElement(ISegment segment);
    }
}