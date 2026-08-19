using System;
using Xunit;

namespace SMath.Functions1;

public class NaturalLogarithmTests
{
    [Fact]
    public void Parity()
    {
        Assert.False(NaturalLogarithm.IsEven);
        Assert.False(NaturalLogarithm.IsOdd);
    }

    [Fact]
    public void IsContinuous()
    {
        Assert.True(NaturalLogarithm.IsContinuous);
    }

    [Fact]
    public void PlainTextFormula()
    {
        Assert.Equal("ln(x)", NaturalLogarithm.PlainTextFormula);
    }

    [Fact]
    public void Domain_IsPositive()
    {
        var domain = NaturalLogarithm.Domain<double>();
        Assert.Equal(0d, domain.Min);
        Assert.Equal(double.PositiveInfinity, domain.Max);
    }

    [Fact]
    public void NumberDomain_IsPositive()
    {
        var domain = NaturalLogarithm.NumberDomain<double>();
        Assert.Equal(0d, domain.Min);
        Assert.Equal(double.MaxValue, domain.Max);
    }

    [Fact]
    public void Image_IsFullRange()
    {
        var image = NaturalLogarithm.Image<double>();
        Assert.Equal(double.NegativeInfinity, image.Min);
        Assert.Equal(double.PositiveInfinity, image.Max);
    }

    [Fact]
    public void GlobalExtremes()
    {
        Assert.Equal(double.PositiveInfinity, NaturalLogarithm.GlobalMaximum<double>());
        Assert.Equal(double.NegativeInfinity, NaturalLogarithm.GlobalMinimum<double>());
    }

    [Theory]
    [InlineData(1d, 0d)]
    [InlineData(Math.E, 1d)]
    [InlineData(1d / Math.E, -1d)]
    public void Eval(double x, double expected)
    {
        Assert.Equal(expected, NaturalLogarithm.Eval(x), 6);
    }

    [Fact]
    public void Eval_EqualsGeneralLogarithmWithBaseE()
    {
        foreach (var x in new[] { 0.4d, 1d, 5.6d })
            Assert.Equal(Logarithm.Eval(x, Math.E), NaturalLogarithm.Eval(x), 6);
    }

    [Fact]
    public void DerivativeEval()
    {
        Assert.Equal(0.25d, NaturalLogarithm.DerivativeEval(4d), 9);
    }
}
