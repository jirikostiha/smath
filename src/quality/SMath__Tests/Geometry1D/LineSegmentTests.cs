namespace SMath.Geometry1D;

using System.Linq;
using Xunit;

public class LineSegmentTests
{
    [Fact]
    public void Indexes()
    {
        var indices = LineSegment.Indexes(count: 3, length: 3d).ToArray();

        Assert.Equal(0d, indices[0]);
        Assert.Equal(1d, indices[1]);
        Assert.Equal(2d, indices[2]);
    }

    [Fact]
    public void Indices()
    {
        var indices = LineSegment.Indices(count: 3, length: 3d).ToArray();

        Assert.Equal(0d, indices[0]);
        Assert.Equal(1d, indices[1]);
        Assert.Equal(2d, indices[2]);
    }

    [Fact]
    public void Indices_Unit()
    {
        var indices = LineSegment.Indices<double>(count: 2).ToArray();

        Assert.Equal(0d, indices[0]);
        Assert.Equal(0.5d, indices[1]);
    }

    [Fact]
    public void LengthFromTwoPoints()
    {
        Assert.Equal(5d, LineSegment.Length.FromTwoPoints(2d, 7d));
        Assert.Equal(5d, LineSegment.Length.FromTwoPoints(7d, 2d));
    }

    [Fact]
    public void PointsGet()
    {
        var points = LineSegment.Points.Get(0d, 4d, 3).ToArray();

        Assert.Equal(3, points.Length);
        Assert.Equal(1d, points[0]);
        Assert.Equal(2d, points[1]);
        Assert.Equal(3d, points[2]);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    [InlineData(-5)]
    public void PointsGet_NonPositiveCount_ReturnsEmpty(int count)
    {
        Assert.Empty(LineSegment.Points.Get(0d, 4d, count));
    }

    [Fact]
    public void PointDistanceAndIntersection()
    {
        Assert.True(LineSegment.And.Point.Intersection.FromPoints(1d, 5d, 3d));
        Assert.False(LineSegment.And.Point.Intersection.FromPoints(1d, 5d, 6d));

        Assert.Equal(0d, LineSegment.And.Point.Distance.FromPoints(1d, 5d, 3d));
        Assert.Equal(1d, LineSegment.And.Point.Distance.FromPoints(1d, 5d, 6d));
        Assert.Equal(2d, LineSegment.And.Point.Distance.FromPoints(1d, 5d, -1d));
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public void Indexes_NonPositiveCount_ReturnsEmpty(int count)
    {
        Assert.Empty(LineSegment.Indexes(count, 3d));
        Assert.Empty(LineSegment.Indices(count, 3d));
        Assert.Empty(LineSegment.Indexes<double>(count));
    }

    [Fact]
    public void PointDistanceAndIntersection_DegenerateSegment()
    {
        Assert.True(LineSegment.And.Point.Intersection.FromPoints(3d, 3d, 3d));
        Assert.False(LineSegment.And.Point.Intersection.FromPoints(3d, 3d, 4d));

        Assert.Equal(0d, LineSegment.And.Point.Distance.FromPoints(3d, 3d, 3d));
        Assert.Equal(1d, LineSegment.And.Point.Distance.FromPoints(3d, 3d, 4d));
        Assert.Equal(2d, LineSegment.And.Point.Distance.FromPoints(3d, 3d, 1d));
    }
}
