using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Capstone.Classes;

namespace CapstoneTests
{
    [TestClass]
    public class FinishTransationMethod
    {
    
        [TestMethod]
        public void Does_It_End_Transactions_With_Zero_Balance()
        {
            VendingMachine test = new VendingMachine();
            test.FinishTransaction();
            Assert.AreEqual(0, test.Balance);

        }
        [TestMethod]
        public void Does_It_End_Transactions_With_Correct_Balance_Returned()
        {
            VendingMachine test = new VendingMachine();
            test.FinishTransaction();
            test.Balance = 5;
            CollectionAssert.ReferenceEquals("{20} Quarters, {0} Dimes, {0} Nickels.", test.BalanceReturned);

        }


    }
}
