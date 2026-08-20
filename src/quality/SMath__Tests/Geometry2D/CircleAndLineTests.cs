using Xunit;

namespace SMath.Geometry2D;

public class CircleAndLineTests
{
    [Theory]
    [InlineData(1, 0, 1, -2, 1)] // line y = 2, above the circle
    [InlineData(1, 1, 0, 0, 0)]  // y-axis, secant through the center
    [InlineData(1, 0, 1, -1, 0)] // line y = 1, tangent
    [InlineData(1, 1, 0, -3, 2)] // line x = 3, right of the circle
    public void Perimeter_Line_Distance(double radius, double a, double b, double c, double expected)
        => Assert.Equal(expected, Circle.Perimeter.And.Line.Distance.FromRadius(radius, (a, b, c)), 6);

    [Fact]
    public void Perimeter_Line_Distance_WithCenter()
        // circle centered at (5, 0), line x = 0 -> distance 4
        => Assert.Equal(4d, Circle.Perimeter.And.Line.Distance.FromRadius((5d, 0d), 1d, (1d, 0d, 0d)), 6);

    [Fact]
    public void Perimeter_Line_Intersection_Secant()
    {
        // y-axis through the unit circle
        var points = Circle.Perimeter.And.Line.Intersection.FromRadius(1d, (1d, 0d, 0d));

        Assert.NotNull(points);
        Assert.Equal(0d, points.Value.Point1.X, 6);
        Assert.Equal(1d, points.Value.Point1.Y, 6);
        Assert.Equal(0d, points.Value.Point2.X, 6);
        Assert.Equal(-1d, points.Value.Point2.Y, 6);
    }

    [Fact]
    public void Perimeter_Line_Intersection_Tangent()
    {
        // line y = 1 touches the unit circle at (0, 1)
        var points = Circle.Perimeter.And.Line.Intersection.FromRadius(1d, (0d, 1d, -1d));

        Assert.NotNull(points);
        Assert.Equal(points.Value.Point1, points.Value.Point2);
        Assert.Equal(0d, points.Value.Point1.X, 6);
        Assert.Equal(1d, points.Value.Point1.Y, 6);
    }

    [Fact]
    public void Perimeter_Line_Intersection_Diagonal()
    {
        // line x + y = 0
        var points = Circle.Perimeter.And.Line.Intersection.FromRadius(1d, (1d, 1d, 0d));
        var sqrtHalf = double.Sqrt(0.5);

        Assert.NotNull(points);
        Assert.Equal(-sqrtHalf, points.Value.Point1.X, 6);
        Assert.Equal(sqrtHalf, points.Value.Point1.Y, 6);
        Assert.Equal(sqrtHalf, points.Value.Point2.X, 6);
        Assert.Equal(-sqrtHalf, points.Value.Point2.Y, 6);
    }

    [Fact]
    public void Perimeter_Line_Intersection_Miss()
        // line y = 2 never reaches the unit circle
        => Assert.Null(Circle.Perimeter.And.Line.Intersection.FromRadius(1d, (0d, 1d, -2d)));
}
