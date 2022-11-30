// See https://aka.ms/new-console-template for more information
using Automov;
using Automov.Logger;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using Practice;

//Console.WriteLine("Hello, World!");

//var sp = new Sample();
//sp.ExecuteTest();

IWebDriver webDriver = new ChromeDriver();
ILogger logger = new ConsoleLogger();

var list = new List<Segment>();

var automov = new Automov.Automov(webDriver, logger, 200);
automov.Operative("http://localhost:5001/Account/Login", )