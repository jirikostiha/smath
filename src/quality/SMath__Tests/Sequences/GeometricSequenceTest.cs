using Xunit;

namespace SMath.Sequences;

public class GeometricSequenceTest
{
    [Theory]
    [InlineData(1, 1, 1, 1)]
    [InlineData(1, 2, 4, 8)]
    [InlineData(8, 0.5, 4, 1)]
    [InlineData(-3, -2, 3, -12)]
    public void Term(double initial, double ratio, int n, double expected)
    {
        Assert.Equal(expected, GeometricSequence.Term(initial, ratio, n));
    }

    [Fact]
    public void Term_OfIntegers()
    {
        Assert.Equal(1024, GeometricSequence.Term(1, 2, 11));
    }

    [Fact]
    public void Term_NonPositivePositionThrows()
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => GeometricSequence.Term(1d, 2d, 0));
    }

    [Fact]
    public void Terms()
    {
        Assert.Equal(new double[] { 1, 1, 1 }, GeometricSequence.Terms(1d, 1d, 3).ToArray());
        Assert.Equal(new double[] { 1, 2, 4 }, GeometricSequence.Terms(1d, 2d, 3).ToArray());
        Assert.Equal(new double[] { 8, 4, 2, 1, 0.5 }, GeometricSequence.Terms(8d, 0.5d, 5).ToArray());
        Assert.Equal(new double[] { -3, 6, -12 }, GeometricSequence.Terms(-3d, -2d, 3).ToArray());
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public void Terms_NonPositiveCountIsEmpty(int count)
    {
        Assert.Empty(GeometricSequence.Terms(1d, 2d, count));
    }

    [Fact]
    public void Terms_BufferMatchesEnumerable()
    {
        Span<double> buffer = stackalloc double[5];
        var count = GeometricSequence.Terms(8d, 0.5d, 5, buffer);

        Assert.Equal(5, count);
        Assert.Equal(GeometricSequence.Terms(8d, 0.5d, 5).ToArray(), buffer[..count].ToArray());
    }

    [Fact]
    public void Terms_ShortBufferThrows()
    {
        var tooShort = new double[2];

        Assert.Throws<ArgumentException>(() => GeometricSequence.Terms(1d, 2d, 3, tooShort));
    }
}
