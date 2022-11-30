using OpenQA.Selenium;

namespace Automov
{
    public interface IAutomov
    {

    }

    public class Automov : IAutomov
    {
        private readonly IWebDriver _driver;
        private readonly ILogger _logger;

        public Automov(IWebDriver driver, ILogger logger)
        {
            _driver = driver;
            _logger = logger;
        }

        public void Operative(string navigateURL, List<ISegment> segments)
        {

        }
    }
}
