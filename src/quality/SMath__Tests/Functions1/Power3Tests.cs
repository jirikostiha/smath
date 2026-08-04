using Xunit;

namespace SMath.Functions1;

public class Power3Tests
{
    [Fact]
    public void IsOdd_NotEven()
    {
        Assert.True(Power3.IsOdd);
        Assert.False(Power3.IsEven);
    }

    [Fact]
    public void IsContinuous()
    {
        Assert.True(Power3.IsContinuous);
    }

    [Fact]
    public void PlainTextFormula_IsXPow3()
    {
        Assert.Equal("x^3", Power3.PlainTextFormula);
    }

    [Fact]
    public void Domain_IsFullRange()
    {
        var domain = Power3.Domain<double>();
        Assert.Equal(double.NegativeInfinity, domain.Min);
        Assert.Equal(double.PositiveInfinity, domain.Max);
    }

    [Fact]
    public void NumberDomain_IsFullRange()
    {
        var domain = Power3.NumberDomain<double>();
        Assert.Equal(double.MinValue, domain.Min);
        Assert.Equal(double.MaxValue, domain.Max);
    }

    [Fact]
    public void Image_IsFullRange()
    {
        var image = Power3.Image<double>();
        Assert.Equal(double.NegativeInfinity, image.Min);
        Assert.Equal(double.PositiveInfinity, image.Max);
    }

    [Fact]
    public void NumberImage_IsFullRange()
    {
        var image = Power3.NumberImage<double>();
        Assert.Equal(double.MinValue, image.Min);
        Assert.Equal(double.MaxValue, image.Max);
    }

    [Fact]
    public void GlobalExtremes_AreInfinite()
    {
        Assert.Equal(double.PositiveInfinity, Power3.GlobalMaximum<double>());
        Assert.Equal(double.NegativeInfinity, Power3.GlobalMinimum<double>());
    }

    [Theory]
    [InlineData(0d, 0d)]
    [InlineData(1d, 1d)]
    [InlineData(-1d, -1d)]
    [InlineData(2d, 8d)]
    [InlineData(-2d, -8d)]
    [InlineData(3d, 27d)]
    [InlineData(-3d, -27d)]
    [InlineData(0.5d, 0.125d)]
    [InlineData(-0.5d, -0.125d)]
    public void Eval(double x, double expected)
    {
        Assert.Equal(expected, Power3.Eval(x), 6);
    }

    [Fact]
    public void Eval_IsOddFunction()
    {
        foreach (var x in new[] { 0.3d, 1.7d, 4.2d, 12.5d })
            Assert.Equal(-Power3.Eval(x), Power3.Eval(-x), 6);
    }

    [Fact]
    public void Eval_Int()
    {
        Assert.Equal(27, Power3.Eval(3));
        Assert.Equal(-27, Power3.Eval(-3));
    }

    [Theory]
    [InlineData(0d, 0d)]
    [InlineData(1d, 3d)]
    [InlineData(-1d, 3d)]
    [InlineData(2d, 12d)]
    [InlineData(-2d, 12d)]
    [InlineData(0.5d, 0.75d)]
    public void DerivativeEval(double x, double expected)
    {
        Assert.Equal(expected, Power3.DerivativeEval(x), 6);
    }
}
