using System;
using Xunit;

namespace SMath.Functions1;

public class ExponentialTests
{
    [Fact]
    public void Parity()
    {
        Assert.False(Exponential.IsEven);
        Assert.False(Exponential.IsOdd);
    }

    [Fact]
    public void IsContinuous()
    {
        Assert.True(Exponential.IsContinuous);
    }

    [Fact]
    public void PlainTextFormula()
    {
        Assert.Equal("b^x", Exponential.PlainTextFormula);
    }

    [Fact]
    public void Domain_IsFullRange()
    {
        var domain = Exponential.Domain<double>();
        Assert.Equal(double.NegativeInfinity, domain.Min);
        Assert.Equal(double.PositiveInfinity, domain.Max);
    }

    [Fact]
    public void Image_IsPositive()
    {
        var image = Exponential.Image<double>();
        Assert.Equal(0d, image.Min);
        Assert.Equal(double.PositiveInfinity, image.Max);
    }

    [Theory]
    [InlineData(0d, 2d, 1d)]
    [InlineData(1d, 2d, 2d)]
    [InlineData(3d, 2d, 8d)]
    [InlineData(-1d, 2d, 0.5d)]
    [InlineData(2d, 10d, 100d)]
    public void Eval(double x, double @base, double expected)
    {
        Assert.Equal(expected, Exponential.Eval(x, @base), 6);
    }

    [Fact]
    public void Eval_WithBaseE_EqualsNaturalExponential()
    {
        foreach (var x in new[] { -2.5d, 0d, 1.3d })
            Assert.Equal(NaturalExponential.Eval(x), Exponential.Eval(x, Math.E), 6);
    }

    [Fact]
    public void DerivativeEval()
    {
        Assert.Equal(8d * Math.Log(2d), Exponential.DerivativeEval(3d, 2d), 6);
    }
}
