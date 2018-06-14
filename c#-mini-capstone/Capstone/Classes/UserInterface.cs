using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capstone.Classes
{
    public class UserInterface
    {
        private VendingMachine vendingMachine;

        public UserInterface(VendingMachine vendingMachine)
        {

        }

        public void RunInterface()
        {
            bool done = false;
            while (!done)
            {
                Console.WriteLine("{1} Display Vending Machine Items");
                Console.WriteLine("{2} Purchase");
                Console.WriteLine("[3] Exit");
                string mainMenuSelectionString = Console.ReadLine();
                int mainMenuSelection = Convert.ToInt32(mainMenuSelectionString);

                if ( mainMenuSelection == 1)
                {
                    //display Inventory
                    Console.WriteLine();
                }
                else if (mainMenuSelection == 2)
                {
                    Console.WriteLine("{1} Feed Money");
                    Console.WriteLine("{2} Select Product");
                    Console.WriteLine("[3] Finish Transation");
                    // Console.WriteLine(balance);
                    string purchaseMenuSelectionString = Console.ReadLine();
                    int purchaseMenuSelection = Convert.ToInt32(purchaseMenuSelectionString);

                    if (purchaseMenuSelection==1)
                    {
                        //balance
                    }
                    else if (purchaseMenuSelection==2)
                    {
                        //call select product method
                    }
                    else if (purchaseMenuSelection==3)
                    {
                        //call finish transaction method
                    }
                    else
                    {
                        //if they enter in a wrong thing
                    }
                }
                else if (mainMenuSelection==3)
                {
                    //Exit Program
                }
                else
                {
                    //if they enter in a wrong thing
                }

            }

        }

    }
}
