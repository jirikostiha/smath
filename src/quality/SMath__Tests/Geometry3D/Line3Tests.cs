using Xunit;

namespace SMath.Geometry3D;

public class Line3Tests
{
    [Fact]
    public void PointProjectionAndDistance()
    {
        Assert.Equal((2d, 0d, 0d), Line3.And.Point.Projection.FromPoints((0d, 0d, 0d), (1d, 0d, 0d), (2d, 3d, 4d)));
        Assert.Equal(5d, Line3.And.Point.Distance.FromPoints((0d, 0d, 0d), (1d, 0d, 0d), (2d, 3d, 4d)), 12);
    }

    [Fact]
    public void PointDistanceFromDegeneratedLineIsTheDistanceFromThePoint()
        => Assert.Equal(5d, Line3.And.Point.Distance.FromPoints((1d, 1d, 1d), (1d, 1d, 1d), (1d, 4d, 5d)), 12);

    [Fact]
    public void PointIntersection()
    {
        Assert.True(Line3.And.Point.Intersection.FromPoints((0d, 0d, 0d), (1d, 2d, 3d), (-2d, -4d, -6d)));
        Assert.False(Line3.And.Point.Intersection.FromPoints((0d, 0d, 0d), (1d, 2d, 3d), (1d, 2d, 4d)));
    }

    [Fact]
    public void SkewLinesDistanceAndClosestPoints()
    {
        // the x axis and a line parallel to the y axis 2 units above it
        var distance = Line3.And.Line.Distance.FromPoints((0d, 0d, 0d), (1d, 0d, 0d), (3d, 0d, 2d), (3d, 1d, 2d));
        var closest = Line3.And.Line.ClosestPoints.FromPoints((0d, 0d, 0d), (1d, 0d, 0d), (3d, 0d, 2d), (3d, 1d, 2d));

        Assert.Equal(2d, distance, 12);
        Assert.Equal(((3d, 0d, 0d), (3d, 0d, 2d)), closest);
    }

    [Fact]
    public void ParallelLinesDistance()
    {
        Assert.Equal(5d, Line3.And.Line.Distance.FromPoints((0d, 0d, 0d), (1d, 0d, 0d), (7d, 3d, 4d), (9d, 3d, 4d)), 12);
        Assert.Null(Line3.And.Line.ClosestPoints.FromPoints((0d, 0d, 0d), (1d, 0d, 0d), (7d, 3d, 4d), (9d, 3d, 4d)));
    }

    [Fact]
    public void SegmentLengthMidpointAndPoints()
    {
        Assert.Equal(7d, Line3.Segment.Length.FromPoints((1d, 1d, 1d), (3d, 4d, 7d)), 12);
        Assert.Equal((2d, 2.5d, 4d), Line3.Segment.Midpoint.FromPoints((1d, 1d, 1d), (3d, 4d, 7d)));
        Assert.Equal([(0d, 0d, 0d), (1d, 2d, 3d), (2d, 4d, 6d)], Line3.Segment.Points.FromPoints((0d, 0d, 0d), (2d, 4d, 6d), 3));
        Assert.Equal([(5d, 5d, 5d)], Line3.Segment.Points.FromPoints((5d, 5d, 5d), (2d, 4d, 6d), 1));
    }

    [Theory]
    [InlineData(-3d, 4d, 0d, 0d)]  // before the start
    [InlineData(1d, 4d, 3d, 1d)]   // beside the inside
    [InlineData(5d, 0d, 0d, 2d)]   // past the end
    public void SegmentClosestPointAndDistance(double x, double y, double z, double closestX)
    {
        var closest = Line3.Segment.And.Point.ClosestPoint.FromPoints((0d, 0d, 0d), (2d, 0d, 0d), (x, y, z));

        Assert.Equal((closestX, 0d, 0d), closest);
        Assert.Equal(Point3.Distance(closest, (x, y, z)), Line3.Segment.And.Point.Distance.FromPoints((0d, 0d, 0d), (2d, 0d, 0d), (x, y, z)), 12);
    }

    [Fact]
    public void SegmentPointIntersection()
    {
        Assert.True(Line3.Segment.And.Point.Intersection.FromPoints((0d, 0d, 0d), (2d, 4d, 6d), (1d, 2d, 3d)));
        Assert.False(Line3.Segment.And.Point.Intersection.FromPoints((0d, 0d, 0d), (2d, 4d, 6d), (3d, 6d, 9d)));
        Assert.False(Line3.Segment.And.Point.Intersection.FromPoints((0d, 0d, 0d), (2d, 4d, 6d), (1d, 2d, 4d)));
    }

    [Theory]
    [InlineData(1d, 1d, 2d, 1d, 1d, -2d, 1d)]  // passing over the first segment
    [InlineData(1d, 0d, 2d, 1d, 0d, -2d, 0d)]  // crossing it
    [InlineData(3d, 0d, 2d, 3d, 1d, 2d, 2.2360679774997898)] // the closest point of the lines lies past the first segment
    [InlineData(4d, 0d, 0d, 6d, 0d, 0d, 2d)]   // collinear and apart
    [InlineData(1d, 3d, 0d, 1d, 3d, 0d, 3d)]   // the second segment is a point
    public void SegmentSegmentDistance(double x1, double y1, double z1, double x2, double y2, double z2, double expected)
        => Assert.Equal(expected, Line3.Segment.And.Segment.Distance.FromPoints((0d, 0d, 0d), (2d, 0d, 0d), (x1, y1, z1), (x2, y2, z2)), 12);
}
