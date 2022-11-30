// See https://aka.ms/new-console-template for more information
using Automov;
using Automov.Enums;
using Automov.Interfaces;
using Automov.Loggers;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using Practice;

//Console.WriteLine("Hello, World!");

//var sp = new Sample();
//sp.ExecuteTest();

IWebDriver webDriver = new ChromeDriver();
ILogger logger = new ConsoleLogger();

var list = new List<IValueSegment>()
{
    new ValueSegment
    {
        SelectorText = "Email",
        Value = "admin",
        InputType = InputType.Textbox,
        SelectorType = SelectorType.Id
    },
    new ValueSegment
    {
        SelectorText = "Password",
        Value = "riad",
        InputType = InputType.Textbox,
        SelectorType = SelectorType.Id
    }
};

var automov = new Aviator(webDriver, logger, 200);
automov.Operative("http://localhost:5001/Account/Login", list);