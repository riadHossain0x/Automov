using Automov;
using Automov.Enums;
using Automov.Interfaces;
using Automov.Loggers;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using Practice;


//var sp = new Sample();
//sp.ExecuteTest();

IWebDriver webDriver = new ChromeDriver();
ILogger logger = new ConsoleLogger();

var automov = new Aviator(webDriver, logger, 200);

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
    SelectorText = "//button[@type='submit']",
    Result = null!
    //Result = new ValueSegment
    //{
    //    SelectorType = SelectorType.XPath,
    //    SelectorText = "//li[contains(text(),'Invalid login attempt.')]",
    //    Value = "Invalid login", // expected return value
    //    InputType = InputType.Label
    //}
};

automov.Operative("http://localhost:5001/Account/Login", loginValueSegment, loginActionSegment);

var stdValueSegment = new List<IValueSegment>()
{
    new ValueSegment
    {
        //SelectorType = SelectorType.Id,
        SelectorText = "Name",
        Value = "Math",
        //InputType = InputType.Textbox
    },
    new ValueSegment
    {
        SelectorText = "ShortName",
        Value = "M",
    },
    new ValueSegment
    {
        SelectorText = "CommonName",
        Value = "M-2002",
    },
    new ValueSegment
    {
        SelectorText = "CommonShortName",
        Value = "Math-4",
    },
    new ValueSegment
    {
        SelectorText = "SubjectCode",
        Value = "2002",
    },
    new ValueSegment // for dropdown field
    {
        SelectorType = SelectorType.XPath,
        SelectorText= "//select[@id='standardId']",
        Value = "Seven",
        InputType = InputType.Dropdown
    }
    ,
    new ValueSegment
    {
        SelectorText= "isScience",
        Value = "true",
        InputType = InputType.Checkbox
    }
};

var stdActionSegment = new ActionSegment
{
    SelectorText = "btnSubmit",
    Result = new ValueSegment
    {
        SelectorType = SelectorType.ClassName,
        SelectorText = "alert",
        Value = "successfull",
        InputType = InputType.Label
    }
};

//automov.Operative("http://localhost:5001/sm/Subject/CreateEdit", stdValueSegment, stdActionSegment);

automov.Operative("http://localhost:5001/sm/Exam/CreateEdit", stdValueSegment, stdActionSegment);