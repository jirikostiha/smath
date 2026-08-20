using System;
using Xunit;

namespace SMath.Functions1;

public class BinaryLogarithmTests
{
    [Fact]
    public void Parity()
    {
        Assert.False(BinaryLogarithm.IsEven);
        Assert.False(BinaryLogarithm.IsOdd);
    }

    [Fact]
    public void IsContinuous()
    {
        Assert.True(BinaryLogarithm.IsContinuous);
    }

    [Fact]
    public void PlainTextFormula()
    {
        Assert.Equal("log2(x)", BinaryLogarithm.PlainTextFormula);
    }

    [Fact]
    public void Domain_IsPositive()
    {
        var domain = BinaryLogarithm.Domain<double>();
        Assert.Equal(0d, domain.Min);
        Assert.Equal(double.PositiveInfinity, domain.Max);
    }

    [Fact]
    public void NumberDomain_IsPositive()
    {
        var domain = BinaryLogarithm.NumberDomain<double>();
        Assert.Equal(0d, domain.Min);
        Assert.Equal(double.MaxValue, domain.Max);
    }

    [Fact]
    public void Image_IsFullRange()
    {
        var image = BinaryLogarithm.Image<double>();
        Assert.Equal(double.NegativeInfinity, image.Min);
        Assert.Equal(double.PositiveInfinity, image.Max);
    }

    [Fact]
    public void GlobalExtremes()
    {
        Assert.Equal(double.PositiveInfinity, BinaryLogarithm.GlobalMaximum<double>());
        Assert.Equal(double.NegativeInfinity, BinaryLogarithm.GlobalMinimum<double>());
    }

    [Theory]
    [InlineData(1d, 0d)]
    [InlineData(2d, 1d)]
    [InlineData(8d, 3d)]
    [InlineData(0.5d, -1d)]
    public void Eval(double x, double expected)
    {
        Assert.Equal(expected, BinaryLogarithm.Eval(x), 6);
    }

    [Fact]
    public void Eval_EqualsGeneralLogarithmWithBase2()
    {
        foreach (var x in new[] { 0.4d, 1d, 5.6d })
            Assert.Equal(Logarithm.Eval(x, 2d), BinaryLogarithm.Eval(x), 6);
    }

    [Fact]
    public void DerivativeEval()
    {
        Assert.Equal(1d / (8d * Math.Log(2d)), BinaryLogarithm.DerivativeEval(8d), 9);
    }
}
