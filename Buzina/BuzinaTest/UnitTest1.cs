using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Buzina;

namespace BuzinaTest
{
    [TestClass]
    public class UnitTest1
    {
        Discount discount = new Discount();

        [TestMethod]
        public void GetDiscount0()
        {
            int expected = 0;

            int count = 5000;
            int actual = discount.GetDiscount(count);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void GetDiscount5()
        {
            int expected = 5;

            int count = 15000;
            int actual = discount.GetDiscount(count);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void GetDiscount10()
        {
            int expected = 10;

            int count = 55000;
            int actual = discount.GetDiscount(count);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void GetDiscount15()
        {
            int expected = 15;

            int count = 355000;
            int actual = discount.GetDiscount(count);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void GetDiscount0WithNegativeNumber()
        {
            int expected = 0;

            int count = -1500;
            int actual = discount.GetDiscount(count);

            Assert.AreEqual(expected, actual);
        }
    }
}
