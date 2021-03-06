﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;

namespace nextAutomation
{
    class EntryMode
    {
        public static void CheckDefaultFieldsValue(IList<IWebElement> myFields)
        {

            foreach (var field in myFields)
            {
                if (field.GetAttribute("value") == "")
                {
                    Console.WriteLine("Pass: All fields are empty");
                }
                else
                {
                    Console.WriteLine(field.GetAttribute("value"));
                    Console.WriteLine("Failed: Reset functionality is not working");
                }

            }
        }
    }
}
