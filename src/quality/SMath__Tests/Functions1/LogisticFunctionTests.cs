using Xunit;

namespace SMath.Functions1;

public class LogisticFunctionTests
{
    [Fact]
    public void Parity()
    {
        Assert.False(LogisticFunction.IsEven);
        Assert.False(LogisticFunction.IsOdd);
    }

    [Fact]
    public void IsContinuous()
    {
        Assert.True(LogisticFunction.IsContinuous);
    }

    [Fact]
    public void PlainTextFormula()
    {
        Assert.Equal("l / (1 + e^(-k*(x - x0)))", LogisticFunction.PlainTextFormula);
    }

    [Fact]
    public void Domain_IsFullRange()
    {
        var domain = LogisticFunction.Domain<double>();
        Assert.Equal(double.NegativeInfinity, domain.Min);
        Assert.Equal(double.PositiveInfinity, domain.Max);
    }

    [Theory]
    [InlineData(0d, 1d, 1d, 0.5d)]
    [InlineData(0d, 4d, 1d, 2d)]
    [InlineData(1d, 1d, 1d, 0.731059d)]
    [InlineData(1d, 1d, 2d, 0.880797d)]
    public void Eval(double x, double l, double k, double expected)
    {
        Assert.Equal(expected, LogisticFunction.Eval(x, l, k), 6);
    }

    [Fact]
    public void Eval_AtMidpoint_IsHalfOfSupremum()
    {
        Assert.Equal(2.5d, LogisticFunction.Eval(3d, 5d, 1.5d, 3d), 6);
    }

    [Fact]
    public void Eval_WithoutMidpoint_EqualsMidpointAtZero()
    {
        foreach (var x in new[] { -2d, 0d, 1.4d })
            Assert.Equal(LogisticFunction.Eval(x, 3d, 2d, 0d), LogisticFunction.Eval(x, 3d, 2d), 6);
    }

    [Fact]
    public void Eval_WithUnitParameters_EqualsSigmoid()
    {
        foreach (var x in new[] { -3.5d, 0d, 2.1d })
            Assert.Equal(SigmoidFunction.Eval(x), LogisticFunction.Eval(x, 1d, 1d), 6);
    }

    [Fact]
    public void DerivativeEval_AtMidpoint_IsMaximal()
    {
        // l * k / 4 at the midpoint
        Assert.Equal(5d * 1.5d / 4d, LogisticFunction.DerivativeEval(3d, 5d, 1.5d, 3d), 6);
    }
}
