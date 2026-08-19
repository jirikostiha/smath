using Xunit;

namespace SMath.Functions1;

public class Power5Tests
{
    [Fact]
    public void Parity()
    {
        Assert.False(Power5.IsEven);
        Assert.True(Power5.IsOdd);
    }

    [Fact]
    public void IsContinuous()
    {
        Assert.True(Power5.IsContinuous);
    }

    [Fact]
    public void PlainTextFormula_IsXPow5()
    {
        Assert.Equal("x^5", Power5.PlainTextFormula);
    }

    [Fact]
    public void Domain_IsFullRange()
    {
        var domain = Power5.Domain<double>();
        Assert.Equal(double.NegativeInfinity, domain.Min);
        Assert.Equal(double.PositiveInfinity, domain.Max);
    }

    [Fact]
    public void NumberDomain_IsFullRange()
    {
        var domain = Power5.NumberDomain<double>();
        Assert.Equal(double.MinValue, domain.Min);
        Assert.Equal(double.MaxValue, domain.Max);
    }

    [Fact]
    public void Image()
    {
        var image = Power5.Image<double>();
        Assert.Equal(double.NegativeInfinity, image.Min);
        Assert.Equal(double.PositiveInfinity, image.Max);
    }

    [Fact]
    public void NumberImage()
    {
        var image = Power5.NumberImage<double>();
        Assert.Equal(double.MinValue, image.Min);
        Assert.Equal(double.MaxValue, image.Max);
    }

    [Fact]
    public void GlobalExtremes()
    {
        Assert.Equal(double.PositiveInfinity, Power5.GlobalMaximum<double>());
        Assert.Equal(double.NegativeInfinity, Power5.GlobalMinimum<double>());
    }

    [Theory]
    [InlineData(0d, 0d)]
    [InlineData(1d, 1d)]
    [InlineData(-1d, -1d)]
    [InlineData(2d, 32d)]
    [InlineData(-2d, -32d)]
    public void Eval(double x, double expected)
    {
        Assert.Equal(expected, Power5.Eval(x), 6);
    }

    [Fact]
    public void Eval_Int()
    {
        Assert.Equal(32, Power5.Eval(2));
        Assert.Equal(-32, Power5.Eval(-2));
    }

    [Fact]
    public void Eval_IsOddFunction()
    {
        foreach (var x in new[] { 0.3d, 1.7d, 4.2d })
            Assert.Equal(-Power5.Eval(x), Power5.Eval(-x), 6);
    }

    [Theory]
    [InlineData(0d, 0d)]
    [InlineData(2d, 80d)]
    [InlineData(-2d, 80d)]
    public void DerivativeEval(double x, double expected)
    {
        Assert.Equal(expected, Power5.DerivativeEval(x), 6);
    }
}
