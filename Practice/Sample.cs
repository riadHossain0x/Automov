using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.DevTools.V105.CSS;
using OpenQA.Selenium.Support.UI;
using System.Threading;

namespace Practice
{
    internal class Sample
    {
        IWebDriver driver = new ChromeDriver();
        public void Login_Operative(string user, string pass)
        {
            driver.Navigate().GoToUrl("http://localhost:5001/Login");
            driver.Manage().Window.Maximize();

            Print(300, "\n-------------- Login Operative --------------");

            IWebElement ele = driver.FindElement(By.Id("Email"));
            ele.SendKeys(user);

            Print(300, "Username value is entered.\n");

            ele = driver.FindElement(By.Id("Password"));
            ele.SendKeys(pass);

            Print(300, "Password is entered.\n");

            ele = driver.FindElement(By.XPath("//button[@type='submit']"));
            ele.Click();

            Print(400, "login button is clicked.\n");

            if(IsElementPresent(By.ClassName("validation-summary-errors")))
                throw new InvalidOperationException("Failed to login.");

           
        }

        public void Class_Operative(string className)
        {
            driver.Navigate().GoToUrl("http://localhost:5001/sm/Standard/Manage");

            Print(300, "-------------- Class Operative --------------");

            IWebElement ele = driver.FindElement(By.XPath("//a[@class='btn btn-outline btn-primary btn-xs']"));
            ele.Click();

            Print(400, "Add New button is clicked.\n");

            ele = driver.FindElement(By.Id("Name"));
            ele.SendKeys(className);

            Print(300, "class is entered.\n");

            ele = driver.FindElement(By.Id("btnSubmit"));
            ele.Click();

            Print(300, "Save is clicked.\n");

            ele = driver.FindElement(By.ClassName("alert"));

            Print(100, "Checking is data saved or not!\n");

            if (!ele.Text.Contains("successfull"))
                throw new InvalidOperationException($"Failed to saved data. Return message - '{ele.Text}'");

            Print(400, "Data successfully saved.\n");
        }

        public void Section_Operative(string className, string sectionName)
        {
            driver.Navigate().GoToUrl("http://localhost:5001/sm/Section/Manage");

            Print(300, "-------------- Section Operative --------------");

            IWebElement ele = driver.FindElement(By.XPath("//a[@class='btn btn-outline btn-primary btn-xs']"));
            ele.Click();

            Print(300, "Add New button is clicked\n");

            SelectElement dropDown = new SelectElement(driver.FindElement(By.XPath("//select[@id='StandardId']")));
            dropDown.SelectByText(className);

            Print(300, "Selecting class for section\n");

            ele = driver.FindElement(By.Id("name"));
            ele.SendKeys(sectionName);

            Print(300, "Section is entered\n");

            ele = driver.FindElement(By.Id("btnSubmit"));
            ele.Click();

            Print(300, "Save is clicked\n");

            ele = driver.FindElement(By.ClassName("alert"));

            Print(100, "Checking is data saved or not!\n");

            if (!ele.Text.Contains("successfull"))
                throw new InvalidOperationException($"Failed to saved data. Return message - '{ele.Text}'");

            Print(400, "Data successfully saved.\n");
        }

        public void Subject_Operative(string name, string sName, string cName, string csName, string sCode, string className)
        {
            driver.Navigate().GoToUrl("http://localhost:5001/sm/Subject/Manage");

            Print(300, "-------------- Subject Operative --------------");

            IWebElement ele = driver.FindElement(By.XPath("//a[@class='btn btn-outline btn-primary btn-xs']"));
            ele.Click();

            Print(300, "Add New button is clicked\n");

            ele = driver.FindElement(By.Id("Name"));
            ele.SendKeys(name);

            Print(300, "Name is entered\n");

            ele = driver.FindElement(By.Id("ShortName"));
            ele.SendKeys(sName);

            Print(300, "Short Name is entered\n");

            ele = driver.FindElement(By.Id("CommonName"));
            ele.SendKeys(cName);

            Print(300, "Common Name is entered\n");

            ele = driver.FindElement(By.Id("CommonShortName"));
            ele.SendKeys(csName);

            Print(300, "Common Short Name is entered\n");

            ele = driver.FindElement(By.Id("SubjectCode"));
            ele.SendKeys(sCode);

            Print(300, "Subject Code is entered\n");

            SelectElement dropDown = new SelectElement(driver.FindElement(By.XPath("//select[@id='standardId']")));
            dropDown.SelectByText(className);

            ele = driver.FindElement(By.Id("isScience"));
            ele.Click();

            ele = driver.FindElement(By.Id("btnSubmit"));
            ele.Click();

            Print(300, "Save is clicked\n");

            ele = driver.FindElement(By.ClassName("alert"));

            Print(100, "Checking is data saved or not!\n");

            if (!ele.Text.Contains("successfull"))
                throw new InvalidOperationException($"Failed to saved data. Return message - '{ele.Text}'");

            Print(400, "Data successfully saved.\n");
        }

        public void Student_Operative()
        {
            driver.Navigate().GoToUrl("http://localhost:5001/sm/Student/Manage");

            Print(300, "-------------- Student Operative --------------");

            IWebElement ele = driver.FindElement(By.XPath("//a[@class='btn btn-outline btn-primary btn-xs']"));
            ele.Click();

            Print(300, "Add New button is clicked\n");
        }

        public void ExecuteTest()
        {
            var userName = "admin";
            var password = "riad";
            var className = "Six";
            var section = "Section-A";

            Login_Operative(userName, password);

            //Class_Operative(className);

            //Section_Operative(className, section);

            Subject_Operative("Data Structure and Algorithm", "DSA", "DSA-101", "DSA-101", "101", "Nine");
        }

        public void EndTest()
        {

        }

        #region Helper
        private void Print(int sleep, string message)
        {
            Thread.Sleep(sleep);
            Console.WriteLine(message);
        }

        private bool IsElementPresent(By by)
        {
            try
            {
                driver.FindElement(by);
                return true;
            }
            catch (NoSuchElementException)
            {
                return false;
            }
        } 
        #endregion
    }
}
