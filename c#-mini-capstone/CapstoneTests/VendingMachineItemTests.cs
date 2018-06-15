using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Capstone.Classes;

namespace CapstoneTests
{
    [TestClass]
    public class VendingMachineItemTests
    {
        [TestMethod]
        public void Constructor_Initial_Quantity_Correct()
        {
            VendingMachineItem test = new VendingMachineItem("name", 5.5M, "type", "message");
            Assert.AreEqual(5, test.QuantityRemaining);
        }
    }
}
