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
        private Automov.Interfaces.IMove _aviator;
        private string _url = string.Empty;

        [SetUp]
        public void Setup()
        {
            _driver = new EdgeDriver();
            _logger = new ConsoleLogger();
            _aviator = new Automov.Move(_driver, _logger, 300);
        }

        [Test]
        public void LoginPage_ValidLoginAttempt_ReturnsLoggedIn()
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
                    Value = "riad",
                }
            };

            var loginActionSegment = new ActionSegment
            {
                SelectorType = SelectorType.XPath,
                SelectorText = "//button[@type='submit']"
            };


            // Act
            _aviator.Next(_url, loginValueSegment, loginActionSegment);

        }

        [Test]
        public void LoginPage_InvalidLoginAttempt_ReturnsInvalid()
        {
            // Arrange
            _url = "http://localhost:5001/Login";

            var loginValueSegment = new List<IValueSegment>()
            {
                new ValueSegment
                {
                    SelectorType = SelectorType.ClassName,
                    SelectorText = "Email",
                    Value = "admin",
                    InputType = InputType.Textbox
                },
                new ValueSegment
                {
                    SelectorText = "Password",
                    Value = "riad",
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
            _aviator.Next(_url, loginValueSegment, loginActionSegment);

        }
    }
}