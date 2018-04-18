using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using NUnit.Framework;
using NUnit.Framework.Internal;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.PageObjects;
using OpenQA.Selenium.Support.UI;

namespace nextAutomation
{
    internal class Program
    {
    
        private static void Main(string[] args)
        {

        }

        [SetUp]
        public void OpenBrowser()
        {
            PropertiesCollection.driver = new ChromeDriver();

            PropertiesCollection.driver.Manage().Window.Maximize();
            PropertiesCollection.driver.Navigate().GoToUrl("http://192.168.11.6:2000");
            WaitFor.ElementNotVisible(".//*[@id='overlay-content']/i");
            var login = new LogInPageObject();
            login.LogIn("admin", "nextcba");
            //PropertiesCollection.driver.FindElement(By.XPath(".//input[@id='userName']")).SendKeys("admin");
            //PropertiesCollection.driver.FindElement(By.XPath(".//input[@id='password']")).SendKeys("nextcba");

            WaitFor.ElementNotVisible(".//*[@id='overlay-content']/i");
            WaitFor.ElementToVisible(By.XPath(".//*[@id='appMenu']/div[1]/div[3]")).Click();
            WaitFor.ElementToVisible(By.XPath(".//div/ul[@class = 'top-menu']/li[2]/a/span"));

        }


        [Test]
        [Ignore("Ignore this test")]
        public void BuyerNameTest()
        {
            WaitFor.ElementToVisible(By.XPath(".//*[@id='100000198']/a/span")).Click();
            WaitFor.ElementToVisible(By.XPath(".//*[@id='100000376']/a/span")).Click();
            MainMenuSection.SelectBuyerDropdownValue(1);
            WaitFor.ElementNotVisible(".//*[@id='overlay-content']/i");
            WaitFor.AnyErr();

            var formField = GetMethod.WebElementsList(BuyerName.XTextFields);
            var checkBox = GetMethod.WebElement(BuyerName.XCheckBox);

            //Check the Empty Form

            EntryMode.CheckDefaultFieldsValue(formField);
            SetMethod.CheckDisabledSaveBtn();
            SetMethod.CheckDisabledReseTBtn();
            SetMethod.CheckboxStateByDefault(checkBox);

            //Check the Reset Button Functionality after Fillup the Form

            formField[0].SendKeys(SetValueFor.BuyerName[0]); //Buyer Name
            formField[1].SendKeys(SetValueFor.BuyerShortName[0]); //Buyer Short Name
            SetMethod.CheckEnabledResetBtnAndClickAfterFillup();
            WaitFor.AnyErr();

            //Check the Form after Reset

            EntryMode.CheckDefaultFieldsValue(formField);
            SetMethod.CheckDisabledSaveBtn();
            SetMethod.CheckDisabledReseTBtn();
            SetMethod.CheckboxStateByDefault(checkBox);

            //Check the Save Button Functionality after Fillup the Form

            formField[0].SendKeys(SetValueFor.BuyerName[0]); //Buyer Name
            formField[1].SendKeys(SetValueFor.BuyerShortName[0]); //Buyer Short Name

            String[] inputValues =
            {
                formField[0].GetAttribute("value"), formField[1].GetAttribute("value"),
                GetMethod.CheckBoxState(checkBox)
            };

            SetMethod.CheckEnabledSaveBtnAndClickAfterFillup();
            WaitFor.ElementNotVisible(".//*[@id='overlay-content']/i");
            WaitFor.AnyErr();
            WaitFor.SuccessMessage();

            //Grid Check for Data Available and Click on Edit Icon


            var heaElement = GetMethod.WebElementsList(BuyerName.XGridHeadId);
            int numberOfColumn = heaElement.Count;
            int numberOfDataColumn = numberOfColumn - 1;

            SetMethod.ClickOnEditNextToNewEntryAfterCheckGrid(BuyerName.XGridCommonRowId, numberOfColumn, numberOfDataColumn,
                inputValues);
            WaitFor.ElementNotVisible(".//*[@id='overlay-content']/i");
            WaitFor.AnyErr();

            //Form check after click on edit icon

            EditMode.CheckDefaultFieldsValue(formField, inputValues);
            EditMode.CheckCheckBoxState(checkBox, inputValues[2]);
            SetMethod.CheckDisabledReseTBtn();
            SetMethod.CheckDisabledSaveBtn();


            //Check the Form After Edit

            //Check the Reset Button Functionality after Edit the Form 

            formField[0].SendKeys("Test"); //Buyer Name
            formField[1].SendKeys("Test"); //Buyer Short Name
            SetMethod.ClickOn(checkBox);
            SetMethod.CheckEnabledResetBtnAndClickAfterFillup();
            WaitFor.AnyErr();

            // Check the Form after Reset in Edit Mode

            EditMode.CheckDefaultFieldsValue(formField, inputValues);
            EditMode.CheckCheckBoxState(checkBox, inputValues[2]);
            SetMethod.CheckDisabledReseTBtn();
            SetMethod.CheckDisabledSaveBtn();

            // Check the Save Button Functionality after Edit the Form

            formField[0].SendKeys("Test"); //Buyer Name
            formField[1].SendKeys("Test"); //Buyer Short Name
            SetMethod.ClickOn(checkBox);

            string[] editInputValues =
            {
                formField[0].GetAttribute("value"), formField[1].GetAttribute("value"),
                GetMethod.CheckBoxState(checkBox)
            };

            SetMethod.CheckEnabledSaveBtnAndClickAfterFillup();
            WaitFor.ElementNotVisible(".//*[@id='overlay-content']/i");
            WaitFor.AnyErr();
            WaitFor.SuccessMessage();

            //Check the Grid after Edit

            int desiredRow = GetMethod.CheckGridAndGetDesiredRow(BuyerName.XGridCommonRowId, numberOfDataColumn,
                editInputValues);

            //Check Delete Functionality

            SetMethod.ClickOnDeleteNextToNewEntry(BuyerName.XGridCommonRowId, desiredRow, numberOfColumn);
            WaitFor.ElementToVisible(By.XPath(".//*[@id='btnClose']")).Click();
            desiredRow = GetMethod.CheckGridAndGetDesiredRow(BuyerName.XGridCommonRowId, numberOfDataColumn, editInputValues);

            SetMethod.ClickOnDeleteNextToNewEntry(BuyerName.XGridCommonRowId, desiredRow, numberOfColumn);
            WaitFor.ElementToVisible(By.XPath(".//*[@id='btnOk']")).Click();
            WaitFor.AnyErr();
            WaitFor.SuccessMessage();
            desiredRow = GetMethod.CheckGridAndGetDesiredRow(BuyerName.XGridCommonRowId, numberOfDataColumn, editInputValues);

            if (desiredRow == 0)
            {
                Console.WriteLine("Pass: Delete is Successful");
            }
            else
            {
                Console.WriteLine("Delete is not working");
            }

            // Create a Buyer for future use

            formField[0].SendKeys(SetValueFor.BuyerName[0]);
            formField[1].SendKeys(SetValueFor.BuyerShortName[0]);
            SetMethod.CheckEnabledSaveBtnAndClickAfterFillup();
            WaitFor.AnyErr();
            WaitFor.SuccessMessage();
        }

        //  [Test]
        [Ignore("Ignore this test")]
        public void BuyerProfileTest()
        {
            #region Landing On Buyers Profile Page

            WaitFor.ElementToVisible(By.XPath(".//*[@id='100000198']/a/span")).Click();
            WaitFor.ElementToVisible(By.XPath(".//*[@id='100000376']/a/span")).Click();
            MainMenuSection.SelectBuyerDropdownValue(2);
            WaitFor.ElementNotVisible(".//div[@id='overlay-container']/div[2][@id='overlay-content']");
            WaitFor.AnyErr();
            WaitFor.ElementToVisible(By.XPath(BuyerDetails.XSelectButton));

            #endregion

            #region Input Values and All Fields in the Form

            var textFieldValues = new List<string>
            {
                SetMethod.RandomValue(SetValueFor.RegiNo),
                SetMethod.RandomValue(SetValueFor.TinNo),
                SetMethod.RandomValue(SetValueFor.VatNo),
                SetMethod.RandomValue(SetValueFor.AddressLine1),
                SetMethod.RandomValue(SetValueFor.AddressLine2),
                SetMethod.RandomValue(SetValueFor.Village),
                SetMethod.RandomValue(SetValueFor.PostBox),
                SetMethod.RandomValue(SetValueFor.PostOffice),
                SetMethod.RandomValue(SetValueFor.ZipCode),
                SetMethod.RandomValue(SetValueFor.PoliceStation),
                SetMethod.RandomValue(SetValueFor.SubDistrict),
                SetMethod.RandomValue(SetValueFor.District),
                SetMethod.RandomValue(SetValueFor.Division),
                SetMethod.RandomValue(SetValueFor.City),
                SetMethod.RandomValue(SetValueFor.State)
            };

            IList<IWebElement> textFieldsInBasicInfo =
                PropertiesCollection.driver.FindElements(By.XPath(BuyerDetails.XTextFieldsInBasicInfoNPerson));

            IList<IWebElement> textFieldsInOfficeInfo =
                PropertiesCollection.driver.FindElements(By.XPath(BuyerDetails.XTextFieldsInOfficeInfo));

            IList<IWebElement> allTextFields =
                new List<IWebElement>(textFieldsInBasicInfo.Concat(textFieldsInOfficeInfo));

            IList<IWebElement> closeMultiSelectDropdownField =
                PropertiesCollection.driver.FindElements(By.XPath(BuyerDetails.XCloseMultiSelectDropdownField));

            IList<IWebElement> closeMultiTextField =
                PropertiesCollection.driver.FindElements(By.XPath(BuyerProfile.XCloseMultiTextField));

            #endregion

            #region Save Reset Functionality in Entry Mode

            //In Entry Mode default value of all fields

            var defaultValuesInEntryMode = BuyerProfile.GetValuesOfAllFields();

            Console.WriteLine("Save and Reset button state in new form: ");
            SetMethod.CheckDisabledSaveBtn();
            SetMethod.CheckDisabledReseTBtn();

            BuyerProfile.SelectBuyer(SetValueFor.BuyerName[0]);

            Console.WriteLine("Save and Reset button state after select a buyer: ");
            SetMethod.CheckDisabledSaveBtn();
            SetMethod.CheckDisabledReseTBtn();

            SetMethod.EnterText(allTextFields, textFieldValues);

            MultiSelectDropDown s = new MultiSelectDropDown();
            //var selectedElements = s.SelectcheckboxFrm1StField();
            //GetMethod.GetListElementsConsole(selectedElements);

            SetMethod.FillupAllMultiTextFields(BuyerDetails.XCloseMultiTextField, 3);

            BuyerProfile.SelectValueFromAllDropdown();

            Console.WriteLine("Reset button state after fillup the form: ");

            SetMethod.CheckEnabledResetBtnAndClickAfterFillup();

            Console.WriteLine("Form checkup after reset: ");

            WaitFor.ElementToVisible(By.XPath(BuyerProfile.XCloseMultiTextField));

            var valuesAfterReset = BuyerProfile.GetValuesOfAllFields();

            SetMethod.CheckTwoEqualLists(valuesAfterReset, defaultValuesInEntryMode);

            Console.WriteLine("Save button state after fillup the form: ");

            BuyerProfile.SelectBuyer(SetValueFor.BuyerName[0]);
            SetMethod.EnterText(allTextFields, textFieldValues);

            //SetMethod.SelectDeselectElementsFromAllMdd(BuyerProfile.XCheckboxOfMddElements,
            //    closeMultiSelectDropdownField, selectedElementNumber: 1, xpathForValueOfMddElements: BuyerProfile.XValueOfMddElements, numberOfMddfield: 2);

            SetMethod.FillupAllMultiTextFields(BuyerDetails.XCloseMultiTextField, 4);

            BuyerProfile.SelectValueFromAllDropdown();

            var formInputValues = BuyerProfile.GetValuesOfAllFields();

            SetMethod.CheckEnabledSaveBtnAndClickAfterFillup();
            WaitFor.ElementNotVisible(".//div[@id='overlay-container']/div[2][@id='overlay-content']");
            WaitFor.AnyErr();
            WaitFor.SuccessMessage();

            var valuesAfterSave = BuyerProfile.GetValuesOfAllFields();

            SetMethod.CheckTwoEqualLists(formInputValues, valuesAfterSave);

            #endregion

            #region Save Reset Functionality in Edit Mode

            SetMethod.EditAllTextFields(allTextFields, "test");
            SetMethod.EditAllMultiTextFields(closeMultiTextField, BuyerProfile.XMultiTextField);
            //SetMethod.SelectDeselectElementsFromAllMdd(BuyerProfile.XCheckboxOfMddElements,
                //closeMultiSelectDropdownField, selectedElementNumber: 2, numberOfMddfield: 2);

            //SeleniumSetMethod.CheckEnabledResetBtnAndClickAfterFillup();

            #endregion

        }

        // [Test]

        public void BuyerWisePaymentDetailTest()
        {
            #region Landing On Buyer Wise Payment Detail Page

            WaitFor.ElementToVisible(By.XPath(".//*[@id='100000198']/a/span")).Click();
            BuyerDetails.LandingOnThisPageNWaitForSelectBtn("Buyer Wise Payment Detail");

            #endregion



            BuyerDetails.SelectBuyer(SetValueFor.BuyerName[0]);

            var dropDownList = GetMethod.WebElementsList(BuyerDetails.XDropdown);
            SetMethod.SelectDropdownValue(dropDownList, whichField: 0, indexOfDdElement: 7);

            var multiSelectField = GetMethod.WebElement(BuyerDetails.XCloseMultiSelectDropdownField);
            //SetMethod.SelectDeselectElementsFromSingleMdd(multiSelectField, BuyerDetails.XCheckboxOfMddElements, 2);

            SetMethod.SaveNWaitforSuccess();
            SetMethod.ClosePage();


            #region Add Payment Detail to the Selected Buyer


            #endregion


        }

        //  [Test]

        public void BuyerBillingInfoTest()
        {
            int startDropdownFieldIndex = 0;
            int startcloseMultiTextFieldIndex = 0;
            int startTextFieldInContactPersonAddressIndex = 0;
            int startTextFieldsInBillingAddressIndex = 0;

            WaitFor.ElementToVisible(By.XPath(".//*[@id='100000198']/a/span")).Click();
            BuyerDetails.LandingOnThisPageNWaitForSelectBtn("Buyer Billing Info");

            BuyerDetails.SelectBuyer("Test Buyer");
            WaitFor.ElementNotVisible(".//div[@id='overlay-container']/div[2][@id='overlay-content']");
            WaitFor.AnyErr();
            WaitFor.ElementToVisible(By.XPath(BuyerDetails.XTextFieldsInOfficeInfo));

            var textFieldsInBillingAddress = GetMethod.WebElementsList(BuyerDetails.XTextFieldsInOfficeInfo);
            SetMethod.EnterText(textFieldsInBillingAddress, BuyerDetails.Input4OfficeAddress());
            startTextFieldsInBillingAddressIndex = textFieldsInBillingAddress.Count;

            var dropdownList = GetMethod.WebElementsList(BuyerDetails.XDropdown);
            SetMethod.SelectDropdownValue(dropdownList);
            startDropdownFieldIndex = dropdownList.Count;

            #region Contact Person Address Entry

            SetMethod.ClickOn(xpath: BuyerDetails.XButtonContactPersonAddress);

            var textFieldInContactPersonAddress = GetMethod.WebElementsList(BuyerDetails.XTextFieldsInBasicInfoNPerson);
            SetMethod.EnterText(textFieldInContactPersonAddress, BuyerDetails.Input4ContactPersonAddress());
            startTextFieldInContactPersonAddressIndex = textFieldInContactPersonAddress.Count;

            dropdownList = GetMethod.WebElementsList(BuyerDetails.XDropdown);
            SetMethod.SelectDropdownValue(dropdownList, startFieldIndex: startDropdownFieldIndex);
            startDropdownFieldIndex = dropdownList.Count;

            IList<IWebElement> closeMultiTextFieldList = GetMethod.WebElementsList(BuyerDetails.XCloseMultiTextField);
            SetMethod.FillupAllMultiTextFields(BuyerDetails.XCloseMultiTextField, 3);
            startcloseMultiTextFieldIndex = closeMultiTextFieldList.Count;

            SetMethod.ClickOn(xpath: BuyerDetails.XButtonAnotherContactPersonAddress);

            textFieldInContactPersonAddress = GetMethod.WebElementsList(BuyerDetails.XTextFieldsInBasicInfoNPerson);
            SetMethod.EnterText(textFieldInContactPersonAddress, BuyerDetails.Input4ContactPersonAddress(), startFieldIndex: startTextFieldInContactPersonAddressIndex);
            startTextFieldInContactPersonAddressIndex = textFieldInContactPersonAddress.Count;

            dropdownList = GetMethod.WebElementsList(BuyerDetails.XDropdown);
            SetMethod.SelectDropdownValue(dropdownList, startFieldIndex: startDropdownFieldIndex);
            startDropdownFieldIndex = dropdownList.Count;

            closeMultiTextFieldList = GetMethod.WebElementsList(BuyerDetails.XCloseMultiTextField);
            SetMethod.FillupAllMultiTextFields(BuyerDetails.XCloseMultiTextField, 3);
            startcloseMultiTextFieldIndex = closeMultiTextFieldList.Count;

            SetMethod.ClickOn(xpath: BuyerDetails.XButtonAnotherBillingAddress);

            textFieldsInBillingAddress = GetMethod.WebElementsList(BuyerDetails.XTextFieldsInOfficeInfo);
            SetMethod.EnterText(textFieldsInBillingAddress, BuyerDetails.Input4OfficeAddress(), startFieldIndex: startTextFieldsInBillingAddressIndex);
            startTextFieldsInBillingAddressIndex = textFieldsInBillingAddress.Count;

            dropdownList = GetMethod.WebElementsList(BuyerDetails.XDropdown);
            SetMethod.SelectDropdownValue(dropdownList, startFieldIndex: startDropdownFieldIndex);
            startDropdownFieldIndex = dropdownList.Count;

            #endregion

        }

       // [Test]

        public void CreateBuyerWithAllDetails()
        {
            //WaitFor.ElementToVisible(By.XPath(".//*[@id='100000198']/a/span")).Click();

            //BuyerName.CreateBuyer("Test Buyer 1", "TB1");

            //BuyerProfile.CreateProfileFor("friends");

            //BuyerPaymentDetail.AddPaymentDetailFor("friends");

            //BuyerBillingInfo.AddBillingInfoFor("CECIL", 3, true);
            //BuyerShippingInfo.AddShippingInfoFor("Cecil", 3, true);
            // BuyerContactPerson.AddBuyerContactPersonFor("Cecil", numberOfContactPerson: 3);
            //BuyerBankInfo.AddBuyerBankInfoFor("C&A", numberOfBank: 2, contactPersonAddress: true, numberOfContactPerson: 2);

            //BuyerSeason.AddBuyerSeason4("C&A", 5, SetValueFor.Month.November);
           // BuyerBrand.AddBuyerBrand("Etam", 100);
            //BuyerDepartment.AddBuyerDepartment("Etam", 15);
            //BuyerCountry.AddBuyerCountry("Etam", 14);

        }

        [Test]
        public void UniqueItemPage ()
        {            
          UniqueItemStructureEntry.CreateUniqueItemStructure();
          //CustomerSetup.CreateProfileFor("A.K.M Knit Wear Ltd.");
          //CustomerSetup.AddCustomerContactPersonFor("Ayesha Clothing Co. Ltd.", numberOfContactPerson: 4);
          //CustomerSetup.AddBillingInfoFor("A J Super Garments Ltd.", numberOfBillingAddress: 3);
          //CustomerSetup.AddShippingInfoFor("A J Super Garments Ltd.", numberOfBillingAddress: 3);
          //BuyerItemCode.CreateBic("C&A", BuyerItemCode.ItmName[2], BuyerItemCode.Type[3]);
            //Order.LandingOnCreateOrderPage();
            //Order.EntryInBasicInfo();
            //CustomerBankInfo.CustomerMenu();
            //CustomerBankInfo.CustomerBankInfoEntry();
            
            
            
        }

        //   [TearDown]

        public void CloseBrowser()
        {
            PropertiesCollection.driver.Close();
        }
    }


}

