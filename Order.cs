using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;


namespace nextAutomation
{
    public class Order
    {
        public static string XallTxtFld = "//input[@type='text' and not(@name='searchText' " +
                                          "or @data-ng-readonly='true' or parent::* [@ng-hide] " +
                                          "or @readonly or @disabled = 'disabled' or @data-ng-disabled='true' " +
                                          "or @data-ng-model='i.text'or ancestor::*[@ng-show = 'false'] " +
                                          "or ancestor::*[@style='display: none;'] or ancestor::*[contains(@class, 'ng-hide')] " +
                                          "or contains(@class, 'hasDatepicker') or ancestor::*[contains(@class,'time-picker-container')])]";

        public static string XallDrpDwn = "//div[contains(@class, 'ni-select fa-caret-down')]/select[not(@disabled = 'disabled' " +
                                          "or ancestor::*[contains(@class, 'ng-hide')])]";

        public static void LandingOnCreateOrderPage()
        {
            MainMenuSection.ClickTopMenuItem("ACCESSORY");
            MainMenuSection.ClickOnMenuItem("Merchandising And Support");
            MainMenuSection.ClickOnMenuItem("Order Management");
            MainMenuSection.ClickOnMenuItem("Order");
            MainMenuSection.ClickOnMenuItem("Create Order");
            WaitFor.ElementNotVisible(".//*[@id='overlay-content']/i");
            WaitFor.AnyErr();            
        }

        public static void EntryInBasicInfo()
        {
            string xallTxtFld = ".//div[@class = 'col-md-8']" + XallTxtFld;
            string xallDrpDwn = ".//div[@class = 'col-md-8']" + XallDrpDwn;
            string xPath4Label = "//ancestor::div[@class = 'col-md-8']/preceding-sibling::label[@title]";
            

            #region Handle Dropdown In Basic Info Section

            var allDrpDwn = GetMethod.WebElementsList(xallDrpDwn);
            var allDrpDwnLbl = GetMethod.WebElementsList(xallDrpDwn + xPath4Label);

            if (allDrpDwn.Count == allDrpDwnLbl.Count)
            {

                for (int l = 0; l < allDrpDwn.Count; l++)
                {
                    int numberDdElement;
                    var drpDwn = new SelectElement(allDrpDwn[l]);

                    switch (allDrpDwnLbl[l].GetAttribute("title"))
                    {
                        case "Customer":
                            drpDwn.SelectByText("Knit Concern Group");
                            WaitFor.ElementNotVisible(".//div[@id='overlay-container']/div[2][@id='overlay-content']");
                            break;
                       
                        case "Buyer":
                            drpDwn.SelectByText("H&M HENNES & MAURITZ AB");
                            WaitFor.ElementNotVisible(".//div[@id='overlay-container']/div[2][@id='overlay-content']");
                            break;
                        
                        case  "Order Type":
                            drpDwn.SelectByText("Bulk Order");
                            WaitFor.ElementNotVisible(".//div[@id='overlay-container']/div[2][@id='overlay-content']");
                            break;

                        case "Season":
                             numberDdElement = drpDwn.Options.Count - 1;
                            if (numberDdElement > 0)
                            {
                                drpDwn.SelectByValue(SetMethod.RandomNumber(0, numberDdElement).ToString());

                            }
                            WaitFor.ElementNotVisible(".//div[@id='overlay-container']/div[2][@id='overlay-content']");
                            var year = WaitFor.ElementToAppear(By.XPath(".//ul[starts-with(@id, 'autoExpnd')]/li[2]"));
                            year.Click();
                            break;

                        default:

                             numberDdElement = drpDwn.Options.Count - 1;
                            if (numberDdElement > 0)
                            {
                                drpDwn.SelectByValue(SetMethod.RandomNumber(0, numberDdElement).ToString());

                            }
                            break;

                            
                    }
                    
                }

            }

           #endregion

           #region Handle Text Field In Basic Info Section

            var allTxtFld = GetMethod.WebElementsList(xallTxtFld);
            var allTxtFldLbl = GetMethod.WebElementsList(xallTxtFld + xPath4Label);

            if (allTxtFld.Count == allTxtFldLbl.Count)
            {
                for (int i = 0; i < allTxtFld.Count; i++)
                {
                    switch (allTxtFldLbl[i].GetAttribute("title"))
                    {
                        case "Customer's PO":
                            allTxtFld[i].SendKeys("PO-" + SetMethod.RandomNumberGenerator(4));
                            break;

                        case "Style/ProductNo":
                            allTxtFld[i].SendKeys(SetMethod.RandomNumberGenerator(6));
                            break;

                        case "Style/ProductName":
                            allTxtFld[i].SendKeys(SetValueFor.Style_ProductName[SetMethod.RandomNumber(0, SetValueFor.Style_ProductName.Count)]);
                            break;

                        case "RefernceOrderNumber":
                            allTxtFld[i].SendKeys(SetMethod.RandomNumberGenerator(6));
                            break;

                        default:
                            allTxtFld[i].SendKeys("Unknown Field");
                            break;
                    }
                }
            }

            else
            {
                Console.WriteLine("There is a problem betwen text field and it's label");
            }

            #endregion

            MultiSelectDropDown msdd = new MultiSelectDropDown();
            //msdd.SelectcheckboxFrm1StField(checkboxIndex:);
           
        }
    }

}
