using Automov.Interfaces;
using OpenQA.Selenium;

namespace Automov
{
    public class Move : IMove
    {
        private readonly IWebDriver _driver = null!;
        private readonly ILogger _logger = null!;
        private readonly ICore _core = null!;
        private readonly int _delayTime;

        public Move(IWebDriver driver, ILogger logger, int delayTime = 200)
        {
            _driver = driver;
            _logger = logger;
            _delayTime = delayTime;
            _core = new Core(_driver, _logger, _delayTime);
        }

        public IWebElement Next(IActionSegment actionSegment)
        {
            var webElement = Imitation(actionSegment);
            return webElement;
        }

        public IWebElement Next(List<IActionSegment> actionSegments)
        {
            if (actionSegments == null)
                throw new ArgumentNullException(nameof(actionSegments));

            IWebElement webElement = null!;

            foreach (var action in actionSegments)
            {
                webElement = Next(action);
            }

            return webElement;
        }

        public IWebElement Next(string navigateURL, List<IActionSegment> actionSegments)
        {
            if (actionSegments == null)
                throw new ArgumentNullException(nameof(actionSegments));

            _core.Navigate(navigateURL);

            var webElement = Next(actionSegments);

            return webElement;
        }

        public IWebElement Next(List<IValueSegment> valueSegments)
        {
            if (valueSegments == null)
                throw new ArgumentNullException(nameof(valueSegments));

            IWebElement webElement = null!;

            foreach (var valueSegment in valueSegments)
            {
                if (valueSegment.IsMultiple)
                {
                    foreach (var element in _core.GetWebElements(valueSegment))
                    {
                        webElement = element;

                        SetValue(webElement, valueSegment);
                    }
                }
                else
                {
                    webElement = _core.GetWebElement(valueSegment);

                    SetValue(webElement, valueSegment);
                }
            }

            return webElement;
        }

        public IWebElement Next(string navigateURL, List<IValueSegment> valueSegments)
        {
            if (valueSegments == null)
                throw new ArgumentNullException(nameof(valueSegments));

            _core.Navigate(navigateURL);

            IWebElement webElement = Next(valueSegments);

            return webElement;
        }

        public IWebElement Next(string navigateURL, List<IValueSegment> valueSegments, IActionSegment actionSegment)
        {
            if (valueSegments == null || actionSegment == null)
                throw new ArgumentNullException(nameof(valueSegments));

            _core.Navigate(navigateURL);

            IWebElement webElement = Next(valueSegments);

            Imitation(actionSegment);

            return webElement;
        }

        private void SetValue(IWebElement webElement, IValueSegment valueSegment)
        {
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

        private IWebElement Imitation(IActionSegment actionSegment)
        {
            IWebElement webElement = null!;

            if(actionSegment.IsMultiple)
            {
                foreach (var element in _core.GetWebElements(actionSegment))
                {
                    webElement = element;

                    ImitationAction(webElement, actionSegment);
                }
            }else
            {
                webElement = _core.GetWebElement(actionSegment);

                ImitationAction(webElement, actionSegment);
            }

            return webElement;
        }

        private void ImitationAction(IWebElement webElement, IActionSegment actionSegment)
        {
            _logger.Write($"Executing action on '{actionSegment.SelectorText}'", Enums.LogType.info);

            webElement.Click();

            if (actionSegment.Result != null)
                _core.CheckElementValue(actionSegment.Result);
        }

    }
}
