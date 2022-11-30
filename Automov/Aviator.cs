using Automov.Enums;
using Automov.Exceptions;
using Automov.Interfaces;
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

        public virtual bool DoBefore() => true;
        public virtual bool DoAfter() => true;

        public void Operative(string navigateURL, List<IValueSegment> valueSegments)
        {
            Navigate(navigateURL);

            DoBefore();

            foreach (var segment in valueSegments)
            {
                var element = GetWebElement(segment);

                SetElementValue(element, segment);
            }

            DoAfter();
        }

        public void TakeOff(IActionSegment actionSegment)
        {

        }

        #region Helper
        private void Navigate(string url)
        {
            _logger.Write($"Navigating - {url}", Enums.LogType.info);

            _driver.Navigate().GoToUrl(url);
            _driver.Manage().Window.Maximize();

            Thread.Sleep(_delayTime);
        }

        private void SetElementValue(IWebElement webElement, IValueSegment segment)
        {
            _logger.Write($"Set foot in '{segment.SelectorText}' element for value '{segment.Value}'", Enums.LogType.info);

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

        private IWebElement GetWebElement(IValueSegment segment)
        {
            IWebElement element = null!;

            switch (segment.SelectorType)
            {
                case SelectorType.Id:
                    element = IsElementPresent(By.Id(segment.SelectorText), segment.SelectorText);
                    break;
                case SelectorType.LinkText:
                    element = IsElementPresent(By.LinkText(segment.SelectorText), segment.SelectorText);
                    break;
                case SelectorType.Name:
                    element = IsElementPresent(By.Name(segment.SelectorText), segment.SelectorText);
                    break;
                case SelectorType.XPath:
                    element = IsElementPresent(By.XPath(segment.SelectorText), segment.SelectorText);
                    break;
                case SelectorType.ClassName:
                    element = IsElementPresent(By.ClassName(segment.SelectorText), segment.SelectorText);
                    break;
                case SelectorType.PartialLinkText:
                    element = IsElementPresent(By.PartialLinkText(segment.SelectorText), segment.SelectorText);
                    break;
                case SelectorType.TagName:
                    element = IsElementPresent(By.TagName(segment.SelectorText), segment.SelectorText);
                    break;
                case SelectorType.CssSelector:
                    element = IsElementPresent(By.CssSelector(segment.SelectorText), segment.SelectorText);
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

            _logger.Write($"Checking element '{text}'", Enums.LogType.info);

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
