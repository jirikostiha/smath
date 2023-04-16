namespace SMath.Sequences
{
    using SMath.Sequences;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Xunit;

    public class ArithmeticSequenceTest
    {
        [Fact]
        public void Terms()
        {
            Assert.Equal(new double[] { 0, 0, 0 }, ArithmeticSequence.Terms(0d, 0, 3u).ToArray());
            Assert.Equal(new double[] { 1, 1, 1 }, ArithmeticSequence.Terms(1d, 0, 3u).ToArray());
            Assert.Equal(new double[] { -1, -1, -1 }, ArithmeticSequence.Terms(-1d, 0, 3u).ToArray());

            Assert.Equal(new double[] { 0, 1, 2, 3 }, ArithmeticSequence.Terms(0d, 1, 4u).ToArray());
            Assert.Equal(new double[] { -2, 0, 2 }, ArithmeticSequence.Terms(-2d, 2, 3u).ToArray());
        }
    }
}
