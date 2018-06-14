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

        public UserInterface(VendingMachine vendingMachine) //Constructor takes in an thing of type VendingMachine or calls a method that does
        {
            this.vendingMachine = vendingMachine;
        }

        public void MainMenu()
        {
            Console.WriteLine("{1} Display Vending Machine Items");
            Console.WriteLine("{2} Purchase");
            Console.WriteLine("{3} Exit");

        }
        public void PurchaseMenu()
        {
            Console.WriteLine("{1} Feed Money");
            Console.WriteLine("{2} Select Product");
            Console.WriteLine("[3] Finish Transation");
            Console.WriteLine($"Current Money Provided: ${vendingMachine.Balance}");
        }
        public void PrintInventory()
        {
            foreach(KeyValuePair<string, VendingMachineItem> kvp in vendingMachine.Inventory)
            {
                Console.Write("{" + kvp.Key + "} " + kvp.Value.Name + " " + kvp.Value.QuantityRemaining + " $" + kvp.Value.Price);

            }
        }
        public void PurchaseProcess()
        {
            PurchaseMenu();
            string purchaseMenuSelectionString = Console.ReadLine();

            if (purchaseMenuSelectionString == "1")
            {
                //balance
            }
            else if (purchaseMenuSelectionString == "2")
            {
                //call select product method
            }
            else if (purchaseMenuSelectionString == "3")
            {
                //call finish transaction method
            }
            else
            {
                Console.WriteLine("Please enter a valid choice");
            }
        }



        public void RunInterface()
        {

            bool done = false;
            while (!done)
            {
                MainMenu();
                string mainMenuSelectionString = Console.ReadLine();

                if (mainMenuSelectionString == "1")
                {
                    //display Inventory
                    PrintInventory();
                }
                else if (mainMenuSelectionString == "2")
                {
                    PurchaseProcess();
                }
                else if (mainMenuSelectionString == "3")
                {
                    //Exit Program
                    done = true;
                }
                else
                {
                    Console.WriteLine("Please enter a valid choice");
                }
            }
        }
    }
}
