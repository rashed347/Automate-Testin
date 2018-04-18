using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.PageObjects;
using OpenQA.Selenium.Support.UI;

namespace nextAutomation
{
    class SetMethod
    {
        public static IWebElement SaveBtn()
        {
            var savBtn = GetMethod.WebElement(".//button[@class = 'btn btn-success' and contains(text(), 'Save') and not(@disabled)]");
            return savBtn;
        }

        public static void EnterText(IList<IWebElement> allTextFields, List<string> value, int startFieldIndex = 0)
        {

            for (int i = startFieldIndex, j = 0; i < allTextFields.Count; i++, j++)
            {
                allTextFields[i].SendKeys(value[j]);
            }


        }

        public static void ClickOn(IWebElement element = null, string xpath = null, string cssValue = null, string btnName = null)
        {
            if (element != null)
            {
                element.Click();
            }

            else if (xpath != null)
            {
                PropertiesCollection.driver.FindElement(By.XPath(xpath)).Click();
            }

            else if (cssValue != null)
            {
                PropertiesCollection.driver.FindElement(By.CssSelector(cssValue)).Click();
            }

            else if (btnName != null)
            {
                string xButton =
                    ".//button[contains (text(), \'" + btnName + "\') and not(ancestor::*[@style='display: none;'] or ancestor::*[@aria-hidden='true'] or ancestor::*[@ng-show = 'false'])]";
                IWebElement button = GetMethod.WebElement(xButton);
                button.Click();
            }


        }



        public static void MouseOver(IWebElement element)
        {   
            
            var action = new Actions(PropertiesCollection.driver);
            action.MoveToElement(element).Perform();
        }

        public static void SelectArtwork()
        {
            string xFromComp = ".//i[@class = 'fa fa-file-image-o']/following-sibling:: input";
            
            ClickOn(btnName: "Browse");
         IWebElement fromComputer =   WaitFor.ElementToAppear(By.XPath(xFromComp));
         
         fromComputer.SendKeys(@"C:\Users\Rashed\Documents\Bandicam\Order\BUG_57476.png");
            ClickOn(btnName:"Upload");
            WaitFor.ElementNotVisible(".//div[@id='overlay-container']/div[2][@id='overlay-content']");
            //Thread.Sleep(1000);
            ClickOn(btnName:"Close");
        }

        public static void SelectDropdownValue(IList<IWebElement> element, int? whichField = null, int? indexOfDdElement = null, int? startFieldIndex = null)
        {
            int numberofDropdownField = element.Count;
            if (indexOfDdElement.HasValue & startFieldIndex.HasValue & whichField == null)
            {
                for (int i = startFieldIndex.Value; i < numberofDropdownField; i++)
                {
                    var dropDown = new SelectElement(element[i]);
                    dropDown.SelectByIndex(indexOfDdElement.Value);

                }
            }

            else if (indexOfDdElement == null & startFieldIndex.HasValue & whichField == null)
            {

                for (int i = startFieldIndex.Value; i < numberofDropdownField; i++)
                {

                    var dropDown = new SelectElement(element[i]);
                    IList<IWebElement> getAllOptions = dropDown.Options;
                    int numberOfOptions = getAllOptions.Count;
                    dropDown.SelectByIndex(new Random().Next(1, numberOfOptions));

                }
            }

            else if (indexOfDdElement == null & startFieldIndex == null & whichField == null)
            {
                for (int i = 0; i < numberofDropdownField; i++)
                {
                    var dropDown = new SelectElement(element[i]);
                    IList<IWebElement> getAllOptions = dropDown.Options;
                    int numberOfOptions = getAllOptions.Count;
                    dropDown.SelectByIndex(new Random().Next(1, numberOfOptions));

                }
            }

            else if (indexOfDdElement.HasValue & startFieldIndex == null & whichField.HasValue)
            {
                var dropDown = new SelectElement(element[whichField.Value]);
                dropDown.SelectByIndex(indexOfDdElement.Value);
            }

            else if (indexOfDdElement == null & startFieldIndex == null & whichField.HasValue)
            {
                var dropDown = new SelectElement(element[whichField.Value]);
                IList<IWebElement> getAllOptions = dropDown.Options;
                int numberOfOptions = getAllOptions.Count;
                dropDown.SelectByIndex(new Random().Next(1, numberOfOptions));
            }
            else
            {
                Console.WriteLine("problem in dropdown Method");
            }

        }

        public static void CheckDisabledSaveBtn()
        {
            IWebElement SaveBtn = PropertiesCollection.driver.FindElement(By.CssSelector(".btn.btn-success"));

            if (SaveBtn.Enabled)
            {
                Console.WriteLine(" Failed: Save Button is Active");
                Console.WriteLine(" Expected: Save Button should be Inactive");

            }

            else
            {
                Console.WriteLine("Pass: Save Button is Inactive");
            }
        }

        public static void CheckEnabledSaveBtnAndClickAfterFillup()
        {
            if (SaveBtn().Enabled)
            {
                Console.WriteLine(" Pass: Save Button is Active");
                SaveBtn().Click();
            }

            else
            {
                Console.WriteLine("Failed: Save Button is Inactive");
            }
        }

        public static void CheckDisabledReseTBtn()
        {
            IWebElement ResetBtn = PropertiesCollection.driver.FindElement(By.CssSelector(".btn.btn-danger"));

            if (ResetBtn.Enabled)
            {
                Console.WriteLine(" Failed: Reset Button is Active");
                Console.WriteLine(" Expected: Reset Button should be Inactive");

            }

            else
            {
                Console.WriteLine("Pass: Reset Button is Inactive");
            }
        }

        public static void CheckEnabledResetBtnAndClickAfterFillup()
        {
            IWebElement ResetBtn = WaitFor.ElementToClickable(By.CssSelector(".btn.btn-danger"));

            if (ResetBtn.Enabled)
            {
                Console.WriteLine(" Pass: Reset Button is Active");
                ResetBtn.Click();
            }

            else
            {
                Console.WriteLine("Failed: Reset Button is Inactive");
                Console.WriteLine(" Expected: Reset Button should be Active");
            }
        }

        public static void CheckboxStateByDefault(IWebElement checkBox)
        {
            if (GetMethod.IsCheckboxSelected(checkBox))
            {
                Console.WriteLine("Pass: Checkbox is selected by default");
            }
            else
            {
                Console.WriteLine("Failed: Checkbox is not selected by default");
                Console.WriteLine("Expected: Checkbox should be selected by default");
            }
        }

        public static void ClickOnEditNextToNewEntryAfterCheckGrid(string commonRowId, int numberOfColumn, int numberOfDataColumn, string[] inputValues)
        {
            int desiredRow = GetMethod.CheckGridAndGetDesiredRow(commonRowId, numberOfDataColumn, inputValues);

            String cellId = commonRowId + "[" + desiredRow + "]/td[" + numberOfColumn + "]/ul/li[1]/a/i";

            IWebElement editIcon = PropertiesCollection.driver.FindElement(By.XPath(cellId));
            editIcon.Click();
        }

        public static void ClickOnDeleteNextToNewEntry(string commonRowId, int desiredRow, int numberOfColumn)
        {
            String cellId = commonRowId + "[" + desiredRow + "]/td[" + numberOfColumn + "]/ul/li[2]/a/i";

            IWebElement deleteIcon = PropertiesCollection.driver.FindElement(By.XPath(cellId));
            deleteIcon.Click();
        }

        #region Random Methods

        private static readonly Random Ran = new Random();
        private static readonly object SyncLock = new object();
        public static int RandomNumber(int min, int max)
        {
            lock (SyncLock)
            { // synchronize
                return Ran.Next(min, max);
            }
        }


        public static string RandomValue(List<string> listName)
        {
            string randomValue = listName[RandomNumber(0,listName.Count)];
            return randomValue;
        }

        public static string RandomNumberGenerator(int digit)
        {
            string a = "";


            for (int i = 0; i < digit; i++)
            {


                if (i != 0)
                {
                    a = a + RandomNumber(0,10);
                }
                else
                {
                    a = a + RandomNumber(1, 10);
                }

            }
            return a;
        }

        public static string RandomAlphabetic(int numberOfCharacter)
        {
            string a = "";
            

            for (int i = 0; i < numberOfCharacter; i++)
            {

                a = a + RandomValue(SetValueFor.Alphabetic);
            }
            return a;
        }

        public static string RandomAlphanumeric(int numberOfCharacter, int digit, string prefix = "alphabetic")
        {
            string a;
            if (prefix == "alphabetic")
            {
                a = RandomAlphabetic(numberOfCharacter) + RandomNumberGenerator(digit);
            }
            else
            {
                a = RandomNumberGenerator(digit) + RandomAlphabetic(numberOfCharacter);
            }
            return a;
        } 
        #endregion

        //public static List<string> SelectDeselectElementsFromSingleMdd(string xcloseField, string xpathForCheckboxOfMddElements, int selectedElementNumber = 0, string xpathForValueOfMddElements = null)
        //{
        //    var mddValues = new List<string>();
        //    string xpath4EmptyCheckbox =
        //         xcloseField + "//parent::div[contains(@class, 'open')] //child::div/ul/li//i[@class = 'fa fa-square-o']";

        //    var closeField = GetMethod.WebElement(xcloseField);
        //    closeField.Click();

        //    WaitFor.ElementToAppear(By.XPath(xpath4EmptyCheckbox));

        //    IList<IWebElement> emptyCheckboxOfMddElements =
        //        PropertiesCollection.driver.FindElements(By.XPath(xpath4EmptyCheckbox));
        //    if (selectedElementNumber == 0 || selectedElementNumber >= emptyCheckboxOfMddElements.Count)
        //    {
        //        for (int j = 0; j < emptyCheckboxOfMddElements.Count; j++)
        //        {
        //            emptyCheckboxOfMddElements[j].Click();

        //            if (xpathForValueOfMddElements != null)
        //            {
        //                IList<IWebElement> valueOfMddElements =
        //                PropertiesCollection.driver.FindElements(By.XPath(xpathForValueOfMddElements));
        //                mddValues.Add(valueOfMddElements[j].Text);
        //            }

        //        }
        //    }
        //    else if (selectedElementNumber <= emptyCheckboxOfMddElements.Count)
        //    {
        //        for (int j = 0; j < selectedElementNumber; j++)
        //        {
        //            emptyCheckboxOfMddElements[j].Click();

        //            if (xpathForValueOfMddElements != null)
        //            {
        //                IList<IWebElement> valueOfMddElements =
        //                    PropertiesCollection.driver.FindElements(By.XPath(xpathForValueOfMddElements));
        //                mddValues.Add(valueOfMddElements[j].Text);
        //            }

        //        }
        //    }

        //    else
        //    {
        //        throw new Exception("Some problem in select mdd values method");
        //    }


        //    ClickOnFooter();
        //    return mddValues;

        //}

        //public static List<string> SelectDeselectElementsFromAllMdd(string xpathForCheckboxOfMddElements, IList<IWebElement> closeMultiSelectDropdownField, int numberOfMddfield = 0, int selectedElementNumber = 0, string xpathForValueOfMddElements = null)
        //{
        //    var mddValues = new List<string>();


        //    if (numberOfMddfield == 0 || numberOfMddfield >= closeMultiSelectDropdownField.Count)
        //    {
        //        foreach (IWebElement closeField in closeMultiSelectDropdownField)
        //        {
        //            mddValues.AddRange(SelectDeselectElementsFromSingleMdd(closeField, xpathForCheckboxOfMddElements,
        //                selectedElementNumber: selectedElementNumber,
        //                xpathForValueOfMddElements: xpathForValueOfMddElements));
        //        }

        //    }

        //    else
        //    {
        //        for (int i = 0; i < numberOfMddfield; i++)
        //        {
        //            mddValues.AddRange(SelectDeselectElementsFromSingleMdd(closeMultiSelectDropdownField[i], xpathForCheckboxOfMddElements,
        //                selectedElementNumber: selectedElementNumber,
        //                xpathForValueOfMddElements: xpathForValueOfMddElements));
        //        }
        //    }
        //    return mddValues;
        //}

        public static void ClickOnFooter()
        {
            //PropertiesCollection.driver.FindElement(By.XPath(".//ul[@id='mw-footer-tab']")).Click();
            var workPhoneFields = GetMethod.WebElementsList(xpath: ".//input[@data-ng-model='contact.workPhone']");
            var accountNameField = GetMethod.WebElementsList(xpath: ".//input[@data-ng-model='bank.acctName']");
            var allTextField =
                GetMethod.WebElementsList(
                    xpath:
                        "//input[@type='text' and not(@name='searchText' or @data-ng-readonly='true'  or parent::* [@ng-hide]  or @readonly or @disabled = 'disabled' or @data-ng-disabled='true' or @data-ng-model='i.text'or ancestor::*[@ng-show = 'false'] or ancestor::*[@style='display: none;'] or ancestor::*[contains(@class, 'ng-hide')] or contains(@class, 'hasDatepicker') or ancestor::*[contains(@class,'time-picker-container')])]");

            if (workPhoneFields.Count != 0)
                workPhoneFields[workPhoneFields.Count - 1].Click();
            
            else if (accountNameField.Count != 0)
            {
                accountNameField[accountNameField.Count - 1].Click();
            }

            else
            {
                allTextField[allTextField.Count-1].Click();
            }
            //PropertiesCollection.driver.FindElement(By.XPath(".//input[@data-ng-model='contact.workPhone']")).Click();
        }

        public static void FillupMultiTextField(IWebElement closeField, string xpathForCloseMultiTextField, string value = null, List<string> inputList = null)
        {
            string xpathForMultiTextField = xpathForCloseMultiTextField + "[contains(@class, 'open')]/ul/li/div/input";
            string xpathForAddMoreBtn = xpathForCloseMultiTextField + "[contains(@class, 'open')]/ul/li/button";
            closeField.Click();
            if (inputList == null && value != null)
            {
                IList<IWebElement> multiTextField =
                    PropertiesCollection.driver.FindElements(By.XPath(xpathForMultiTextField));

                multiTextField[0].SendKeys(value);
            }
            else if (inputList != null && value == null)
            {
                IList<IWebElement> multiTextField =
                    PropertiesCollection.driver.FindElements(By.XPath(xpathForMultiTextField));
                multiTextField[0].SendKeys(inputList[0]);

                for (int i = 1; i < inputList.Count; i++)
                {
                    //IWebElement addMoreBtn = PropertiesCollection.driver.FindElement(By.XPath(xpathForAddMoreBtn));
                    //addMoreBtn.Click();
                    WaitFor.ElementToVisible(By.XPath(xpathForAddMoreBtn)).Click();

                    multiTextField = PropertiesCollection.driver.FindElements(By.XPath(xpathForMultiTextField));
                    multiTextField[i].SendKeys(inputList[i]);
                }
            }

            else
            {
                throw new Exception("There are no input values");
            }
            ClickOnFooter();
            Thread.Sleep(500);
        }

        public static void FillupAllMultiTextFields(string xpathForCloseMultiTextField, int numberofMultiTextField, bool singleField = true)
        {
            IList<IWebElement> closeMultiTextField = GetMethod.WebElementsList(xpathForCloseMultiTextField);

            for (int i = closeMultiTextField.Count - numberofMultiTextField; i < closeMultiTextField.Count; i++)
            {
                if (closeMultiTextField[i].GetAttribute("data-ng-model").Contains("mobile"))
                {
                    if (singleField)
                    {
                        FillupMultiTextField(closeMultiTextField[i], xpathForCloseMultiTextField, value: SetValueFor.MobileNoList[RandomNumber(0, SetValueFor.MobileNoList.Count)]);
                    }

                    else
                    {
                        FillupMultiTextField(closeMultiTextField[i], xpathForCloseMultiTextField, inputList: SetValueFor.MobileNoList);
                    }

                }

                #region DisableRegion
                else if (closeMultiTextField[i].GetAttribute("data-ng-model").Contains("phone"))
                {
                    if (singleField)
                    {
                        FillupMultiTextField(closeMultiTextField[i], xpathForCloseMultiTextField, value: SetValueFor.PhoneNoList[RandomNumber(0, SetValueFor.PhoneNoList.Count)]);
                    }

                    else
                    {
                        FillupMultiTextField(closeMultiTextField[i], xpathForCloseMultiTextField, inputList: SetValueFor.PhoneNoList);
                    }

                }

                else if (closeMultiTextField[i].GetAttribute("data-ng-model").Contains("fax"))
                {
                    if (singleField)
                    {
                        FillupMultiTextField(closeMultiTextField[i], xpathForCloseMultiTextField, value: SetValueFor.FaxNoList[RandomNumber(0, SetValueFor.FaxNoList.Count)]);
                    }

                    else
                    {
                        FillupMultiTextField(closeMultiTextField[i], xpathForCloseMultiTextField, inputList: SetValueFor.FaxNoList);
                    }
                }
                #endregion

                else if (closeMultiTextField[i].GetAttribute("type") == "email")
                {
                    if (singleField)
                    {
                        FillupMultiTextField(closeMultiTextField[i], xpathForCloseMultiTextField, value: SetValueFor.EmailIdList[RandomNumber(0, SetValueFor.EmailIdList.Count)]);
                    }

                    else
                    {
                        FillupMultiTextField(closeMultiTextField[i], xpathForCloseMultiTextField, inputList: SetValueFor.EmailIdList);
                    }
                }

                else if (closeMultiTextField[i].GetAttribute("type") == "website")
                {
                    if (singleField)
                    {
                        FillupMultiTextField(closeMultiTextField[i], xpathForCloseMultiTextField, value: SetValueFor.WebsiteList[RandomNumber(0, SetValueFor.WebsiteList.Count)]);
                    }

                    else
                    {
                        FillupMultiTextField(closeMultiTextField[i], xpathForCloseMultiTextField, inputList: SetValueFor.WebsiteList);
                    }

                }

                else
                {
                    Console.WriteLine(closeMultiTextField[i].GetAttribute("id") + "id is not present in the method");
                }

            }
        }

        public static void EditMultiTextField(IWebElement closeField, string xpathForMultiTextField)
        {
            closeField.Click();
            IList<IWebElement> multiTextField = PropertiesCollection.driver.FindElements(By.XPath(xpathForMultiTextField));


            for (int i = 0; i < multiTextField.Count; i++)
            {
                multiTextField[0].SendKeys(Keys.Home + "test");
            }

            ClickOnFooter();
        }

        public static void EditAllMultiTextFields(IList<IWebElement> closeMultiTextField, string xpathForMultiTextField)
        {
            foreach (var closeField in closeMultiTextField)
            {
                if (closeField.GetAttribute("id") == "mobile")
                {
                    EditMultiTextField(closeField, xpathForMultiTextField);
                }

                else if (closeField.GetAttribute("id") == "phone")
                {
                    EditMultiTextField(closeField, xpathForMultiTextField);

                }

                else if (closeField.GetAttribute("id") == "email")
                {
                    EditMultiTextField(closeField, xpathForMultiTextField);
                }

                else if (closeField.GetAttribute("id") == "fax")
                {
                    EditMultiTextField(closeField, xpathForMultiTextField);
                }

                else if (closeField.GetAttribute("id") == "website")
                {
                    EditMultiTextField(closeField, xpathForMultiTextField);
                }

                else
                {
                    Console.WriteLine(closeField.GetAttribute("id") + "id is not present in the method");
                }

            }
        }

        public static void EditAllTextFields(IList<IWebElement> allTextFields, string value)
        {
            foreach (var textField in allTextFields)
            {
                textField.SendKeys(value);
            }
        }

        public static void CheckTwoEqualLists(List<string> firstList, List<string> secondList)
        {
            if (firstList.SequenceEqual(secondList))
            {
                Console.WriteLine("Pass: Two lists are equal");
            }
            else
            {
                Console.WriteLine("Failed: Two lists are not equal");
            }
        }

        public static void ClosePage()
        {
            ClickOn(xpath: ".//div[@class='wi-header-actions']//i[@class='ni ni-close']");
        }

        public static void SaveNWaitforSuccess()
        {
            ClickOn(btnName:"Save");
            WaitFor.ElementNotVisible(".//div[@id='overlay-container']/div[2][@id='overlay-content']");
            WaitFor.AnyErr();
            WaitFor.SuccessMessage();
        }

    }

}
