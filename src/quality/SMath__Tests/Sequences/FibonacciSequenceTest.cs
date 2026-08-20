using Xunit;

namespace SMath.Sequences;

public class FibonacciSequenceTest
{
    [Theory]
    [InlineData(1, 0)]
    [InlineData(2, 1)]
    [InlineData(3, 1)]
    [InlineData(4, 2)]
    [InlineData(6, 5)]
    [InlineData(11, 55)]
    public void Term(int n, uint expected)
    {
        Assert.Equal(expected, FibonacciSequence.Term<uint>(n));
    }

    [Fact]
    public void Term_NonPositivePositionThrows()
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => FibonacciSequence.Term<uint>(0));
    }

    [Fact]
    public void Terms()
    {
        Assert.Equal(new uint[] { 0 }, FibonacciSequence.Terms<uint>(1).ToArray());
        Assert.Equal(new uint[] { 0, 1 }, FibonacciSequence.Terms<uint>(2).ToArray());
        Assert.Equal(new uint[] { 0, 1, 1, 2, 3, 5 }, FibonacciSequence.Terms<uint>(6).ToArray());
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public void Terms_NonPositiveCountIsEmpty(int count)
    {
        Assert.Empty(FibonacciSequence.Terms<uint>(count));
    }

    [Fact]
    public void Terms_BufferMatchesEnumerable()
    {
        Span<uint> buffer = stackalloc uint[6];
        var count = FibonacciSequence.Terms(6, buffer);

        Assert.Equal(6, count);
        Assert.Equal(FibonacciSequence.Terms<uint>(6).ToArray(), buffer[..count].ToArray());
    }

    [Fact]
    public void Terms_ShortBufferThrows()
    {
        var tooShort = new uint[2];

        Assert.Throws<ArgumentException>(() => FibonacciSequence.Terms<uint>(3, tooShort));
    }
}
