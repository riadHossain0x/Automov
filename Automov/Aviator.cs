using Automov.Enums;
using Automov.Exceptions;
using Automov.Interfaces;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using static System.Net.Mime.MediaTypeNames;

namespace Automov
{
    public class Aviator : IAviator
    {
        private readonly IWebDriver _driver = null!;
        private readonly ILogger _logger = null!;
        private readonly IHelper _helper = null!;
        private readonly int _delayTime;

        public Aviator(IWebDriver driver, ILogger logger, int delayTime = 200)
        {
            _driver = driver;
            _logger = logger;
            _delayTime = delayTime;
            _helper = new Helper(_driver, _logger, _delayTime);
        }

        public IWebElement Operative(IActionSegment actionSegment)
        {
            var webElement = Imitation(actionSegment);
            return webElement;
        }

        public IWebElement Operative(List<IValueSegment> valueSegments)
        {
            IWebElement webElement = null!;

            foreach (var valueSegment in valueSegments)
            {
                webElement = _helper.GetWebElement(valueSegment);

                try
                {
                    _logger.Write($"Set foot in '{valueSegment.SelectorText}' element for value '{valueSegment.Value}'", Enums.LogType.info);

                    _helper.SetElementValue(webElement, valueSegment.InputType, valueSegment.Value);
                }
                catch (Exception)
                {
                    _logger.Write($"Unable to set foot in '{valueSegment.SelectorText}' element for value '{valueSegment.Value}'", Enums.LogType.Error);
                }
            }

            return webElement;
        }

        public IWebElement Operative(string navigateURL, List<IValueSegment> valueSegments, IActionSegment actionSegment)
        {
            _helper.Navigate(navigateURL);

            IWebElement webElement = Operative(valueSegments);

            Imitation(actionSegment);

            return webElement;
        }

        private IWebElement Imitation(IActionSegment actionSegment)
        {
            var element = _helper.GetWebElement(actionSegment);

            _logger.Write($"Executing action on '{actionSegment.SelectorText}'", Enums.LogType.info);

            element.Click();

            if(actionSegment.Result != null)
                _helper.CheckElementValue(actionSegment.Result);

            return element;
        }

    }
}
