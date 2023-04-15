namespace Wayout.Mathematics.Sequences
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class GeneralisedFibonacciSequenceTest
    {
        [TestMethod]
        public void Terms()
        {
            CollectionAssert.AreEqual(new double[] { 0, 1, 1, 2, 3, 5 }, GeneralisedFibonacciSequence.Terms(0, 1, 6).ToArray());
            CollectionAssert.AreEqual(new double[] { -1, 2, 1, 3, 4 }, GeneralisedFibonacciSequence.Terms(-1, 2, 5).ToArray());
        }
    }
}
