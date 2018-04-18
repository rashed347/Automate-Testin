using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework.Constraints;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace nextAutomation
{
    class BuyerItemCode
    {
        public static List<string> BuyerName = new List<string>()
        {
            "C&A",
            "CECIL",
            "H&M HENNES & MAURITZ AB"
        };

        public static List<string> ItmName = new List<string>()
        {
            "Rotary Printed Label",
            "Screen Printed Label",
            "Woven label"
        };

        public static List<string> Type = new List<string>()
        {
            "Size",
            "Main",
            "Care",
            "Main with Size",
            "Care with Size"
        };

        public static string  XallTextField =
                ".//input[@type='text' and not(@name='searchText' or @data-ng-readonly='true'  or parent::* [@ng-hide]  or @readonly or @disabled = 'disabled' or @data-ng-disabled='true' or @data-ng-model='i.text'or ancestor::*[@ng-show = 'false'] or ancestor::*[@style='display: none;'] or ancestor::*[@class = 'row ng-hide'] )] | .//textarea";

        public static string XdropDown = ".//div[contains(@class, 'ni-select fa-caret-down')]/select[not(@disabled = 'disabled' or ancestor::*[contains(@class, 'ng-hide')])]";

        public static string XCloseMultiSelectDropdownField = ".//div/button[@class = 'btn btn-multiselect ng-binding' and not(@disabled = 'disabled' or ancestor::*[contains(@class, 'ng-hide')])]";

        public static string XValueOfMddElements =
            ".//div[contains(@class, 'dropdown multi-select') and contains(@class, 'open')]//li[not(@class='list-group-item p-none')]";

        public static string XCheckboxOfMddElements = XValueOfMddElements + "/div/label/input";

        public static string XItmtblHeader4FixedClmn =
            ".//div[@class = 'ni-table-header' and not(ancestor::*[@ng-show = 'false'] or ancestor::*[@style='display: none;'] or ancestor::*[@class = 'row ng-hide'])]/child :: div[contains(@class, 'ni-table-cell') and @title and not (contains(@class, 'ng-hide') or contains(@data-ng-repeat, 'attrValue'))]";
       
        public static string XItmtblHeader4AttrValue = ".//div[@class = 'ni-table-header' and not(ancestor::*[@ng-show = 'false'] or ancestor::*[@style='display: none;'] or ancestor::*[@class = 'row ng-hide'])]/child :: div[contains(@class, 'ni-table-cell') and contains(@data-ng-repeat, 'attrValue') and @title and not (contains(@class, 'ng-hide'))]";

        public static string XallAttrValueFldInItmtbl =
            ".//div[@class = 'ni-table-row' and not(ancestor::*[@ng-show = 'false'] or ancestor::*[@style='display: none;'] or ancestor::*[@class = 'row ng-hide'])]/child :: div[contains(@class, 'ni-table-cell') and not (contains(@class, 'ng-hide'))]/div[contains(@data-ng-if, 'attrValue')]";

        public static string XallFxdFldInItmtbl =
            ".//div[@class = 'ni-table-row' and not(ancestor::*[@ng-show = 'false'] or ancestor::*[@style='display: none;'] or ancestor::*[@class = 'row ng-hide'])]/child :: div[contains(@class, 'ni-table-cell') and not (contains(@class, 'ng-hide') or contains(@ng-repeat, 'attrValue') or child::*[@ng-form = 'optionForm'])]";

        public static IList<IWebElement> AllTextfield()
        {
            var allTextField = GetMethod.WebElementsList(XallTextField);
            return allTextField;
        }

        public static IList<IWebElement> AllDropdown()
        {
            var allDropdown = GetMethod.WebElementsList(XdropDown);
            return allDropdown;
        }

        public static void FillupAttrFlds(string headerXpath, string fieldXpath)
        {
            var headerList = GetMethod.WebElementsList(headerXpath);
            var fldList = GetMethod.WebElementsList(fieldXpath);

            var textFieldList = GetMethod.WebElementsList(fieldXpath + "//input");
            var dropDownFieldList = GetMethod.WebElementsList(fieldXpath + "//select");

            var textFldsName = new List<string>();
            var drpDwnFldsName = new List<string>();

            var textFieldValues = SetValueFor.AttributeValues4TextField();

            if (headerList.Count == fldList.Count)
            {
                for (int i = 0; i < headerList.Count; i++)
                {
                    if (fldList[i].GetAttribute("data-ng-if").Contains("3"))
                    {
                        textFldsName.Add(headerList[i].GetAttribute("title"));
                    }

                    else if (fldList[i].GetAttribute("data-ng-if").Contains("1"))
                    {
                        drpDwnFldsName.Add(headerList[i].GetAttribute("title")); 
                    }

                    else
                    {
                        Console.WriteLine("Here are some others field");
                    }
                }
            }
            if (textFieldList.Count == textFldsName.Count & textFieldList.Count <= textFieldValues.Count)
            {
                for (int j = 0; j < textFieldList.Count; j++)
                {
                    for (int k = 0; k < textFieldValues.Count; k++)
                    {
                        if (textFldsName[j] == textFieldValues.Keys.ElementAt(k))
                        {
                            textFieldList[j].SendKeys(textFieldValues[textFieldValues.Keys.ElementAt(k)]);
                        }
                    }
                }
            }

            
                for (int l = 0; l < drpDwnFldsName.Count; l++)
                {
                    var dropDown = new SelectElement(dropDownFieldList[l]);
                    int numberDdElement = dropDown.Options.Count - 1;
                    //Console.WriteLine(numberDdElement);
                    if (numberDdElement > 0)
                    {
                        dropDown.SelectByValue(SetMethod.RandomNumber(0, numberDdElement).ToString());

                    }
                }
            }
            

        public static void LandingOnBicPage()
        {
            MainMenuSection.ClickTopMenuItem("ACCESSORY");
            MainMenuSection.ClickOnMenuItem("Merchandising And Support");
            MainMenuSection.ClickOnMenuItem("General Settings");
            MainMenuSection.ClickOnMenuItem("Buyer Item Code");
            MainMenuSection.ClickOnMenuItem("Create Buyer Item Code");
            WaitFor.ElementNotVisible(".//*[@id='overlay-content']/i");
            WaitFor.AnyErr();

            
        }

        public static void EntryInBasicInfo(string buyer)
        {
            WaitFor.ElementToAppear(By.XPath(XdropDown));
            var allDropdown = AllDropdown();
            //var allDropdown = GetMethod.WebElementsList(XdropDown);

            var buyerDropdown = new SelectElement(allDropdown[0]);
            buyerDropdown.SelectByText(buyer);
            WaitFor.ElementNotVisible(".//div[@id='overlay-container']/div[2][@id='overlay-content']");

            var brandDropdown = new SelectElement(allDropdown[1]);
            int numbDropdownElm = brandDropdown.Options.Count;
            brandDropdown.SelectByIndex(SetMethod.RandomNumber(1,numbDropdownElm));

            var descriptionField = AllTextfield()[1];
            descriptionField.SendKeys("This is a test description");
           
            SetMethod.SelectArtwork();

        }

        public static void EntryInItemDetailGrid(string itm, string typ)
        {
            var fxdDrpDwn = GetMethod.WebElementsList(XallFxdFldInItmtbl + "//select");
            var fxdTxtFld = GetMethod.WebElementsList(XallFxdFldInItmtbl + "//input");

            new SelectElement(fxdDrpDwn[0]).SelectByText(itm);
            new SelectElement(fxdDrpDwn[1]).SelectByText(typ);

            for (int l = 2; l < fxdDrpDwn.Count; l++)
            {
                var dropDown = new SelectElement(fxdDrpDwn[l]);
                int numberDdElement = dropDown.Options.Count - 1;
                //Console.WriteLine(numberDdElement);
                if (numberDdElement > 0)
                {
                    dropDown.SelectByValue(SetMethod.RandomNumber(0, numberDdElement).ToString());

                }
            }

            fxdTxtFld[0].SendKeys(SetMethod.RandomNumberGenerator(4));
            fxdTxtFld[1].Click();
            fxdTxtFld[1].SendKeys(Keys.Backspace + SetMethod.RandomNumberGenerator(2));

            FillupAttrFlds(XItmtblHeader4AttrValue, XallAttrValueFldInItmtbl);

        }

        public static void CreateBic(string buyer, string itm, string typ)
        {
            LandingOnBicPage();
            EntryInBasicInfo(buyer);
            EntryInItemDetailGrid(itm, typ);
            var bicCodeField = AllTextfield()[0];
            bicCodeField.SendKeys("BIC_" + typ.Replace(" ", "") + "_" + SetMethod.RandomNumberGenerator(2));

            SetMethod.SaveNWaitforSuccess();
        }


    
    }
}
