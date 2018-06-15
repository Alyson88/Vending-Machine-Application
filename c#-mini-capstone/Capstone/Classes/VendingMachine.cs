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
        private decimal balance = 0M;
        private string balanceReturned = "";
        private string returnMessage = "";
        private Dictionary<string, VendingMachineItem> inventory = new Dictionary<string, VendingMachineItem>();
        private Dictionary<string, string> message = new Dictionary<string, string>();

        public decimal Balance
        {
            get { return balance; }
            set { balance = value; }
        }

        public string BalanceReturned
        {
            get { return balanceReturned; }
        }

        public Dictionary<string, VendingMachineItem> Inventory
        {
            get { return inventory; }
            set { inventory = value; }
        }

        public Dictionary<string, string> Message
        {
            get { return message; }
            set { message = value; }
        }

        //public string ReturnMessage
        //{
        //    get { return returnMessage; }
        //    set { returnMessage = value; }
        //}
        
        public void ReadFile()
        {
            string currentDirectory = Directory.GetCurrentDirectory();
            string inputFile = "vendingmachine.csv";
            string fullInputPath = Path.Combine(currentDirectory, inputFile);
            AddMessages();

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

        //public void AuditLog() // Append
        //{
        //    string currentDirectory = Directory.GetCurrentDirectory();
        //    string outputFile = @"Log.txt";
        //    string fullOutputPath = Path.Combine(currentDirectory, outputFile);
        //}

        public void AddMessages()
        {
            message.Add("A", "Crunch Crunch, Yum!");
            message.Add("B", "Munch Munch, Yum!");
            message.Add("C", "Glug Glug, Yum!");
            message.Add("D", "Chew Chew, Yum!");
        }
    

        public void DecreaseQuantity(string key)
        {
            VendingMachineItem item = inventory[key];
            item.QuantityRemaining -= 1;
        }

        public void IncreaseBalance(decimal money)
        {
            balance += money;
        }

        public void DecreaseBalance(decimal money)
        {
            balance -= money;
        }

        public void DispenseItem(string slotID)
        {
            DecreaseBalance(inventory[slotID].Price);
            DecreaseQuantity(slotID);
        }

        public void FinishTransaction()
        {
            decimal b = balance * 100M;
            int q = (int)b / 25;
            int d = ((int)b - (q * 25)) / 10;
            int n = ((int)b - (q * 25) - (d * 10)) / 5;
            balanceReturned = $"{q} Quarters, {d} Dimes, {n} Nickels.";
            balance = 0M;
            //AuditLog();
        }
    }
}
