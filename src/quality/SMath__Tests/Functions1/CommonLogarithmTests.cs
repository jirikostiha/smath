using System;
using Xunit;

namespace SMath.Functions1;

public class CommonLogarithmTests
{
    [Fact]
    public void Parity()
    {
        Assert.False(CommonLogarithm.IsEven);
        Assert.False(CommonLogarithm.IsOdd);
    }

    [Fact]
    public void IsContinuous()
    {
        Assert.True(CommonLogarithm.IsContinuous);
    }

    [Fact]
    public void PlainTextFormula()
    {
        Assert.Equal("log10(x)", CommonLogarithm.PlainTextFormula);
    }

    [Fact]
    public void Domain_IsPositive()
    {
        var domain = CommonLogarithm.Domain<double>();
        Assert.Equal(0d, domain.Min);
        Assert.Equal(double.PositiveInfinity, domain.Max);
    }

    [Fact]
    public void NumberDomain_IsPositive()
    {
        var domain = CommonLogarithm.NumberDomain<double>();
        Assert.Equal(0d, domain.Min);
        Assert.Equal(double.MaxValue, domain.Max);
    }

    [Fact]
    public void Image_IsFullRange()
    {
        var image = CommonLogarithm.Image<double>();
        Assert.Equal(double.NegativeInfinity, image.Min);
        Assert.Equal(double.PositiveInfinity, image.Max);
    }

    [Fact]
    public void GlobalExtremes()
    {
        Assert.Equal(double.PositiveInfinity, CommonLogarithm.GlobalMaximum<double>());
        Assert.Equal(double.NegativeInfinity, CommonLogarithm.GlobalMinimum<double>());
    }

    [Theory]
    [InlineData(1d, 0d)]
    [InlineData(10d, 1d)]
    [InlineData(100d, 2d)]
    [InlineData(0.1d, -1d)]
    public void Eval(double x, double expected)
    {
        Assert.Equal(expected, CommonLogarithm.Eval(x), 6);
    }

    [Fact]
    public void Eval_EqualsGeneralLogarithmWithBase10()
    {
        foreach (var x in new[] { 0.4d, 1d, 5.6d })
            Assert.Equal(Logarithm.Eval(x, 10d), CommonLogarithm.Eval(x), 6);
    }

    [Fact]
    public void DerivativeEval()
    {
        Assert.Equal(1d / (100d * Math.Log(10d)), CommonLogarithm.DerivativeEval(100d), 9);
    }
}
