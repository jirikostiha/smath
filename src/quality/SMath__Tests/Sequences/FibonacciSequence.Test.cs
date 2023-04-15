namespace Wayout.Mathematics.Sequences
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class FibonacciSequenceTest
    {
        [TestMethod]
        public void Terms()
        {
            CollectionAssert.AreEqual(new long[] { 0, 1, 1, 2, 3, 5 }, FibonacciSequence.Terms(6).ToArray());
        }
    }
}
