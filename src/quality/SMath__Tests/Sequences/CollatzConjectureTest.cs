using Xunit;

namespace SMath.Sequences;

public class CollatzConjectureTest
{
    [Theory]
    [InlineData(1, 4)]
    [InlineData(2, 1)]
    [InlineData(4, 2)]
    [InlineData(5, 16)]
    [InlineData(6, 3)]
    public void NextTerm(int value, int expected)
    {
        Assert.Equal(expected, CollatzConjecture.NextTerm(value));
    }

    [Fact]
    public void Terms()
    {
        Assert.Equal(new[] { 3, 10, 5, 16, 8, 4, 2, 1 }, CollatzConjecture.Terms(6, 8).ToArray());
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public void Terms_NonPositiveCountIsEmpty(int count)
    {
        Assert.Empty(CollatzConjecture.Terms(6, count));
    }

    [Fact]
    public void Terms_BufferMatchesEnumerable()
    {
        Span<int> buffer = stackalloc int[8];
        var count = CollatzConjecture.Terms(6, 8, buffer);

        Assert.Equal(8, count);
        Assert.Equal(CollatzConjecture.Terms(6, 8).ToArray(), buffer[..count].ToArray());
    }

    [Theory]
    [InlineData(1, 0)]
    [InlineData(2, 1)]
    [InlineData(3, 7)]
    [InlineData(6, 8)]
    [InlineData(7, 16)]
    [InlineData(27, 111)]
    public void StoppingTime(int initial, int expected)
    {
        Assert.Equal(expected, CollatzConjecture.StoppingTime(initial));
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public void StoppingTime_NonPositiveInitialThrows(int initial)
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => CollatzConjecture.StoppingTime(initial));
    }
}
