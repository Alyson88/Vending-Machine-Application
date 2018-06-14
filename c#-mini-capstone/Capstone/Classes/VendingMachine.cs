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
        

        // Variables:
        private decimal balance = 0M;

        private Dictionary<string, VendingMachineItem> inventory = new Dictionary<string, VendingMachineItem>();
        

        // Properties:
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

        // Constructor:

        // Methods:
        
        public void AdjustQuantity(string key)
        {
            VendingMachineItem item = inventory[key];
            item.QuantityRemaining -= 1;
        }

        public ListItems()
        {
            //pull info from text file
            //(A = chip, B = candy, C = drink, D = gum)
            //dictionary key = slot id
            //dictionary value = VendingMachineItems including {itemName, itemPrice, itemQuantity, itemType}
        }
        
        public SelectProduct()
        {
            //display ListItems()
            // w/ ReadLine
        }

        public void DispenseItem()
        {
            //change money provided
            //change inventory
            //print message
            //return to purchase menu
        }

        public void FinishTransaction()
        {
            //give them back balance in nickles, dimes & quarters
            //print new balance of 0
            //return to MAIN menu
        }
    }
}
