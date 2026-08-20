using Xunit;

namespace SMath.Functions1;

public class ReciprocalTests
{
    [Fact]
    public void IsOdd_NotEven()
    {
        Assert.True(Reciprocal.IsOdd);
        Assert.False(Reciprocal.IsEven);
    }

    [Fact]
    public void IsNotContinuous()
    {
        Assert.False(Reciprocal.IsContinuous);
    }

    [Fact]
    public void PlainTextFormula()
    {
        Assert.Equal("1/x", Reciprocal.PlainTextFormula);
    }

    [Fact]
    public void Domain_IsFullRange()
    {
        var domain = Reciprocal.Domain<double>();
        Assert.Equal(double.NegativeInfinity, domain.Min);
        Assert.Equal(double.PositiveInfinity, domain.Max);
    }

    [Fact]
    public void Image_IsFullRange()
    {
        var image = Reciprocal.Image<double>();
        Assert.Equal(double.NegativeInfinity, image.Min);
        Assert.Equal(double.PositiveInfinity, image.Max);
    }

    [Theory]
    [InlineData(1d, 1d)]
    [InlineData(-1d, -1d)]
    [InlineData(2d, 0.5d)]
    [InlineData(-4d, -0.25d)]
    [InlineData(0.125d, 8d)]
    public void Eval(double x, double expected)
    {
        Assert.Equal(expected, Reciprocal.Eval(x), 6);
    }

    [Fact]
    public void Eval_IsOddFunction()
    {
        foreach (var x in new[] { 0.3d, 1.7d, 4.2d })
            Assert.Equal(-Reciprocal.Eval(x), Reciprocal.Eval(-x), 6);
    }

    [Fact]
    public void Eval_AtZero_IsInfinity()
    {
        Assert.Equal(double.PositiveInfinity, Reciprocal.Eval(0d));
    }

    [Theory]
    [InlineData(1d, -1d)]
    [InlineData(2d, -0.25d)]
    [InlineData(-2d, -0.25d)]
    public void DerivativeEval(double x, double expected)
    {
        Assert.Equal(expected, Reciprocal.DerivativeEval(x), 6);
    }
}
