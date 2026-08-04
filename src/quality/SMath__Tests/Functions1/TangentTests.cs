using System;
using Xunit;

namespace SMath.Functions1;

public class TangentTests
{
    [Fact]
    public void IsOdd_NotEven()
    {
        Assert.True(Tangent.IsOdd);
        Assert.False(Tangent.IsEven);
    }

    [Fact]
    public void IsNotContinuous()
    {
        Assert.False(Tangent.IsContinuous);
    }

    [Fact]
    public void PlainTextFormula_IsTan()
    {
        Assert.Equal("tan(x)", Tangent.PlainTextFormula);
    }

    [Fact]
    public void Domain_IsFullRange()
    {
        var domain = Tangent.Domain<double>();
        Assert.Equal(double.NegativeInfinity, domain.Min);
        Assert.Equal(double.PositiveInfinity, domain.Max);
    }

    [Fact]
    public void NumberDomain_IsFullRange()
    {
        var domain = Tangent.NumberDomain<double>();
        Assert.Equal(double.MinValue, domain.Min);
        Assert.Equal(double.MaxValue, domain.Max);
    }

    [Fact]
    public void Image_IsFullRange()
    {
        var image = Tangent.Image<double>();
        Assert.Equal(double.NegativeInfinity, image.Min);
        Assert.Equal(double.PositiveInfinity, image.Max);
    }

    [Fact]
    public void NumberImage_IsFullRange()
    {
        var image = Tangent.NumberImage<double>();
        Assert.Equal(double.MinValue, image.Min);
        Assert.Equal(double.MaxValue, image.Max);
    }

    [Fact]
    public void GlobalExtremes_AreInfinite()
    {
        Assert.Equal(double.PositiveInfinity, Tangent.GlobalMaximum<double>());
        Assert.Equal(double.NegativeInfinity, Tangent.GlobalMinimum<double>());
    }

    [Fact]
    public void Period_IsPi()
    {
        Assert.Equal(Math.PI, Tangent.Period<double>(), 6);
    }

    [Theory]
    [InlineData(0d, 0d)]
    [InlineData(Math.PI / 6d, 0.5773502691896257d)]
    [InlineData(Math.PI / 4d, 1d)]
    [InlineData(Math.PI / 3d, 1.7320508075688772d)]
    [InlineData(-Math.PI / 4d, -1d)]
    [InlineData(Math.PI, 0d)]
    public void Eval(double x, double expected)
    {
        Assert.Equal(expected, Tangent.Eval(x), 6);
    }

    [Fact]
    public void Eval_IsOddFunction()
    {
        foreach (var x in new[] { 0.3d, 0.7d, 1.2d, 1.4d })
            Assert.Equal(-Tangent.Eval(x), Tangent.Eval(-x), 6);
    }

    [Fact]
    public void Eval_IsPeriodicWithPi()
    {
        foreach (var x in new[] { 0.3d, 0.7d, 1.2d, -0.9d })
            Assert.Equal(Tangent.Eval(x), Tangent.Eval(x + Tangent.Period<double>()), 6);
    }

    [Fact]
    public void Eval_EqualsSineOverCosine()
    {
        foreach (var x in new[] { -1.2d, -0.4d, 0d, 0.9d, 1.3d })
            Assert.Equal(Sine.Eval(x) / Cosine.Eval(x), Tangent.Eval(x), 6);
    }

    [Fact]
    public void Eval_NearAsymptote_IsLarge()
    {
        Assert.True(Math.Abs(Tangent.Eval((Math.PI / 2d) - 1e-8d)) > 1e6d);
        Assert.True(Math.Abs(Tangent.Eval((Math.PI / 2d) + 1e-8d)) > 1e6d);
    }

    [Theory]
    [InlineData(0d, 1d)]
    [InlineData(Math.PI / 4d, 2d)]
    [InlineData(Math.PI / 3d, 4d)]
    [InlineData(-Math.PI / 4d, 2d)]
    public void DerivativeEval(double x, double expected)
    {
        Assert.Equal(expected, Tangent.DerivativeEval(x), 6);
    }

    [Fact]
    public void DerivativeEval_IsAlwaysPositive()
    {
        foreach (var x in new[] { -1.4d, -0.5d, 0d, 0.5d, 1.4d })
            Assert.True(Tangent.DerivativeEval(x) > 0d);
    }
}
