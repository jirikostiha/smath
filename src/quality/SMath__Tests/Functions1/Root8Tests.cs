using Xunit;

namespace SMath.Functions1;

public class Root8Tests
{
    [Fact]
    public void Parity()
    {
        Assert.False(Root8.IsEven);
        Assert.False(Root8.IsOdd);
    }

    [Fact]
    public void IsContinuous()
    {
        Assert.True(Root8.IsContinuous);
    }

    [Fact]
    public void PlainTextFormula()
    {
        Assert.Equal("x^(1/8)", Root8.PlainTextFormula);
    }

    [Fact]
    public void Domain()
    {
        var domain = Root8.Domain<double>();
        Assert.Equal(0d, domain.Min);
        Assert.Equal(double.PositiveInfinity, domain.Max);
    }

    [Fact]
    public void NumberDomain()
    {
        var domain = Root8.NumberDomain<double>();
        Assert.Equal(0d, domain.Min);
        Assert.Equal(double.MaxValue, domain.Max);
    }

    [Fact]
    public void Image()
    {
        var image = Root8.Image<double>();
        Assert.Equal(0d, image.Min);
        Assert.Equal(double.PositiveInfinity, image.Max);
    }

    [Fact]
    public void NumberImage()
    {
        var image = Root8.NumberImage<double>();
        Assert.Equal(0d, image.Min);
        Assert.Equal(double.MaxValue, image.Max);
    }

    [Fact]
    public void GlobalExtremes()
    {
        Assert.Equal(double.PositiveInfinity, Root8.GlobalMaximum<double>());
        Assert.Equal(0d, Root8.GlobalMinimum<double>());
    }

    [Theory]
    [InlineData(0d, 0d)]
    [InlineData(1d, 1d)]
    [InlineData(256d, 2d)]
    public void Eval(double x, double expected)
    {
        Assert.Equal(expected, Root8.Eval(x), 6);
    }

    [Fact]
    public void Eval_IsInverseOfPower8()
    {
        foreach (var x in new[] { 0.5d, 1.3d, 2d, 7.9d })
            Assert.Equal(x, Root8.Eval(Power8.Eval(x)), 6);
    }

    [Fact]
    public void DerivativeEval()
    {
        Assert.Equal(1d / 1024d, Root8.DerivativeEval(256d), 9);
    }
}
