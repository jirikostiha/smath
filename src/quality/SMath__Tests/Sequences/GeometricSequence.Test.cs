namespace SMath.Sequences
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Xunit;

    public class GeometricSequenceTest
    {
        [Theory]
        [InlineData()]
        public void Terms()
        {
            //CollectionAssert.Equal(new double[] { 0, 0, 0 }, GeometricSequence.Terms(0, 1, 3).ToArray());
            //CollectionAssert.Equal(new double[] { 1, 1, 1 }, GeometricSequence.Terms(1, 1, 3).ToArray());
            //CollectionAssert.Equal(new double[] { 1, -1, 1 }, GeometricSequence.Terms(1, -1, 3).ToArray());

            //CollectionAssert.Equal(new double[] { 1, 2, 4 }, GeometricSequence.Terms(1, 2, 3).ToArray());
            //CollectionAssert.Equal(new double[] { 8, 4, 2, 1, 0.5 }, GeometricSequence.Terms(8, 0.5, 5).ToArray());
            //CollectionAssert.Equal(new double[] { -3, 6, -12 }, GeometricSequence.Terms(-3, -2, 3).ToArray());

            //CollectionAssert.Equal(new double[] { 1, 0.5, 0.25 }, GeometricSequence.Terms(1, 0.5, 3).ToArray());
        }
    }
}
