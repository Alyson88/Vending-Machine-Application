using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Capstone.Classes;

namespace CapstoneTests
{
    [TestClass]
    public class DecreaseBalance
    {
        [TestMethod]
        public void Does_The_Balance_Decrease()
        {
            VendingMachine test = new VendingMachine();
            test.Balance = 10;
            test.DecreaseBalance(5);
            Assert.AreEqual(5, test.Balance);
        }
      
    }
}
