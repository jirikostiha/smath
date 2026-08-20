using Xunit;

namespace SMath.Functions1;

public class Root5Tests
{
    [Fact]
    public void Parity()
    {
        Assert.False(Root5.IsEven);
        Assert.True(Root5.IsOdd);
    }

    [Fact]
    public void IsContinuous()
    {
        Assert.True(Root5.IsContinuous);
    }

    [Fact]
    public void PlainTextFormula()
    {
        Assert.Equal("x^(1/5)", Root5.PlainTextFormula);
    }

    [Fact]
    public void Domain()
    {
        var domain = Root5.Domain<double>();
        Assert.Equal(double.NegativeInfinity, domain.Min);
        Assert.Equal(double.PositiveInfinity, domain.Max);
    }

    [Fact]
    public void NumberDomain()
    {
        var domain = Root5.NumberDomain<double>();
        Assert.Equal(double.MinValue, domain.Min);
        Assert.Equal(double.MaxValue, domain.Max);
    }

    [Fact]
    public void Image()
    {
        var image = Root5.Image<double>();
        Assert.Equal(double.NegativeInfinity, image.Min);
        Assert.Equal(double.PositiveInfinity, image.Max);
    }

    [Fact]
    public void NumberImage()
    {
        var image = Root5.NumberImage<double>();
        Assert.Equal(double.MinValue, image.Min);
        Assert.Equal(double.MaxValue, image.Max);
    }

    [Fact]
    public void GlobalExtremes()
    {
        Assert.Equal(double.PositiveInfinity, Root5.GlobalMaximum<double>());
        Assert.Equal(double.NegativeInfinity, Root5.GlobalMinimum<double>());
    }

    [Theory]
    [InlineData(0d, 0d)]
    [InlineData(1d, 1d)]
    [InlineData(32d, 2d)]
    [InlineData(-1d, -1d)]
    public void Eval(double x, double expected)
    {
        Assert.Equal(expected, Root5.Eval(x), 6);
    }

    [Fact]
    public void Eval_IsInverseOfPower5()
    {
        foreach (var x in new[] { 0.5d, 1.3d, 2d, 7.9d })
            Assert.Equal(x, Root5.Eval(Power5.Eval(x)), 6);
    }

    [Fact]
    public void DerivativeEval()
    {
        Assert.Equal(1d / 80d, Root5.DerivativeEval(32d), 9);
    }
}
