using System;
using Xunit;

namespace SMath.Functions1;

public class BipolarSigmoidTests
{
    [Fact]
    public void IsOdd_NotEven()
    {
        Assert.True(BipolarSigmoid.IsOdd);
        Assert.False(BipolarSigmoid.IsEven);
    }

    [Fact]
    public void PlainTextFormula()
    {
        Assert.Equal("2 / (1 + e^(-x)) - 1", BipolarSigmoid.PlainTextFormula);
    }

    [Fact]
    public void Image_IsMinusOneToOne()
    {
        var image = BipolarSigmoid.Image<double>();
        Assert.Equal(-1d, image.Min);
        Assert.Equal(1d, image.Max);
    }

    [Fact]
    public void GlobalExtremes_AreMinusOneAndOne()
    {
        Assert.Equal(1d, BipolarSigmoid.GlobalMaximum<double>());
        Assert.Equal(-1d, BipolarSigmoid.GlobalMinimum<double>());
    }

    [Theory]
    [InlineData(0d, 0d)]
    [InlineData(1d, 0.462117d)]
    [InlineData(-1d, -0.462117d)]
    [InlineData(20d, 1d)]
    [InlineData(-20d, -1d)]
    public void Eval(double x, double expected)
    {
        Assert.Equal(expected, BipolarSigmoid.Eval(x), 6);
    }

    [Fact]
    public void Eval_IsOddFunction()
    {
        foreach (var x in new[] { 0.3d, 1.7d, 4.2d })
            Assert.Equal(-BipolarSigmoid.Eval(x), BipolarSigmoid.Eval(-x), 6);
    }

    [Fact]
    public void Eval_EqualsHyperbolicTangentOfHalf()
    {
        foreach (var x in new[] { -3.1d, -0.4d, 0d, 1.2d, 5d })
            Assert.Equal(Math.Tanh(x / 2d), BipolarSigmoid.Eval(x), 6);
    }

    [Fact]
    public void Eval_IsRescaledSigmoid()
    {
        foreach (var x in new[] { -3.1d, 0d, 1.2d })
            Assert.Equal((2d * SigmoidFunction.Eval(x)) - 1d, BipolarSigmoid.Eval(x), 6);
    }

    [Theory]
    [InlineData(0d, 0.5d)]
    [InlineData(1d, 0.393224d)]
    public void DerivativeEval(double x, double expected)
    {
        Assert.Equal(expected, BipolarSigmoid.DerivativeEval(x), 6);
    }
}
