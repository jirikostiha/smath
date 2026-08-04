using System;
using Xunit;

namespace SMath.Functions1;

public class CotangentTests
{
    [Fact]
    public void IsOdd_NotEven()
    {
        Assert.True(Cotangent.IsOdd);
        Assert.False(Cotangent.IsEven);
    }

    [Fact]
    public void IsNotContinuous()
    {
        Assert.False(Cotangent.IsContinuous);
    }

    [Fact]
    public void PlainTextFormula_IsCot()
    {
        Assert.Equal("cot(x)", Cotangent.PlainTextFormula);
    }

    [Fact]
    public void Domain_IsFullRange()
    {
        var domain = Cotangent.Domain<double>();
        Assert.Equal(double.NegativeInfinity, domain.Min);
        Assert.Equal(double.PositiveInfinity, domain.Max);
    }

    [Fact]
    public void NumberDomain_IsFullRange()
    {
        var domain = Cotangent.NumberDomain<double>();
        Assert.Equal(double.MinValue, domain.Min);
        Assert.Equal(double.MaxValue, domain.Max);
    }

    [Fact]
    public void Image_IsFullRange()
    {
        var image = Cotangent.Image<double>();
        Assert.Equal(double.NegativeInfinity, image.Min);
        Assert.Equal(double.PositiveInfinity, image.Max);
    }

    [Fact]
    public void NumberImage_IsFullRange()
    {
        var image = Cotangent.NumberImage<double>();
        Assert.Equal(double.MinValue, image.Min);
        Assert.Equal(double.MaxValue, image.Max);
    }

    [Fact]
    public void GlobalExtremes_AreInfinite()
    {
        Assert.Equal(double.PositiveInfinity, Cotangent.GlobalMaximum<double>());
        Assert.Equal(double.NegativeInfinity, Cotangent.GlobalMinimum<double>());
    }

    [Fact]
    public void Period_IsPi()
    {
        Assert.Equal(Math.PI, Cotangent.Period<double>(), 6);
    }

    [Theory]
    [InlineData(Math.PI / 6d, 1.7320508075688772d)]
    [InlineData(Math.PI / 4d, 1d)]
    [InlineData(Math.PI / 3d, 0.5773502691896257d)]
    [InlineData(Math.PI / 2d, 0d)]
    [InlineData(-Math.PI / 4d, -1d)]
    [InlineData(3d * Math.PI / 4d, -1d)]
    public void Eval(double x, double expected)
    {
        Assert.Equal(expected, Cotangent.Eval(x), 6);
    }

    [Fact]
    public void Eval_IsOddFunction()
    {
        foreach (var x in new[] { 0.3d, 0.7d, 1.2d, 1.4d })
            Assert.Equal(-Cotangent.Eval(x), Cotangent.Eval(-x), 6);
    }

    [Fact]
    public void Eval_IsPeriodicWithPi()
    {
        foreach (var x in new[] { 0.3d, 0.7d, 1.2d, 2.4d })
            Assert.Equal(Cotangent.Eval(x), Cotangent.Eval(x + Cotangent.Period<double>()), 6);
    }

    [Fact]
    public void Eval_EqualsCosineOverSine()
    {
        foreach (var x in new[] { 0.4d, 0.9d, 1.3d, 2.7d })
            Assert.Equal(Cosine.Eval(x) / Sine.Eval(x), Cotangent.Eval(x), 6);
    }

    [Fact]
    public void Eval_IsReciprocalOfTangent()
    {
        foreach (var x in new[] { 0.4d, 0.9d, 1.3d, 2.7d })
            Assert.Equal(1d / Tangent.Eval(x), Cotangent.Eval(x), 6);
    }

    [Fact]
    public void Eval_NearAsymptote_IsLarge()
    {
        Assert.True(Math.Abs(Cotangent.Eval(1e-8d)) > 1e6d);
        Assert.True(Math.Abs(Cotangent.Eval(-1e-8d)) > 1e6d);
        Assert.True(Math.Abs(Cotangent.Eval(Math.PI - 1e-8d)) > 1e6d);
    }

    [Theory]
    [InlineData(Math.PI / 4d, -2d)]
    [InlineData(Math.PI / 2d, -1d)]
    [InlineData(Math.PI / 6d, -4d)]
    [InlineData(-Math.PI / 4d, -2d)]
    public void DerivativeEval(double x, double expected)
    {
        Assert.Equal(expected, Cotangent.DerivativeEval(x), 6);
    }

    [Fact]
    public void DerivativeEval_IsAlwaysNegative()
    {
        foreach (var x in new[] { -2.5d, -0.5d, 0.5d, 1.5d, 2.5d })
            Assert.True(Cotangent.DerivativeEval(x) < 0d);
    }
}
