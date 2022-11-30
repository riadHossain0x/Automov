using Automov.Exceptions;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System.Xml.Linq;

namespace Automov
{
    public interface IAutomov
    {

    }

    public class Automov : IAutomov
    {
        private readonly IWebDriver _driver = null!;
        private readonly ILogger _logger = null!;

        public Automov(IWebDriver driver, ILogger logger)
        {
            _driver = driver;
            _logger = logger;
        }

        public void Operative(string navigateURL, List<ISegment> segments)
        {
            foreach (var segment in segments)
            {
                Navigate(navigateURL);

                var element = GetWebElement(segment);

                SetElementValue(element, segment);
            }
        }

        #region Helper
        private void Navigate(string url)
        {
            _driver.Navigate().GoToUrl(url);
            _driver.Manage().Window.Maximize();
        }

        public void SetElementValue(IWebElement webElement, ISegment segment)
        {
            switch (segment.InputType)
            {
                case InputType.Textbox:
                    webElement.SendKeys(segment.Value);
                    break;
                case InputType.Radiobutton:
                    webElement.Click();
                    break;
                case InputType.Checkbox:
                    webElement.Click();
                    break;
                case InputType.Dropdown:
                    SelectElement dropDown = new SelectElement(webElement);
                    dropDown.SelectByValue(segment.Value);
                    break;
                default:
                    throw new NotImplementedException();
            }
        }

        public IWebElement GetWebElement(ISegment segment)
        {
            IWebElement element = null!;

            switch (segment.SelectorType)
            {
                case SelectorType.Id:
                    element = IsElementPresent(By.Id(segment.Text), segment.Text);
                    break;
                case SelectorType.LinkText:
                    element = IsElementPresent(By.LinkText(segment.Text), segment.Text);
                    break;
                case SelectorType.Name:
                    element = IsElementPresent(By.Name(segment.Text), segment.Text);
                    break;
                case SelectorType.XPath:
                    element = IsElementPresent(By.XPath(segment.Text), segment.Text);
                    break;
                case SelectorType.ClassName:
                    element = IsElementPresent(By.ClassName(segment.Text), segment.Text);
                    break;
                case SelectorType.PartialLinkText:
                    element = IsElementPresent(By.PartialLinkText(segment.Text), segment.Text);
                    break;
                case SelectorType.TagName:
                    element = IsElementPresent(By.TagName(segment.Text), segment.Text);
                    break;
                case SelectorType.CssSelector:
                    element = IsElementPresent(By.CssSelector(segment.Text), segment.Text);
                    break;
                default:
                    throw new NotImplementedException();
            }

            return element;
        }

        private IWebElement IsElementPresent(By by, string? text)
        {
            IWebElement element = null!;

            if (string.IsNullOrEmpty(text))
                throw new InvalidOperationException("'text' can not be null");

            if (ValueObject.IsElementPresent(_driver, by))
            {
                element = _driver.FindElement(by);
            }else
                throw new ElementNotFoundException($"'{text}' element not found");

            return element;
        }
        #endregion
    }
}
