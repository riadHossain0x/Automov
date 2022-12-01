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
        private readonly ICore _core = null!;
        private readonly int _delayTime;

        public Aviator(IWebDriver driver, ILogger logger, int delayTime = 200)
        {
            _driver = driver;
            _logger = logger;
            _delayTime = delayTime;
            _core = new Core(_driver, _logger, _delayTime);
        }

        public IWebElement Operative(IActionSegment actionSegment)
        {
            var webElement = Imitation(actionSegment);
            return webElement;
        }

        public IWebElement Operative(List<IActionSegment> actionSegments)
        {
            if (actionSegments == null)
                throw new ArgumentNullException(nameof(actionSegments));

            IWebElement webElement = null!;

            foreach (var action in actionSegments)
            {
                webElement = Operative(action);
            }

            return webElement;
        }

        public IWebElement Operative(string navigateURL, List<IActionSegment> actionSegments)
        {
            if (actionSegments == null)
                throw new ArgumentNullException(nameof(actionSegments));

            _core.Navigate(navigateURL);

            var webElement = Operative(actionSegments);

            return webElement;
        }

        public IWebElement Operative(List<IValueSegment> valueSegments)
        {
            if (valueSegments == null)
                throw new ArgumentNullException(nameof(valueSegments));

            IWebElement webElement = null!;

            foreach (var valueSegment in valueSegments)
            {
                webElement = _core.GetWebElement(valueSegment);

                try
                {
                    _logger.Write($"Set foot in '{valueSegment.SelectorText}' element for value '{valueSegment.Value}'", Enums.LogType.info);

                    _core.SetElementValue(webElement, valueSegment.InputType, valueSegment.Value);
                }
                catch (Exception)
                {
                    _logger.Write($"Unable to set foot in '{valueSegment.SelectorText}' element for value '{valueSegment.Value}'", Enums.LogType.Error);
                }
            }

            return webElement;
        }

        public IWebElement Operative(string navigateURL, List<IValueSegment> valueSegments)
        {
            if (valueSegments == null)
                throw new ArgumentNullException(nameof(valueSegments));

            _core.Navigate(navigateURL);

            IWebElement webElement = Operative(valueSegments);

            return webElement;
        }

        public IWebElement Operative(string navigateURL, List<IValueSegment> valueSegments, IActionSegment actionSegment)
        {
            if (valueSegments == null || actionSegment == null)
                throw new ArgumentNullException(nameof(valueSegments));

            _core.Navigate(navigateURL);

            IWebElement webElement = Operative(valueSegments);

            Imitation(actionSegment);

            return webElement;
        }

        private IWebElement Imitation(IActionSegment actionSegment)
        {
            var element = _core.GetWebElement(actionSegment);

            _logger.Write($"Executing action on '{actionSegment.SelectorText}'", Enums.LogType.info);

            element.Click();

            if (actionSegment.Result != null)
                _core.CheckElementValue(actionSegment.Result);

            return element;
        }

    }
}
