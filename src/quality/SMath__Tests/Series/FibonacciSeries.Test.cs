namespace Wayout.Mathematics.Series
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class FibonacciSeriesTest
    {
        [TestMethod]
        public void Terms()
        {
            CollectionAssert.AreEqual(new double[] { 0, 1, 2, 4, 7, 12 }, FibonacciSeries.Terms(6).ToArray());
        }
    }
}
