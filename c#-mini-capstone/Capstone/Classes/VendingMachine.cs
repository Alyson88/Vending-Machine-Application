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
        private Dictionary<string, VendingMachineItem> inventory = new Dictionary<string, VendingMachineItem>();


        public decimal Balance
        {
            get
            {
                return balance;
            }
            set
            {
                balance = value;
            }
        }
        public string BalanceReturned
        {
            get
            {
                return balanceReturned;
            }
        }
        public Dictionary<string, VendingMachineItem> Inventory
        {
            get
            {
                return inventory;
            }
            set
            {
                inventory = value;
            }
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
        }
        //public ListItems()
        //{
        //    //pull info from text file
        //    //(A = chip, B = candy, C = drink, D = gum)
        //    //dictionary key = slot id
        //    //dictionary value = VendingMachineItems including {itemName, itemPrice, itemQuantity, itemType}
        //}

    }
}
