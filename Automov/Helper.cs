﻿using Automov.Enums;
using Automov.Exceptions;
using Automov.Interfaces;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Automov
{
    public class Helper : IHelper
    {
        private readonly IWebDriver _driver;
        private readonly ILogger _logger;
        private readonly int _delayTime;

        public Helper(IWebDriver driver, ILogger logger, int delayTime = 200)
        {
            _driver = driver;
            _logger = logger;
            _delayTime = delayTime;
        }

        public void Navigate(string url)
        {
            if (string.IsNullOrEmpty(url))
                throw new Exceptions.NotFoundException(_logger, nameof(url));

            _logger.Write($"Navigate Url - {url}", Enums.LogType.info);

            _driver.Navigate().GoToUrl(url);
            _driver.Manage().Window.Maximize();

            Thread.Sleep(_delayTime);
        }

        public void CheckElementValue(IValueSegment valueSegment)
        {
            try
            {
                if (string.IsNullOrEmpty(valueSegment.Value))
                    throw new Exceptions.NotFoundException(_logger, nameof(valueSegment.Value));

                _logger.Write($"Checking given result '{valueSegment.Value}'", Enums.LogType.info);
                var element = GetWebElement(valueSegment);

                if (element.Text.Contains(valueSegment.Value))
                {
                    _logger.Write($"Given '{valueSegment.Value}' result found", Enums.LogType.Success);
                    //return element.Text;
                }

                throw new Exceptions.NotFoundException(_logger, $"Given '{valueSegment.Value}' result not found! System returns - '{element.Text}'");
            }
            catch (Exception)
            {

            }
        }

        public void SetElementValue(IWebElement webElement, InputType inputType, string value)
        {
            try
            {
                if (string.IsNullOrEmpty(value))
                    throw new Exceptions.NotFoundException(_logger, nameof(value));

                switch (inputType)
                {
                    case InputType.Textbox:
                        if (string.IsNullOrWhiteSpace(value)) throw new Exceptions.NotFoundException(_logger, "There is no value found.");
                        webElement.SendKeys(value);
                        break;
                    case InputType.Radiobutton:
                        webElement.Click();
                        break;
                    case InputType.Checkbox:
                        webElement.Click();
                        break;
                    case InputType.Dropdown:
                        if (string.IsNullOrWhiteSpace(value)) throw new Exceptions.NotFoundException(_logger, "There is no value found.");
                        SelectElement dropDown = new SelectElement(webElement);
                        dropDown.SelectByText(value);
                        break;
                    default:
                        throw new NotImplementedException();
                }

                Thread.Sleep(_delayTime);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IWebElement GetWebElement(ISegment segment)
        {
            if (string.IsNullOrEmpty(segment.SelectorText))
                throw new Exceptions.NotFoundException(_logger, "There is no selector found.");

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
            _logger.Write($"Checking element '{text}'", Enums.LogType.info);

            IWebElement element = null!;

            if (ValueObject.IsElementPresent(_driver, by))
            {
                element = _driver.FindElement(by);
            }
            else
                throw new ElementNotFoundException(_logger, $"'{text}' element not found");

            Thread.Sleep(_delayTime);
            return element;
        }
    }
}
