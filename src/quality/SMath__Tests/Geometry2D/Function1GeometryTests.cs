using SMath.Functions1;
using Xunit;

namespace SMath.Geometry2D;

public class Function1GeometryTests
{
    [Theory]
    [InlineData(0, 1)]
    [InlineData(double.Pi / 2, 0)]
    [InlineData(double.Pi, -1)]
    [InlineData(double.Pi * 1.5, 0)]
    public void Sine_SlopeInPoint(double x, double slope)
    {
        Assert.Equal(slope, Function1Geometry.TangentLine.Slope.FromX(Sine.DerivativeEval(x)), 6);
    }

    [Theory]
    [InlineData(0, -1, 1, 0)]
    [InlineData(double.Pi / 2, 0, 1, -1)]
    [InlineData(double.Pi, 1, 1, -double.Pi)]
    public void Sine_TangentLineInPoint(double x, double a, double b, double c)
    {
        var tl = Function1Geometry.TangentLine.FromX(x, Sine.Eval(x), Sine.DerivativeEval(x));
        Assert.Equal(a, tl.A, 6);
        Assert.Equal(b, tl.B, 6);
        Assert.Equal(c, tl.C, 6);
    }

    [Theory]
    [InlineData(0d, 1, 0, 0)]  // y-axis
    [InlineData(1, 0.5, 1, -1.5)]
    [InlineData(-1, -0.5, 1, -1.5)]
    public void Power2_NormalLineInPoint(double x, double a, double b, double c)
    {
        var tl = Function1Geometry.NormalLine.FromX(x, Power2.Eval(x),
            Function1Geometry.NormalLine.Slope.FromX(Power2.DerivativeEval(x)));

        Assert.Equal(a, tl.A, 6);
        Assert.Equal(b, tl.B, 6);
        Assert.Equal(c, tl.C, 6);
    }

    [Fact]
    public void Sine_NormalLineAtOrigin_FiniteSlope()
    {
        // Regression: the vertical-line branch was keyed on 'x == 0' instead of
        // an infinite slope. At x == 0 the sine normal has a finite slope (-1),
        // so the result must be the real normal line y = -x  ->  (1, 1, 0),
        // not the y-axis (1, 0, 0).
        var nl = Function1Geometry.NormalLine.FromX(0d, Sine.Eval(0d),
            Function1Geometry.NormalLine.Slope.FromX(Sine.DerivativeEval(0d)));

        Assert.Equal(1d, nl.A, 6);
        Assert.Equal(1d, nl.B, 6);
        Assert.Equal(0d, nl.C, 6);
    }

    [Fact]
    public void NormalLine_VerticalAtNonZeroX_UsesPointX()
    {
        // Regression: a vertical normal (infinite slope) at x != 0 must produce
        // the vertical line X = x  ->  (1, 0, -x), not the hardcoded (1, 0, 0).
        var nl = Function1Geometry.NormalLine.FromX(3d, 9d, double.NegativeInfinity);

        Assert.Equal(1d, nl.A, 6);
        Assert.Equal(0d, nl.B, 6);
        Assert.Equal(-3d, nl.C, 6);
    }
}
