using Xunit;

namespace SMath.Functions1;

public class SigmoidFunctionTests
{
    [Fact]
    public void Parity()
    {
        Assert.False(SigmoidFunction.IsEven);
        Assert.False(SigmoidFunction.IsOdd);
    }

    [Fact]
    public void IsContinuous()
    {
        Assert.True(SigmoidFunction.IsContinuous);
    }

    [Fact]
    public void PlainTextFormula()
    {
        Assert.Equal("1 / (1 + e^(-x))", SigmoidFunction.PlainTextFormula);
    }

    [Fact]
    public void Domain_IsFullRange()
    {
        var domain = SigmoidFunction.Domain<double>();
        Assert.Equal(double.NegativeInfinity, domain.Min);
        Assert.Equal(double.PositiveInfinity, domain.Max);
    }

    [Fact]
    public void Image_IsZeroToOne()
    {
        var image = SigmoidFunction.Image<double>();
        Assert.Equal(0d, image.Min);
        Assert.Equal(1d, image.Max);
    }

    [Fact]
    public void GlobalExtremes_AreZeroAndOne()
    {
        Assert.Equal(1d, SigmoidFunction.GlobalMaximum<double>());
        Assert.Equal(0d, SigmoidFunction.GlobalMinimum<double>());
    }

    [Theory]
    [InlineData(0d, 0.5d)]
    [InlineData(1d, 0.731059d)]
    [InlineData(-1d, 0.268941d)]
    [InlineData(-20d, 0d)]
    [InlineData(20d, 1d)]
    public void Eval(double x, double expected)
    {
        Assert.Equal(expected, SigmoidFunction.Eval(x), 6);
    }

    [Fact]
    public void Eval_IsBoundedByImage()
    {
        for (var x = -10d; x <= 10d; x += 0.25d)
            Assert.InRange(SigmoidFunction.Eval(x), 0d, 1d);
    }

    [Fact]
    public void Eval_IsSymmetricAroundHalf()
    {
        foreach (var x in new[] { 0.3d, 1.7d, 4.2d })
            Assert.Equal(1d - SigmoidFunction.Eval(x), SigmoidFunction.Eval(-x), 6);
    }

    [Theory]
    [InlineData(0d, 0.25d)]
    [InlineData(1d, 0.196612d)]
    [InlineData(-1d, 0.196612d)]
    public void DerivativeEval(double x, double expected)
    {
        Assert.Equal(expected, SigmoidFunction.DerivativeEval(x), 6);
    }
}
