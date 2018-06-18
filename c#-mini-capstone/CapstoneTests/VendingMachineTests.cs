using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Capstone.Classes;
using System.Collections.Generic;
using System.IO;

namespace CapstoneTests
{
    [TestClass]
    public class VendingMachineTests
    {
        static VendingMachine test = new VendingMachine();
        UserInterface myTestUI = new UserInterface(test);
        VendingMachineItem myTestItem = new VendingMachineItem("Potato Crisps", 3.05M, "Chip", "Crunch Crunch, Yum!");
        
        [TestMethod]
        public void CreateAuditLogTest()
        {
            string fullAuditLogPath = Path.Combine(Directory.GetCurrentDirectory(), "Log.txt");
            test.CreateAuditLog();
            Assert.IsTrue(File.Exists(fullAuditLogPath));
        }

        [TestMethod]
        public void DecreaseQuantityTest()
        {
            test.Inventory.Add("A1", myTestItem);
            test.ReadFile();
            test.DecreaseQuantity("A1");
            Assert.AreEqual(4, test.Inventory["A1"].QuantityRemaining);
        }

        [TestMethod]
        public void IncreaseBalance_Does_The_Balance_Increase()
        {
            test.Balance = 5M;
            test.IncreaseBalance(5M);
            Assert.AreEqual(10M, test.Balance);
        }

        [TestMethod]
        public void DecreaseBalance_Does_The_Balance_Decrease_And_Total_Sales_Increase()
        {
            test.DecreaseBalance(4.85M);
            Assert.AreEqual(5.15M, test.Balance);
            Assert.AreEqual(4.85M, test.TotalSales);
        }

        [TestMethod]
        public void CalculateBalanceReturned_Does_It_End_Transactions_With_Correct_Balance_Returned()
        {
            test.CalculateBalanceReturned();
            Assert.AreEqual("$5.15: 20 Quarters, 1 Dime, and 1 Nickel", test.BalanceReturned);
        }

        [TestMethod]
        public void CalculateBalanceReturned_Does_It_End_Transactions_With_Zero_Balance()
        {
            test.CalculateBalanceReturned();
            Assert.AreEqual(0M, test.Balance);
        }

        [TestMethod]
        public void SalesReportTest()
        {
            string fullSalesReportPath = Path.Combine(Directory.GetCurrentDirectory(), (DateTime.Now.ToString("yyyyMMdd_hh_mm_ss_tt")) + "_Total_Sales_Report.txt");
            test.SalesReport();
            Assert.AreEqual(4.85M, test.TotalSales);
            Assert.IsTrue(File.Exists(fullSalesReportPath));
        }
    }
}
