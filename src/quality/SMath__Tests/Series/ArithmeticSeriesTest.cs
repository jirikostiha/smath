using Xunit;

namespace SMath.Series;

public class ArithmeticSeriesTest
{
    [Theory]
    [InlineData(0, 0, 5, 0)]   // 0 + 0 + 0 + 0 + 0
    [InlineData(5, 0, 3, 15)]  // 5 + 5 + 5
    [InlineData(1, 1, 3, 6)]   // 1 + 2 + 3
    [InlineData(2, 3, 4, 26)]  // 2 + 5 + 8 + 11
    public void Term(double initial, double diff, int n, double result)
    {
        Assert.Equal(result, ArithmeticSeries.Term(initial, diff, n));
    }

    [Fact]
    public void Terms()
    {
        // partial sums of 0,1,2,3,4 -> triangular numbers
        Assert.Equal(new double[] { 0, 1, 3, 6, 10 }, ArithmeticSeries.Terms(0d, 1, 5).ToArray());
    }
}
