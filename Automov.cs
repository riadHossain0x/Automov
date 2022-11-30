using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Automov
{
    public interface IAutomov
    {

    }

    public class Automov : IAutomov
    {
        private readonly IWebDriver _driver;

        public Automov(IWebDriver driver)
        {
            _driver = driver;
        }

        public void Operative(string navigateURL, List<ISegment> segments)
        {

        }
    }
}
