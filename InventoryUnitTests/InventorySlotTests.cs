using Microsoft.VisualStudio.TestTools.UnitTesting;
using SharpInventory;
using System;
using System.Collections.Generic;
using System.Text;

namespace InventoryUnitTests
{
    [TestClass]
    public class InventorySlotTests
    {
        [TestClass]
        public class IsEmptyTests
        {
            [TestMethod]
            public void TestIsEmptyOnCountZero()
            {
                Item testItem = new Item("A");
                InventorySlot<Item> testSlot = new InventorySlot<Item>(testItem, 0);

                Assert.IsTrue(testSlot.IsEmpty);
            }
            [TestMethod]
            public void TestIsEmptyOnNullItem()
            {
                InventorySlot<Item> testSlot = new InventorySlot<Item>(null, 1);

                Assert.IsTrue(testSlot.IsEmpty);
            }
            [TestMethod]
            public void TestIsEmptyOnEmptyInit()
            {
                InventorySlot<Item> testSlot = new InventorySlot<Item>();

                Assert.IsTrue(testSlot.IsEmpty);
            }
        }
    }
}
