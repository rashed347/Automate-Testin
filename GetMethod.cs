using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace nextAutomation
{
    class GetMethod
    {
        public static bool isAlertPresent()
        {
            IAlert alert = ExpectedConditions.AlertIsPresent().Invoke(PropertiesCollection.driver);
            return (alert != null);
        }

        public static IAlert MoveOnAlert()
        {
            return PropertiesCollection.driver.SwitchTo().Alert();
            
        }

        public static bool IsCheckboxSelected(IWebElement checkBox)
        {
           return checkBox.Selected;
        }

        public static string CheckBoxState(IWebElement checkBox)
        {
            if (IsCheckboxSelected(checkBox))
            {
                return  "Yes";
            }
            else
            {
                return "No";
            }
        }


        public static int CheckGridAndGetDesiredRow(string rowId, int numberOfDataColumn, string [] inputValues)
        {
            
            IList<IWebElement> allRow = PropertiesCollection.driver.FindElements(By.XPath(rowId));
            int numberOfRow = allRow.Count;
            int desiredRow = 0;
            string [] cellValues = new string[numberOfDataColumn];
            
            for (int i = 0; i < numberOfDataColumn; i++)
            {
                for (int j = 1; j <= numberOfRow; j++)
                {
                    String cellId = rowId + "[" + j + "]/td[" + (i+1) + "]";

                    IWebElement cell = PropertiesCollection.driver.FindElement(By.XPath(cellId));
                    
                    if (cell.Text == inputValues[i])
                    {
                        cellValues[i] = cell.Text;
                        j = j;
                        numberOfRow = j;
                        desiredRow = j;
                        break;
                    }
                }
            }

            if (cellValues.SequenceEqual(inputValues))
            {
                Console.WriteLine("Pass: Input values are showing in grid");
                return desiredRow;
            }
            else
            {
                Console.WriteLine("Failed: Input values are not appeared on the grid");
                return 0;
            }
        }

        public static List<string> GetValuesFromMultiValuesField(IWebElement closeField)
        {
            
            return closeField.Text.Split(',').ToList();
        } 

       public static void GetListElementsConsole(List<string> testList)
    {
        for (int i = 0; i < testList.Count; i++)
        {
            Console.WriteLine(testList[i]);
        }
    }

        public static IWebElement WebElement(string xpath)
        {
            IWebElement webElement = PropertiesCollection.driver.FindElement(By.XPath(xpath));
            return webElement;
        }

        public static IList<IWebElement> WebElementsList(string xpath)
        {
            IList<IWebElement>  webElementsList = 
                PropertiesCollection.driver.FindElements(By.XPath(xpath));
            return webElementsList;
        } 

     }
}
