using Xunit;

namespace SMath.Series;

public class GeometricSeriesTest
{
    [Theory]
    [InlineData(1, 2, 1, 1)]     // 1
    [InlineData(1, 2, 4, 15)]    // 1 + 2 + 4 + 8
    [InlineData(5, 1, 3, 15)]    // 5 + 5 + 5, the ratio of one
    [InlineData(8, 0.5, 4, 15)]  // 8 + 4 + 2 + 1
    [InlineData(1, -2, 3, 3)]    // 1 - 2 + 4
    [InlineData(1, 2, 0, 0)]     // empty sum
    [InlineData(1, 2, -1, 0)]    // empty sum
    public void Term(double initial, double ratio, int n, double result)
    {
        Assert.Equal(result, GeometricSeries.Term(initial, ratio, n));
    }

    [Fact]
    public void Term_OfIntegersIsExact()
    {
        Assert.Equal(1023, GeometricSeries.Term(1, 2, 10));
        Assert.Equal(3069, GeometricSeries.Term(3, 2, 10));
    }

    [Fact]
    public void Term_OfUnsignedIntegersDoesNotUnderflow()
    {
        Assert.Equal(1023u, GeometricSeries.Term(1u, 2u, 10));
    }

    [Fact]
    public void Term_MatchesSummedSequence()
    {
        for (int n = 1; n <= 10; n++)
            Assert.Equal(
                Summation.Eval(Sequences.GeometricSequence.Terms(3d, 2d, n)),
                GeometricSeries.Term(3d, 2d, n));
    }

    [Fact]
    public void Terms()
    {
        Assert.Equal(new double[] { 1, 3, 7, 15 }, GeometricSeries.Terms(1d, 2d, 4).ToArray());
        Assert.Equal(new double[] { 8, 12, 14, 15 }, GeometricSeries.Terms(8d, 0.5d, 4).ToArray());
    }

    [Fact]
    public void Terms_BufferMatchesEnumerable()
    {
        Span<double> buffer = stackalloc double[4];
        var count = GeometricSeries.Terms(1d, 2d, 4, buffer);

        Assert.Equal(4, count);
        Assert.Equal(GeometricSeries.Terms(1d, 2d, 4).ToArray(), buffer[..count].ToArray());
    }

    [Theory]
    [InlineData(1, 0.5, 2)]
    [InlineData(1, -0.5, 0.6666666666666666)]
    [InlineData(3, 0.25, 4)]
    public void Limit(double initial, double ratio, double result)
    {
        Assert.Equal(result, GeometricSeries.Limit(initial, ratio), 12);
    }
}
