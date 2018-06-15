using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Capstone.Classes;
using System.Collections.Generic;

namespace CapstoneTests
{
    [TestClass]
    public class VendingMachineTests
    {
        [TestMethod]
        public void DecreaseBalance_Does_The_Balance_Decrease()
        {
            VendingMachine test = new VendingMachine();
            test.Balance = 10;
            test.DecreaseBalance(5);
            Assert.AreEqual(5, test.Balance);
        }

        [TestMethod]
        public void FinishTransation_Does_It_End_Transactions_With_Zero_Balance()
        {
            VendingMachine test = new VendingMachine();
            test.FinishTransaction();
            Assert.AreEqual(0, test.Balance);
        }

        [TestMethod]
        public void FinishTransation_Does_It_End_Transactions_With_Correct_Balance_Returned()
        {
            VendingMachine test = new VendingMachine();
            test.FinishTransaction();
            test.Balance = 5;
            CollectionAssert.ReferenceEquals("{20} Quarters, {0} Dimes, {0} Nickels.", test.BalanceReturned);
        }

        [TestMethod]
        public void IncreaseBalance_Does_The_Balance_Increase_With_A_Positive_Number()
        {
            VendingMachine test = new VendingMachine();
            test.Balance = 5;
            test.IncreaseBalance(5);
            Assert.AreEqual(10, test.Balance);
        }

        [TestMethod]
        public void DecreaseBalance_Does_The_Balance_Increase_With_A_Negative_Number()
        {
            VendingMachine test = new VendingMachine();
            test.Balance = 10;
            test.IncreaseBalance(-5);
            Assert.AreEqual(5, test.Balance);
        }
    }
}
