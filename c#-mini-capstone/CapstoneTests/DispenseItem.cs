using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Capstone.Classes;

namespace CapstoneTests
{
    [TestClass]
    public class DispenseItem
    {
     
        [TestMethod]
        public void Does_The_Item_Take_Away_The_Correct_Price()
        {
            //this seems wrong but I don't know how to fix if so
            VendingMachine test = new VendingMachine();
            test.Balance = 5;
            test.DecreaseBalance(1);
            Assert.AreEqual(4, test.Balance);

        }
    }
}
