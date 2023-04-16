namespace Wayout.Mathematics.Geometry.D1
{
    using System;
    using System.Linq;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class LineSegmentTest
    {
        [TestMethod]
        public void Indices()
        {
            var indices = LineSegment.Indexes(count: 3, length: 3).ToArray();

            Assert.AreEqual(0, indices[0]);
            Assert.AreEqual(1, indices[1]);
            Assert.AreEqual(2, indices[2]);
        }
    }
}
