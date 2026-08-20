using Xunit;

namespace SMath.Functions1;

public class GaussianFunctionTests
{
    [Fact]
    public void Parity()
    {
        Assert.False(GaussianFunction.IsEven);
        Assert.False(GaussianFunction.IsOdd);
    }

    [Fact]
    public void IsContinuous()
    {
        Assert.True(GaussianFunction.IsContinuous);
    }

    [Fact]
    public void PlainTextFormula()
    {
        Assert.Equal("a * e^(-((x - b)^2 / (2*c^2)))", GaussianFunction.PlainTextFormula);
    }

    [Fact]
    public void Eval_AtCenter_IsPeak()
    {
        Assert.Equal(3d, GaussianFunction.Eval(2d, 3d, 2d, 1.5d), 6);
    }

    [Fact]
    public void Eval_IsSymmetricAroundCenter()
    {
        foreach (var distance in new[] { 0.4d, 1d, 2.6d })
        {
            Assert.Equal(
                GaussianFunction.Eval(2d + distance, 3d, 2d, 1.5d),
                GaussianFunction.Eval(2d - distance, 3d, 2d, 1.5d),
                6);
        }
    }

    [Theory]
    [InlineData(0d, 1d)]
    [InlineData(1d, 0.606531d)]
    [InlineData(-1d, 0.606531d)]
    [InlineData(2d, 0.135335d)]
    public void Eval_StandardBell(double x, double expected)
    {
        Assert.Equal(expected, GaussianFunction.Eval(x, 1d, 0d, 1d), 6);
    }

    [Fact]
    public void DerivativeEval_AtCenter_IsZero()
    {
        Assert.Equal(0d, GaussianFunction.DerivativeEval(2d, 3d, 2d, 1.5d), 6);
    }

    [Fact]
    public void DerivativeEval_StandardBell()
    {
        // -x * e^(-x^2/2)
        Assert.Equal(-0.606531d, GaussianFunction.DerivativeEval(1d, 1d, 0d, 1d), 6);
    }
}
