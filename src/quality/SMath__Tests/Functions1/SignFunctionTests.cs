using Xunit;

namespace SMath.Functions1;

public class SignFunctionTests
{
    [Fact]
    public void IsOdd_NotEven()
    {
        Assert.True(SignFunction.IsOdd);
        Assert.False(SignFunction.IsEven);
    }

    [Fact]
    public void IsNotContinuous()
    {
        Assert.False(SignFunction.IsContinuous);
    }

    [Fact]
    public void PlainTextFormula()
    {
        Assert.Equal("-1, x < 0; 0, x = 0; 1, x > 0", SignFunction.PlainTextFormula);
    }

    [Fact]
    public void Image_IsMinusOneToOne()
    {
        var image = SignFunction.Image<double>();
        Assert.Equal(-1d, image.Min);
        Assert.Equal(1d, image.Max);
    }

    [Fact]
    public void GlobalExtremes_AreMinusOneAndOne()
    {
        Assert.Equal(1d, SignFunction.GlobalMaximum<double>());
        Assert.Equal(-1d, SignFunction.GlobalMinimum<double>());
    }

    [Theory]
    [InlineData(0d, 0d)]
    [InlineData(0.001d, 1d)]
    [InlineData(12.5d, 1d)]
    [InlineData(-0.001d, -1d)]
    [InlineData(-12.5d, -1d)]
    public void Eval(double x, double expected)
    {
        Assert.Equal(expected, SignFunction.Eval(x));
    }

    [Fact]
    public void Eval_Int()
    {
        Assert.Equal(1, SignFunction.Eval(7));
        Assert.Equal(0, SignFunction.Eval(0));
        Assert.Equal(-1, SignFunction.Eval(-7));
    }

    [Fact]
    public void Eval_IsOddFunction()
    {
        foreach (var x in new[] { 0.3d, 1.7d, 4.2d })
            Assert.Equal(-SignFunction.Eval(x), SignFunction.Eval(-x));
    }
}
