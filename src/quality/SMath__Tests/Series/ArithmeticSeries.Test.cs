namespace Wayout.Mathematics.Series
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Wayout.Mathematics.Series;

    [TestClass]
    public class ArithmeticSeriesTest
    {
        [TestMethod]
        public void Term()
        {
            Assert.AreEqual(0, ArithmeticSeries.Term(0, 0, 1));
            Assert.AreEqual(0, ArithmeticSeries.Term(0, 0, 2));
            Assert.AreEqual(0, ArithmeticSeries.Term(0, 1, 1));
            Assert.AreEqual(1, ArithmeticSeries.Term(0, 1, 2));
            Assert.AreEqual(1, ArithmeticSeries.Term(1, 0, 1));
            Assert.AreEqual(2, ArithmeticSeries.Term(1, 0, 2));
            Assert.AreEqual(1, ArithmeticSeries.Term(1, 1, 1));
            Assert.AreEqual(3, ArithmeticSeries.Term(1, 1, 2));
        }

        [TestMethod]
        public void Terms()
        {
            CollectionAssert.AreEqual(new double[] { 0, 1, 3, 6, 10 }, ArithmeticSeries.Terms(0, 1, 5).ToArray());
        }
    }
}
