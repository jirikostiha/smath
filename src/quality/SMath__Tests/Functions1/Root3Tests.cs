using Xunit;

namespace SMath.Functions1;

public class Root3Tests
{
    [Fact]
    public void Parity()
    {
        Assert.False(Root3.IsEven);
        Assert.True(Root3.IsOdd);
    }

    [Fact]
    public void IsContinuous()
    {
        Assert.True(Root3.IsContinuous);
    }

    [Fact]
    public void PlainTextFormula()
    {
        Assert.Equal("x^(1/3)", Root3.PlainTextFormula);
    }

    [Fact]
    public void Domain()
    {
        var domain = Root3.Domain<double>();
        Assert.Equal(double.NegativeInfinity, domain.Min);
        Assert.Equal(double.PositiveInfinity, domain.Max);
    }

    [Fact]
    public void NumberDomain()
    {
        var domain = Root3.NumberDomain<double>();
        Assert.Equal(double.MinValue, domain.Min);
        Assert.Equal(double.MaxValue, domain.Max);
    }

    [Fact]
    public void Image()
    {
        var image = Root3.Image<double>();
        Assert.Equal(double.NegativeInfinity, image.Min);
        Assert.Equal(double.PositiveInfinity, image.Max);
    }

    [Fact]
    public void NumberImage()
    {
        var image = Root3.NumberImage<double>();
        Assert.Equal(double.MinValue, image.Min);
        Assert.Equal(double.MaxValue, image.Max);
    }

    [Fact]
    public void GlobalExtremes()
    {
        Assert.Equal(double.PositiveInfinity, Root3.GlobalMaximum<double>());
        Assert.Equal(double.NegativeInfinity, Root3.GlobalMinimum<double>());
    }

    [Theory]
    [InlineData(0d, 0d)]
    [InlineData(1d, 1d)]
    [InlineData(8d, 2d)]
    [InlineData(-1d, -1d)]
    public void Eval(double x, double expected)
    {
        Assert.Equal(expected, Root3.Eval(x), 6);
    }

    [Fact]
    public void Eval_IsInverseOfPower3()
    {
        foreach (var x in new[] { 0.5d, 1.3d, 2d, 7.9d })
            Assert.Equal(x, Root3.Eval(Power3.Eval(x)), 6);
    }

    [Fact]
    public void DerivativeEval()
    {
        Assert.Equal(1d / 12d, Root3.DerivativeEval(8d), 9);
    }
}
