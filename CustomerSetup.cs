using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;

namespace nextAutomation
{
    class CustomerSetup
    {
        public static void LandingOnPage(string x)
        {
            MainMenuSection.ClickTopMenuItem("Master Settings");
            MainMenuSection.ClickOnMenuItem("Customer");

            MainMenuSection.ClickOnMenuItem(x);
            WaitFor.ElementNotVisible(".//*[@id='overlay-content']/i");
            WaitFor.AnyErr();
        }

        public static void SelectCustomer(string customerName)
        {

            SetMethod.ClickOn(btnName: "Select");

            WaitFor.ElementToVisible(By.XPath(BuyerDetails.XBuyerNameSearchField)).SendKeys(customerName);

            SetMethod.ClickOn(xpath: BuyerDetails.XSelectIconBlPopup);
        }

         public static void CreateProfileFor(string customerName)
        {

            #region Landing On Customer Profile Page

            LandingOnPage("Customer Profile");

            SelectCustomer(customerName);
            WaitFor.ElementNotVisible(".//div[@id='overlay-container']/div[2][@id='overlay-content']");
            WaitFor.AnyErr();

            #endregion


            #region Input Values and All Fields in the Form

            var textFieldValues = new List<string>
            {
                "REG-" + SetMethod.RandomNumberGenerator(4),
                "TIN-" + SetMethod.RandomNumberGenerator(4),
                "VAT-" + SetMethod.RandomNumberGenerator(4),
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
                SetMethod.RandomValue(SetValueFor.State),
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

            //IList<IWebElement> textFieldsInBasicInfo =
            //    PropertiesCollection.driver.FindElements(By.XPath(XTextFieldsInBasicInfoNPerson));

            //IList<IWebElement> textFieldsInOfficeInfo =
            //    PropertiesCollection.driver.FindElements(By.XPath(XTextFieldsInOfficeInfo));

             var allTextFields =
                 GetMethod.WebElementsList(".//input[@type='text' and not(@name='searchText' or @data-ng-readonly='true'  or parent::* [@ng-hide] or parent::* [@class = 'time-picker-container'] or @readonly or @disabled = 'disabled' or @data-ng-disabled='true' or @data-ng-model='i.text'or ancestor::*[@ng-show = 'false'] or ancestor::*[@style='display: none;'] or ancestor::*[@class = 'row ng-hide'] )] | .//textarea");

            IList<IWebElement> closeMultiSelectDropdownField =
                PropertiesCollection.driver.FindElements(By.XPath(BuyerDetails.XCloseMultiSelectDropdownField));

            IList<IWebElement> closeMultiTextField =
                PropertiesCollection.driver.FindElements(By.XPath(BuyerDetails.XCloseMultiTextField));

            IList<IWebElement> dropDownList =
                PropertiesCollection.driver.FindElements(By.XPath(BuyerDetails.XDropdown));

            #endregion

            SetMethod.EnterText(allTextFields, textFieldValues);

            //var selectedElements = SetMethod.SelectDeselectElementsFromAllMdd(BuyerDetails.XCheckboxOfMddElements,
            //    closeMultiSelectDropdownField, selectedElementNumber: 1, xpathForValueOfMddElements: BuyerDetails.XValueOfMddElements);
            //GetMethod.GetListElementsConsole(selectedElements);

            SetMethod.FillupAllMultiTextFields(BuyerDetails.XCloseMultiTextField, 5);

            BuyerProfile.SelectValueFromAllDropdown();

            //SetMethod.SaveNWaitforSuccess();

            //SetMethod.ClosePage();
        }

         public static void AddCustomerContactPersonFor(string customerName, int numberOfContactPerson = 1)
         {
             int startDropdownFieldIndex = 0;
             int startTextFieldInContactPersonAddressIndex = 0;
             int startTextFieldsInBillingAddressIndex = 0;

             #region Landing On BuyerContactPerson page

             LandingOnPage("Customer Contact Person Info");

             SelectCustomer(customerName);
             WaitFor.ElementNotVisible(".//div[@id='overlay-container']/div[2][@id='overlay-content']");
             WaitFor.AnyErr();
             //WaitFor.ElementToVisible(By.XPath(XTextFieldsInOfficeInfo));

             #endregion
             int k = 0;

             for (int i = 0; i < numberOfContactPerson; i++)
             {
                 if (k > 0 && k < numberOfContactPerson)
                 {
                     SetMethod.ClickOn(btnName: "Add Another Contact Person");

                     BuyerDetails.ScrollDown();
                 }
                 var textFieldInContactPersonAddress = GetMethod.WebElementsList(BuyerDetails.XTextFieldsInBasicInfoNPerson);

                 var contactInput = BuyerDetails.Input4ContactPersonAddress();

                 SetMethod.EnterText(textFieldInContactPersonAddress, contactInput,
                                 startFieldIndex: startTextFieldInContactPersonAddressIndex);
                 startTextFieldInContactPersonAddressIndex = textFieldInContactPersonAddress.Count;

                 var dropdownList = GetMethod.WebElementsList(BuyerDetails.XDropdown);
                 SetMethod.SelectDropdownValue(dropdownList, startFieldIndex: startDropdownFieldIndex);
                 startDropdownFieldIndex = dropdownList.Count;

                 // var closeMultiTextFieldList = GetMethod.WebElementsList(XCloseMultiTextField);
                 SetMethod.FillupAllMultiTextFields(BuyerDetails.XCloseMultiTextField, 3);


                 var textFieldsInBillingAddress = GetMethod.WebElementsList(BuyerDetails.XTextFieldsInOfficeInfo);

                 List<string> input = BuyerDetails.Input4OfficeAddress();


                 SetMethod.EnterText(textFieldsInBillingAddress, input,
                     startFieldIndex: startTextFieldsInBillingAddressIndex);
                 startTextFieldsInBillingAddressIndex = textFieldsInBillingAddress.Count;

                 dropdownList = GetMethod.WebElementsList(BuyerDetails.XDropdown);
                 SetMethod.SelectDropdownValue(dropdownList, startFieldIndex: startDropdownFieldIndex);
                 startDropdownFieldIndex = dropdownList.Count;
                 k++;
             }

             SetMethod.SaveNWaitforSuccess();
             //SetMethod.ClosePage();
         }

         public static void AddBillingInfoFor(string customerName, int numberOfBillingAddress = 1, bool contactPersonAddress = false,
             int numberofContactPersonAddress = 1)
         {
             int startDropdownFieldIndex = 0;
             int startTextFieldInContactPersonAddressIndex = 0;
             int startTextFieldsInBillingAddressIndex = 0;

             #region Landing On BillingInfo page

             LandingOnPage("Customer wise Billing Info");

             SelectCustomer(customerName);
             WaitFor.ElementNotVisible(".//div[@id='overlay-container']/div[2][@id='overlay-content']");
             WaitFor.AnyErr();
             WaitFor.ElementToVisible(By.XPath(BuyerDetails.XTextFieldsInOfficeInfo));

             #endregion

             int k = 0;

             for (int i = 0; i < numberOfBillingAddress; i++)
             {
                 if (k > 0 && k < numberOfBillingAddress)
                 {
                     SetMethod.ClickOn(btnName: "Add Another Billing Address");
                     BuyerDetails.ScrollDown();
                 }

                 var textFieldsInBillingAddress = GetMethod.WebElementsList(BuyerDetails.XTextFieldsInOfficeInfo);

                 List<string> input = BuyerDetails.Input4OfficeAddress();


                 SetMethod.EnterText(textFieldsInBillingAddress, input,
                     startFieldIndex: startTextFieldsInBillingAddressIndex);
                 startTextFieldsInBillingAddressIndex = textFieldsInBillingAddress.Count;

                 var dropdownList = GetMethod.WebElementsList(BuyerDetails.XDropdown);
                 SetMethod.SelectDropdownValue(dropdownList, startFieldIndex: startDropdownFieldIndex);
                 startDropdownFieldIndex = dropdownList.Count;

                 #region Contact Person Address

                 if (contactPersonAddress)
                 {
                     SetMethod.ClickOn(btnName: "Add Address Contact Person Details");

                     BuyerDetails.ScrollDown();
                     int l = 0;
                     for (int j = 0; j < numberofContactPersonAddress; j++)
                     {

                         if (l >= 1 && l < numberofContactPersonAddress)
                         {
                             IList<IWebElement> elements = GetMethod.WebElementsList(xpath:BuyerDetails.XButtonAnotherContactPersonAddress);
                             SetMethod.ClickOn(element: elements[elements.Count - 1]);

                             BuyerDetails.ScrollDown();
                         }


                         var textFieldInContactPersonAddress = GetMethod.WebElementsList(xpath:BuyerDetails.XTextFieldsInBasicInfoNPerson);

                         var contactInput = BuyerDetails.Input4ContactPersonAddress();

                         SetMethod.EnterText(textFieldInContactPersonAddress, contactInput,
                             startFieldIndex: startTextFieldInContactPersonAddressIndex);
                         startTextFieldInContactPersonAddressIndex = textFieldInContactPersonAddress.Count;

                         dropdownList = GetMethod.WebElementsList(BuyerDetails.XDropdown);
                         SetMethod.SelectDropdownValue(dropdownList, startFieldIndex: startDropdownFieldIndex);
                         startDropdownFieldIndex = dropdownList.Count;

                         // var closeMultiTextFieldList = GetMethod.WebElementsList(XCloseMultiTextField);
                         SetMethod.FillupAllMultiTextFields(BuyerDetails.XCloseMultiTextField, 3);
                         BuyerDetails.ScrollDown();

                         l++;

                     }
                 }

                 #endregion



                 k++;
             }

             //SetMethod.SaveNWaitforSuccess();
             //SetMethod.ClosePage();

         }

         public static void AddShippingInfoFor(string customerName, int numberOfBillingAddress = 1, bool contactPersonAddress = false,
             int numberofContactPersonAddress = 1)
         {
             int startDropdownFieldIndex = 0;
             int startTextFieldInContactPersonAddressIndex = 0;
             int startTextFieldsInBillingAddressIndex = 0;

             #region Landing On shippingInfo page

             LandingOnPage("Customer wise Ship/Delivery Info");

             SelectCustomer(customerName);
             WaitFor.ElementNotVisible(".//div[@id='overlay-container']/div[2][@id='overlay-content']");
             WaitFor.AnyErr();
             WaitFor.ElementToVisible(By.XPath(BuyerDetails.XTextFieldsInOfficeInfo));

             #endregion

             int k = 0;

             for (int i = 0; i < numberOfBillingAddress; i++)
             {
                 if (k > 0 && k < numberOfBillingAddress)
                 {
                     SetMethod.ClickOn(btnName: "Add Another Shipping Address");
                     BuyerDetails.ScrollDown();
                 }

                 var textFieldsInBillingAddress = GetMethod.WebElementsList(BuyerDetails.XTextFieldsInOfficeInfo);

                 List<string> input = BuyerDetails.Input4OfficeAddress();


                 SetMethod.EnterText(textFieldsInBillingAddress, input,
                     startFieldIndex: startTextFieldsInBillingAddressIndex);
                 startTextFieldsInBillingAddressIndex = textFieldsInBillingAddress.Count;

                 var dropdownList = GetMethod.WebElementsList(BuyerDetails.XDropdown);
                 SetMethod.SelectDropdownValue(dropdownList, startFieldIndex: startDropdownFieldIndex);
                 startDropdownFieldIndex = dropdownList.Count;

                 #region Contact Person Address

                 if (contactPersonAddress)
                 {
                     SetMethod.ClickOn(btnName: "Add Address Contact Person Details");

                     BuyerDetails.ScrollDown();
                     int l = 0;
                     for (int j = 0; j < numberofContactPersonAddress; j++)
                     {

                         if (l >= 1 && l < numberofContactPersonAddress)
                         {
                             IList<IWebElement> elements = GetMethod.WebElementsList(xpath: BuyerDetails.XButtonAnotherContactPersonAddress);
                             SetMethod.ClickOn(element: elements[elements.Count - 1]);

                             BuyerDetails.ScrollDown();
                         }


                         var textFieldInContactPersonAddress = GetMethod.WebElementsList(xpath: BuyerDetails.XTextFieldsInBasicInfoNPerson);

                         var contactInput = BuyerDetails.Input4ContactPersonAddress();

                         SetMethod.EnterText(textFieldInContactPersonAddress, contactInput,
                             startFieldIndex: startTextFieldInContactPersonAddressIndex);
                         startTextFieldInContactPersonAddressIndex = textFieldInContactPersonAddress.Count;

                         dropdownList = GetMethod.WebElementsList(BuyerDetails.XDropdown);
                         SetMethod.SelectDropdownValue(dropdownList, startFieldIndex: startDropdownFieldIndex);
                         startDropdownFieldIndex = dropdownList.Count;

                         // var closeMultiTextFieldList = GetMethod.WebElementsList(XCloseMultiTextField);
                         SetMethod.FillupAllMultiTextFields(BuyerDetails.XCloseMultiTextField, 3);
                         BuyerDetails.ScrollDown();

                         l++;

                     }
                 }

                 #endregion



                 k++;
             }

             SetMethod.SaveNWaitforSuccess();
             //SetMethod.ClosePage();

         }
    }
    }

