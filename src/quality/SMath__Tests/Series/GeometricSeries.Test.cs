namespace Wayout.Mathematics.Series
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Wayout.Mathematics.Series;

    [TestClass]
    public class GeometricSeriesTest
    {
        [TestMethod]
        public void Term()
        {
            Assert.AreEqual(3, GeometricSeries.Term(1, 2, 2));
            Assert.AreEqual(1.5, GeometricSeries.Term(1, 0.5, 2));

            Assert.AreEqual(0, GeometricSeries.Term(0, 1, 1));
            Assert.AreEqual(1, GeometricSeries.Term(1, 1, 1));
            Assert.AreEqual(1, GeometricSeries.Term(1, -1, 1));
        }

        [TestMethod]
        public void Terms()
        {
            CollectionAssert.AreEqual(new double[] { 1, 3, 7 }, GeometricSeries.Terms(1, 2, 3).ToArray());
            CollectionAssert.AreEqual(new double[] { 1, 1.5, 1.75 }, GeometricSeries.Terms(1, 0.5, 3).ToArray());

            CollectionAssert.AreEqual(new double[] { 0, 0, 0 }, GeometricSeries.Terms(0, 2, 3).ToArray());
            CollectionAssert.AreEqual(new double[] { 1, 0, 1 }, GeometricSeries.Terms(1, -1, 3).ToArray());
        }
    }
}
