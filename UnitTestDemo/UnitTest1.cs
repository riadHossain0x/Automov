using Automov;
using Automov.Enums;
using Automov.Interfaces;
using Automov.Loggers;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;

namespace UnitTestDemo
{
    public class Tests
    {
        private IWebDriver _driver;
        private ILogger _logger;
        private IAviator _aviator;
        private string _url = string.Empty;

        [SetUp]
        public void Setup()
        {
            _driver = new EdgeDriver();
            _logger = new ConsoleLogger();
            _aviator = new Aviator(_driver, _logger, 300);
        }

        [Test]
        public void Test1()
        {
            // Arrange
            _url = "http://localhost:5001/Login";

            var loginValueSegment = new List<IValueSegment>()
            {
                new ValueSegment
                {
                    SelectorType = SelectorType.Id,
                    SelectorText = "Email",
                    Value = "admin",
                    InputType = InputType.Textbox
                },
                new ValueSegment
                {
                    SelectorText = "Password",
                    Value = "riada",
                }
            };

            var loginActionSegment = new ActionSegment
            {
                SelectorType = SelectorType.XPath,
                SelectorText = "//button[@type='submit']",
                Result = new ValueSegment
                {
                    SelectorType = SelectorType.XPath,
                    SelectorText = "//li[contains(text(),'Invalid login attempt.')]",
                    Value = "Invalid login", // expected return value
                    InputType = InputType.Label
                }
            };


            // Act
            _aviator.Operative(_url, loginValueSegment, loginActionSegment);

        }
    }
}