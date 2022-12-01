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

var valueSegment = new List<IValueSegment>()
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
        SelectorType = SelectorType.Id,
        SelectorText = "Password",
        Value = "riad",
        InputType = InputType.Textbox
    }
};

var actionSegment = new ActionSegment
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
automov.Operative("http://localhost:5001/Account/Login", valueSegment, actionSegment);