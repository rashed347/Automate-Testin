using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace LoginPageAutomation
{
    class WaitFor
    {
        private static TimeSpan waitForElement = TimeSpan.FromSeconds(10);

        public static void Ready()
        {
            WebDriverWait wait = new WebDriverWait(PropertiesCollection.driver, waitForElement);
            wait.Until(driver =>
            {
                bool isAjaxFinished = (bool)((IJavaScriptExecutor)driver).
                    ExecuteScript("return jQuery.active == 0");
                bool isLoaderHidden = (bool)((IJavaScriptExecutor)driver).
                    ExecuteScript("return $('.spinner').is(':visible') == false");
                return isAjaxFinished & isLoaderHidden;
            });
        }


        public static IWebElement ElementToVisible(By waitingElement)
        {
            try
            {

                WebDriverWait wait = new WebDriverWait(PropertiesCollection.driver, waitForElement);
                return wait.Until(ExpectedConditions.ElementIsVisible(waitingElement));
            }
            catch
            {
                return null;
            }
        }

        public static IWebElement ElementToAppear(By waitingElement)
        {
            try
            {
                WebDriverWait wait = new WebDriverWait(PropertiesCollection.driver, waitForElement);
                return wait.Until(ExpectedConditions.ElementExists(waitingElement));
            }
            catch
            {
                return null;
            }
        }

        public static IWebElement ElementToClickable(By waitingElement)
        {
            try
            {
                WebDriverWait wait = new WebDriverWait(PropertiesCollection.driver, waitForElement);
                return wait.Until(ExpectedConditions.ElementToBeClickable(waitingElement));
            }
            catch
            {
                return null;
            }
        }

        public static void PageLoading()
        {
            PropertiesCollection.driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(20);
        }

        public static void JavaScriptLoading()
        {
            PropertiesCollection.driver.Manage().Timeouts().AsynchronousJavaScript = TimeSpan.FromSeconds(5);
        }

        public static String ElementNotVisible(String elementXPath)
        {
            if (string.IsNullOrEmpty(elementXPath))
            {

                return "Wrong usage of ElementNotVisible()";
            }
            try
            {
                (new WebDriverWait(PropertiesCollection.driver, waitForElement)).Until(ExpectedConditions.InvisibilityOfElementLocated(By.XPath(elementXPath)));
                return null;
            }
            catch
            {
                return "Page takes too much time for loading.";
            }
        }

        public static void AnyErr()
        {
            WebDriverWait wait = new WebDriverWait(PropertiesCollection.driver, TimeSpan.FromMilliseconds(500));

            try
            {
                wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector(".toast.toast-error")));
                Console.WriteLine("Failed: Error is Occurred");

            }
            catch
            {

            }
        }

        public static void SuccessMessage()
        {
            WebDriverWait wait = new WebDriverWait(PropertiesCollection.driver, TimeSpan.FromMilliseconds(500));

            try
            {
                wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector(".toast.toast-success")));
                Console.WriteLine("Pass: Successful message is showing");

            }
            catch
            {
                Console.WriteLine("Failed: There is no Successful message ");
            }
        }
    }
}
