using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using Excel = Microsoft.Office.Interop.Excel;
namespace nextAutomation
{
    public class IntExcel
    {
        //private IWebDriver driver;
        //private Excel.Application loginapp;
        //private Excel.Workbook loginworkbook;
        //private Excel._Worksheet loginworksheet;
        //private Excel.Range loginrange;

        public static Excel.Range GetFromExcel()
        {
            var customerBankInfo = new Excel.Application();
            var bankworkbook =
                customerBankInfo.Workbooks.Open(@"D:\Rashed\Customer Bank Info.xlsx");
            var bankworksheet = bankworkbook.Sheets[1];
            var bankrange = bankworksheet.UsedRange;
            return bankrange;
        }
    }

    public class CsvReport
    {
        private string BrowserType;
        private string url;
        private DateTime date;
        private FileStream fs;
        private StringBuilder reportcsv;
        private string filePath;
        private string fileName;

        public void CreateReportsFile()
        {
            date = DateTime.Now;
            fileName = "CustomerBank";
            reportcsv = new StringBuilder();
            filePath = @"C:\Users\Rashed\Desktop\TestProject\" + fileName + ".csv";

        }

        public void AddValue(string value)
        {
            reportcsv.Append(value + ", ");
        }

        public void Print2Csv()
        {
            File.WriteAllText(filePath, reportcsv.ToString());
        }

        public void AddLine()
        {

            reportcsv.Append(Environment.NewLine);


        }
    }

    public class CustomerBankInfo
    {
        private static string XSelectButton = ".//button[@data-ng-click='showBuyerModal()']";

        private static void TextField(string fieldName, string value)
        {
            //var tt = IntExcel.GetFromExcel();
            if (value != "")
            {
                string xpath = ".//label[contains(text(), '" + fieldName + "')]/following-sibling::input[@type='text' ]";
                var textField = GetMethod.WebElement(xpath);
                textField.Clear();
                textField.SendKeys(value);
            }
            
        }

        private static void Textarea(string fieldName, string value)
        {
            
            string xpath = ".//label[contains(text(), '" + fieldName + "')]/following-sibling::textarea";
            var textField = GetMethod.WebElement(xpath);
            textField.Clear();
            textField.SendKeys(value);
        }

        private static void SelectFromDrpDwn(string fieldName, string value)
        {
             string xallDrpDwn = ".//label[contains(text(), '" + fieldName +"')]/following-sibling::div[contains(@class, 'ni-select fa-caret-down')]/select[not(@disabled = 'disabled' or ancestor::*[contains(@class, 'ng-hide')])]";

            IWebElement drpDwnField = GetMethod.WebElement(xallDrpDwn);
            var drpDwn = new SelectElement(drpDwnField);
            drpDwn.SelectByText(value);
        }



        public static void CustomerMenu()
        {
            MainMenuSection.ClickTopMenuItem("Master Settings");
            MainMenuSection.ClickOnMenuItem("Customer");

        }

        public static void BankInfoPage4(string customerName)
        {
            MainMenuSection.ClickOnMenuItem("Manage Customers Bank Info");
            //BuyerDetails.LandingOnThisPageNWaitForSelectBtn(x: "Buyer Bank Info");
            WaitFor.ElementNotVisible(".//div[@id='overlay-container']/div[2][@id='overlay-content']");
            WaitFor.AnyErr();
            SetMethod.ClickOn(xpath: XSelectButton);
            WaitFor.ElementToVisible(By.XPath(BuyerDetails.XBuyerNameSearchField))
                .SendKeys(customerName);
            SetMethod.ClickOn(xpath: BuyerDetails.XSelectIconBlPopup);
            WaitFor.ElementNotVisible(".//div[@id='overlay-container']/div[2][@id='overlay-content']");
            WaitFor.AnyErr();
            WaitFor.ElementToVisible(By.XPath(BuyerDetails.XTextFieldsInOfficeInfo));
        }

        private static bool IstheValuePresent(string valueFromExcel)
        {
            var tt = IntExcel.GetFromExcel();
            bool value;
            if (valueFromExcel != null)
            {
                value = true;
            }
            else
            {
                value = false;
            }
            return value;
        }

        public static void CustomerBankInfoEntry()
        {

            //var csv = new CsvReport();
            var tt = IntExcel.GetFromExcel();
            var mt = new MultiTextField();
            Console.WriteLine((tt.Cells[5][6].Value2) != null ? tt.Cells[5][6].Value2.ToString() : "null");
            //csv.CreateReportsFile();
           // csv.Print2Csv();
            var fieldsValue = new List<string>();

            for (int i = 1; i <= 111; i++)
            {


                for (int j = 1; j <= 29; j++)
                {

                    if (i == 1)
                    {
                        break;
                    }

                    fieldsValue.Add((tt.Cells[j][i].Value2 + "").ToString());

                    if (j ==1)
                    {
                        if (fieldsValue[0] == "")
                        {
                            break;
                        }
                    }

                    if (j == 3)
                    {
                        if (fieldsValue[2] == "")
                        {
                            break;
                        }
                    }

                    
                    if (j == 29)
                    {
                        if (fieldsValue[28] == "")
                        {
                            break;
                        }

                            BankInfoPage4(fieldsValue[0]);
                            TextField("Bank Name", fieldsValue[2]);
                            TextField("Bank Short Name", fieldsValue[3]);
                            TextField("Branch Name", fieldsValue[4]);
                            TextField("Account Name", fieldsValue[5]);
                            TextField("Account Number", fieldsValue[6]);
                            TextField("Bank Swift Code", fieldsValue[7]);
                            TextField("ABA Number", fieldsValue[8]);
                            TextField("BIN No", fieldsValue[9]);
                            TextField("IBAN No",fieldsValue[10]);
                            mt.SendValue("Telephone No", 0, fieldsValue[11]);
                            mt.SendValue("Fax", 0, fieldsValue[12]);
                            mt.SendValue("Mobile No", 0, fieldsValue[13]);
                            mt.SendValue("Email", 0, fieldsValue[14]);
                            mt.SendValue("Website", 0, fieldsValue[15]);
                            Textarea("Address Line 1", fieldsValue[16]);
                            Textarea("Address Line 2", fieldsValue[17]);
                            TextField("Village", fieldsValue[18]);
                            TextField("Post Box", fieldsValue[19]);
                            TextField("Post Office", fieldsValue[20]);
                            TextField("Zip Code/Post Code", fieldsValue[21]);
                            TextField("Police Station/Thana", fieldsValue[22]);
                            TextField("Sub District", fieldsValue[23]);
                            TextField("District", fieldsValue[24]);
                            TextField("Division", fieldsValue[25]);
                            TextField("City/Town", fieldsValue[26]);
                            TextField("State", fieldsValue[27]);
                            SelectFromDrpDwn("Country", "Bangladesh");
                            SetMethod.SaveNWaitforSuccess();
                            SetMethod.ClosePage(); 
                        }


                        
                    }
                fieldsValue.Clear();


                //csv.AddLine();
                //csv.Print2Csv();


            }
        }
    }
}
