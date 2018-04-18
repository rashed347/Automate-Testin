using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace nextAutomation
{
    public class SetValueFor
    {
        public static List<string> BuyerName = new List<string>{"Test Buyer"};
        
        public static List<string> BuyerShortName = new List<string> { "TB" }; 

        public static List<string> RegiNo = new List<string> {"REG-12345"};

        public static List<string> TinNo = new List<string> { "TIN-67890" };

        public static List<string> VatNo = new List<string> { "VAT-12345" };

        public static List<string> AddressLine1 = new List<string> { "Next IT, Banani" };

        public static List<string> AddressLine2 = new List<string> { "Road-8, Block- I, House- 17" };

        public static List<string> Village = new List<string> { "Amla" };

        public static List<string> PostBox = new List<string> {"PB-7032"};

        public static List<string> PostOffice = new List<string> { "Amla Sadarpur" };

        public static List<string> ZipCode = new List<string> { "PC-1216" };

        public static List<string> PoliceStation = new List<string> { "Mirpur" };

        public static List<string> SubDistrict = new List<string> { "Mirpur" };

        public static List<string> District = new List<string> { "Dhaka" };

        public static List<string> Division = new List<string> { "Dhaka" };

        public static List<string> City = new List<string> { "Mirpur-11" };

        public static List<string> State = new List<string> { "Dhaka" };

        public static List<string> MobileNoList = new List<string>
            {
                "01717405138",
                "01713456790",
                "01914560893",
                "01782765890",
                //"01818578930", "01913960640", "01717405138", "0172661234", "01724567893", "01756234587", "01915986723", "01672547812", "01532678912", "01717234789", "01745678945"
            };

        public static List<string> PhoneNoList = new List<string> {"028794568", "028901234", "013457890", "027845685", "01785634"};

        public static List<string> FaxNoList = new List<string> {"02785678", "04988786232", "02745450435"};

        public static List<string> EmailIdList = new List<string>
            {
                "rashed@nextit.com",
                "mitou@nextit.com",
                "shohel@nextit.com",
                "ashis@nextit.com",
                "amin@nextit.com"
            };

        public static List<string> WebsiteList = new List<string> { "nextit.com", "next-accessories.com", "nextacc-bd.com" };

        public static List<string> FirstName = new List<string> { "Rashed", "Mitou", "Mehedi", "Rashidul", "Rasel", "Nazmul", "Amin", "Ashis", "Shohel", "Ronty" };
        
        public static List<string> LastName = new List<string> { "Mondol", "Mazumder", "Forazi", "Chakladar", "Malitha", "Bissas", "Bhuiya", "Khan", "Chowdhury", "Haque" };
        
        public static List<string> MiddleName = new List<string> { "Momin", "Karim", "Hassan", "Oli", "Ahmed", "Alom", "Uddin", "Ullah", "Rana", "Mohimanul" };
        
        public static List<string> Designation = new List<string> { "Sr. Developer", "Jr. Developer", "Sr. QA Engineer", "QA Engineer", "Sr. Designer", "Jr. Designer", "Business Analyst", "Project Coordinator", "Executive(HR)", "Executiv(Accounts)" };
        
        public static List<string> Alphabetic = new List<string> { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z"};
        
        public static List<string> Bank = new List<string> { "Dutch Bangla Bank", "Asia Bank", "Islami Bank", "Sonali Bank", "Rupali Bank", "Pubali Bank", "Standard Chartered Bank", "Al-Arafah Bank", "Basic Bank", "Janata Bank" };
        
        public static List<string> Branch = new List<string> { "Banani", "Gulshan", "Mirpur", "Badda", "Shamoly", "Mohaamadpur", "Azimpur", "Matijhil", "Farmget", "Shahbag" };

        public static IDictionary <string, string>  Brand = new Dictionary<string, string>()
        {
            {"ADS","Addidas"},
            {"RBK","Rebok"},
            {"PTF","Portfolio"},
            {"HNZ", "Heinz"},
            {"CQ", "CQ"},
            {"DPN", "Dolphin"},
            {"HBR", "Huber"},
            {"IMP", "Impress"},
            {"LDR", "Leaders"},
            {"PMR", "Premier"}
        };

        public static IDictionary<string, string> Region = new Dictionary<string, string>()
        {
            {"ASA", "Asia"},
            {"ERP", "Europ"},
            {"AFR", "Africa"},
            {"OTR", "Others"}
        };

        public enum Month
        {
            January = 1,
            February = 1,
            March = 3,
            April = 4,
            May = 5,
            June = 6,
            July = 7,
            August = 8,
            September = 9,
            October = 10,
            November = 11,
            December = 12
        }

        public static IDictionary<string, string> Color = new Dictionary<string, string>()
        {
            {"Red Orange", "17-1464 TCX"},
            {"Poppy Red", "17-1664 TPG"},
            {"Jet Black", "19-0303 TCX"},
            {"Black Beauty", "19-3911 TCX"},
            {"Ultra Violet", "18-3838"},
            {"Fuchsia Rose", "17-2031"},
            {"True Red", "19-1664"},
            {"Blue Iris", "18-3943"},
            {"Mimosa", "14-0848"},
            {"Honeysuckle", "18-2120"}
        };

        public static List<string> Treatment = new List<string>()
        {
            "Water", "Plasma", "Waste Water", "Effluent", "Biological"
        };

        public static List<string> Style_ProductName = new List<string>()
        {
            "Alicia","Pajama", "Ebba"
        };

        public static IDictionary<string, string> AttributeValues4TextField()
        {
            var color1 = Color.Keys.ElementAt(SetMethod.RandomNumber(0, Color.Count));
            var color2 = Color.Keys.ElementAt(SetMethod.RandomNumber(0, Color.Count));

            var attributeValues4TextField = new Dictionary<string, string>()
                    {
                        {"Ground Color",color1},
                        
                        {"Ground Color Code/Pantone Number", Color[color1]},
                        
                        {"TextColor", color2},
                        
                        {"Text Color Code/Pantone Number", Color[color2]},
                        
                        {"Length", SetMethod.RandomNumberGenerator(2)},
                        
                        {"Width", SetMethod.RandomNumberGenerator(2)},
                        
                        {"LengthAfterFolding", SetMethod.RandomNumberGenerator(2)},
                        
                        {"WidthAfterFolding", SetMethod.RandomNumberGenerator(2)},
                        
                        {"Thickness", SetMethod.RandomNumberGenerator(2)},

                        {"Backing Mic", SetMethod.RandomNumberGenerator(1)},
                        
                        //Treatment[SetMethod.RandomNumber(0, Treatment.Count)]

                    };
            return attributeValues4TextField;
        }
    }
}
