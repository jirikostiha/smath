namespace SMath.Series
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Xunit;

    public class FibonacciSeriesTest
    {
        [Fact]
        public void Terms()
        {
            CollectionAssert.Equal(new double[] { 0, 1, 2, 4, 7, 12 }, FibonacciSeries.Terms(6).ToArray());
        }
    }
}
