using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using NUnit.Framework.Internal;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace LoginPageAutomation
{
    class Program
    {
        
        static void Main(string[] args)
        {

        }

    [SetUp]
        public void OpenBrowser()
        {
            PropertiesCollection.driver = new ChromeDriver();

            PropertiesCollection.driver.Manage().Window.Maximize();
            PropertiesCollection.driver.Navigate().GoToUrl("http://192.168.11.6:2001/");
            WaitFor.PageLoading();
            WaitFor.Ready();
        //WaitFor.ElementToAppear(By.XPath(""));

           }

        [Test]
        public void Test_LogIn_Valid_Credential()
        {
            var login = new LogInPageObject();
            login.LogIn("admin", "nextcba");
        }

    }
}
