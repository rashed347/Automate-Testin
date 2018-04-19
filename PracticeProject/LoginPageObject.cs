using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace LoginPageAutomation
{
    class LogInPageObject
    {
        public LogInPageObject()
        {
            PageFactory.InitElements(PropertiesCollection.driver, this);
        }
        
        [FindsBy(How = How.Id, Using = "userName")]
        public IWebElement TxtUserName { get; set; }

        [FindsBy(How = How.Id, Using = "password")]
        public IWebElement TxtPassword { get; set; }

        [FindsBy(How = How.Id, Using = "btnLogIn")]
        public IWebElement btnLogIn { get; set; }

        public void LogIn(String userName, string password)
        {
            TxtUserName.SendKeys(userName);
            TxtPassword.SendKeys(password);
            btnLogIn.Click();
        }
    }

   
}
