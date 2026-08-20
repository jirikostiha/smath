using Xunit;

namespace SMath.Functions1;

public class StepFunctionTests
{
    [Fact]
    public void Parity()
    {
        Assert.False(StepFunction.IsEven);
        Assert.False(StepFunction.IsOdd);
    }

    [Fact]
    public void IsNotContinuous()
    {
        Assert.False(StepFunction.IsContinuous);
    }

    [Fact]
    public void PlainTextFormula()
    {
        Assert.Equal("ya, x < c; p, x = c; yb, x > c", StepFunction.PlainTextFormula);
    }

    [Fact]
    public void Domain_IsFullRange()
    {
        var domain = StepFunction.Domain<double>();
        Assert.Equal(double.NegativeInfinity, domain.Min);
        Assert.Equal(double.PositiveInfinity, domain.Max);
    }

    [Theory]
    [InlineData(1.9d, -5d)]
    [InlineData(2d, 0d)]
    [InlineData(2.1d, 5d)]
    public void Eval(double x, double expected)
    {
        Assert.Equal(expected, StepFunction.Eval(x, 2d, 0d, -5d, 5d));
    }

    [Fact]
    public void Eval_Int()
    {
        Assert.Equal(-5, StepFunction.Eval(1, 2, 0, -5, 5));
        Assert.Equal(0, StepFunction.Eval(2, 2, 0, -5, 5));
        Assert.Equal(5, StepFunction.Eval(3, 2, 0, -5, 5));
    }
}
