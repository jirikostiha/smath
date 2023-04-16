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
            Assert.Equal(new double[] { 0, 1, 1, 2, 3, 5 }, GeneralisedFibonacciSequence.Terms(0d, 1, 6).ToArray());
            Assert.Equal(new double[] { -1, 2, 1, 3, 4 }, GeneralisedFibonacciSequence.Terms(-1d, 2, 5).ToArray());
        }
    }
}
