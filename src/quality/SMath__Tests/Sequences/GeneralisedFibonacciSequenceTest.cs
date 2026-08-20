using Xunit;

namespace SMath.Sequences;

public class GeneralisedFibonacciSequenceTest
{
    [Theory]
    [InlineData(0, 1, 1, 0)]
    [InlineData(0, 1, 6, 5)]
    [InlineData(-1, 2, 5, 4)]
    public void Term(double first, double second, int n, double expected)
    {
        Assert.Equal(expected, GeneralisedFibonacciSequence.Term(first, second, n));
    }

    [Fact]
    public void Term_NonPositivePositionThrows()
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => GeneralisedFibonacciSequence.Term(0d, 1d, 0));
    }

    [Fact]
    public void Terms()
    {
        Assert.Equal(new double[] { 0, 1, 1, 2, 3, 5 }, GeneralisedFibonacciSequence.Terms(0d, 1d, 6).ToArray());
        Assert.Equal(new double[] { -1, 2, 1, 3, 4 }, GeneralisedFibonacciSequence.Terms(-1d, 2d, 5).ToArray());
    }

    [Fact]
    public void Terms_MatchesFibonacciSequence()
    {
        Assert.Equal(
            FibonacciSequence.Terms<double>(10).ToArray(),
            GeneralisedFibonacciSequence.Terms(0d, 1d, 10).ToArray());
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public void Terms_NonPositiveCountIsEmpty(int count)
    {
        Assert.Empty(GeneralisedFibonacciSequence.Terms(0d, 1d, count));
    }

    [Fact]
    public void Terms_BufferMatchesEnumerable()
    {
        Span<double> buffer = stackalloc double[5];
        var count = GeneralisedFibonacciSequence.Terms(-1d, 2d, 5, buffer);

        Assert.Equal(5, count);
        Assert.Equal(GeneralisedFibonacciSequence.Terms(-1d, 2d, 5).ToArray(), buffer[..count].ToArray());
    }
}
