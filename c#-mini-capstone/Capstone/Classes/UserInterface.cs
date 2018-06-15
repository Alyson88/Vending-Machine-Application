using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capstone.Classes
{
    public class UserInterface
    {
        private readonly VendingMachine vendingMachine;

        public UserInterface(VendingMachine vendingMachine) 
        {
            this.vendingMachine = vendingMachine;
        }

        public void RunInterface()
        {
            vendingMachine.ReadFile();

            bool done = false;
            while (!done)
            {
                MainMenu();
                string mainMenuSelectionString = Console.ReadLine();

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
                }
            }
        }

        public void MainMenu()
        {
            Console.WriteLine("{1} Display Vending Machine Items");
            Console.WriteLine("{2} Purchase");
            Console.WriteLine("{3} Exit");
            Console.WriteLine();
            Console.Write("Please enter your selection: ");
        }

        public void PurchaseMenu()
        {
            Console.WriteLine("{1} Feed Money");
            Console.WriteLine("{2} Select Product");
            Console.WriteLine("{3} Finish Transation");
            Console.WriteLine($"Current Money Provided: ${vendingMachine.Balance}");
            Console.WriteLine();
            Console.Write("Please enter your selection: ");
        }

        public void PrintInventory()
        {
            foreach(KeyValuePair<string, VendingMachineItem> kvp in vendingMachine.Inventory)
            {
                string soldOut = "";
                if (kvp.Value.QuantityRemaining == 0)
                {
                    soldOut = " *****SOLD OUT*****";
                }

                Console.Write("{" + kvp.Key + "} " + kvp.Value.Name + " $" + kvp.Value.Price + " " + kvp.Value.QuantityRemaining + soldOut);
                Console.WriteLine();
            }
        }

        public void FeedMoney()
        {
            Console.Write("Please enter the whole dollar amount you want to transfer: $");
            decimal amtTransferred = decimal.Parse(Console.ReadLine());
            vendingMachine.IncreaseBalance(amtTransferred);
        }

        public void SelectProduct()
        {
            PrintInventory();
            Console.WriteLine();
            Console.Write("Please make a selection: ");
            string selection = Console.ReadLine().ToUpper();

            bool containsItem = vendingMachine.Inventory.ContainsKey(selection);

            while (vendingMachine.Inventory[selection].QuantityRemaining == 0)
            {
                Console.WriteLine("This item is *****SOLD OUT***** please make a different selection: ");
                selection = Console.ReadLine().ToUpper();

            }
            while (!containsItem)
            {
                Console.WriteLine("This entry is not valid, please make a valid selection: ");
                selection = Console.ReadLine().ToUpper();
            }
            vendingMachine.DispenseItem(selection);
            Console.WriteLine($"You purchased a {vendingMachine.Inventory[selection].Name} " +
                $"for ${vendingMachine.Inventory[selection].Price}, and have a remaining balance of ${vendingMachine.Balance}.");
            Console.WriteLine(vendingMachine.Inventory[selection].Message);
        }

        public void FinishTransaction()
        {
            vendingMachine.FinishTransaction();
            Console.WriteLine("Your account will be credited " + vendingMachine.BalanceReturned);
            Console.WriteLine($"Current Money Provided: ${vendingMachine.Balance}");
        }

        public void PurchaseProcess()
        {
            PurchaseMenu();
            string purchaseMenuSelectionString = Console.ReadLine();

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
            }
            else
            {
                Console.WriteLine("Please enter a valid choice");
            }
        }
        
    }
}
