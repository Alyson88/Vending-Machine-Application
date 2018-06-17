using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Capstone.Classes
{
    public class VendingMachine
    {
        private Dictionary<string, string> message = new Dictionary<string, string>();
        private static readonly string currentDirectory = Directory.GetCurrentDirectory();
        private static readonly string inputFile = "vendingmachine.csv";
        private readonly string fullInputPath = Path.Combine(currentDirectory, inputFile);
        private Dictionary<string, VendingMachineItem> inventory = new Dictionary<string, VendingMachineItem>();
        private static readonly string outputFile = "Log.txt";
        private readonly string fullOutputPath = Path.Combine(currentDirectory, outputFile);
        private readonly int itemNameColumnWidth = 23;
        private readonly int dollarAmtColumnWidth = 10;
        private readonly int dateAndTimeColumnWidth = 25;
        private decimal balance = 0.00M;
        private string balanceReturned = "";
        private decimal totalSales = 0.00M;
        private static readonly string salesReportFile = (DateTime.Now.ToString("yyyyMMdd_hh_mm_ss_tt")) + "_Total_Sales_Report.txt";
        private readonly string fullSalesReportPath = Path.Combine(currentDirectory, salesReportFile);

        public Dictionary<string, string> Message
        {
            get { return message; }
            set { message = value; }
        }

        public Dictionary<string, VendingMachineItem> Inventory
        {
            get { return inventory; }
            set { inventory = value; }
        }

        public decimal Balance
        {
            get { return balance; }
            set { balance = value; }
        }

        public string BalanceReturned
        {
            get { return balanceReturned; }
        }

        public VendingMachine()
        {
            AddMessages();
            ReadFile();
            CreateAuditLog();
        }

        public void AddMessages()
        {
            message.Add("A", "Crunch Crunch, Yum!");
            message.Add("B", "Munch Munch, Yum!");
            message.Add("C", "Glug Glug, Yum!");
            message.Add("D", "Chew Chew, Yum!");
        }

        public void ReadFile()
        {
            try
            {
                using (StreamReader sr = new StreamReader(fullInputPath))
                {
                    while (!sr.EndOfStream)
                    {
                        string line = sr.ReadLine();
                        string[] splitLine = line.Split('|');
                        string slotID = splitLine[0];
                        string itemName = splitLine[1];
                        decimal itemPrice = decimal.Parse(splitLine[2]);
                        string itemType = splitLine[3];
                        string message = Message[slotID.Substring(0, 1)];

                        inventory.Add(slotID, new VendingMachineItem(itemName, itemPrice, itemType, message));
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Error reading the file");
                Console.WriteLine(e.Message);
            }
        }

        public void CreateAuditLog()
        {
            using (StreamWriter sw = new StreamWriter(fullOutputPath, false))
            {
                sw.WriteLine("DATE AND TIME:".PadRight(dateAndTimeColumnWidth)
                    + "ACTION:".PadRight(itemNameColumnWidth)
                    + "$ AMT:".PadLeft(dollarAmtColumnWidth)
                    + "$ BAL:".PadLeft(dollarAmtColumnWidth));
            }
        }

        public void FeedMoneyAuditLog(decimal amtTransferred)
        {
            using (StreamWriter sw = new StreamWriter(fullOutputPath, true))
            {
                sw.WriteLine(DateTime.Now.ToString("MM/dd/yyyy hh:mm:ss tt").PadRight(dateAndTimeColumnWidth)
                    + ("FEED MONEY").PadRight(itemNameColumnWidth)
                    + ("$ " + amtTransferred.ToString("#.00")).PadLeft(dollarAmtColumnWidth)
                    + ("$ " + Math.Round(Balance, 2)).PadLeft(dollarAmtColumnWidth));
            }
        }

        public void FinishTransactionAuditLog()
        {
            using (StreamWriter sw = new StreamWriter(fullOutputPath, true))
            {
                sw.WriteLine(DateTime.Now.ToString("MM/dd/yyyy hh:mm:ss tt").PadRight(dateAndTimeColumnWidth)
                    + "GIVE CHANGE".PadRight(itemNameColumnWidth)
                    + ("$ " + Math.Round(Balance, 2)).PadLeft(dollarAmtColumnWidth)
                    + ("$ 0.00").PadLeft(dollarAmtColumnWidth));
            }
        }

        public void SelectProductAuditLog(string name, string slotID, decimal beginningBalance)
        {
            using (StreamWriter sw = new StreamWriter(fullOutputPath, true))
            {
                sw.WriteLine(DateTime.Now.ToString("MM/dd/yyyy hh:mm:ss tt").PadRight(dateAndTimeColumnWidth)
                    + (name + " " + slotID).PadRight(itemNameColumnWidth)
                    + ("$ " + Math.Round(beginningBalance, 2)).PadLeft(dollarAmtColumnWidth)
                    + ("$ " + Math.Round(Balance, 2)).PadLeft(dollarAmtColumnWidth));
            }
        }

        public void DecreaseQuantity(string slotID)
        {
            VendingMachineItem item = inventory[slotID];
            item.QuantityRemaining -= 1;
        }

        public void IncreaseBalance(decimal money)
        {
            balance += Math.Round(money, 2);
        }

        public void DecreaseBalance(decimal money)
        {
            balance -= money;
            totalSales += money;
        }

        public void CalculateBalanceReturned()
        {
            string quarterOrQuarters = "Quarters";
            string dimeOrDimes = "Dimes";
            string nickelOrNickels = "Nickels";
            decimal b = Balance * 100.00M;
            int q = (int)b / 25;
            int d = ((int)b - (q * 25)) / 10;
            int n = ((int)b - (q * 25) - (d * 10)) / 5;

            if (q == 1) { quarterOrQuarters = "Quarter"; }
            if (d == 1) { dimeOrDimes = "Dime"; }
            if (n == 1) { nickelOrNickels = "Nickel"; }

            balanceReturned = $"${Math.Round(Balance, 2)}: {q} {quarterOrQuarters}, {d} {dimeOrDimes}, and {n} {nickelOrNickels}";
            balance = 0.00M;
        }

        public void SalesReport()
        {
            using (StreamWriter sw = new StreamWriter(fullSalesReportPath, false))
            {
                foreach (KeyValuePair<string, VendingMachineItem> kvp in inventory)
                {
                    int quantitySold = kvp.Value.InitialQuantity - kvp.Value.QuantityRemaining;
                    sw.WriteLine(kvp.Value.Name + "|" + quantitySold);
                }
                sw.WriteLine();
                sw.WriteLine("**TOTAL SALES** $" + Math.Round(totalSales, 2));
            }
        }
    }
}
