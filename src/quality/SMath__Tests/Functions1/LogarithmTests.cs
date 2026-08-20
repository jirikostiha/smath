using System;
using Xunit;

namespace SMath.Functions1;

public class LogarithmTests
{
    [Fact]
    public void Parity()
    {
        Assert.False(Logarithm.IsEven);
        Assert.False(Logarithm.IsOdd);
    }

    [Fact]
    public void PlainTextFormula()
    {
        Assert.Equal("log(x, b)", Logarithm.PlainTextFormula);
    }

    [Fact]
    public void Domain_IsPositive()
    {
        var domain = Logarithm.Domain<double>();
        Assert.Equal(0d, domain.Min);
        Assert.Equal(double.PositiveInfinity, domain.Max);
    }

    [Fact]
    public void Image_IsFullRange()
    {
        var image = Logarithm.Image<double>();
        Assert.Equal(double.NegativeInfinity, image.Min);
        Assert.Equal(double.PositiveInfinity, image.Max);
    }

    [Theory]
    [InlineData(1d, 2d, 0d)]
    [InlineData(8d, 2d, 3d)]
    [InlineData(100d, 10d, 2d)]
    [InlineData(0.5d, 2d, -1d)]
    public void Eval(double x, double @base, double expected)
    {
        Assert.Equal(expected, Logarithm.Eval(x, @base), 6);
    }

    [Fact]
    public void Eval_IsInverseOfExponential()
    {
        foreach (var x in new[] { 0.4d, 1d, 5.6d })
            Assert.Equal(x, Logarithm.Eval(Exponential.Eval(x, 3d), 3d), 6);
    }

    [Fact]
    public void DerivativeEval()
    {
        Assert.Equal(1d / (8d * Math.Log(2d)), Logarithm.DerivativeEval(8d, 2d), 9);
    }
}
