using Xunit;

namespace SMath.Functions1;

public class Power7Tests
{
    [Fact]
    public void Parity()
    {
        Assert.False(Power7.IsEven);
        Assert.True(Power7.IsOdd);
    }

    [Fact]
    public void IsContinuous()
    {
        Assert.True(Power7.IsContinuous);
    }

    [Fact]
    public void PlainTextFormula_IsXPow7()
    {
        Assert.Equal("x^7", Power7.PlainTextFormula);
    }

    [Fact]
    public void Domain_IsFullRange()
    {
        var domain = Power7.Domain<double>();
        Assert.Equal(double.NegativeInfinity, domain.Min);
        Assert.Equal(double.PositiveInfinity, domain.Max);
    }

    [Fact]
    public void NumberDomain_IsFullRange()
    {
        var domain = Power7.NumberDomain<double>();
        Assert.Equal(double.MinValue, domain.Min);
        Assert.Equal(double.MaxValue, domain.Max);
    }

    [Fact]
    public void Image()
    {
        var image = Power7.Image<double>();
        Assert.Equal(double.NegativeInfinity, image.Min);
        Assert.Equal(double.PositiveInfinity, image.Max);
    }

    [Fact]
    public void NumberImage()
    {
        var image = Power7.NumberImage<double>();
        Assert.Equal(double.MinValue, image.Min);
        Assert.Equal(double.MaxValue, image.Max);
    }

    [Fact]
    public void GlobalExtremes()
    {
        Assert.Equal(double.PositiveInfinity, Power7.GlobalMaximum<double>());
        Assert.Equal(double.NegativeInfinity, Power7.GlobalMinimum<double>());
    }

    [Theory]
    [InlineData(0d, 0d)]
    [InlineData(1d, 1d)]
    [InlineData(-1d, -1d)]
    [InlineData(2d, 128d)]
    [InlineData(-2d, -128d)]
    public void Eval(double x, double expected)
    {
        Assert.Equal(expected, Power7.Eval(x), 6);
    }

    [Fact]
    public void Eval_Int()
    {
        Assert.Equal(128, Power7.Eval(2));
        Assert.Equal(-128, Power7.Eval(-2));
    }

    [Fact]
    public void Eval_IsOddFunction()
    {
        foreach (var x in new[] { 0.3d, 1.7d, 4.2d })
            Assert.Equal(-Power7.Eval(x), Power7.Eval(-x), 6);
    }

    [Theory]
    [InlineData(0d, 0d)]
    [InlineData(2d, 448d)]
    [InlineData(-2d, 448d)]
    public void DerivativeEval(double x, double expected)
    {
        Assert.Equal(expected, Power7.DerivativeEval(x), 6);
    }
}
