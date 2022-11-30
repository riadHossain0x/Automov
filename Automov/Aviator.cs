using Automov.Exceptions;
using Automov.Logger;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace Automov
{
    public class Aviator : IAviator
    {
        private readonly IWebDriver _driver = null!;
        private readonly ILogger _logger = null!;
        private readonly int _delayTime;

        public Aviator(IWebDriver driver, ILogger logger, int delayTime)
        {
            _driver = driver;
            _logger = logger;
            _delayTime = delayTime;
        }

        public void Operative(string navigateURL, List<ISegment> segments)
        {
            Navigate(navigateURL);

            foreach (var segment in segments)
            {
                var element = GetWebElement(segment);

                SetElementValue(element, segment);
            }
        }

        #region Helper
        private void Navigate(string url)
        {
            _logger.Write($"Navigating - {url}", Logger.LogType.info);

            _driver.Navigate().GoToUrl(url);
            _driver.Manage().Window.Maximize();

            Thread.Sleep(_delayTime);
        }

        private void SetElementValue(IWebElement webElement, ISegment segment)
        {
            _logger.Write($"Set foot in '{segment.Text}' element for value '{segment.Value}'", Logger.LogType.info);

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

            Thread.Sleep(_delayTime);
        }

        private IWebElement GetWebElement(ISegment segment)
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
            if (string.IsNullOrEmpty(text))
                throw new Exceptions.NotFoundException(_logger, "element can not be null");

            _logger.Write($"Checking element '{text}'", Logger.LogType.info);

            IWebElement element = null!;

            if (ValueObject.IsElementPresent(_driver, by))
            {
                element = _driver.FindElement(by);
            }else
                throw new ElementNotFoundException(_logger, $"'{text}' element not found");

            Thread.Sleep(_delayTime);
            return element;
        }
        #endregion
    }
}
