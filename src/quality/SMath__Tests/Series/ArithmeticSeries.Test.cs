namespace SMath.Series
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Xunit;

    public class ArithmeticSeriesTest
    {
        [Theory]
        [InlineData(0, 0, 1, 0)]
        [InlineData(0, 0, 2, 0)]
        [InlineData(0, 1, 1, 0)]
        [InlineData(0, 1, 2, 1)]
        [InlineData(1, 0, 1, 1)]
        [InlineData(1, 0, 0, 2)]
        [InlineData(1, 1, 1, 2)]
        [InlineData(1, 1, 2, 3)]
        public void Term(uint initial, uint diff, uint n, uint result)
        {
            Assert.Equal(result, ArithmeticSeries.Term(initial, diff, n));
        }

        [Fact]
        public void Terms()
        {
            Assert.Equal(new double[] { 0, 1, 3, 6, 10 }, ArithmeticSeries.Terms(0d, 1, 5).ToArray());
        }
    }
}
