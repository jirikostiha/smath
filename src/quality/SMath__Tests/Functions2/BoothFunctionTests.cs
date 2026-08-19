using Xunit;

namespace SMath.Functions2;

public class BoothFunctionTests
{
    [Fact]
    public void PlainTextFormula()
    {
        Assert.Equal("(x1 + 2*x2 - 7)^2 + (2*x1 + x2 - 5)^2", BoothFunction.PlainTextFormula);
    }

    [Fact]
    public void GlobalMinimumPoint_IsOneAndThree()
    {
        var point = BoothFunction.GlobalMinimumPoint<double>();
        Assert.Equal(1d, point.X1);
        Assert.Equal(3d, point.X2);
    }

    [Fact]
    public void Eval_AtGlobalMinimumPoint_IsGlobalMinimum()
    {
        var point = BoothFunction.GlobalMinimumPoint<double>();
        Assert.Equal(BoothFunction.GlobalMinimum<double>(), BoothFunction.Eval(point.X1, point.X2), 6);
    }

    [Theory]
    [InlineData(0d, 0d, 74d)]
    [InlineData(1d, 3d, 0d)]
    [InlineData(2d, 2d, 2d)]
    [InlineData(-1d, 0d, 113d)]
    public void Eval(double x1, double x2, double expected)
    {
        Assert.Equal(expected, BoothFunction.Eval(x1, x2), 6);
    }

    [Fact]
    public void Eval_Int()
    {
        Assert.Equal(0, BoothFunction.Eval(1, 3));
    }

    [Fact]
    public void Eval_IsNotBelowGlobalMinimum()
    {
        for (var x = -5d; x <= 5d; x += 0.5d)
        {
            for (var y = -5d; y <= 5d; y += 0.5d)
                Assert.True(BoothFunction.Eval(x, y) >= 0d);
        }
    }
}
