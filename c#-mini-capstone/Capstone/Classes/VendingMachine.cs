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
        /*
         * what contains
         * calc money
         * MOST of the work here
         * all data about things inside machine
         * constructor to read and hold info from external file
         * data passed to UserInterface for it to use in interface object
         * collection object 
         * variables
         * method for each "action" machine needs to be able to do
         * NO console read/write here, then you can test it
         * console read/write should all be in userinterface class
         * 
         * format from input file: A1|Potato Crisps|3.05|Chip
         * Dictionary<KT, VT> dictionaryName = new Dictionary<KT, VT>();
         * dictionaryName.Add(key, value)
         * dictionaryName[key] = value
         * int valueOfFair = dictionaryName["fair"];
         * foreach (KeyValuePair<KT, VT> kvp in dictionaryName) { }
         * bool containsFair = dictionaryName.ContainsKey("fair");
         * Dictionary<KT, VT> dictionaryName = new Dictionary<KT, VT>()
            {
                {key1, value1 },
                {key2, value2 },
                {key3, value3 }
            };
        */

        // Variables:
        private decimal balance = 0M;

        //Dictionary<string, string[]> dictionaryName = new Dictionary<string, string[]>();
        
        List<VendingMachineItem> myItems = new List<VendingMachineItem>();

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

        // Constructor:

        // Methods:

        public ListItems()
        {
            //pull info from text file
            //create list of items of type vendingmachineitem
                //dictionary key = slot id
                //dictionary value = array{itemName, itemPrice, itemQuantity, itemType}
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
