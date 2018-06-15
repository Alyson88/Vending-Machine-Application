using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Capstone.Classes;

namespace CapstoneTests
{
    [TestClass]
    public class IncreaseBalance
    {
       
        [TestMethod]
        public void Does_The_Balance_Increase_With_A_Positive_Number()
        {
            VendingMachine test = new VendingMachine();
            test.Balance = 5;
            test.IncreaseBalance(5);
            Assert.AreEqual(10, test.Balance);
        }
        [TestMethod]
        public void Does_The_Balance_Increase_With_A_Negative_Number()
        {
            VendingMachine test = new VendingMachine();
            test.Balance = 10;
            test.IncreaseBalance(-5);
            Assert.AreEqual(5, test.Balance);
        }

    }
}
