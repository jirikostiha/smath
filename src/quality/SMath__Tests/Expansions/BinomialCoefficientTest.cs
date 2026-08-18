using Xunit;

namespace SMath.Expansions;

public class BinomialCoefficientTest
{
    [Theory]
    [InlineData(0, 0, 1)]
    [InlineData(4, 0, 1)]
    [InlineData(4, 4, 1)]
    [InlineData(5, 2, 10)]
    [InlineData(6, 3, 20)]
    public void Eval(int n, int k, int expected)
    {
        Assert.Equal(expected, BinomialCoefficient.Eval(n, k));
    }
}
