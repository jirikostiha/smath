using SMath.Geometry2D;
using Xunit;

namespace SMath;

public class InterpolationTests
{
    private const double Tolerance = 1e-10;

    [Theory]
    [InlineData(0d, 10d, 0d, 0d)]
    [InlineData(0d, 10d, 0.5d, 5d)]
    [InlineData(0d, 10d, 1d, 10d)]
    [InlineData(0d, 10d, 2d, 20d)]
    [InlineData(-5d, 5d, 0.5d, 0d)]
    public void Linear_Eval(double from, double to, double t, double expected)
        => Assert.Equal(expected, Interpolation.Linear.Eval(from, to, t), Tolerance);

    [Fact]
    public void Linear_Point()
        => Assert.Equal((1d, 2d), Interpolation.Linear.Point((0d, 0d), (2d, 4d), 0.5d));

    [Fact]
    public void Linear_Parameter_IsInverseOfEval()
    {
        var value = Interpolation.Linear.Eval(2d, 8d, 0.25d);

        Assert.Equal(0.25d, Interpolation.Linear.Parameter(2d, 8d, value), Tolerance);
    }

    [Fact]
    public void Linear_Remap()
        => Assert.Equal(50d, Interpolation.Linear.Remap(0d, 10d, 0d, 100d, 5d), Tolerance);

    [Theory]
    [InlineData(0d, 0d, 1d)]
    [InlineData(1d, 0d, 2d)]
    [InlineData(0d, 1d, 3d)]
    [InlineData(1d, 1d, 4d)]
    [InlineData(0.5d, 0.5d, 2.5d)]
    public void Bilinear_Eval(double tx, double ty, double expected)
        => Assert.Equal(expected, Interpolation.Bilinear.Eval(1d, 2d, 3d, 4d, tx, ty), Tolerance);

    [Fact]
    public void Angular_Difference_TakesTheShorterWay()
    {
        Assert.Equal(-0.2d, Interpolation.Angular.Difference(0.1d, -0.1d), Tolerance);
        // from 0.1 rad below the full turn to 0.1 rad over it, the shorter way is forward
        Assert.Equal(0.2d, Interpolation.Angular.Difference(double.Tau - 0.1d, 0.1d), Tolerance);
    }

    [Fact]
    public void Angular_Eval_CrossesZero()
    {
        var angle = Interpolation.Angular.Eval(double.Tau - 0.1d, 0.1d, 0.5d);

        Assert.Equal(double.Tau, angle, Tolerance);
    }

    [Theory]
    [InlineData(0d, 0d)]
    [InlineData(1d, 1d)]
    [InlineData(-1d, 5.283185307179586d)]
    public void Angular_Wrapped_IsInFullTurn(double angle, double expected)
        => Assert.Equal(expected, Interpolation.Angular.Wrapped(angle), Tolerance);

    [Fact]
    public void Angular_Towards_StopsAtTarget()
    {
        Assert.Equal(0.5d, Interpolation.Angular.Towards(0d, 0.5d, 1d), Tolerance);
        Assert.Equal(1d, Interpolation.Angular.Towards(0d, 3d, 1d), Tolerance);
        Assert.Equal(-1d, Interpolation.Angular.Towards(0d, -3d, 1d), Tolerance);
    }

    [Theory]
    [InlineData(-1d, 0d)]
    [InlineData(0d, 0d)]
    [InlineData(0.5d, 0.5d)]
    [InlineData(1d, 1d)]
    [InlineData(2d, 1d)]
    public void Easing_SmoothStep_KeepsEnds(double t, double expected)
        => Assert.Equal(expected, Interpolation.Easing.SmoothStep(t), Tolerance);

    [Theory]
    [InlineData(-1d, 0d)]
    [InlineData(0d, 0d)]
    [InlineData(0.5d, 0.5d)]
    [InlineData(1d, 1d)]
    [InlineData(2d, 1d)]
    public void Easing_SmootherStep_KeepsEnds(double t, double expected)
        => Assert.Equal(expected, Interpolation.Easing.SmootherStep(t), Tolerance);

    [Fact]
    public void Easing_Quadratic_KeepsEnds()
    {
        Assert.Equal(0d, Interpolation.Easing.QuadraticIn(0d), Tolerance);
        Assert.Equal(1d, Interpolation.Easing.QuadraticIn(1d), Tolerance);
        Assert.Equal(0d, Interpolation.Easing.QuadraticOut(0d), Tolerance);
        Assert.Equal(1d, Interpolation.Easing.QuadraticOut(1d), Tolerance);
    }

    [Fact]
    public void Easing_SineInOut_KeepsEnds()
    {
        Assert.Equal(0d, Interpolation.Easing.SineInOut(0d), Tolerance);
        Assert.Equal(0.5d, Interpolation.Easing.SineInOut(0.5d), Tolerance);
        Assert.Equal(1d, Interpolation.Easing.SineInOut(1d), Tolerance);
    }

    [Fact]
    public void Easing_ExponentialDecay_IsFrameRateIndependent()
    {
        // two half steps must close the same fraction as one whole step
        var whole = Interpolation.Easing.ExponentialDecay(0.5d, 1d);

        var remaining = 1d - Interpolation.Easing.ExponentialDecay(0.5d, 0.5d);
        var half = 1d - (remaining * (1d - Interpolation.Easing.ExponentialDecay(0.5d, 0.5d)));

        Assert.Equal(whole, half, Tolerance);
    }

    [Fact]
    public void Curve_QuadraticBezier_KeepsEnds()
    {
        Assert.Equal((0d, 0d), Interpolation.Curve.QuadraticBezier((0d, 0d), (1d, 2d), (2d, 0d), 0d));
        Assert.Equal((2d, 0d), Interpolation.Curve.QuadraticBezier((0d, 0d), (1d, 2d), (2d, 0d), 1d));
        Assert.Equal((1d, 1d), Interpolation.Curve.QuadraticBezier((0d, 0d), (1d, 2d), (2d, 0d), 0.5d));
    }

    [Fact]
    public void Curve_CubicBezier_KeepsEnds()
    {
        Assert.Equal((0d, 0d), Interpolation.Curve.CubicBezier((0d, 0d), (0d, 1d), (1d, 1d), (1d, 0d), 0d));
        Assert.Equal((1d, 0d), Interpolation.Curve.CubicBezier((0d, 0d), (0d, 1d), (1d, 1d), (1d, 0d), 1d));
    }

    [Fact]
    public void Curve_CatmullRom_PassesThroughInnerPoints()
    {
        var point1 = (1d, 1d);
        var point2 = (2d, 3d);

        Assert.Equal(point1, Interpolation.Curve.CatmullRom((0d, 0d), point1, point2, (3d, 0d), 0d));
        Assert.Equal(point2, Interpolation.Curve.CatmullRom((0d, 0d), point1, point2, (3d, 0d), 1d));
    }
}
