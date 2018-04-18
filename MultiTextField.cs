using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;

namespace nextAutomation
{
   public class MultiTextField
    {
       private static IList<IWebElement> CloseMultiTextFieldList(string fieldName)
       {
           string xpath = ".//label[contains(text(), '"+ fieldName + "')]" +
                          "//following-sibling::div[contains(@class, 'dropdown dynamic-textbox')]/button";

           IList<IWebElement> fieldList = GetMethod.WebElementsList(xpath);

           return fieldList;
       }



       private static IList<IWebElement> MultiTextFieldList()
       {
           string xpath =
               ".//label//following-sibling::div[contains(@class, 'dropdown dynamic-textbox') " +
               "and contains(@class, 'open')]/button/following-sibling::ul/li//input";

           IList<IWebElement> fieldList = GetMethod.WebElementsList(xpath);

           return fieldList;
       }

       private static void ClickAddMoreBtn(string fieldName)
       {
           string xpath = ".//label[contains(text(), '" + fieldName + "')]" +
                          "//following-sibling::div[contains(@class, 'dropdown dynamic-textbox') " +
                          "and contains(@class, 'open')]/button/following-sibling::ul/li/button";

           SetMethod.ClickOn(xpath:xpath);
       }

       public void SendValue(string fieldName, int fieldIndex, string value)
       {
           if (value != "")
           {
               List<string> valueList = value.Split(',').ToList();
               int numberOfValue = valueList.Count;

               CloseMultiTextFieldList(fieldName)[fieldIndex].Click();
               var inputFieldList = MultiTextFieldList();
               inputFieldList[0].Clear();
               inputFieldList[0].SendKeys(valueList[0]);

               if (numberOfValue > 1)
               {
                   for (int i = 1; i < numberOfValue; i++)
                   {
                       ClickAddMoreBtn(fieldName);

                       inputFieldList = MultiTextFieldList();
                       inputFieldList[i].Clear();
                       inputFieldList[i].SendKeys(valueList[0]);
                   }

               }
           }
       } 
    }
}
