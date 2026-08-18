using Xunit;

namespace SMath.Geometry2D;

public class CircleAndCircleTests
{
    [Theory]
    [InlineData(1, 3, 0, 1, 1)]     // separated circles
    [InlineData(1, 0.5, 0, 1, 0)]   // overlapping circles
    [InlineData(1, 0, 0, 0.5, 0.5)] // smaller circle nested in the center
    [InlineData(1, 2, 0, 1, 0)]     // externally tangent circles
    [InlineData(1, 0, 0, 1, 0)]     // coincident circles
    public void Perimeter_Circle_Distance(double radius, double oX, double oY, double oRadius, double expected)
        => Assert.Equal(expected, Circle.Perimeter.And.Circle.Distance.FromRadius(radius, (oX, oY), oRadius), 6);

    [Fact]
    public void Perimeter_Circle_Distance_WithCenter()
        // circles at (1, 1) r=1 and (5, 1) r=1 -> gap 2
        => Assert.Equal(2d, Circle.Perimeter.And.Circle.Distance.FromRadius((1d, 1d), 1d, (5d, 1d), 1d), 6);

    [Fact]
    public void Perimeter_Circle_Intersection_Crossing()
    {
        // two unit circles with centers one unit apart
        var points = Circle.Perimeter.And.Circle.Intersection.FromRadius(1d, (1d, 0d), 1d);
        var halfHeight = double.Sqrt(0.75);

        Assert.NotNull(points);
        Assert.Equal(0.5d, points.Value.Point1.X, 6);
        Assert.Equal(halfHeight, points.Value.Point1.Y, 6);
        Assert.Equal(0.5d, points.Value.Point2.X, 6);
        Assert.Equal(-halfHeight, points.Value.Point2.Y, 6);
    }

    [Fact]
    public void Perimeter_Circle_Intersection_Tangent()
    {
        // externally tangent unit circles touch at (1, 0)
        var points = Circle.Perimeter.And.Circle.Intersection.FromRadius(1d, (2d, 0d), 1d);

        Assert.NotNull(points);
        Assert.Equal(points.Value.Point1, points.Value.Point2);
        Assert.Equal(1d, points.Value.Point1.X, 6);
        Assert.Equal(0d, points.Value.Point1.Y, 6);
    }

    [Fact]
    public void Perimeter_Circle_Intersection_Separated()
        => Assert.Null(Circle.Perimeter.And.Circle.Intersection.FromRadius(1d, (3d, 0d), 1d));

    [Fact]
    public void Perimeter_Circle_Intersection_Coincident()
        => Assert.Null(Circle.Perimeter.And.Circle.Intersection.FromRadius(1d, (0d, 0d), 1d));

    [Fact]
    public void Perimeter_Circle_Intersection_Concentric()
        => Assert.Null(Circle.Perimeter.And.Circle.Intersection.FromRadius(1d, (0d, 0d), 0.5d));

    [Theory]
    [InlineData(1, 3, 0, 1, 1)]   // separated -> positive gap
    [InlineData(1, 0.5, 0, 1, 0)] // overlapping -> 0
    [InlineData(1, 0, 0, 0.5, 0)] // nested regions overlap -> 0
    [InlineData(1, 2, 0, 1, 0)]   // tangent -> 0
    public void Region_Circle_Distance(double radius, double oX, double oY, double oRadius, double expected)
        => Assert.Equal(expected, Circle.Region.And.Circle.Distance.FromRadius(radius, (oX, oY), oRadius), 6);
}
