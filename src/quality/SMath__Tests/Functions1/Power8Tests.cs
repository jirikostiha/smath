using Xunit;

namespace SMath.Functions1;

public class Power8Tests
{
    [Fact]
    public void Parity()
    {
        Assert.True(Power8.IsEven);
        Assert.False(Power8.IsOdd);
    }

    [Fact]
    public void IsContinuous()
    {
        Assert.True(Power8.IsContinuous);
    }

    [Fact]
    public void PlainTextFormula_IsXPow8()
    {
        Assert.Equal("x^8", Power8.PlainTextFormula);
    }

    [Fact]
    public void Domain_IsFullRange()
    {
        var domain = Power8.Domain<double>();
        Assert.Equal(double.NegativeInfinity, domain.Min);
        Assert.Equal(double.PositiveInfinity, domain.Max);
    }

    [Fact]
    public void NumberDomain_IsFullRange()
    {
        var domain = Power8.NumberDomain<double>();
        Assert.Equal(double.MinValue, domain.Min);
        Assert.Equal(double.MaxValue, domain.Max);
    }

    [Fact]
    public void Image()
    {
        var image = Power8.Image<double>();
        Assert.Equal(0d, image.Min);
        Assert.Equal(double.PositiveInfinity, image.Max);
    }

    [Fact]
    public void NumberImage()
    {
        var image = Power8.NumberImage<double>();
        Assert.Equal(0d, image.Min);
        Assert.Equal(double.MaxValue, image.Max);
    }

    [Fact]
    public void GlobalExtremes()
    {
        Assert.Equal(double.PositiveInfinity, Power8.GlobalMaximum<double>());
        Assert.Equal(0d, Power8.GlobalMinimum<double>());
    }

    [Theory]
    [InlineData(0d, 0d)]
    [InlineData(1d, 1d)]
    [InlineData(-1d, 1d)]
    [InlineData(2d, 256d)]
    [InlineData(-2d, 256d)]
    public void Eval(double x, double expected)
    {
        Assert.Equal(expected, Power8.Eval(x), 6);
    }

    [Fact]
    public void Eval_Int()
    {
        Assert.Equal(256, Power8.Eval(2));
        Assert.Equal(256, Power8.Eval(-2));
    }

    [Fact]
    public void Eval_IsEvenFunction()
    {
        foreach (var x in new[] { 0.3d, 1.7d, 4.2d })
            Assert.Equal(Power8.Eval(x), Power8.Eval(-x), 6);
    }

    [Theory]
    [InlineData(0d, 0d)]
    [InlineData(2d, 1024d)]
    [InlineData(-2d, -1024d)]
    public void DerivativeEval(double x, double expected)
    {
        Assert.Equal(expected, Power8.DerivativeEval(x), 6);
    }
}
