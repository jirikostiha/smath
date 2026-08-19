using Xunit;

namespace SMath;

public class FactorialTests
{
    [Theory]
    [InlineData(0, 1)]
    [InlineData(1, 1)]
    [InlineData(2, 2)]
    [InlineData(3, 6)]
    [InlineData(5, 120)]
    [InlineData(12, 479001600)]
    public void Eval(int n, int expected)
    {
        Assert.Equal(expected, Factorial.Eval(n));
    }

    [Fact]
    public void Eval_OfLong()
    {
        Assert.Equal(2432902008176640000L, Factorial.Eval(20L));
    }

    [Fact]
    public void Eval_NegativeThrows()
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => Factorial.Eval(-1));
    }

    [Theory]
    [InlineData(5, 0, 1)]
    [InlineData(5, 1, 5)]
    [InlineData(5, 3, 60)]   // 5 * 4 * 3
    [InlineData(5, 5, 120)]  // 5!
    [InlineData(0, 0, 1)]
    public void Falling(int n, int k, int expected)
    {
        Assert.Equal(expected, Factorial.Falling.Eval(n, k));
    }

    [Theory]
    [InlineData(5, 0, 1)]
    [InlineData(5, 1, 5)]
    [InlineData(5, 3, 210)]  // 5 * 6 * 7
    [InlineData(1, 5, 120)]  // 5!
    public void Rising(int n, int k, int expected)
    {
        Assert.Equal(expected, Factorial.Rising.Eval(n, k));
    }

    [Fact]
    public void Falling_OfWholeNumberIsFactorial()
    {
        for (int n = 0; n <= 12; n++)
            Assert.Equal(Factorial.Eval(n), Factorial.Falling.Eval(n, n));
    }

    [Fact]
    public void Falling_NegativeCountThrows()
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => Factorial.Falling.Eval(5, -1));
    }

    [Fact]
    public void Rising_NegativeCountThrows()
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => Factorial.Rising.Eval(5, -1));
    }
}
