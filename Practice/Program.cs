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

var automov = new Move(webDriver, logger, 200);

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
};

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

var examValueSegment = new List<IValueSegment>()
{
    new ValueSegment
    {
        SelectorText = "AcademicClass",
        Value = "Nine",
        InputType = InputType.Dropdown
    },
    new ValueSegment
    {
        SelectorText = "Name",
        Value = "Semester Final Exam",
    },
    new ValueSegment
    {
        SelectorText = "StartDate",
        Value = "12222022",
    },
    new ValueSegment
    {
        SelectorText = "EndDate",
        Value = "12302022",
    },
    new ValueSegment
    {
        SelectorText = "ResultPublishedDate",
        Value = "01012023",
    },
    new ValueSegment // for dropdown field
    {
        SelectorText= "IsMultiPaperCalculation",
        Value = "true",
        InputType = InputType.Checkbox
    }
    ,
    new ValueSegment
    {
        SelectorText= "IsOnlinePublish",
        Value = "true",
        InputType = InputType.Checkbox
    }
};

var showDefaultAction = new ActionSegment
{
    SelectorText = "ShowHideDefaultControl"
};

var setMarkValueSegment = new List<IValueSegment>
{
    new ValueSegment
    {
        SelectorType = SelectorType.ClassName,
        SelectorText = "DefaultValue",
        InputType = InputType.Textbox,
        Value = "100",
        IsMultiple = true,
    }
};

var setMarkActionSegment = new ActionSegment
{
    SelectorType = SelectorType.ClassName,
    SelectorText = "DefaultValue",
    IsMultiple = true
};

var checkSubValueSegment = new List<IValueSegment>()
{
    new ValueSegment
    {
        SelectorType = SelectorType.ClassName,
        SelectorText = "IsSelected",
        Value = "true",
        InputType = InputType.Checkbox,
        IsMultiple = true
    }
};

var submitAction = new ActionSegment
{
    SelectorType = SelectorType.Id,
    SelectorText = "btnSubmit",
    Result = new ValueSegment
    {
        SelectorType = SelectorType.ClassName,
        SelectorText = "alert",
        InputType = InputType.Label,
        Value = "successfull"
    }
};

automov.Next("http://localhost:5001/Account/Login", loginValueSegment, loginActionSegment)
    .Next("http://localhost:5001/sm/Subject/CreateEdit", stdValueSegment, stdActionSegment)
    .Next("http://localhost:5001/sm/Exam/CreateEdit", examValueSegment)
    .Next(showDefaultAction)
    .Next(setMarkValueSegment)
    .Next(setMarkActionSegment)
    .Next(checkSubValueSegment)
    .Next(submitAction);