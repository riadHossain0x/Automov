// See https://aka.ms/new-console-template for more information
using Automov_Pilot;
using Automov_Pilot.Logger;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using Practice;

//Console.WriteLine("Hello, World!");

//var sp = new Sample();
//sp.ExecuteTest();

IWebDriver webDriver = new ChromeDriver();
ILogger logger = new ConsoleLogger();

var list = new List<ISegment>()
{
    new Segment
    {
        Text = "Email",
        Value = "admin",
        InputType = InputType.Textbox,
        SelectorType = SelectorType.Id
    },
    new Segment
    {
        Text = "Password",
        Value = "riad",
        InputType = InputType.Textbox,
        SelectorType = SelectorType.Id
    }
};

var automov = new Automov(webDriver, logger, 200);
automov.Operative("http://localhost:5001/Account/Login", list);