using Xunit;

namespace SMath.Functions1;

public class Power4Tests
{
    [Fact]
    public void Parity()
    {
        Assert.True(Power4.IsEven);
        Assert.False(Power4.IsOdd);
    }

    [Fact]
    public void IsContinuous()
    {
        Assert.True(Power4.IsContinuous);
    }

    [Fact]
    public void PlainTextFormula_IsXPow4()
    {
        Assert.Equal("x^4", Power4.PlainTextFormula);
    }

    [Fact]
    public void Domain_IsFullRange()
    {
        var domain = Power4.Domain<double>();
        Assert.Equal(double.NegativeInfinity, domain.Min);
        Assert.Equal(double.PositiveInfinity, domain.Max);
    }

    [Fact]
    public void NumberDomain_IsFullRange()
    {
        var domain = Power4.NumberDomain<double>();
        Assert.Equal(double.MinValue, domain.Min);
        Assert.Equal(double.MaxValue, domain.Max);
    }

    [Fact]
    public void Image()
    {
        var image = Power4.Image<double>();
        Assert.Equal(0d, image.Min);
        Assert.Equal(double.PositiveInfinity, image.Max);
    }

    [Fact]
    public void NumberImage()
    {
        var image = Power4.NumberImage<double>();
        Assert.Equal(0d, image.Min);
        Assert.Equal(double.MaxValue, image.Max);
    }

    [Fact]
    public void GlobalExtremes()
    {
        Assert.Equal(double.PositiveInfinity, Power4.GlobalMaximum<double>());
        Assert.Equal(0d, Power4.GlobalMinimum<double>());
    }

    [Theory]
    [InlineData(0d, 0d)]
    [InlineData(1d, 1d)]
    [InlineData(-1d, 1d)]
    [InlineData(2d, 16d)]
    [InlineData(-2d, 16d)]
    public void Eval(double x, double expected)
    {
        Assert.Equal(expected, Power4.Eval(x), 6);
    }

    [Fact]
    public void Eval_Int()
    {
        Assert.Equal(16, Power4.Eval(2));
        Assert.Equal(16, Power4.Eval(-2));
    }

    [Fact]
    public void Eval_IsEvenFunction()
    {
        foreach (var x in new[] { 0.3d, 1.7d, 4.2d })
            Assert.Equal(Power4.Eval(x), Power4.Eval(-x), 6);
    }

    [Theory]
    [InlineData(0d, 0d)]
    [InlineData(2d, 32d)]
    [InlineData(-2d, -32d)]
    public void DerivativeEval(double x, double expected)
    {
        Assert.Equal(expected, Power4.DerivativeEval(x), 6);
    }
}
