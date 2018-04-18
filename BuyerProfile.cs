using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using Excel = Microsoft.Office.Interop.Excel;
namespace nextAutomation
{
    public class BuyerDetails
    {

        #region Xpath Region

        public static string XSelectButton = ".//button[@data-ng-click='showBuyerModal()']";

        public static string XBuyerNameSearchField =
            ".//div[@class = 'modal-body']//input[@data-ng-model = 'searchCriteria.buyer']";

        public static string XSelectIconBlPopup = ".//i[@class = 'ni ni-select-record']";

        public static string XDropdown = ".//div[contains(@class, 'ni-select fa-caret-down')]/select";

        public static string XCloseMultiSelectDropdownField = ".//div/button[@class = 'btn btn-multiselect ng-binding']";

        public static string XValueOfMddElements =
            ".//div[contains(@class, 'form-control dropdown multi-select') and contains(@class, 'open')]//li[not(@class='list-group-item p-none')]";

        public static string XCheckboxOfMddElements = XValueOfMddElements + "/div/label/input";

        public static string XTextFieldsInOfficeInfo =
            ".//div[@class = 'row']//div[@class='col-md-2']//input[@type='text']|//textarea";

        public static string XTextFieldsInBasicInfoNPerson =
            ".//div[@class='col-md-3']//input[@type='text'][not(@name='searchText')][not(@data-ng-readonly='true')][not(@data-ng-model='i.text')][not(contains(@id,'buyerShort'))]";

        public static string XCloseMultiTextField = ".//div[contains(@class,'form-control p-none dropdown dynamic-textbox')]";

        public static string XMultiTextField = XCloseMultiTextField + "[contains(@class, 'open')]/ul/li/div/input";

        public static string XAddMoreBtn = XCloseMultiTextField + "[contains(@class, 'open')]/ul/li/button";

        public static string XButtonContactPersonAddress = ".//button[@class='btn btn-link']";

        public static string XButtonAnotherContactPersonAddress = ".//button[@class='btn btn-primary']";

        public static string XButtonAnotherBillingAddress = ".//button[@class='btn btn-primary ']";

        #endregion




        #region Input Value Region

        public static List<string> Input4OfficeAddress()
        {
            var input = new List<string>
            {
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
            return input;
        }

        public static List<string> Input4ContactPersonAddress()
        {
            var input = new List<string>
            {
                SetMethod.RandomValue(SetValueFor.FirstName),
                SetMethod.RandomValue(SetValueFor.MiddleName),
                SetMethod.RandomValue(SetValueFor.LastName),
                SetMethod.RandomValue(SetValueFor.Designation),
                SetMethod.RandomValue(SetValueFor.PhoneNoList),
                SetMethod.RandomValue(SetValueFor.PhoneNoList)
            };
            return input;
        }

        public static List<string> Input4BankSection()
        {
            string accountNo = SetMethod.RandomNumberGenerator(13);
            string bINNo = SetMethod.RandomNumberGenerator(7);
            string swiftCode = SetMethod.RandomAlphabetic(6);
            string aBANumber = SetMethod.RandomNumberGenerator(6);
            var input = new List<string>

            {
                SetMethod.RandomValue(SetValueFor.Bank),
                SetMethod.RandomValue(SetValueFor.Branch),
                SetMethod.RandomValue(SetValueFor.FirstName),
                accountNo,
                bINNo,
               swiftCode,
               aBANumber,
                SetMethod.RandomNumberGenerator(6)
            };
            return input;
        }

        #endregion



        public static void SelectBuyer(string buyerName)
        {

            SetMethod.ClickOn(xpath: XSelectButton);

            WaitFor.ElementToVisible(By.XPath(XBuyerNameSearchField)).SendKeys(buyerName);

            SetMethod.ClickOn(xpath: XSelectIconBlPopup);
        }

        public static void LandingOnThisPageNWaitForSelectBtn( string x)
        {
            MainMenuSection.ClickTopMenuItem("Master Settings");
            MainMenuSection.ClickOnMenuItem("Buyer");
            
            MainMenuSection.ClickOnMenuItem(x);
            WaitFor.ElementNotVisible(".//*[@id='overlay-content']/i");
            WaitFor.AnyErr();
            WaitFor.ElementToVisible(By.XPath(XSelectButton));
        }

        public static void ScrollDown()
        {
            var villageFields = GetMethod.WebElementsList(xpath: ".//div/input[@data-ng-model='address.village']");
            var lastNameFields = GetMethod.WebElementsList(xpath: ".//div//input[@data-ng-model='contact.lastName']");

            if (lastNameFields.Count != 0)
            {
                lastNameFields[lastNameFields.Count - 1].SendKeys(Keys.PageDown);
                Thread.Sleep(500);
            }

            else
            {
                villageFields[villageFields.Count - 1].SendKeys(Keys.PageDown);
                Thread.Sleep(500);
            }
        }
    }

    public class BuyerName : BuyerDetails
    {
        public static string XTextFields = ".//*[@name='buyerBasicForm']/div//input[@type='text']";

        public static string XCheckBox = ".//*[@class='checkbox']/label/input";

        public static string XGridHeadId =
                ".//*[@class='col-md-8 fl-row-box ng-scope']/div/div[2]/div[2]/div/.//*[@blank-grid='gridOptions']/thead/tr/th";

        public static string XGridCommonRowId =
                ".//*[@class='col-md-8 fl-row-box ng-scope']/div/div[2]/div[2]/div/.//*[@blank-grid='gridOptions']/tbody/tr";

        public static void CreateBuyer(string buyerName, string buyerShortName)
        {
            WaitFor.ElementToVisible(By.XPath(".//*[@id='100000376']/a/span")).Click();
            MainMenuSection.SelectBuyerDropdownValue(1);
            WaitFor.ElementNotVisible(".//*[@id='overlay-content']/i");
            WaitFor.AnyErr();

            var formField = GetMethod.WebElementsList(XTextFields);

            formField[0].SendKeys(buyerName); //Buyer Name
            formField[1].SendKeys(buyerShortName); //Buyer Short Name

            SetMethod.CheckEnabledSaveBtnAndClickAfterFillup();
            WaitFor.ElementNotVisible(".//*[@id='overlay-content']/i");
            WaitFor.AnyErr();
            WaitFor.SuccessMessage();
        }
    }

    public class BuyerProfile : BuyerDetails
    {

        public static void SelectValueFromAllDropdown()
        {
            IList<IWebElement> dropDownList = PropertiesCollection.driver.FindElements(By.XPath(XDropdown));
            SetMethod.SelectDropdownValue(dropDownList);
            //foreach (var dropdown in dropDownList)
            //{
            //    if (dropdown.GetAttribute("data-ng-model") == "model.buyer.companyTypeID")
            //    {
            //        IList<IWebElement> numberOfDropDownValue =
            //            PropertiesCollection.driver.FindElements(By.XPath(XDropdown +
            //                                                              "[@data-ng-model = 'model.buyer.companyTypeID']/option"));

            //        SetMethod.SelectDropdownValue(dropdown, numberOfDropDownValue.Count - 1);
            //    }

            //    if (dropdown.GetAttribute("data-ng-model") == "model.buyer.address.countryID")
            //    {
            //        IList<IWebElement> numberOfDropDownValue =
            //            PropertiesCollection.driver.FindElements(By.XPath(XDropdown +
            //                                                              "[@data-ng-model = 'model.buyer.address.countryID']/option"));

            //        SetMethod.SelectDropdownValue(dropdown, numberOfDropDownValue.Count - 1);
            //    }

            //}
        }

        public static List<string> GetValuesOfAllFields()
        {
            var textFieldsInBasicInfo = GetMethod.WebElementsList(XTextFieldsInBasicInfoNPerson);

            var textFieldsInOfficeInfo = GetMethod.WebElementsList(XTextFieldsInOfficeInfo);

            var closeMultiSelectDropdownField = GetMethod.WebElementsList(XCloseMultiSelectDropdownField);

            var closeMultiTextField = GetMethod.WebElementsList(XCloseMultiTextField);

            var dropDownList = GetMethod.WebElementsList(XDropdown);

            IList<IWebElement> allTextFields =
                new List<IWebElement>(textFieldsInBasicInfo.Concat(textFieldsInOfficeInfo));

            var values = new List<string>();

            for (int i = 0; i < allTextFields.Count(); i++)
            {
                values.Add(allTextFields[i].GetAttribute("value"));
            }

            for (int i = 0; i < closeMultiSelectDropdownField.Count(); i++)
            {
                values.AddRange(GetMethod.GetValuesFromMultiValuesField(closeMultiSelectDropdownField[i]));
            }

            for (int i = 0; i < closeMultiTextField.Count(); i++)
            {
                values.AddRange(GetMethod.GetValuesFromMultiValuesField(closeMultiTextField[i]));
            }

            for (int i = 0; i < dropDownList.Count(); i++)
            {
                values.Add(dropDownList[i].GetAttribute("value"));
            }

            return values;
        }

        public static void CreateProfileFor(string buyerName)
        {

            #region Landing On Buyers Profile Page

            WaitFor.ElementToVisible(By.XPath(".//*[@id='100000198']/a/span")).Click();
            LandingOnThisPageNWaitForSelectBtn(x: "Buyer Profile");
            WaitFor.ElementNotVisible(".//div[@id='overlay-container']/div[2][@id='overlay-content']");
            WaitFor.AnyErr();

            SelectBuyer(buyerName);
            WaitFor.ElementNotVisible(".//div[@id='overlay-container']/div[2][@id='overlay-content']");
            WaitFor.AnyErr();

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
                PropertiesCollection.driver.FindElements(By.XPath(XTextFieldsInBasicInfoNPerson));

            IList<IWebElement> textFieldsInOfficeInfo =
                PropertiesCollection.driver.FindElements(By.XPath(XTextFieldsInOfficeInfo));

            IList<IWebElement> allTextFields =
                new List<IWebElement>(textFieldsInBasicInfo.Concat(textFieldsInOfficeInfo));

            IList<IWebElement> closeMultiSelectDropdownField =
                PropertiesCollection.driver.FindElements(By.XPath(XCloseMultiSelectDropdownField));

            IList<IWebElement> closeMultiTextField =
                PropertiesCollection.driver.FindElements(By.XPath(XCloseMultiTextField));

            IList<IWebElement> dropDownList =
                PropertiesCollection.driver.FindElements(By.XPath(XDropdown));

            #endregion

            SetMethod.EnterText(allTextFields, textFieldValues);

            //var selectedElements = SetMethod.SelectDeselectElementsFromAllMdd(XCheckboxOfMddElements,
            //    closeMultiSelectDropdownField, selectedElementNumber: 1, xpathForValueOfMddElements: XValueOfMddElements, numberOfMddfield: 1);
            //GetMethod.GetListElementsConsole(selectedElements);

            SetMethod.FillupAllMultiTextFields(XCloseMultiTextField, 5);

            SelectValueFromAllDropdown();

            SetMethod.CheckEnabledSaveBtnAndClickAfterFillup();
            WaitFor.ElementNotVisible(".//div[@id='overlay-container']/div[2][@id='overlay-content']");
            WaitFor.AnyErr();
            WaitFor.SuccessMessage();

            SetMethod.ClosePage();
        }
    }

    public class BuyerPaymentDetail : BuyerDetails
    {
        public static void AddPaymentDetailFor(string buyerName)
        {
            MainMenuSection.ClickTopMenuItem("Master Settings");
            MainMenuSection.ClickOnMenuItem("Buyer");
            MainMenuSection.ClickOnMenuItem("Buyer wise Payment Detail Entry");
            
            WaitFor.ElementNotVisible(".//div[@id='overlay-container']/div[2][@id='overlay-content']");
            WaitFor.AnyErr();

            SelectBuyer(buyerName);
            WaitFor.ElementNotVisible(".//div[@id='overlay-container']/div[2][@id='overlay-content']");
            WaitFor.AnyErr();

            var dropDownList = GetMethod.WebElementsList(XDropdown);
            SetMethod.SelectDropdownValue(dropDownList, whichField: 0, indexOfDdElement: 7);

            //var multiSelectField = GetMethod.WebElement(XCloseMultiSelectDropdownField);
            //SetMethod.SelectDeselectElementsFromSingleMdd(multiSelectField, XCheckboxOfMddElements, 2);

            SetMethod.SaveNWaitforSuccess();
            SetMethod.ClosePage();
        }
    }


    public class BuyerBillingInfo : BuyerDetails
    {

        public static void AddBillingInfoFor(string buyerName, int numberOfBillingAddress = 1, bool contactPersonAddress = false,
            int numberofContactPersonAddress = 1)
        {
            int startDropdownFieldIndex = 0;
            int startTextFieldInContactPersonAddressIndex = 0;
            int startTextFieldsInBillingAddressIndex = 0;

            #region Landing On BillingInfo page

            WaitFor.ElementToVisible(By.XPath(".//*[@id='100000198']/a/span")).Click();
            LandingOnThisPageNWaitForSelectBtn(x: "Buyer Billing Info");
            WaitFor.ElementNotVisible(".//div[@id='overlay-container']/div[2][@id='overlay-content']");
            WaitFor.AnyErr();

            SelectBuyer(buyerName);
            WaitFor.ElementNotVisible(".//div[@id='overlay-container']/div[2][@id='overlay-content']");
            WaitFor.AnyErr();
            WaitFor.ElementToVisible(By.XPath(XTextFieldsInOfficeInfo));

            #endregion

            int k = 0;

            for (int i = 0; i < numberOfBillingAddress; i++)
            {
                if (k > 0 && k < numberOfBillingAddress)
                {
                    SetMethod.ClickOn(xpath: XButtonAnotherBillingAddress);

                    ScrollDown();
                }

                var textFieldsInBillingAddress = GetMethod.WebElementsList(XTextFieldsInOfficeInfo);

                List<string> input = Input4OfficeAddress();


                SetMethod.EnterText(textFieldsInBillingAddress, input,
                    startFieldIndex: startTextFieldsInBillingAddressIndex);
                startTextFieldsInBillingAddressIndex = textFieldsInBillingAddress.Count;

                var dropdownList = GetMethod.WebElementsList(XDropdown);
                SetMethod.SelectDropdownValue(dropdownList, startFieldIndex: startDropdownFieldIndex);
                startDropdownFieldIndex = dropdownList.Count;

                #region Contact Person Address

                if (contactPersonAddress)
                {
                    SetMethod.ClickOn(xpath: XButtonContactPersonAddress);

                    ScrollDown();
                    int l = 0;
                    for (int j = 0; j < numberofContactPersonAddress; j++)
                    {

                        if (l >= 1 && l < numberofContactPersonAddress)
                        {
                            IList<IWebElement> elements = GetMethod.WebElementsList(XButtonAnotherContactPersonAddress);
                            SetMethod.ClickOn(element: elements[elements.Count - 1]);

                            ScrollDown();
                        }


                        var textFieldInContactPersonAddress = GetMethod.WebElementsList(XTextFieldsInBasicInfoNPerson);

                        var contactInput = Input4ContactPersonAddress();

                        SetMethod.EnterText(textFieldInContactPersonAddress, contactInput,
                            startFieldIndex: startTextFieldInContactPersonAddressIndex);
                        startTextFieldInContactPersonAddressIndex = textFieldInContactPersonAddress.Count;

                        dropdownList = GetMethod.WebElementsList(XDropdown);
                        SetMethod.SelectDropdownValue(dropdownList, startFieldIndex: startDropdownFieldIndex);
                        startDropdownFieldIndex = dropdownList.Count;

                        // var closeMultiTextFieldList = GetMethod.WebElementsList(XCloseMultiTextField);
                        SetMethod.FillupAllMultiTextFields(XCloseMultiTextField, 3);

                        l++;

                    }
                }

                #endregion



                k++;
            }

            SetMethod.SaveNWaitforSuccess();
            SetMethod.ClosePage();

        }
    }

    public class BuyerShippingInfo : BuyerDetails
    {
        public static void AddShippingInfoFor(string buyerName, int numberOfBillingAddress = 1, bool contactPersonAddress = false,
            int numberofContactPersonAddress = 1)
        {
            int startDropdownFieldIndex = 0;
            int startTextFieldInContactPersonAddressIndex = 0;
            int startTextFieldsInBillingAddressIndex = 0;

            #region Landing On shippingInfo page

            WaitFor.ElementToVisible(By.XPath(".//*[@id='100000198']/a/span")).Click();
            LandingOnThisPageNWaitForSelectBtn(x: "Buyer Shipping Info");
            WaitFor.ElementNotVisible(".//div[@id='overlay-container']/div[2][@id='overlay-content']");
            WaitFor.AnyErr();

            SelectBuyer(buyerName);
            WaitFor.ElementNotVisible(".//div[@id='overlay-container']/div[2][@id='overlay-content']");
            WaitFor.AnyErr();
            WaitFor.ElementToVisible(By.XPath(XTextFieldsInOfficeInfo));

            #endregion

            int k = 0;

            for (int i = 0; i < numberOfBillingAddress; i++)
            {
                if (k > 0 && k < numberOfBillingAddress)
                {
                    SetMethod.ClickOn(xpath: XButtonAnotherBillingAddress);

                    ScrollDown();
                }

                var textFieldsInBillingAddress = GetMethod.WebElementsList(XTextFieldsInOfficeInfo);

                List<string> input = Input4OfficeAddress();


                SetMethod.EnterText(textFieldsInBillingAddress, input,
                    startFieldIndex: startTextFieldsInBillingAddressIndex);
                startTextFieldsInBillingAddressIndex = textFieldsInBillingAddress.Count;

                var dropdownList = GetMethod.WebElementsList(XDropdown);
                SetMethod.SelectDropdownValue(dropdownList, startFieldIndex: startDropdownFieldIndex);
                startDropdownFieldIndex = dropdownList.Count;

                #region Contact Person Address

                if (contactPersonAddress)
                {
                    SetMethod.ClickOn(xpath: XButtonContactPersonAddress);

                    ScrollDown();
                    int l = 0;
                    for (int j = 0; j < numberofContactPersonAddress; j++)
                    {

                        if (l >= 1 && l < numberofContactPersonAddress)
                        {
                            IList<IWebElement> elements = GetMethod.WebElementsList(XButtonAnotherContactPersonAddress);
                            SetMethod.ClickOn(element: elements[elements.Count - 1]);

                            ScrollDown();
                        }


                        var textFieldInContactPersonAddress = GetMethod.WebElementsList(XTextFieldsInBasicInfoNPerson);

                        var contactInput = Input4ContactPersonAddress();

                        SetMethod.EnterText(textFieldInContactPersonAddress, contactInput,
                            startFieldIndex: startTextFieldInContactPersonAddressIndex);
                        startTextFieldInContactPersonAddressIndex = textFieldInContactPersonAddress.Count;

                        dropdownList = GetMethod.WebElementsList(XDropdown);
                        SetMethod.SelectDropdownValue(dropdownList, startFieldIndex: startDropdownFieldIndex);
                        startDropdownFieldIndex = dropdownList.Count;

                        // var closeMultiTextFieldList = GetMethod.WebElementsList(XCloseMultiTextField);
                        SetMethod.FillupAllMultiTextFields(XCloseMultiTextField, 3);

                        l++;

                    }
                }

                #endregion



                k++;
            }

            SetMethod.SaveNWaitforSuccess();
            SetMethod.ClosePage();

        }
    }

    public class BuyerContactPerson : BuyerDetails
    {
        public static void AddBuyerContactPersonFor(string buyerName, int numberOfContactPerson = 1)
        {
            int startDropdownFieldIndex = 0;
            int startTextFieldInContactPersonAddressIndex = 0;
            int startTextFieldsInBillingAddressIndex = 0;

            #region Landing On BuyerContactPerson page

            WaitFor.ElementToVisible(By.XPath(".//*[@id='100000198']/a/span")).Click();
            LandingOnThisPageNWaitForSelectBtn(x: "Buyer Contact Person");
            WaitFor.ElementNotVisible(".//div[@id='overlay-container']/div[2][@id='overlay-content']");
            WaitFor.AnyErr();

            SelectBuyer(buyerName);
            WaitFor.ElementNotVisible(".//div[@id='overlay-container']/div[2][@id='overlay-content']");
            WaitFor.AnyErr();
            WaitFor.ElementToVisible(By.XPath(XTextFieldsInOfficeInfo));

            #endregion
            int k = 0;

            for (int i = 0; i < numberOfContactPerson; i++)
            {
                if (k > 0 && k < numberOfContactPerson)
                {
                    SetMethod.ClickOn(xpath: XButtonAnotherBillingAddress);

                    ScrollDown();
                }
                var textFieldInContactPersonAddress = GetMethod.WebElementsList(XTextFieldsInBasicInfoNPerson);

                var contactInput = Input4ContactPersonAddress();

                SetMethod.EnterText(textFieldInContactPersonAddress, contactInput,
                                startFieldIndex: startTextFieldInContactPersonAddressIndex);
                startTextFieldInContactPersonAddressIndex = textFieldInContactPersonAddress.Count;

                var dropdownList = GetMethod.WebElementsList(XDropdown);
                SetMethod.SelectDropdownValue(dropdownList, startFieldIndex: startDropdownFieldIndex);
                startDropdownFieldIndex = dropdownList.Count;

                // var closeMultiTextFieldList = GetMethod.WebElementsList(XCloseMultiTextField);
                SetMethod.FillupAllMultiTextFields(XCloseMultiTextField, 3);


                var textFieldsInBillingAddress = GetMethod.WebElementsList(XTextFieldsInOfficeInfo);

                List<string> input = Input4OfficeAddress();


                SetMethod.EnterText(textFieldsInBillingAddress, input,
                    startFieldIndex: startTextFieldsInBillingAddressIndex);
                startTextFieldsInBillingAddressIndex = textFieldsInBillingAddress.Count;

                dropdownList = GetMethod.WebElementsList(XDropdown);
                SetMethod.SelectDropdownValue(dropdownList, startFieldIndex: startDropdownFieldIndex);
                startDropdownFieldIndex = dropdownList.Count;
                k++;
            }

            SetMethod.SaveNWaitforSuccess();
            SetMethod.ClosePage();
        }
    }

    public class BuyerBankInfo : BuyerDetails
    {
        public static void AddBuyerBankInfoFor(string buyerName, int numberOfBank = 1, bool contactPersonAddress = false,
            int numberOfContactPerson = 1)
        {
            int startDropdownFieldIndex = 0;
            int startTextFieldInContactPersonAddressIndex = 0;
            int startTextFieldsInBillingAddressIndex = 0;

            #region Landing On BuyerContactPerson page

            WaitFor.ElementToVisible(By.XPath(".//*[@id='100000198']/a/span")).Click();
            LandingOnThisPageNWaitForSelectBtn(x: "Buyer Bank Info");
            WaitFor.ElementNotVisible(".//div[@id='overlay-container']/div[2][@id='overlay-content']");
            WaitFor.AnyErr();

            SelectBuyer(buyerName);
            WaitFor.ElementNotVisible(".//div[@id='overlay-container']/div[2][@id='overlay-content']");
            WaitFor.AnyErr();
            WaitFor.ElementToVisible(By.XPath(XTextFieldsInOfficeInfo));

            #endregion

            #region Bank Section

            int m = 0;

            for (int i = 0; i < numberOfBank; i++)
            {
                if (m > 0 && m < numberOfBank)
                {
                    var blockButtonList = GetMethod.WebElementsList(xpath: XButtonAnotherContactPersonAddress);
                    blockButtonList[blockButtonList.Count-1].Click();
                    ScrollDown();
                }
                var textFieldInContactPersonAddress = GetMethod.WebElementsList(XTextFieldsInBasicInfoNPerson);

                var bankInput = Input4BankSection();

                SetMethod.EnterText(textFieldInContactPersonAddress, bankInput,
                    startFieldIndex: startTextFieldInContactPersonAddressIndex);
                startTextFieldInContactPersonAddressIndex = textFieldInContactPersonAddress.Count;

                //var dropdownList = GetMethod.WebElementsList(XDropdown);
                //SetMethod.SelectDropdownValue(dropdownList, startFieldIndex: startDropdownFieldIndex);
                //startDropdownFieldIndex = dropdownList.Count;

                // var closeMultiTextFieldList = GetMethod.WebElementsList(XCloseMultiTextField);
                SetMethod.FillupAllMultiTextFields(XCloseMultiTextField, 5);


                var textFieldsInBillingAddress = GetMethod.WebElementsList(XTextFieldsInOfficeInfo);

                List<string> input = Input4OfficeAddress();


                SetMethod.EnterText(textFieldsInBillingAddress, input,
                    startFieldIndex: startTextFieldsInBillingAddressIndex);
                startTextFieldsInBillingAddressIndex = textFieldsInBillingAddress.Count;

                var dropdownList = GetMethod.WebElementsList(XDropdown);
                SetMethod.SelectDropdownValue(dropdownList, startFieldIndex: startDropdownFieldIndex);
                startDropdownFieldIndex = dropdownList.Count;

                if (contactPersonAddress)
                {

                    int k = 0;
                    for (int j = 0; j < numberOfContactPerson; j++)
                    {
                        

                        if (k > 0 && k < numberOfContactPerson)
                        {
                            var blockButtonList = GetMethod.WebElementsList(xpath: XButtonAnotherContactPersonAddress);
                            blockButtonList[blockButtonList.Count - 2].Click();

                            ScrollDown();
                        }

                        else
                        {
                            SetMethod.ClickOn(xpath: XButtonContactPersonAddress);
                            ScrollDown();
                        }
                        textFieldInContactPersonAddress = GetMethod.WebElementsList(XTextFieldsInBasicInfoNPerson);

                        var contactInput = Input4ContactPersonAddress();

                        SetMethod.EnterText(textFieldInContactPersonAddress, contactInput,
                            startFieldIndex: startTextFieldInContactPersonAddressIndex);
                        startTextFieldInContactPersonAddressIndex = textFieldInContactPersonAddress.Count;

                        dropdownList = GetMethod.WebElementsList(XDropdown);
                        SetMethod.SelectDropdownValue(dropdownList, startFieldIndex: startDropdownFieldIndex);
                        startDropdownFieldIndex = dropdownList.Count;

                        // var closeMultiTextFieldList = GetMethod.WebElementsList(XCloseMultiTextField);
                        SetMethod.FillupAllMultiTextFields(XCloseMultiTextField, 3);


                        textFieldsInBillingAddress = GetMethod.WebElementsList(XTextFieldsInOfficeInfo);

                        input = Input4OfficeAddress();


                        SetMethod.EnterText(textFieldsInBillingAddress, input,
                            startFieldIndex: startTextFieldsInBillingAddressIndex);
                        startTextFieldsInBillingAddressIndex = textFieldsInBillingAddress.Count;

                        dropdownList = GetMethod.WebElementsList(XDropdown);
                        SetMethod.SelectDropdownValue(dropdownList, startFieldIndex: startDropdownFieldIndex);
                        startDropdownFieldIndex = dropdownList.Count;
                        k++;
                    }

                }
                m++;

            }

            #endregion
            SetMethod.SaveNWaitforSuccess();
            SetMethod.ClosePage();

        }

       
    }

    public class BuyerSeason: BuyerDetails
    {
        public static void AddBuyerSeason4(string buyerName, int seasonRange, SetValueFor.Month month)
        {
            LandingOnThisPageNWaitForSelectBtn("Buyer Season");
            SelectBuyer(buyerName);
            Thread.Sleep(1000);

            var startMonthIndex = (int)month;
            var endMonthIndex = startMonthIndex + (seasonRange - 1);

            for (int i = 0; i < 12/seasonRange; i++)
            {
                if (startMonthIndex > 12)
                {
                    startMonthIndex = startMonthIndex - 12;
                }

                if (endMonthIndex > 12)
                {
                    endMonthIndex = endMonthIndex - 12;
                }
                string xPath = ".//table/thead/following-sibling::tbody/tr[" + (i + 1) + "]";

                string checkbox = xPath + "//input[@type = 'checkbox']";
                SetMethod.ClickOn(xpath:checkbox);

                IWebElement startMonth = GetMethod.WebElement(xpath: xPath + "/td[4]/div/select");
                IWebElement endMonth = GetMethod.WebElement(xpath: xPath + "/td[5]/div/select");

                var dropDown = new SelectElement(startMonth);
                dropDown.SelectByIndex(startMonthIndex);

                dropDown = new SelectElement(endMonth);
                dropDown.SelectByIndex(endMonthIndex);

                startMonthIndex = endMonthIndex + 1;
                endMonthIndex = startMonthIndex + (seasonRange - 1);
            }

            if (12%seasonRange != 0)
            {
                if (startMonthIndex > 12)
                {
                    startMonthIndex = startMonthIndex - 12;
                }

                endMonthIndex = startMonthIndex + (12%seasonRange - 1);

                string xPath = ".//table/thead/following-sibling::tbody/tr[" + (12 / seasonRange + 1) + "]";

                string checkbox = xPath + "//input[@type = 'checkbox']";
                SetMethod.ClickOn(xpath: checkbox);

                IWebElement startMonth = GetMethod.WebElement(xpath: xPath + "/td[4]/div/select");
                IWebElement endMonth = GetMethod.WebElement(xpath: xPath + "/td[5]/div/select");

                var dropDown = new SelectElement(startMonth);
                dropDown.SelectByIndex(startMonthIndex);

                dropDown = new SelectElement(endMonth);
                dropDown.SelectByIndex(endMonthIndex);
            }
        }

    }

    public class BuyerBrand : BuyerDetails
    {
        public static void AddBuyerBrand(string buyerName, int brandNumber)
        {
            LandingOnThisPageNWaitForSelectBtn("Buyer Brand");
            SelectBuyer(buyerName);
            Thread.Sleep(1000);

            string xAllTextField =
                ".//input[@type='text' and not(@name='searchText' or @data-ng-readonly='true' or @readonly or @data-ng-model='i.text'  or ancestor::*[@ng-show = 'false'] or ancestor::*[@style='display: none;'])] | .//textarea";

            int j = 0;
            int k = 0;
            for (int i = 0; i < brandNumber; i++)
            {
                if (i > 0)
                {
                    SetMethod.ClickOn(btnName:"Add Brand");
                }
                IList<IWebElement> allTextField = GetMethod.WebElementsList(xAllTextField);
                int numberOfTextField = allTextField.Count;

                //int index = SetMethod.RandomNumber(0, SetValueFor.Brand.Count);
                string shortName = SetValueFor.Brand.Keys.ElementAt(j);
                if (k>0)
                {
                    allTextField[numberOfTextField - 2].SendKeys(SetValueFor.Brand[shortName] + k);
                    allTextField[numberOfTextField - 1].SendKeys(shortName + k); 
                }
                else
                {
                    allTextField[numberOfTextField - 2].SendKeys(SetValueFor.Brand[shortName]);
                    allTextField[numberOfTextField - 1].SendKeys(shortName);
                }
                
                j++;

                if (j == SetValueFor.Brand.Count)
                {
                    j = 0;
                    k++;
                }
            }

            SetMethod.ClickOn(btnName:"Save");
            

        }
    }

    public class BuyerDepartment : BuyerDetails
    {
        public static void AddBuyerDepartment(string buyerName, int departmentNumber)
        {
            LandingOnThisPageNWaitForSelectBtn("Buyer Department");
            SelectBuyer(buyerName);
            Thread.Sleep(500);

            string xAllTextField =
                ".//input[@type='text' and not(@name='searchText' or @data-ng-readonly='true' or @readonly or @data-ng-model='i.text'  or ancestor::*[@ng-show = 'false'] or ancestor::*[@style='display: none;'])] | .//textarea";

            int j = 0;
            int k = 0;
            for (int i = 0; i < departmentNumber; i++)
            {
                if (i > 0)
                {
                    SetMethod.ClickOn(btnName: "Add Department");
                }
                IList<IWebElement> allTextField = GetMethod.WebElementsList(xAllTextField);
                int numberOfTextField = allTextField.Count;

                //int index = SetMethod.RandomNumber(0, SetValueFor.Brand.Count);
                string shortName = SetValueFor.Brand.Keys.ElementAt(j);
                if (k > 0)
                {
                    allTextField[numberOfTextField - 2].SendKeys(SetValueFor.Brand[shortName] + k);
                    allTextField[numberOfTextField - 1].SendKeys(shortName + k);
                }
                else
                {
                    allTextField[numberOfTextField - 2].SendKeys(SetValueFor.Brand[shortName]);
                    allTextField[numberOfTextField - 1].SendKeys(shortName);
                }

                j++;

                if (j == SetValueFor.Brand.Count)
                {
                    j = 0;
                    k++;
                }
            }

           SetMethod.ClickOn(btnName: "Save");
        }
    }

    public class BuyerCountry : BuyerDetails
    {
        public static void AddBuyerCountry(string buyerName, int numberOfCountry)
        {
            LandingOnThisPageNWaitForSelectBtn("Buyer Country");
            SelectBuyer(buyerName);
            Thread.Sleep(500);

            IList<IWebElement> checkBoxes = GetMethod.WebElementsList(xpath: ".//input[@type = 'checkbox']");
            if (numberOfCountry != 0 & numberOfCountry < checkBoxes.Count)
            {
                for (int i = 1; i <= numberOfCountry ; i++)
                {
                    checkBoxes[i].Click();

                    if (i == 15)
                    {
                        checkBoxes[i].SendKeys(Keys.PageDown);
                        Thread.Sleep(500);
                    }
                }
                SetMethod.ClickOn(btnName:"Save");
            }
            else
            {
                Console.WriteLine("numberOfCountry can't be zero or it cross the number of country");
            }
            
        }
    }

}

