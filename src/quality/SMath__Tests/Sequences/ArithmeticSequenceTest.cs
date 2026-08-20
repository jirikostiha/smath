using Xunit;

namespace SMath.Sequences;

public class ArithmeticSequenceTest
{
    [Theory]
    [InlineData(0, 0, 1, 0)]
    [InlineData(1, 0, 5, 1)]
    [InlineData(0, 1, 4, 3)]
    [InlineData(-2, 2, 3, 2)]
    [InlineData(1, -0.5, 3, 0)]
    public void Term(double initial, double difference, int n, double expected)
    {
        Assert.Equal(expected, ArithmeticSequence.Term(initial, difference, n));
    }

    [Fact]
    public void Term_NonPositivePositionThrows()
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => ArithmeticSequence.Term(1d, 1d, 0));
    }

    [Fact]
    public void Terms()
    {
        Assert.Equal(new double[] { 0, 0, 0 }, ArithmeticSequence.Terms(0d, 0d, 3).ToArray());
        Assert.Equal(new double[] { 1, 1, 1 }, ArithmeticSequence.Terms(1d, 0d, 3).ToArray());
        Assert.Equal(new double[] { -1, -1, -1 }, ArithmeticSequence.Terms(-1d, 0d, 3).ToArray());

        Assert.Equal(new double[] { 0, 1, 2, 3 }, ArithmeticSequence.Terms(0d, 1d, 4).ToArray());
        Assert.Equal(new double[] { -2, 0, 2 }, ArithmeticSequence.Terms(-2d, 2d, 3).ToArray());
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public void Terms_NonPositiveCountIsEmpty(int count)
    {
        Assert.Empty(ArithmeticSequence.Terms(1d, 1d, count));
    }

    [Fact]
    public void Terms_BufferMatchesEnumerable()
    {
        Span<double> buffer = stackalloc double[4];
        var count = ArithmeticSequence.Terms(-2d, 2.5, 4, buffer);

        Assert.Equal(4, count);
        Assert.Equal(ArithmeticSequence.Terms(-2d, 2.5, 4).ToArray(), buffer[..count].ToArray());
    }

    [Fact]
    public void Terms_ShortBufferThrows()
    {
        var tooShort = new double[2];

        Assert.Throws<ArgumentException>(() => ArithmeticSequence.Terms(0d, 1d, 3, tooShort));
    }
}
