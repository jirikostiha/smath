using Xunit;

namespace SMath.Functions1;

public class Root7Tests
{
    [Fact]
    public void Parity()
    {
        Assert.False(Root7.IsEven);
        Assert.True(Root7.IsOdd);
    }

    [Fact]
    public void IsContinuous()
    {
        Assert.True(Root7.IsContinuous);
    }

    [Fact]
    public void PlainTextFormula()
    {
        Assert.Equal("x^(1/7)", Root7.PlainTextFormula);
    }

    [Fact]
    public void Domain()
    {
        var domain = Root7.Domain<double>();
        Assert.Equal(double.NegativeInfinity, domain.Min);
        Assert.Equal(double.PositiveInfinity, domain.Max);
    }

    [Fact]
    public void NumberDomain()
    {
        var domain = Root7.NumberDomain<double>();
        Assert.Equal(double.MinValue, domain.Min);
        Assert.Equal(double.MaxValue, domain.Max);
    }

    [Fact]
    public void Image()
    {
        var image = Root7.Image<double>();
        Assert.Equal(double.NegativeInfinity, image.Min);
        Assert.Equal(double.PositiveInfinity, image.Max);
    }

    [Fact]
    public void NumberImage()
    {
        var image = Root7.NumberImage<double>();
        Assert.Equal(double.MinValue, image.Min);
        Assert.Equal(double.MaxValue, image.Max);
    }

    [Fact]
    public void GlobalExtremes()
    {
        Assert.Equal(double.PositiveInfinity, Root7.GlobalMaximum<double>());
        Assert.Equal(double.NegativeInfinity, Root7.GlobalMinimum<double>());
    }

    [Theory]
    [InlineData(0d, 0d)]
    [InlineData(1d, 1d)]
    [InlineData(128d, 2d)]
    [InlineData(-1d, -1d)]
    public void Eval(double x, double expected)
    {
        Assert.Equal(expected, Root7.Eval(x), 6);
    }

    [Fact]
    public void Eval_IsInverseOfPower7()
    {
        foreach (var x in new[] { 0.5d, 1.3d, 2d, 7.9d })
            Assert.Equal(x, Root7.Eval(Power7.Eval(x)), 6);
    }

    [Fact]
    public void DerivativeEval()
    {
        Assert.Equal(1d / 448d, Root7.DerivativeEval(128d), 9);
    }
}
