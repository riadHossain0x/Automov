using Automov;
using Automov.Enums;
using Automov.Interfaces;
using Automov.Loggers;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using Practice;


//var sp = new Sample();
//sp.ExecuteTest();

IWebDriver webDriver = new ChromeDriver();
ILogger logger = new ConsoleLogger();

var loginValueSegment = new List<IValueSegment>()
{
    new ValueSegment
    {
        //SelectorType = SelectorType.Id,
        SelectorText = "Email",
        Value = "admin",
        //InputType = InputType.Textbox
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
        Value = "Invalid login", // aspected return value
        InputType = InputType.Label
    }
};


var automov = new Aviator(webDriver, logger, 200);
automov.Operative("http://localhost:5001/Account/Login", loginValueSegment, loginActionSegment);