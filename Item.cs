using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace nextAutomation
{
    public class Item
    {
        public static List<string> Head = new List<string>()
        {
            
            "Accessories&Trims",
            "Raw Materials"
            
        }; 

        public static void LandingOnPage(string x)
        {
            MainMenuSection.ClickTopMenuItem("Master Settings");
            MainMenuSection.ClickOnMenuItem("Item");

            MainMenuSection.ClickOnMenuItem(x);
            WaitFor.ElementNotVisible(".//*[@id='overlay-content']/i");
            WaitFor.AnyErr();
        }
    }

    public class UniqueItemStructureEntry : Item
    {
        public static void CreateUniqueItemStructure()
        {
            LandingOnPage("Unique Item Structure Entry");

           const string xAllTextField =
                ".//input[@type='text' and not(@name='searchText' or @data-ng-readonly='true' or @readonly or " +
                "@data-ng-disabled='true' or @data-ng-model='i.text'  or ancestor::*[@ng-show = 'false'] or " +
                "ancestor::*[@style='display: none;'])] | .//textarea";
            
            IList<IWebElement> dropDowns = GetMethod.WebElementsList(BuyerDetails.XDropdown);

            var dd1 = new SelectElement(dropDowns[0]);
            dd1.SelectByText(Head[0]);

            new SelectElement(dropDowns[1]).SelectByText("Trims");
            new SelectElement(dropDowns[2]).SelectByText("Trims2");

            //int numberOfItemTypes = new SelectElement(dropDowns[3]).Options.Count - 1;
            //Console.WriteLine(numberOfItemTypes);
            //for (int i = 0; i < numberOfItemTypes; i++)
            //{
            new SelectElement(dropDowns[3]).SelectByText("Type2");

                for (int l = 0; l < 3; l++)
                {



                    dropDowns = GetMethod.WebElementsList(BuyerDetails.XDropdown);

                    var textFields = GetMethod.WebElementsList(xAllTextField);

                    var color1 = SetValueFor.Color.Keys.ElementAt(SetMethod.RandomNumber(0, SetValueFor.Color.Count));
                    var color2 = SetValueFor.Color.Keys.ElementAt(SetMethod.RandomNumber(0, SetValueFor.Color.Count));

                    var textFieldValues = new List<string>
                    {
                        color1,
                        SetValueFor.Color[color1],
                        color2,
                        SetValueFor.Color[color2],
                        SetMethod.RandomNumberGenerator(2),
                        SetMethod.RandomNumberGenerator(2),
                        SetMethod.RandomNumberGenerator(2),
                        SetMethod.RandomNumberGenerator(2),
                        SetValueFor.Treatment[SetMethod.RandomNumber(0, SetValueFor.Treatment.Count)]

                    };

                    for (int j = 6; j < dropDowns.Count - 1; j++)
                    {

                        var restOfDd = new SelectElement(dropDowns[j]);
                        int numberDdElement = restOfDd.Options.Count - 1;
                        Console.WriteLine(numberDdElement);
                        if (numberDdElement > 0)
                        {
                            restOfDd.SelectByValue(SetMethod.RandomNumber(0, numberDdElement).ToString());

                        }

                    }

                    for (int k = 2; k < textFields.Count - 1; k++)
                    {
                        textFields[k].SendKeys(textFieldValues[k - 2]);
                    }
                    SetMethod.SaveNWaitforSuccess();
                    SetMethod.ClickOn(btnName: "Add Another Detail");


                }
            }


        }
    }
//}
