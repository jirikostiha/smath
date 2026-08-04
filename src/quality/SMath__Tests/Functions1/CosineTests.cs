using System;
using Xunit;

namespace SMath.Functions1;

public class CosineTests
{
    [Fact]
    public void IsEven_NotOdd()
    {
        Assert.True(Cosine.IsEven);
        Assert.False(Cosine.IsOdd);
    }

    [Fact]
    public void IsContinuous()
    {
        Assert.True(Cosine.IsContinuous);
    }

    [Fact]
    public void PlainTextFormula_IsCos()
    {
        Assert.Equal("cos(x)", Cosine.PlainTextFormula);
    }

    [Fact]
    public void Domain_IsFullRange()
    {
        var domain = Cosine.Domain<double>();
        Assert.Equal(double.NegativeInfinity, domain.Min);
        Assert.Equal(double.PositiveInfinity, domain.Max);
    }

    [Fact]
    public void NumberDomain_IsFullRange()
    {
        var domain = Cosine.NumberDomain<double>();
        Assert.Equal(double.MinValue, domain.Min);
        Assert.Equal(double.MaxValue, domain.Max);
    }

    [Fact]
    public void Image_IsMinusOneToOne()
    {
        var image = Cosine.Image<double>();
        Assert.Equal(-1d, image.Min, 6);
        Assert.Equal(1d, image.Max, 6);
    }

    [Fact]
    public void NumberImage_IsMinusOneToOne()
    {
        var image = Cosine.NumberImage<double>();
        Assert.Equal(-1d, image.Min, 6);
        Assert.Equal(1d, image.Max, 6);
    }

    [Fact]
    public void GlobalExtremes_AreMinusOneAndOne()
    {
        Assert.Equal(1d, Cosine.GlobalMaximum<double>(), 6);
        Assert.Equal(-1d, Cosine.GlobalMinimum<double>(), 6);
    }

    [Theory]
    [InlineData(0d, 1d)]
    [InlineData(Math.PI / 3d, 0.5d)]
    [InlineData(Math.PI / 2d, 0d)]
    [InlineData(Math.PI, -1d)]
    [InlineData(3d * Math.PI / 2d, 0d)]
    [InlineData(2d * Math.PI, 1d)]
    public void Eval(double x, double expected)
    {
        Assert.Equal(expected, Cosine.Eval(x), 6);
    }

    [Fact]
    public void Eval_IsEvenFunction()
    {
        foreach (var x in new[] { 0.3d, 1.7d, 4.2d, 12.5d })
            Assert.Equal(Cosine.Eval(x), Cosine.Eval(-x), 6);
    }

    [Fact]
    public void Eval_IsBoundedByImage()
    {
        for (var x = -10d; x <= 10d; x += 0.25d)
        {
            var y = Cosine.Eval(x);
            Assert.InRange(y, -1d, 1d);
        }
    }

    [Theory]
    [InlineData(0d, 0d)]
    [InlineData(Math.PI / 2d, -1d)]
    [InlineData(Math.PI, 0d)]
    [InlineData(3d * Math.PI / 2d, 1d)]
    public void DerivativeEval_IsMinusSine(double x, double expected)
    {
        Assert.Equal(expected, Cosine.DerivativeEval(x), 6);
    }

    [Fact]
    public void DerivativeEval_EqualsMinusSineEval()
    {
        foreach (var x in new[] { -2.5d, -0.4d, 0d, 0.9d, 3.3d })
            Assert.Equal(-Sine.Eval(x), Cosine.DerivativeEval(x), 6);
    }

    [Fact]
    public void Eval_ShiftedSine_Equivalence()
    {
        foreach (var x in new[] { -2.5d, -0.4d, 0d, 0.9d, 3.3d })
            Assert.Equal(Sine.Eval(x + (Math.PI / 2d)), Cosine.Eval(x), 6);
    }
}
