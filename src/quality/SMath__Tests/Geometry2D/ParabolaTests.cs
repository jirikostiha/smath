using Xunit;

namespace SMath.Geometry2D;

public class ParabolaTests
{
    [Theory]
    [InlineData(1d, 0d, 0d)]
    [InlineData(1d, 2d, 1d)]     // 2^2 / (4*1)
    [InlineData(2d, 4d, 2d)]     // 4^2 / (4*2)
    [InlineData(0.5d, 1d, 0.5d)] // 1^2 / (4*0.5)
    public void Point_YFromX(double focalLength, double x, double y)
    {
        Assert.Equal(y, Parabola.Point.YFromX(focalLength, x), 9);
    }

    [Theory]
    [InlineData(1d, 2d)]
    [InlineData(3d, -4d)]
    public void Point_FromX(double focalLength, double x)
    {
        var point = Parabola.Point.FromX(focalLength, x);

        Assert.Equal(x, point.X);
        Assert.Equal(Parabola.Point.YFromX(focalLength, x), point.Y);
    }

    [Theory]
    [InlineData(1d)]
    [InlineData(2.5d)]
    public void Focus_FromFocalLength(double focalLength)
    {
        var focus = Parabola.Focus.FromFocalLength(focalLength);

        Assert.Equal(0d, focus.X);
        Assert.Equal(focalLength, focus.Y);
    }

    [Theory]
    [InlineData(1d)]
    [InlineData(3d)]
    public void Directrix_IsHorizontalLineBelowVertex(double focalLength)
    {
        var (a, b, c) = Parabola.Directrix.FromFocalLength(focalLength);

        // 0*x + 1*y + f = 0  =>  y = -f
        Assert.Equal(0d, a);
        Assert.Equal(1d, b);
        Assert.Equal(focalLength, c);
    }

    [Theory]
    [InlineData(1d, 2d)]
    [InlineData(2d, 5d)]
    public void Focus_And_Directrix_AreEquidistantFromParabolaPoint(double focalLength, double x)
    {
        // definition of a parabola: distance to focus == distance to directrix
        var point = Parabola.Point.FromX(focalLength, x);
        var focus = Parabola.Focus.FromFocalLength(focalLength);

        var toFocus = PT.Hypotenuse(point.X - focus.X, point.Y - focus.Y);
        // directrix is y = -f, distance from a point above it is y + f
        var toDirectrix = point.Y + focalLength;

        Assert.Equal(toDirectrix, toFocus, 9);
    }

    [Theory]
    [InlineData(1d)]
    [InlineData(2d)]
    public void SemiLatusRectum_Length_IsTwiceFocalLength(double focalLength)
    {
        Assert.Equal(2d * focalLength, Parabola.SemiLatusRectum.Length.FromFocalLength(focalLength), 9);
    }

    [Theory]
    [InlineData(1d, 0d, 0d)]
    [InlineData(1d, 2d, 1d)]      // x / (2*f)
    [InlineData(2d, 4d, 1d)]
    [InlineData(0.5d, -1d, -1d)]
    public void TangentLine_Slope_FromX(double focalLength, double x, double slope)
    {
        Assert.Equal(slope, Parabola.TangentLine.Slope.FromX(focalLength, x), 9);
    }

    [Theory]
    [InlineData(1d, 2d)]
    [InlineData(2d, -3d)]
    public void TangentLine_PassesThroughPointWithTangentSlope(double focalLength, double x)
    {
        var (a, b, c) = Parabola.TangentLine.FromX(focalLength, x);
        var point = Parabola.Point.FromX(focalLength, x);
        var slope = Parabola.TangentLine.Slope.FromX(focalLength, x);

        // the point lies on the line: a*x + b*y + c == 0
        Assert.Equal(0d, a * point.X + b * point.Y + c, 9);
        // general-form slope is -a/b
        Assert.Equal(slope, -a / b, 9);
    }

    [Theory]
    [InlineData(1d, 4d, -0.5d)] // -1 / (x/(2f)) = -1 / 2
    [InlineData(2d, 4d, -1d)]   // -1 / (4/(2*2)) = -1 / 1
    public void NormalLine_Slope_FromX(double focalLength, double x, double slope)
    {
        Assert.Equal(slope, Parabola.NormalLine.Slope.FromX(focalLength, x), 9);
    }

    [Theory]
    [InlineData(1d)]
    [InlineData(2d)]
    public void NormalLine_AtVertex_IsAxisOfSymmetry(double focalLength)
    {
        // at the vertex the tangent is horizontal, so the normal is the vertical line x = 0
        var (a, b, c) = Parabola.NormalLine.FromX(focalLength, 0d);

        Assert.Equal(1d, a);
        Assert.Equal(0d, b);
        Assert.Equal(0d, c);
    }

    [Fact]
    public void NormalLine_AtVertex_DecimalDoesNotThrow()
    {
        var (a, b, c) = Parabola.NormalLine.FromX(1m, 0m);

        Assert.Equal(1m, a);
        Assert.Equal(0m, b);
        Assert.Equal(0m, c);
    }

    [Theory]
    [InlineData(1d, 2d)]
    [InlineData(2d, -3d)]
    public void NormalLine_PassesThroughPointAndIsPerpendicularToTangent(double focalLength, double x)
    {
        var (a, b, c) = Parabola.NormalLine.FromX(focalLength, x);
        var point = Parabola.Point.FromX(focalLength, x);
        var tangentSlope = Parabola.TangentLine.Slope.FromX(focalLength, x);
        var normalSlope = -a / b;

        // the point lies on the normal line
        Assert.Equal(0d, a * point.X + b * point.Y + c, 9);
        // tangent and normal are perpendicular
        Assert.Equal(-1d, tangentSlope * normalSlope, 9);
    }
}
