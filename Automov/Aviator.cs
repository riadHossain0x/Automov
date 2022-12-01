using Automov.Enums;
using Automov.Exceptions;
using Automov.Interfaces;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;

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

        public void Operative(string navigateURL, List<IValueSegment> valueSegments, IActionSegment actionSegment)
        {
            Navigate(navigateURL);

            foreach (var valueSegment in valueSegments)
            {
                var element = GetWebElement(valueSegment);

                SetElementValue(element, valueSegment);
            }

            TakeOff(actionSegment);
        }

        public void TakeOff(IActionSegment actionSegment)
        {
            var element = GetWebElement(actionSegment);

            _logger.Write($"Performing action on '{actionSegment.SelectorText}'", Enums.LogType.info);

            element.Click();

            CheckElementValue(actionSegment.Result);
        }

        #region Helper
        private void Navigate(string url)
        {
            if(string.IsNullOrEmpty(url))
                throw new Exceptions.NotFoundException(_logger, nameof(url));

            _logger.Write($"Navigate Url - {url}", Enums.LogType.info);

            _driver.Navigate().GoToUrl(url);
            _driver.Manage().Window.Maximize();

            Thread.Sleep(_delayTime);
        }

        private string CheckElementValue(IValueSegment valueSegment)
        {
            if(string.IsNullOrEmpty(valueSegment.Value))
                throw new Exceptions.NotFoundException(_logger, nameof(valueSegment.Value));

            _logger.Write($"Checking given result '{valueSegment.Value}'", Enums.LogType.info);

            var element = GetWebElement(valueSegment);

            if (element.Text.Contains(valueSegment.Value))
            {
                _logger.Write($"Given '{valueSegment.Value}' result found", Enums.LogType.Success);
                return element.Text;
            }

            throw new Exceptions.NotFoundException(_logger, $"Given '{valueSegment.Value}' result not found! System returns - '{element.Text}'");
        }

        private void SetElementValue(IWebElement webElement, IValueSegment segment)
        {
            if (string.IsNullOrEmpty(segment.Value))
                throw new Exceptions.NotFoundException(_logger, nameof(segment.Value));

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

        private IWebElement GetWebElement(ISegment segment)
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
