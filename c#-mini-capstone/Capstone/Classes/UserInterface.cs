using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Capstone.Classes
{
    public class UserInterface
    {
        // Variables:
        private readonly VendingMachine vendingMachine;
        private int idColumnWidth = 6;
        private int itemNameColumnWidth = 20;
        private int dollarAmtColumnWidth = 10;
        private int itemQtyColumnWidth = 3;

        // Constructor?
        public UserInterface(VendingMachine vendingMachine)
        {
            this.vendingMachine = vendingMachine;
        }

        // Methods:
        public void RunInterface()
        {
            bool done = false;
            while (!done)
            {
                MainMenu();
                string mainMenuSelectionString = Console.ReadLine();
                Console.WriteLine();

                if (mainMenuSelectionString == "1")
                {
                    PrintInventory();
                }
                else if (mainMenuSelectionString == "2")
                {
                    PurchaseProcess();
                }
                else if (mainMenuSelectionString == "3")
                {
                    done = true;
                }
                else
                {
                    Console.WriteLine("Please enter a valid choice");
                    Console.WriteLine();
                }
            }
        }

        public void MainMenu()
        {
            Console.WriteLine("MAIN MENU:");
            Console.WriteLine("{1} Display Vending Machine Items");
            Console.WriteLine("{2} Make a Purchase");
            Console.WriteLine("{3} Exit");
            Console.WriteLine();
            Console.Write("Please enter your menu selection: ");
        }

        public void PrintInventory()
        {
            Console.WriteLine("PRODUCT INVENTORY:");
            Console.WriteLine("{ID}".PadRight(idColumnWidth) + "PRODUCT".PadRight(itemNameColumnWidth) + "PRICE".PadLeft(dollarAmtColumnWidth) + "  QTY");
            foreach (KeyValuePair<string, VendingMachineItem> kvp in vendingMachine.Inventory)
            {
                string soldOut = "";
                if (kvp.Value.QuantityRemaining == 0)
                {
                    soldOut = " *****SOLD OUT*****";
                }

                Console.WriteLine(("{" + kvp.Key + "}").PadRight(idColumnWidth) + (kvp.Value.Name).PadRight(itemNameColumnWidth) + ("$" + kvp.Value.Price).PadLeft(dollarAmtColumnWidth) + "  " + (kvp.Value.QuantityRemaining + "").PadRight(itemQtyColumnWidth) + soldOut);
            }
            Console.WriteLine();
        }

        public void PurchaseProcess()
        {
            bool done = false;
            while (!done)
            {
                PurchaseMenu();
                string purchaseMenuSelectionString = Console.ReadLine();
                Console.WriteLine();

                if (purchaseMenuSelectionString == "1")
                {
                    FeedMoney();
                }
                else if (purchaseMenuSelectionString == "2")
                {
                    SelectProduct();
                }
                else if (purchaseMenuSelectionString == "3")
                {
                    FinishTransaction();
                    done = true;
                }
                else
                {
                    Console.WriteLine("Please enter a valid choice");
                    Console.WriteLine();
                }
            }
        }

        public void PurchaseMenu()
        {
            Console.WriteLine("PURCHASE MENU:");
            Console.WriteLine("{1} Feed Money");
            Console.WriteLine("{2} Select Product");
            Console.WriteLine("{3} Finish Transation");
            Console.WriteLine($"Current Money Provided: ${vendingMachine.Balance}");
            Console.WriteLine();
            Console.Write("Please enter your menu selection: ");
        }
        
        public void FeedMoney()
        {
            Console.Write("Please enter the whole dollar amount you want to transfer: $");
            decimal amtTransferred = 0.00M;
            amtTransferred = decimal.Parse(Console.ReadLine());
            Console.WriteLine();

            while (amtTransferred <= 0 || amtTransferred % 1 != 0)
            {
                Console.Write("Please enter a whole dollar amount greater than zero (0): ");
                amtTransferred = decimal.Parse(Console.ReadLine());
                Console.WriteLine();
            }

            vendingMachine.IncreaseBalance(Math.Round(amtTransferred, 2));
            vendingMachine.FeedMoneyAuditLog(Math.Round(amtTransferred, 2));
        }

        public void SelectProduct()
        {
            PrintInventory();
            Console.Write("Please enter your product selection: ");
            string selection = Console.ReadLine().ToUpper();
            Console.WriteLine();

            bool containsItem = vendingMachine.Inventory.ContainsKey(selection);
            decimal beginningBalance = vendingMachine.Balance;

            if (containsItem && beginningBalance < vendingMachine.Inventory[selection].Price)
            {
                Console.WriteLine($"The item you selected is ${vendingMachine.Inventory[selection].Price}, your available balance is ${vendingMachine.Balance}.");
                Console.WriteLine();
                Console.WriteLine("Please add additional money before attempting to make this purchase.");
                Console.WriteLine();
            }
            else if (containsItem && vendingMachine.Inventory[selection].QuantityRemaining != 0 && beginningBalance >= vendingMachine.Inventory[selection].Price)
            {
                vendingMachine.DispenseItem(selection);
                Console.WriteLine($"You purchased a {vendingMachine.Inventory[selection].Name} " +
                    $"for ${vendingMachine.Inventory[selection].Price}, and have a remaining balance of ${vendingMachine.Balance}.");
                Console.WriteLine(vendingMachine.Inventory[selection].Message);
                Console.WriteLine();
                vendingMachine.SelectProductAuditLog(vendingMachine.Inventory[selection].Name, selection, beginningBalance);
            }
            else if (!containsItem)
            {
                while (!containsItem)
                {
                    Console.WriteLine("This entry is not valid, please make a valid selection: ");
                    selection = Console.ReadLine().ToUpper();
                    containsItem = vendingMachine.Inventory.ContainsKey(selection);
                }
                if (beginningBalance < vendingMachine.Inventory[selection].Price)
                {
                    Console.WriteLine($"The item you selected is ${vendingMachine.Inventory[selection].Price}, your available balance is ${vendingMachine.Balance}.");
                    Console.WriteLine("Please add additional money before attempting to make this purchase.");
                    Console.WriteLine();
                }
                else
                {
                    vendingMachine.DispenseItem(selection);
                    Console.WriteLine($"You purchased a {vendingMachine.Inventory[selection].Name} " +
                        $"for ${vendingMachine.Inventory[selection].Price}, and have a remaining balance of ${vendingMachine.Balance}.");
                    Console.WriteLine(vendingMachine.Inventory[selection].Message);
                    Console.WriteLine();
                    vendingMachine.SelectProductAuditLog(vendingMachine.Inventory[selection].Name, selection, beginningBalance);
                }
            }
            else
            {
                while (vendingMachine.Inventory[selection].QuantityRemaining == 0)
                {
                    Console.Write("This item is *****SOLD OUT***** please make a different selection: ");
                    selection = Console.ReadLine().ToUpper();
                }
                if (beginningBalance < vendingMachine.Inventory[selection].Price)
                {
                    Console.WriteLine($"The item you selected is ${vendingMachine.Inventory[selection].Price}, your available balance is ${vendingMachine.Balance}.");
                    Console.WriteLine();
                    Console.WriteLine("Please add additional money before attempting to make this purchase.");
                    Console.WriteLine();
                }
                else
                {
                    vendingMachine.DispenseItem(selection);
                    Console.WriteLine($"You purchased a {vendingMachine.Inventory[selection].Name} " +
                        $"for ${vendingMachine.Inventory[selection].Price}, and have a remaining balance of ${vendingMachine.Balance}. {vendingMachine.Inventory[selection].Message}");
                    Console.WriteLine();

                    vendingMachine.SelectProductAuditLog(vendingMachine.Inventory[selection].Name, selection, beginningBalance);
                }
            }
        }

        public void FinishTransaction()
        {
            vendingMachine.FinishTransaction();
            Console.WriteLine("Your account will be credited " + vendingMachine.BalanceReturned);
            Console.WriteLine();
            Console.WriteLine($"Current Money Provided: ${vendingMachine.Balance}");
            Console.WriteLine();
        }

    }
}
