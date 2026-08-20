using Xunit;

namespace SMath.Functions1;

public class Root4Tests
{
    [Fact]
    public void Parity()
    {
        Assert.False(Root4.IsEven);
        Assert.False(Root4.IsOdd);
    }

    [Fact]
    public void IsContinuous()
    {
        Assert.True(Root4.IsContinuous);
    }

    [Fact]
    public void PlainTextFormula()
    {
        Assert.Equal("x^(1/4)", Root4.PlainTextFormula);
    }

    [Fact]
    public void Domain()
    {
        var domain = Root4.Domain<double>();
        Assert.Equal(0d, domain.Min);
        Assert.Equal(double.PositiveInfinity, domain.Max);
    }

    [Fact]
    public void NumberDomain()
    {
        var domain = Root4.NumberDomain<double>();
        Assert.Equal(0d, domain.Min);
        Assert.Equal(double.MaxValue, domain.Max);
    }

    [Fact]
    public void Image()
    {
        var image = Root4.Image<double>();
        Assert.Equal(0d, image.Min);
        Assert.Equal(double.PositiveInfinity, image.Max);
    }

    [Fact]
    public void NumberImage()
    {
        var image = Root4.NumberImage<double>();
        Assert.Equal(0d, image.Min);
        Assert.Equal(double.MaxValue, image.Max);
    }

    [Fact]
    public void GlobalExtremes()
    {
        Assert.Equal(double.PositiveInfinity, Root4.GlobalMaximum<double>());
        Assert.Equal(0d, Root4.GlobalMinimum<double>());
    }

    [Theory]
    [InlineData(0d, 0d)]
    [InlineData(1d, 1d)]
    [InlineData(16d, 2d)]
    public void Eval(double x, double expected)
    {
        Assert.Equal(expected, Root4.Eval(x), 6);
    }

    [Fact]
    public void Eval_IsInverseOfPower4()
    {
        foreach (var x in new[] { 0.5d, 1.3d, 2d, 7.9d })
            Assert.Equal(x, Root4.Eval(Power4.Eval(x)), 6);
    }

    [Fact]
    public void DerivativeEval()
    {
        Assert.Equal(1d / 32d, Root4.DerivativeEval(16d), 9);
    }
}
