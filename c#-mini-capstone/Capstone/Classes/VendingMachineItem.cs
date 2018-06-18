using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capstone.Classes
{
    public class VendingMachineItem
    {
        private int initialQuantity = 5;

        public string SlotID
        {
            get;
        }

        public string Name
        {
            get;
        }

        public decimal Price
        {
            get;
        }

        public int InitialQuantity
        {
            get
            {
                return initialQuantity;
            }
            set
            {
                initialQuantity = value;
            }
        }

        public int QuantityRemaining
        {
            get;
            set;
        }

        public string Type
        {
            get;
        }

        public string Message
        {
            get;
            set;
        }

        public VendingMachineItem(string slotID,  string itemName, decimal itemPrice, string itemType, string message)
        {
            SlotID = slotID;
            Name = itemName;
            Price = itemPrice;
            QuantityRemaining = initialQuantity;
            Type = itemType;
            Message = message;
            InitialQuantity = initialQuantity;
        }
    }
}
