using Xunit;

namespace SMath.Functions1;

public class Root2Tests
{
    [Fact]
    public void Parity()
    {
        Assert.False(Root2.IsEven);
        Assert.False(Root2.IsOdd);
    }

    [Fact]
    public void IsContinuous()
    {
        Assert.True(Root2.IsContinuous);
    }

    [Fact]
    public void PlainTextFormula()
    {
        Assert.Equal("x^(1/2)", Root2.PlainTextFormula);
    }

    [Fact]
    public void Domain()
    {
        var domain = Root2.Domain<double>();
        Assert.Equal(0d, domain.Min);
        Assert.Equal(double.PositiveInfinity, domain.Max);
    }

    [Fact]
    public void NumberDomain()
    {
        var domain = Root2.NumberDomain<double>();
        Assert.Equal(0d, domain.Min);
        Assert.Equal(double.MaxValue, domain.Max);
    }

    [Fact]
    public void Image()
    {
        var image = Root2.Image<double>();
        Assert.Equal(0d, image.Min);
        Assert.Equal(double.PositiveInfinity, image.Max);
    }

    [Fact]
    public void NumberImage()
    {
        var image = Root2.NumberImage<double>();
        Assert.Equal(0d, image.Min);
        Assert.Equal(double.MaxValue, image.Max);
    }

    [Fact]
    public void GlobalExtremes()
    {
        Assert.Equal(double.PositiveInfinity, Root2.GlobalMaximum<double>());
        Assert.Equal(0d, Root2.GlobalMinimum<double>());
    }

    [Theory]
    [InlineData(0d, 0d)]
    [InlineData(1d, 1d)]
    [InlineData(4d, 2d)]
    public void Eval(double x, double expected)
    {
        Assert.Equal(expected, Root2.Eval(x), 6);
    }

    [Fact]
    public void Eval_IsInverseOfPower2()
    {
        foreach (var x in new[] { 0.5d, 1.3d, 2d, 7.9d })
            Assert.Equal(x, Root2.Eval(Power2.Eval(x)), 6);
    }

    [Fact]
    public void DerivativeEval()
    {
        Assert.Equal(0.25d, Root2.DerivativeEval(4d), 9);
    }
}
