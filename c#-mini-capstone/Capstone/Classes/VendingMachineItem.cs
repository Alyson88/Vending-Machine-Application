using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capstone.Classes
{
    public class VendingMachineItem
    {
        private string name;
        private decimal price;
        private int quantityRemaining;
        private string type;
        
        public string Name
        {
            get
            {
                return name;
            }
        }
        public decimal Price
        {
            get
            {
                return price;
            }
        }
        public int QuantityRemaining
        {
            get
            {
                return quantityRemaining;
            }
            set
            {
                quantityRemaining = value;
            }
        }
        public string Type
        {
            get
            {
                return type;
            }
        }
        
        public VendingMachineItem()
        {
            quantityRemaining = 5;
        }
    }
}
