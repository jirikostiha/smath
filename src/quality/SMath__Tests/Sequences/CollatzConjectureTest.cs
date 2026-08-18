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
}
