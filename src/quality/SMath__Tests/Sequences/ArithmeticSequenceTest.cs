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
            ArithmeticSequence.Term();
            CollectionAssert.Equal(new double[] { 0, 0, 0 }, ArithmeticSequence.Terms(0, 0, 3).ToArray());
            CollectionAssert.Equal(new double[] { 1, 1, 1 }, ArithmeticSequence.Terms(1, 0, 3).ToArray());
            CollectionAssert.Equal(new double[] { -1, -1, -1 }, ArithmeticSequence.Terms(-1, 0, 3).ToArray());

            CollectionAssert.Equal(new double[] { 0, 1, 2, 3 }, ArithmeticSequence.Terms(0, 1, 4).ToArray());
            CollectionAssert.Equal(new double[] { -2, 0, 2 }, ArithmeticSequence.Terms(-2, 2, 3).ToArray());
        }
    }
}
