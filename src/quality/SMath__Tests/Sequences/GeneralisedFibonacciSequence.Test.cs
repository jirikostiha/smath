namespace SMath.Sequences
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Xunit;

    public class GeneralisedFibonacciSequenceTest
    {
        [Fact]
        public void Terms()
        {
            CollectionAssert.Equal(new double[] { 0, 1, 1, 2, 3, 5 }, GeneralisedFibonacciSequence.Terms(0, 1, 6).ToArray());
            CollectionAssert.Equal(new double[] { -1, 2, 1, 3, 4 }, GeneralisedFibonacciSequence.Terms(-1, 2, 5).ToArray());
        }
    }
}
