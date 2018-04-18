using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium.Support.PageObjects;

namespace nextAutomation
{
    public static class Pages
    {

        private static T GetPages<T>() where T : new()
        {
            var page = new T();
            PageFactory.InitElements(PropertiesCollection.driver, page);
            return page;
        }
        public static Order Order
        {
            get
            {
                return GetPages<Order>();
            }
        }
    }
}


