using System;
using SMath.Geometry2D;
using Xunit;

namespace SMath;

public class AabbTests
{
    [Fact]
    public void FromCenter()
        => Assert.Equal((((-1d, -2d), (3d, 4d))), Aabb.FromCenter((1d, 1d), (2d, 3d)));

    [Fact]
    public void FromSize()
        => Assert.Equal((((1d, 1d), (4d, 3d))), Aabb.FromSize((1d, 1d), (3d, 2d)));

    [Fact]
    public void FromPoints()
    {
        ReadOnlySpan<(double X, double Y)> points = stackalloc (double X, double Y)[] { (1, 5), (-2, 3), (4, -1) };

        Assert.Equal((((-2d, -1d), (4d, 5d))), Aabb.FromPoints(points));
    }

    [Fact]
    public void FromPoints_OfNothing_Throws()
        => Assert.Throws<ArgumentException>(() => Aabb.FromPoints(ReadOnlySpan<(double X, double Y)>.Empty));

    [Fact]
    public void Center_Size_Area_Perimeter()
    {
        var box = ((0d, 0d), (4d, 2d));

        Assert.Equal((2d, 1d), Aabb.Center(box));
        Assert.Equal((4d, 2d), Aabb.Size(box));
        Assert.Equal(8d, Aabb.Area(box));
        Assert.Equal(12d, Aabb.Perimeter(box));
    }

    [Theory]
    [InlineData(1d, 1d, true)]
    [InlineData(0d, 0d, true)]
    [InlineData(2d, 2d, true)]
    [InlineData(3d, 1d, false)]
    [InlineData(-1d, 1d, false)]
    public void Contains_Point(double x, double y, bool expected)
        => Assert.Equal(expected, Aabb.Contains(((0d, 0d), (2d, 2d)), (x, y)));

    [Fact]
    public void Contains_Box()
    {
        var box = ((0d, 0d), (4d, 4d));

        Assert.True(Aabb.Contains(box, ((1d, 1d), (2d, 2d))));
        Assert.True(Aabb.Contains(box, box));
        Assert.False(Aabb.Contains(box, ((1d, 1d), (5d, 2d))));
    }

    [Fact]
    public void Overlaps()
    {
        var box = ((0d, 0d), (2d, 2d));

        Assert.True(Aabb.Overlaps(box, ((1d, 1d), (3d, 3d))));
        Assert.True(Aabb.Overlaps(box, ((2d, 0d), (3d, 3d))));
        Assert.False(Aabb.Overlaps(box, ((3d, 0d), (4d, 3d))));
    }

    [Fact]
    public void Union()
        => Assert.Equal((((0d, 0d), (5d, 4d))),
            Aabb.Union(((0d, 0d), (2d, 2d)), ((3d, 1d), (5d, 4d))));

    [Fact]
    public void Intersection()
    {
        Assert.Equal((((1d, 1d), (2d, 2d))),
            Aabb.Intersection(((0d, 0d), (2d, 2d)), ((1d, 1d), (3d, 3d))));

        Assert.Null(Aabb.Intersection(((0d, 0d), (1d, 1d)), ((2d, 2d), (3d, 3d))));
    }

    [Fact]
    public void Expanded()
        => Assert.Equal((((-1d, -1d), (3d, 3d))), Aabb.Expanded(((0d, 0d), (2d, 2d)), 1d));

    [Fact]
    public void Translated()
        => Assert.Equal((((1d, 2d), (3d, 4d))), Aabb.Translated(((0d, 0d), (2d, 2d)), (1d, 2d)));

    [Fact]
    public void Swept_ContainsBothEnds()
    {
        var swept = Aabb.Swept(((0d, 0d), (1d, 1d)), (3d, 0d));

        Assert.True(Aabb.Contains(swept, ((0d, 0d), (1d, 1d))));
        Assert.True(Aabb.Contains(swept, ((3d, 0d), (4d, 1d))));
    }

    [Fact]
    public void ClosestPoint()
    {
        var box = ((0d, 0d), (2d, 2d));

        Assert.Equal((2d, 1d), Aabb.ClosestPoint(box, (5d, 1d)));
        Assert.Equal((1d, 1d), Aabb.ClosestPoint(box, (1d, 1d)));
    }

    [Fact]
    public void Distance()
    {
        var box = ((0d, 0d), (2d, 2d));

        Assert.Equal(0d, Aabb.Distance(box, (1d, 1d)));
        Assert.Equal(3d, Aabb.Distance(box, (5d, 1d)));
        Assert.Equal(5d, Aabb.Distance(box, (5d, 6d)), 10);
    }

    [Fact]
    public void Penetration_PushesOutOverTheShortestAxis()
    {
        // the first box overlaps the second one by 1 in x and by 2 in y
        var penetration = Aabb.Penetration(((0d, 0d), (2d, 2d)), ((1d, 0d), (3d, 2d)));

        Assert.NotNull(penetration);
        Assert.Equal((-1d, 0d), penetration!.Value);
    }

    [Fact]
    public void Penetration_OfSeparatedBoxes_IsNull()
        => Assert.Null(Aabb.Penetration(((0d, 0d), (1d, 1d)), ((2d, 2d), (3d, 3d))));

    [Fact]
    public void Vertices_AreCounterclockwise()
        => Assert.Equal(((0d, 0d), (2d, 0d), (2d, 2d), (0d, 2d)),
            Aabb.Vertices(((0d, 0d), (2d, 2d))));

    [Fact]
    public void RayIntersection_HittingBox()
    {
        var hit = Aabb.RayIntersection(((1d, -1d), (2d, 1d)), (0d, 0d), (1d, 0d));

        Assert.NotNull(hit);
        Assert.Equal(1d, hit!.Value.Entry, 10);
        Assert.Equal(2d, hit.Value.Exit, 10);
    }

    [Fact]
    public void RayIntersection_MissingBox_IsNull()
        => Assert.Null(Aabb.RayIntersection(((1d, 5d), (2d, 6d)), (0d, 0d), (1d, 0d)));

    [Fact]
    public void RayIntersection_FromInside_HasNegativeEntry()
    {
        var hit = Aabb.RayIntersection(((0d, 0d), (2d, 2d)), (1d, 1d), (1d, 0d));

        Assert.NotNull(hit);
        Assert.True(hit!.Value.Entry < 0d);
    }

    [Fact]
    public void Sweep_HittingObstacle()
    {
        // a unit box at the origin moving right by 10 hits an obstacle starting at x = 3
        var hit = Aabb.Sweep(((0d, 0d), (1d, 1d)), (10d, 0d), ((3d, 0d), (4d, 1d)));

        Assert.NotNull(hit);
        Assert.Equal(0.2d, hit!.Value.Time, 10);
        Assert.Equal((-1d, 0d), hit.Value.Normal);
    }

    [Fact]
    public void Sweep_TooSlowToReach_IsNull()
        => Assert.Null(Aabb.Sweep(((0d, 0d), (1d, 1d)), (1d, 0d), ((5d, 0d), (6d, 1d))));

    [Fact]
    public void Sweep_MissingObstacle_IsNull()
        => Assert.Null(Aabb.Sweep(((0d, 0d), (1d, 1d)), (10d, 0d), ((3d, 5d), (4d, 6d))));
}
