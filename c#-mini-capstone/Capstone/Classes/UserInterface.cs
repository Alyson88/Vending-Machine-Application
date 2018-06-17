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
        private readonly VendingMachine vendingMachine;
        private readonly int idColumnWidth = 6;
        private readonly int itemNameColumnWidth = 20;
        private readonly int dollarAmtColumnWidth = 10;
        
        public UserInterface(VendingMachine vendingMachine)
        {
            this.vendingMachine = vendingMachine;
        }

        public void RunInterface()
        {
            bool done = false;
            while (!done)
            {
                MainMenu();
                string mainMenuSelectionString = Console.ReadLine();
                Console.WriteLine();

                while (mainMenuSelectionString != "1" && mainMenuSelectionString != "2" && mainMenuSelectionString != "3" && mainMenuSelectionString != "4")
                {
                    Console.Write("Please enter a valid selection: ");
                    mainMenuSelectionString = Console.ReadLine();
                    Console.WriteLine();
                }
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
                else if (mainMenuSelectionString == "4")
                {
                    vendingMachine.SalesReport();
                    string salesReportPath = (DateTime.Now.ToString("yyyyMMdd_hh_mm_ss_tt")) + "_Total_Sales_Report.txt";
                    string fullSalesReportPath = Path.Combine(Directory.GetCurrentDirectory(), salesReportPath);
                    Console.WriteLine($"Your Sales Report has been saved here: ");
                    Console.WriteLine(fullSalesReportPath + "\n");
                }
            }
        }

        public void MainMenu()
        {
            Console.WriteLine("MAIN MENU:");
            Console.WriteLine("{1} Display Vending Machine Items");
            Console.WriteLine("{2} Make a Purchase");
            Console.WriteLine("{3} Exit\n");
            Console.Write("Please enter your menu selection: ");
        }

        public void PrintInventory()
        {
            Console.WriteLine("PRODUCT INVENTORY:");
            Console.WriteLine("{ID}".PadRight(idColumnWidth) + "PRODUCT".PadRight(itemNameColumnWidth) + "PRICE".PadLeft(dollarAmtColumnWidth) + "   QTY");
            foreach (KeyValuePair<string, VendingMachineItem> kvp in vendingMachine.Inventory)
            {
                string qtyOrSoldOut = " (" + kvp.Value.QuantityRemaining + ")";
                if (kvp.Value.QuantityRemaining == 0)
                {
                    qtyOrSoldOut = " *****SOLD OUT*****";
                }

                Console.WriteLine(("{" + kvp.Key + "}").PadRight(idColumnWidth)
                    + (kvp.Value.Name).PadRight(itemNameColumnWidth)
                    + ("$" + kvp.Value.Price).PadLeft(dollarAmtColumnWidth) + "  "
                    + qtyOrSoldOut);
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

                while (purchaseMenuSelectionString != "1" && purchaseMenuSelectionString != "2" && purchaseMenuSelectionString != "3")
                {
                    Console.Write("Please enter a valid selection: ");
                    purchaseMenuSelectionString = Console.ReadLine();
                    Console.WriteLine();
                }
                if (purchaseMenuSelectionString == "1")
                {
                    FeedMoney();
                }
                else if (purchaseMenuSelectionString == "2")
                {
                    if (vendingMachine.Balance == 0M)
                    {
                        Console.WriteLine("You need to add money before making a purchase\n");
                    }
                    else
                    {
                        SelectProduct();
                    }
                }
                else if (purchaseMenuSelectionString == "3")
                {
                    FinishTransaction();
                    done = true;
                }
            }
        }

        public void PurchaseMenu()
        {
            Console.WriteLine("PURCHASE MENU:");
            Console.WriteLine("{1} Feed Money");
            Console.WriteLine("{2} Select Product");
            Console.WriteLine("{3} Finish Transation");
            Console.WriteLine($"Current Money Provided: ${vendingMachine.Balance}\n");
            Console.Write("Please enter your menu selection: ");
        }

        public void FeedMoney()
        {
            decimal amtTransferred = 0.00M;

            try
            {
                Console.Write("Please enter the whole dollar amount you want to transfer: $");
                amtTransferred = decimal.Parse(Console.ReadLine());
                Console.WriteLine();

                while (amtTransferred <= 0 || amtTransferred % 1 != 0)
                {
                    Console.Write("Please enter a whole dollar amount greater than zero (0): ");
                    amtTransferred = decimal.Parse(Console.ReadLine());
                    Console.WriteLine();
                }
            }
            catch (FormatException)
            {
                Console.WriteLine();
                FeedMoney();
            }

            vendingMachine.IncreaseBalance(Math.Round((decimal)amtTransferred, 2));
            vendingMachine.FeedMoneyAuditLog(Math.Round((decimal)amtTransferred, 2));
        }

        public void SelectProduct()
        {
            try
            {
                PrintInventory();
                Console.Write("Please enter your product selection: ");
                string selection = Console.ReadLine().ToUpper();
                decimal beginningBalance = vendingMachine.Balance;
                bool containsItem = vendingMachine.Inventory.ContainsKey(selection);
                bool enoughQuantity = (vendingMachine.Inventory[selection].QuantityRemaining > 0);
                bool enoughMoney = (beginningBalance >= vendingMachine.Inventory[selection].Price);
                Console.WriteLine();

                if (!enoughMoney)
                {
                    Console.WriteLine($"The item you selected costs ${vendingMachine.Inventory[selection].Price}\n" +
                        $"Your available balance is ${vendingMachine.Balance}\n" +
                        $"Please add additional money before attempting to make this purchase\n");
                }
                else if (!enoughQuantity)
                {
                    Console.WriteLine("This item is *****SOLD OUT*****\n");
                }
                else
                {
                    vendingMachine.DecreaseBalance(vendingMachine.Inventory[selection].Price);
                    vendingMachine.DecreaseQuantity(selection);
                    Console.WriteLine($"You purchased {vendingMachine.Inventory[selection].Name} for ${vendingMachine.Inventory[selection].Price}\n" +
                        $"Your remaining balance is ${vendingMachine.Balance}\n" +
                        $"{vendingMachine.Inventory[selection].Message}\n");
                    vendingMachine.SelectProductAuditLog(vendingMachine.Inventory[selection].Name, selection, beginningBalance);
                }
            }
            catch (KeyNotFoundException)
            {
                Console.WriteLine();
                Console.WriteLine("That is not a valid selection\n");
            }
        }

        public void FinishTransaction()
        {
            vendingMachine.FinishTransactionAuditLog();
            vendingMachine.CalculateBalanceReturned();
            Console.WriteLine("Your account will be credited " + vendingMachine.BalanceReturned);
            Console.WriteLine();
            Console.WriteLine($"Current Money Provided: ${vendingMachine.Balance}");
            Console.WriteLine();
        }

    }
}
