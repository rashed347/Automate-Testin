using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework.Constraints;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using OpenQA.Selenium.Support.UI;

namespace nextAutomation
{
    class MainMenuSection
    {
        public static void SelectBuyerDropdownValue(int index)
        {
            string webElement = ".//*[@id='dropdown-menu100000376']/li[" + index + "]/a";
            WaitFor.ElementToVisible(By.XPath(webElement)).Click();

        }

        public static void ClickTopMenuItem(string menuItemName)
        {
            var topMenuItemList = new List<string>{"home", "common modules", "accessory", "master settings", "system admin"};

            for (int i = 0; i < topMenuItemList.Count; i++)
            {
                if (topMenuItemList[i] == menuItemName.ToLower())
                {
                    GetMethod.WebElement(xpath: ".//div/ul[@class = 'top-menu']/li[" + (i+1) + "]/a/span").Click();
                    WaitFor.ElementToVisible(By.XPath(".//div[contains(@class, 'sm-item-link')]"));
                }
            }
        }

        public static void ClickOnMenuItem(string x)
        {
            string itemXpath1 = ".//div[contains(@class, 'sm-item-link')]//child::span[contains (text(), \'" + x + "\')]";
            string conditionXpath1 = ".//span[contains (text(), \'" + x + "\')]//ancestor :: div[contains(@class, 'sm-item-link')]";

            string itemXpath2 = ".//div[@class = 'sm-item-link dropdown sub-active open']//child:: li/a[contains(text (), \'" + x + "\')]";

            string itemXpath3 = ".//div[@class = 'sm-item-link dropdown sub-active open']//child::li[@class = 'dropdown-submenu']/ul/li/a[contains(text(),\'" + x + "\')]";
                   

                    IList<IWebElement> conditionElement1 = GetMethod.WebElementsList(xpath: conditionXpath1);
                    IList<IWebElement> conditionElement2 = GetMethod.WebElementsList(xpath: itemXpath2);
                    IList<IWebElement> conditionElement3 = GetMethod.WebElementsList(xpath: itemXpath3);

            if (conditionElement1.Count != 0)
            {
                if (conditionElement1[0].GetAttribute("data-ng-click").Contains("\"pageTitle\":null"))
                {

                    if (conditionElement1[0].GetAttribute("class").Contains("sm-item-link dropdown"))
                    {
                        GetMethod.WebElement(xpath: itemXpath1).Click();
                        WaitFor.ElementToVisible(
                            By.XPath(".//div[@class = 'sm-item-link dropdown sub-active open']//child:: li/a"));
                    }
                    else
                    {
                        GetMethod.WebElement(xpath: itemXpath1).Click();
                        WaitFor.ElementToVisible(By.XPath(".//div[contains(@class, 'sm-item-link')]"));
                    }
                }

                else
                {
                    GetMethod.WebElement(xpath: itemXpath1).Click();
                    WaitFor.ElementToVisible(By.XPath(".//div[@class = 'wi-title']"));
                }
            }

            else if (conditionElement2.Count != 0)
            {
                if (conditionElement2[0].GetAttribute("class") == "dropdown-toggle")
                {
                    SetMethod.MouseOver(conditionElement2[0]);
                    WaitFor.ElementToVisible(By.XPath(".//div[@class = 'sm-item-link dropdown sub-active open']//child:: li/ul/li[1]"));
                }

                else
                {
                    GetMethod.WebElement(xpath:itemXpath2).Click();
                    WaitFor.ElementToVisible(By.XPath(".//div[@class = 'wi-title']"));
                }
            }

            else if (conditionElement3.Count != 0)
            {
                GetMethod.WebElement(xpath: itemXpath3).Click();
                WaitFor.ElementToVisible(By.XPath(".//div[@class = 'wi-title']"));
            }

            else
            {
                Console.WriteLine("This Item Does not exist or item name spelling is wrong");
            }

}



    }
}
