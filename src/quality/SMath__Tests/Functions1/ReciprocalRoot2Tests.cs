using Xunit;

namespace SMath.Functions1;

public class ReciprocalRoot2Tests
{
    [Fact]
    public void Parity()
    {
        Assert.False(ReciprocalRoot2.IsEven);
        Assert.False(ReciprocalRoot2.IsOdd);
    }

    [Fact]
    public void PlainTextFormula()
    {
        Assert.Equal("1/x^(1/2)", ReciprocalRoot2.PlainTextFormula);
    }

    [Fact]
    public void Domain_IsPositive()
    {
        var domain = ReciprocalRoot2.Domain<double>();
        Assert.Equal(0d, domain.Min);
        Assert.Equal(double.PositiveInfinity, domain.Max);
    }

    [Fact]
    public void Image_IsPositive()
    {
        var image = ReciprocalRoot2.Image<double>();
        Assert.Equal(0d, image.Min);
        Assert.Equal(double.PositiveInfinity, image.Max);
    }

    [Theory]
    [InlineData(1d, 1d)]
    [InlineData(4d, 0.5d)]
    [InlineData(16d, 0.25d)]
    [InlineData(0.25d, 2d)]
    public void Eval(double x, double expected)
    {
        Assert.Equal(expected, ReciprocalRoot2.Eval(x), 6);
    }

    [Fact]
    public void Eval_IsReciprocalOfRoot2()
    {
        foreach (var x in new[] { 0.3d, 1.7d, 4.2d })
            Assert.Equal(1d / Root2.Eval(x), ReciprocalRoot2.Eval(x), 6);
    }

    [Fact]
    public void DerivativeEval()
    {
        Assert.Equal(-1d / 16d, ReciprocalRoot2.DerivativeEval(4d), 9);
    }
}
