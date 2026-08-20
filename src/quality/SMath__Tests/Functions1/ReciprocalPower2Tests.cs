using Xunit;

namespace SMath.Functions1;

public class ReciprocalPower2Tests
{
    [Fact]
    public void IsEven_NotOdd()
    {
        Assert.True(ReciprocalPower2.IsEven);
        Assert.False(ReciprocalPower2.IsOdd);
    }

    [Fact]
    public void IsNotContinuous()
    {
        Assert.False(ReciprocalPower2.IsContinuous);
    }

    [Fact]
    public void PlainTextFormula()
    {
        Assert.Equal("1/x^2", ReciprocalPower2.PlainTextFormula);
    }

    [Fact]
    public void Image_IsPositive()
    {
        var image = ReciprocalPower2.Image<double>();
        Assert.Equal(0d, image.Min);
        Assert.Equal(double.PositiveInfinity, image.Max);
    }

    [Theory]
    [InlineData(1d, 1d)]
    [InlineData(-1d, 1d)]
    [InlineData(2d, 0.25d)]
    [InlineData(-2d, 0.25d)]
    [InlineData(0.5d, 4d)]
    public void Eval(double x, double expected)
    {
        Assert.Equal(expected, ReciprocalPower2.Eval(x), 6);
    }

    [Fact]
    public void Eval_IsEvenFunction()
    {
        foreach (var x in new[] { 0.3d, 1.7d, 4.2d })
            Assert.Equal(ReciprocalPower2.Eval(x), ReciprocalPower2.Eval(-x), 6);
    }

    [Theory]
    [InlineData(1d, -2d)]
    [InlineData(2d, -0.25d)]
    [InlineData(-1d, 2d)]
    public void DerivativeEval(double x, double expected)
    {
        Assert.Equal(expected, ReciprocalPower2.DerivativeEval(x), 6);
    }
}
