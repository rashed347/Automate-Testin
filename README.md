# Automate-Testing

using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.PageObjects;
using OpenQA.Selenium.Support.UI;

namespace nextAutomation
{
    class Program
    {
        static void Main(string[] args)
        {
            
        }
        [SetUp]
        public void openBrowser()
        {
            PropertiesCollection.driver = new ChromeDriver();
            PropertiesCollection.driver.Manage().Window.Maximize();
            PropertiesCollection.driver.Navigate().GoToUrl("http://192.168.11.6:2000");
            WaitFor.ElementNotVisible(".//*[@id='overlay-content']/i");
            LogInPageObject Login = new LogInPageObject();
            Login.LogIn("ashraf", "1234");
            WaitFor.ElementNotVisible(".//*[@id='overlay-content']/i");
            WaitFor.ElementToVisible(By.XPath(".//*[@id='appMenu']/div[1]/div[3]")).Click();

        }
       [Ignore("Skip this test")]
        [Test]
        public void BuyerName()
        {
            WaitFor.ElementToVisible(By.XPath(".//*[@id='100000198']/a/span")).Click();
            WaitFor.ElementToVisible(By.XPath(".//*[@id='100000376']/a/span")).Click();
            MainMenuSection.SelectBuyerDropdownValue(1);
            WaitFor.ElementNotVisible(".//*[@id='overlay-content']/i");
            WaitFor.AnyErr();

            IList<IWebElement> formField = PropertiesCollection.driver.FindElements
                (By.XPath(".//*[@name='buyerBasicForm']/div//input[@type='text']"));
            IWebElement checkBox = PropertiesCollection.driver.FindElement(By.XPath(".//*[@class='checkbox']/label/input"));

            //Check the Empty Form

            EntryMode.CheckDefaultFieldsValue(formField);
            SeleniumSetMethod.CheckDisabledSaveBtn();
            SeleniumSetMethod.CheckDisabledReseTBtn();
            SeleniumSetMethod.CheckboxStateByDefault(checkBox);

            //Check the Reset Button Functionality after Fillup the Form

            formField[0].SendKeys("Test Buyer"); //Buyer Name
            formField[1].SendKeys("TB");         //Buyer Short Name
            SeleniumSetMethod.CheckEnabledResetBtnAndClickAfterFillup();
            WaitFor.AnyErr();

            //Check the Form after Reset

            EntryMode.CheckDefaultFieldsValue(formField);
            SeleniumSetMethod.CheckDisabledSaveBtn();
            SeleniumSetMethod.CheckDisabledReseTBtn();
            SeleniumSetMethod.CheckboxStateByDefault(checkBox);

            //Check the Save Button Functionality after Fillup the Form

            formField[0].SendKeys("Test Buyer"); //Buyer Name
            formField[1].SendKeys("TB");         //Buyer Short Name

            String[] inputValues = {formField[0].GetAttribute("value"), formField[1].GetAttribute("value"),
                SeleniumGetMethod.checkBoxState(checkBox)};

            SeleniumSetMethod.CheckEnabledSaveBtnAndClickAfterFillup();
            WaitFor.ElementNotVisible(".//*[@id='overlay-content']/i");
            WaitFor.AnyErr();
            WaitFor.SuccessMessage();
            
            //Grid Check for Data Available and Click on Edit Icon
           
            String headID = ".//*[@class='col-md-8 fl-row-box ng-scope']/div/div[2]/div[2]/div/.//*[@blank-grid='gridOptions']/thead/tr/th";
            IList<IWebElement> heaElement = PropertiesCollection.driver.FindElements(By.XPath(headID));
            int numberOfColumn = heaElement.Count;
            int numberOfDataColumn = numberOfColumn - 1;

            String commonRowId = ".//*[@class='col-md-8 fl-row-box ng-scope']/div/div[2]/div[2]/div/.//*[@blank-grid='gridOptions']/tbody/tr";

            SeleniumSetMethod.ClickOnEditNextToNewEntryAfterCheckGrid(commonRowId, numberOfColumn, numberOfDataColumn, inputValues);
            WaitFor.ElementNotVisible(".//*[@id='overlay-content']/i");
            WaitFor.AnyErr();

            //Form check after click on edit icon
           
            EditMode.CheckDefaultFieldsValue(formField, inputValues);
            EditMode.CheckCheckBoxState(checkBox, inputValues[2]);
            SeleniumSetMethod.CheckDisabledReseTBtn();
            SeleniumSetMethod.CheckDisabledSaveBtn();


            //Check the Form After Edit

            //Check the Reset Button Functionality after Edit the Form 

            formField[0].SendKeys("Test"); //Buyer Name
            formField[1].SendKeys("Test"); //Buyer Short Name
            SeleniumSetMethod.clickOn(checkBox);
            SeleniumSetMethod.CheckEnabledResetBtnAndClickAfterFillup();
            WaitFor.AnyErr();

            // Check the Form after Reset in Edit Mode

            EditMode.CheckDefaultFieldsValue(formField, inputValues);
            EditMode.CheckCheckBoxState(checkBox, inputValues[2]);
            SeleniumSetMethod.CheckDisabledReseTBtn();
            SeleniumSetMethod.CheckDisabledSaveBtn();

            // Check the Save Button Functionality after Edit the Form

            formField[0].SendKeys("Test"); //Buyer Name
            formField[1].SendKeys("Test"); //Buyer Short Name
            SeleniumSetMethod.clickOn(checkBox);

           string [] editInputValues = {formField[0].GetAttribute("value"), formField[1].GetAttribute("value"),
                SeleniumGetMethod.checkBoxState(checkBox)};

            SeleniumSetMethod.CheckEnabledSaveBtnAndClickAfterFillup();
            WaitFor.ElementNotVisible(".//*[@id='overlay-content']/i");
            WaitFor.AnyErr();
            WaitFor.SuccessMessage();

            //Check the Grid after Edit

           int desiredRow = SeleniumGetMethod.CheckGridAndGetDesiredRow(commonRowId, numberOfDataColumn, editInputValues);

            //Check Delete Functionality

            SeleniumSetMethod.ClickOnDeleteNextToNewEntry(commonRowId, desiredRow, numberOfColumn);
            WaitFor.ElementToVisible(By.XPath(".//*[@id='btnClose']")).Click();
            desiredRow = SeleniumGetMethod.CheckGridAndGetDesiredRow(commonRowId, numberOfDataColumn, editInputValues);

            SeleniumSetMethod.ClickOnDeleteNextToNewEntry(commonRowId, desiredRow, numberOfColumn);
            WaitFor.ElementToVisible(By.XPath(".//*[@id='btnOk']")).Click();
            WaitFor.AnyErr();
            WaitFor.SuccessMessage();
            desiredRow = SeleniumGetMethod.CheckGridAndGetDesiredRow(commonRowId, numberOfDataColumn, editInputValues);

            if (desiredRow == 0)
            {
                Console.WriteLine("Pass: Delete is Successful");
            }
            else
            {
                Console.WriteLine("Delete is not working");
            }

            // Create a Buyer for future use

            formField[0].SendKeys("Test Buyer");
            formField[1].SendKeys("TB");
            SeleniumSetMethod.CheckEnabledSaveBtnAndClickAfterFillup();
            WaitFor.AnyErr();
            WaitFor.SuccessMessage();
        }

        [Test]
        public void BuyersProfile()
        {
            WaitFor.ElementToVisible(By.XPath(".//*[@id='100000198']/a/span")).Click();
            WaitFor.ElementToVisible(By.XPath(".//*[@id='100000376']/a/span")).Click();
            MainMenuSection.SelectBuyerDropdownValue(2);
            WaitFor.ElementNotVisible(".//*[@id='overlay-content']/i");
            WaitFor.AnyErr();

         


        }

     /*
       [TearDown]
        public void CloseBrowser()
        {
            PropertiesCollection.driver.Close();
        } */


    }
}
