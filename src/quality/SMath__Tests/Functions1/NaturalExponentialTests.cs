using System;
using Xunit;

namespace SMath.Functions1;

public class NaturalExponentialTests
{
    [Fact]
    public void Parity()
    {
        Assert.False(NaturalExponential.IsEven);
        Assert.False(NaturalExponential.IsOdd);
    }

    [Fact]
    public void IsContinuous()
    {
        Assert.True(NaturalExponential.IsContinuous);
    }

    [Fact]
    public void PlainTextFormula()
    {
        Assert.Equal("e^x", NaturalExponential.PlainTextFormula);
    }

    [Fact]
    public void Image_IsPositive()
    {
        var image = NaturalExponential.Image<double>();
        Assert.Equal(0d, image.Min);
        Assert.Equal(double.PositiveInfinity, image.Max);
    }

    [Theory]
    [InlineData(0d, 1d)]
    [InlineData(1d, Math.E)]
    [InlineData(-1d, 1d / Math.E)]
    public void Eval(double x, double expected)
    {
        Assert.Equal(expected, NaturalExponential.Eval(x), 6);
    }

    [Fact]
    public void DerivativeEval_EqualsEval()
    {
        foreach (var x in new[] { -1.5d, 0d, 2.2d })
            Assert.Equal(NaturalExponential.Eval(x), NaturalExponential.DerivativeEval(x), 6);
    }

    [Fact]
    public void Eval_IsInverseOfNaturalLogarithm()
    {
        foreach (var x in new[] { 0.4d, 1d, 5.6d })
            Assert.Equal(x, NaturalExponential.Eval(NaturalLogarithm.Eval(x)), 6);
    }
}
