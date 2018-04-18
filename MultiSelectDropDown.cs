using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using NUnit.Framework.Constraints;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace nextAutomation
{
    public class MultiSelectDropDown
    {
        private const string XCloseField = ".//div/button[@class = 'btn btn-multiselect ng-binding']";
        private const string XFieldLabel = XCloseField + "/parent::div/parent::div/preceding-sibling::label";
        private const string XEmptyCheckboxLabel = XCloseField + "//parent::div[contains(@class, 'open')] //child::li/div[descendant::input[@type = 'checkbox' and not (@checked)]]";
        private const string XEmptyCheckbox = XEmptyCheckboxLabel + "//input";//".//div/div/ul/li/div/label/input";
        private const string X4SelectedCheckboxLabel = XCloseField + "//parent::div[contains(@class, 'open')] //child::li/div[descendant::input[@type = 'checkbox' and @checked]]";
        private const string X4SelectedCheckbox = X4SelectedCheckboxLabel + "//input";
        

        //private string closeField;
        //private string fieldLabel;
        //private 

        private IList<IWebElement> getElement(string xpath)
        {
            return GetMethod.WebElementsList(xpath);
        }

        private string SelectCheckboxByIndex(int index)
        {
            var lblList = getElement(XEmptyCheckboxLabel);
            var chkBxList = getElement(XEmptyCheckbox);

            if (index < chkBxList.Count & index >= 0)
            {

                chkBxList[index].Click();
                return lblList[index].Text;
            }

            else
            {
                throw new Exception("Entered Index for checkbox is wrong");

            }
        }

        private string SelectCheckboxByLabel(string lbl)
        {
            string lblTxt = "";

            var lblList = getElement(XEmptyCheckboxLabel);
            var chkBxList = getElement(XEmptyCheckbox);

            if (chkBxList.Count == lblList.Count)
            {
                int j = 0;
                for (int i = 0; i < lblList.Count; i++)
                {
                    if (lblList[i].Text == lbl)
                    {
                        lblTxt = SelectCheckboxByIndex(i);
                        j++;
                        break;
                    }
                }
                if (j == 0)
                {
                    throw new Exception("Checkbox label which you entred is not exist in MSDD");

                }

            }
            else
            {
                throw new Exception("Something Wrong Between Checkbox and it's label");

            }

            return lblTxt;


        }


        private string ClickOnCloseField(int? fieldIndex = null, string fieldLabel = null)
        {
            var fieldLblList = getElement(XFieldLabel);
            var fldList = getElement(XCloseField);
            string lblTxt = "";

            if (fldList.Count == fieldLblList.Count)
            {
                if (fieldLabel != null)
                {
                    int j = 0;
                    for (int i = 0; i < fieldLblList.Count; i++)
                    {
                        if (fieldLblList[i].Text == fieldLabel)
                        {
                            fldList[i].Click();
                            j++;
                            lblTxt = fieldLblList[i].Text;
                            break;
                        }
                    }
                    if (j == 0)
                    {
                        throw new Exception("Checkbox label which you entred is not exist in MSDD");

                    }
                }
                else if (fieldIndex.HasValue)
                {
                    if (fieldIndex.Value < fldList.Count & fieldIndex.Value >= 0)
                    {

                        fldList[fieldIndex.Value].Click();
                        lblTxt = fieldLblList[fieldIndex.Value].Text;
                    }

                    else
                    {
                        throw new Exception("Entered Index for checkbox is wrong");

                    }

                }

                else
                {
                    IWebElement fld = fldList[SetMethod.RandomNumber(0, GetMethod.WebElementsList(XCloseField).Count)];
                    fld.Click();
                    lblTxt = fld.Text;
                }

            }
            else
            {
                throw new Exception("Something Wrong Between Checkbox and it's label");

            }

            return lblTxt;


        }

        public void SelectcheckboxFrmMsdd(int? fieldIndex = null, string fieldLabel = null, int? checkboxIndex = null, string checkboxLabel = null, bool allCheckbox = false, List<int> checkboxIndexs = null, List<string>checkboxLabels = null, params object[] checkboxvalues)
        {
            if (fieldIndex.HasValue & fieldLabel != null)
            {
                throw new Exception("Enter either field index or field Label not both");
                return;
            }
            else if (checkboxIndex.HasValue & checkboxLabel != null)
            {
                throw new Exception("Enter either checkbox index or checkbox Label not both");
                return;
            }

            else
            {
                if (fieldLabel != null)
                {
                    ClickOnCloseField(fieldLabel: fieldLabel);
                }
                else if (fieldIndex.HasValue)
                {
                    ClickOnCloseField(fieldIndex: fieldIndex);
                }
                else
                {
                    ClickOnCloseField();
                }

                WaitFor.ElementToAppear(By.XPath(XEmptyCheckbox));
                
                SetMethod.ClickOnFooter();
            }

        }




    }
}
