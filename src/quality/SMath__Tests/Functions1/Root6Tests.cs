using Xunit;

namespace SMath.Functions1;

public class Root6Tests
{
    [Fact]
    public void Parity()
    {
        Assert.False(Root6.IsEven);
        Assert.False(Root6.IsOdd);
    }

    [Fact]
    public void IsContinuous()
    {
        Assert.True(Root6.IsContinuous);
    }

    [Fact]
    public void PlainTextFormula()
    {
        Assert.Equal("x^(1/6)", Root6.PlainTextFormula);
    }

    [Fact]
    public void Domain()
    {
        var domain = Root6.Domain<double>();
        Assert.Equal(0d, domain.Min);
        Assert.Equal(double.PositiveInfinity, domain.Max);
    }

    [Fact]
    public void NumberDomain()
    {
        var domain = Root6.NumberDomain<double>();
        Assert.Equal(0d, domain.Min);
        Assert.Equal(double.MaxValue, domain.Max);
    }

    [Fact]
    public void Image()
    {
        var image = Root6.Image<double>();
        Assert.Equal(0d, image.Min);
        Assert.Equal(double.PositiveInfinity, image.Max);
    }

    [Fact]
    public void NumberImage()
    {
        var image = Root6.NumberImage<double>();
        Assert.Equal(0d, image.Min);
        Assert.Equal(double.MaxValue, image.Max);
    }

    [Fact]
    public void GlobalExtremes()
    {
        Assert.Equal(double.PositiveInfinity, Root6.GlobalMaximum<double>());
        Assert.Equal(0d, Root6.GlobalMinimum<double>());
    }

    [Theory]
    [InlineData(0d, 0d)]
    [InlineData(1d, 1d)]
    [InlineData(64d, 2d)]
    public void Eval(double x, double expected)
    {
        Assert.Equal(expected, Root6.Eval(x), 6);
    }

    [Fact]
    public void Eval_IsInverseOfPower6()
    {
        foreach (var x in new[] { 0.5d, 1.3d, 2d, 7.9d })
            Assert.Equal(x, Root6.Eval(Power6.Eval(x)), 6);
    }

    [Fact]
    public void DerivativeEval()
    {
        Assert.Equal(1d / 192d, Root6.DerivativeEval(64d), 9);
    }
}
