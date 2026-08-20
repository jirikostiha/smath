using Xunit;

namespace SMath.Functions1;

public class UnitStepFunctionTests
{
    [Fact]
    public void Parity()
    {
        Assert.False(UnitStepFunction.IsEven);
        Assert.False(UnitStepFunction.IsOdd);
    }

    [Fact]
    public void IsNotContinuous()
    {
        Assert.False(UnitStepFunction.IsContinuous);
    }

    [Fact]
    public void PlainTextFormula()
    {
        Assert.Equal("0, x < 0; p, x = 0; 1, x > 0", UnitStepFunction.PlainTextFormula);
    }

    [Fact]
    public void Image_IsZeroToOne()
    {
        var image = UnitStepFunction.Image<double>();
        Assert.Equal(0d, image.Min);
        Assert.Equal(1d, image.Max);
    }

    [Fact]
    public void GlobalExtremes_AreZeroAndOne()
    {
        Assert.Equal(1d, UnitStepFunction.GlobalMaximum<double>());
        Assert.Equal(0d, UnitStepFunction.GlobalMinimum<double>());
    }

    [Theory]
    [InlineData(-0.001d, 0d)]
    [InlineData(0d, 0.25d)]
    [InlineData(0.001d, 1d)]
    public void Eval_WithValueAtZero(double x, double expected)
    {
        Assert.Equal(expected, UnitStepFunction.Eval(x, 0.25d));
    }

    [Theory]
    [InlineData(-1d, 0d)]
    [InlineData(0d, 0.5d)]
    [InlineData(1d, 1d)]
    public void Eval_HalfMaximumConvention(double x, double expected)
    {
        Assert.Equal(expected, UnitStepFunction.Eval(x));
    }

    [Fact]
    public void Eval_IsStepFunctionAtZero()
    {
        foreach (var x in new[] { -2.5d, 0d, 3.1d })
            Assert.Equal(StepFunction.Eval(x, 0d, 0.5d, 0d, 1d), UnitStepFunction.Eval(x));
    }
}
