using Xunit;

namespace SMath.Geometry2D;

public class CircleAndPointTests
{
    [Theory]
    [InlineData(1, 2, 0, 1)]     // point outside
    [InlineData(1, 0.5, 0, 0.5)] // point inside
    [InlineData(1, 1, 0, 0)]     // point on perimeter
    [InlineData(1, 0, 0, 1)]     // point in the center
    [InlineData(2, 0, 3, 1)]     // point outside on the y-axis
    public void Perimeter_Point_Distance(double radius, double pX, double pY, double expected)
        => Assert.Equal(expected, Circle.Perimeter.And.Point.Distance.FromRadius(radius, (pX, pY)), 6);

    [Fact]
    public void Perimeter_Point_Distance_WithCenter()
        => Assert.Equal(1d, Circle.Perimeter.And.Point.Distance.FromRadius((1d, 1d), 1d, (1d, 3d)), 6);

    [Theory]
    [InlineData(1, 1, 0, true)]   // on perimeter
    [InlineData(1, 0, 1, true)]   // on perimeter
    [InlineData(1, 2, 0, false)]  // outside
    [InlineData(1, 0.5, 0, false)] // inside
    public void Perimeter_Point_Intersection(double radius, double pX, double pY, bool expected)
        => Assert.Equal(expected, Circle.Perimeter.And.Point.Intersection.FromRadius(radius, (pX, pY)));

    [Fact]
    public void Perimeter_Point_Intersection_WithCenter()
        => Assert.True(Circle.Perimeter.And.Point.Intersection.FromRadius((1d, 1d), 1d, (2d, 1d)));

    [Theory]
    [InlineData(1, 2, 0, 1)]      // point outside
    [InlineData(1, 0.5, 0, 0)]    // point inside the region
    [InlineData(1, 1, 0, 0)]      // point on the perimeter
    [InlineData(1, 0, 0, 0)]      // point in the center
    public void Region_Point_Distance(double radius, double pX, double pY, double expected)
        => Assert.Equal(expected, Circle.Region.And.Point.Distance.FromRadius(radius, (pX, pY)), 6);

    [Theory]
    [InlineData(1, 0.5, 0, true)]  // inside
    [InlineData(1, 1, 0, true)]    // on the boundary
    [InlineData(1, 0, 0, true)]    // center
    [InlineData(1, 2, 0, false)]   // outside
    public void Region_Point_Intersection(double radius, double pX, double pY, bool expected)
        => Assert.Equal(expected, Circle.Region.And.Point.Intersection.FromRadius(radius, (pX, pY)));
}
