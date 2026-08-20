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
    [InlineData(52, 5, 2598960)]
    [InlineData(30, 15, 155117520)]
    public void Eval(int n, int k, int expected)
    {
        Assert.Equal(expected, BinomialCoefficient.Eval(n, k));
    }

    [Theory]
    [InlineData(-1, 2, 0)]
    [InlineData(2, -1, 0)]
    [InlineData(2, 3, 0)]
    public void Eval_OutOfRangeIsZero(int n, int k, int expected)
    {
        Assert.Equal(expected, BinomialCoefficient.Eval(n, k));
    }

    [Fact]
    public void Eval_IsSymmetric()
    {
        for (int k = 0; k <= 20; k++)
            Assert.Equal(BinomialCoefficient.Eval(20, k), BinomialCoefficient.Eval(20, 20 - k));
    }

    [Theory]
    [InlineData(30, 15, 155117520)]
    [InlineData(33, 16, 1166803110)]
    public void Eval_DoesNotOverflowOnThePartialProduct(int n, int k, int expected)
    {
        // the plain multiplicative formula overflows an int on the last steps,
        // although the result itself fits
        Assert.Equal(expected, BinomialCoefficient.Eval(n, k));
    }

    [Fact]
    public void Eval_DoesNotOverflowOnTheFactorials()
    {
        // the factorial form would overflow a long already at 21!
        Assert.Equal(118264581564861424L, BinomialCoefficient.Eval(60L, 30L));
    }
}
