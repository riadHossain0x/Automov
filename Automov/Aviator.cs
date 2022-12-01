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
        private readonly IHelper _helper = null!;
        private readonly int _delayTime;

        public Aviator(IWebDriver driver, ILogger logger, int delayTime = 200)
        {
            _driver = driver;
            _logger = logger;
            _delayTime = delayTime;
            _helper = new Helper(_driver, _logger, _delayTime);
        }

        public void Operative(string navigateURL, List<IValueSegment> valueSegments, IActionSegment actionSegment)
        {
            _helper.Navigate(navigateURL);

            foreach (var valueSegment in valueSegments)
            {
                var element = _helper.GetWebElement(valueSegment);

                _helper.SetElementValue(element, valueSegment);
            }

            TakeOff(actionSegment);
        }

        public void TakeOff(IActionSegment actionSegment)
        {
            var element = _helper.GetWebElement(actionSegment);

            _logger.Write($"Performing action on '{actionSegment.SelectorText}'", Enums.LogType.info);

            element.Click();

            if(actionSegment.Result != null)
                _helper.CheckElementValue(actionSegment.Result);
        }

    }
}
