using Xunit;

namespace SMath.Series;

public class ArithmeticSeriesTest
{
    [Theory]
    [InlineData(0, 0, 5, 0)]   // 0 + 0 + 0 + 0 + 0
    [InlineData(5, 0, 3, 15)]  // 5 + 5 + 5
    [InlineData(1, 1, 3, 6)]   // 1 + 2 + 3
    [InlineData(2, 3, 4, 26)]  // 2 + 5 + 8 + 11
    [InlineData(1, 1, 0, 0)]   // empty sum
    [InlineData(1, 1, -1, 0)]  // empty sum
    public void Term(double initial, double diff, int n, double result)
    {
        Assert.Equal(result, ArithmeticSeries.Term(initial, diff, n));
    }

    [Fact]
    public void Term_OfIntegersIsExact()
    {
        // the closed form halves an always even numerator
        Assert.Equal(55, ArithmeticSeries.Term(1, 1, 10));
        Assert.Equal(45, ArithmeticSeries.Term(0, 1, 10));
    }

    [Fact]
    public void Term_MatchesSummedSequence()
    {
        for (int n = 1; n <= 10; n++)
            Assert.Equal(
                Summation.Eval(Sequences.ArithmeticSequence.Terms(2d, 3d, n)),
                ArithmeticSeries.Term(2d, 3d, n));
    }

    [Fact]
    public void Terms()
    {
        // partial sums of 0,1,2,3,4 -> triangular numbers
        Assert.Equal(new double[] { 0, 1, 3, 6, 10 }, ArithmeticSeries.Terms(0d, 1d, 5).ToArray());
    }

    [Fact]
    public void Terms_BufferMatchesEnumerable()
    {
        Span<double> buffer = stackalloc double[5];
        var count = ArithmeticSeries.Terms(0d, 1d, 5, buffer);

        Assert.Equal(5, count);
        Assert.Equal(ArithmeticSeries.Terms(0d, 1d, 5).ToArray(), buffer[..count].ToArray());
    }

    [Fact]
    public void Terms_ShortBufferThrows()
    {
        var tooShort = new double[2];

        Assert.Throws<ArgumentException>(() => ArithmeticSeries.Terms(0d, 1d, 3, tooShort));
    }
}
