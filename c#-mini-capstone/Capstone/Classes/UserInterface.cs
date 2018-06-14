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

        public UserInterface(VendingMachine vendingMachine) //Constructor takes in an thing of type VendingMachine or calls a method that does
        {
            this.vendingMachine = vendingMachine;
        }

        public void RunInterface() // 15 lines max
        {
            bool done = false;
            while (!done)
            {
                Console.WriteLine($"This is the UserInterface");
                Console.ReadLine();
            }
        }
    }
}
