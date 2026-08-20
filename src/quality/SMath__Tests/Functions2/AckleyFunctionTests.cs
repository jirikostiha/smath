using Xunit;

namespace SMath.Functions2;

public class AckleyFunctionTests
{
    [Fact]
    public void PlainTextFormula_IsNotEmpty()
    {
        Assert.NotEmpty(AckleyFunction.PlainTextFormula);
    }

    [Fact]
    public void GlobalMinimumPoint_IsOrigin()
    {
        var point = AckleyFunction.GlobalMinimumPoint<double>();
        Assert.Equal(0d, point.X1);
        Assert.Equal(0d, point.X2);
    }

    [Fact]
    public void Eval_AtGlobalMinimumPoint_IsGlobalMinimum()
    {
        var point = AckleyFunction.GlobalMinimumPoint<double>();
        Assert.Equal(AckleyFunction.GlobalMinimum<double>(), AckleyFunction.Eval(point.X1, point.X2), 6);
    }

    [Theory]
    [InlineData(1d, 1d, 3.625385d)]
    [InlineData(-1d, -1d, 3.625385d)]
    [InlineData(2d, 0d, 4.927234d)]
    public void Eval(double x1, double x2, double expected)
    {
        Assert.Equal(expected, AckleyFunction.Eval(x1, x2), 6);
    }

    [Fact]
    public void Eval_IsSymmetricInArguments()
    {
        Assert.Equal(AckleyFunction.Eval(0.7d, -2.3d), AckleyFunction.Eval(-2.3d, 0.7d), 9);
    }

    [Fact]
    public void Eval_IsNotBelowGlobalMinimum()
    {
        for (var x = -3d; x <= 3d; x += 0.5d)
        {
            for (var y = -3d; y <= 3d; y += 0.5d)
                Assert.True(AckleyFunction.Eval(x, y) >= -1e-9d);
        }
    }
}
