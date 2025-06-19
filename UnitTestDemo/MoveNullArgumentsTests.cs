using Automov;
using Automov.Interfaces;
using Automov.Loggers;
using OpenQA.Selenium;

namespace UnitTestDemo
{
    public class MoveNullArgumentsTests
    {
        [Test]
        public void Next_WithNullValueSegments_ThrowsArgumentNullException()
        {
            // Arrange
            IWebDriver? driver = null;
            ILogger logger = new ConsoleLogger();
            IMove move = new Automov.Move(driver!, logger);
            IActionSegment action = new ActionSegment();

            // Act
            var ex = Assert.Throws<ArgumentNullException>(() => move.Next("http://example.com", (List<IValueSegment>)null!, action));

            // Assert
            Assert.That(ex!.ParamName, Is.EqualTo("valueSegments"));
        }

        [Test]
        public void Next_WithNullActionSegment_ThrowsArgumentNullException()
        {
            // Arrange
            IWebDriver? driver = null;
            ILogger logger = new ConsoleLogger();
            IMove move = new Automov.Move(driver!, logger);
            var values = new List<IValueSegment>();

            // Act
            var ex = Assert.Throws<ArgumentNullException>(() => move.Next("http://example.com", values, null!));

            // Assert
            Assert.That(ex!.ParamName, Is.EqualTo("actionSegment"));
        }
    }
}
