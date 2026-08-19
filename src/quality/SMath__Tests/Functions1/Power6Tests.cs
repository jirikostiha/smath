using Xunit;

namespace SMath.Functions1;

public class Power6Tests
{
    [Fact]
    public void Parity()
    {
        Assert.True(Power6.IsEven);
        Assert.False(Power6.IsOdd);
    }

    [Fact]
    public void IsContinuous()
    {
        Assert.True(Power6.IsContinuous);
    }

    [Fact]
    public void PlainTextFormula_IsXPow6()
    {
        Assert.Equal("x^6", Power6.PlainTextFormula);
    }

    [Fact]
    public void Domain_IsFullRange()
    {
        var domain = Power6.Domain<double>();
        Assert.Equal(double.NegativeInfinity, domain.Min);
        Assert.Equal(double.PositiveInfinity, domain.Max);
    }

    [Fact]
    public void NumberDomain_IsFullRange()
    {
        var domain = Power6.NumberDomain<double>();
        Assert.Equal(double.MinValue, domain.Min);
        Assert.Equal(double.MaxValue, domain.Max);
    }

    [Fact]
    public void Image()
    {
        var image = Power6.Image<double>();
        Assert.Equal(0d, image.Min);
        Assert.Equal(double.PositiveInfinity, image.Max);
    }

    [Fact]
    public void NumberImage()
    {
        var image = Power6.NumberImage<double>();
        Assert.Equal(0d, image.Min);
        Assert.Equal(double.MaxValue, image.Max);
    }

    [Fact]
    public void GlobalExtremes()
    {
        Assert.Equal(double.PositiveInfinity, Power6.GlobalMaximum<double>());
        Assert.Equal(0d, Power6.GlobalMinimum<double>());
    }

    [Theory]
    [InlineData(0d, 0d)]
    [InlineData(1d, 1d)]
    [InlineData(-1d, 1d)]
    [InlineData(2d, 64d)]
    [InlineData(-2d, 64d)]
    public void Eval(double x, double expected)
    {
        Assert.Equal(expected, Power6.Eval(x), 6);
    }

    [Fact]
    public void Eval_Int()
    {
        Assert.Equal(64, Power6.Eval(2));
        Assert.Equal(64, Power6.Eval(-2));
    }

    [Fact]
    public void Eval_IsEvenFunction()
    {
        foreach (var x in new[] { 0.3d, 1.7d, 4.2d })
            Assert.Equal(Power6.Eval(x), Power6.Eval(-x), 6);
    }

    [Theory]
    [InlineData(0d, 0d)]
    [InlineData(2d, 192d)]
    [InlineData(-2d, -192d)]
    public void DerivativeEval(double x, double expected)
    {
        Assert.Equal(expected, Power6.DerivativeEval(x), 6);
    }
}
