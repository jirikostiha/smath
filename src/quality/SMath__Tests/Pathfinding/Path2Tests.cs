using System;
using SMath.Grid2D;
using SMath.Pathfinding;
using Xunit;

namespace SMath;

public class Path2Tests
{
    private readonly struct NoObstacle : IGridPredicate
    {
        public bool Test(int x, int y)
            => false;
    }

    private readonly struct WallAt : IGridPredicate
    {
        private readonly (int X, int Y) _cell;

        public WallAt((int X, int Y) cell)
            => _cell = cell;

        public bool Test(int x, int y)
            => (x, y) == _cell;
    }

    [Fact]
    public void Simplify_OfStraightPath_KeepsEndsOnly()
    {
        Span<(int X, int Y)> path = stackalloc (int X, int Y)[] { (0, 0), (1, 0), (2, 0), (3, 0) };

        var count = Path2.Simplify(path);

        Assert.Equal(2, count);
        Assert.Equal(new[] { (0, 0), (3, 0) }, path[..count].ToArray());
    }

    [Fact]
    public void Simplify_KeepsCorners()
    {
        Span<(int X, int Y)> path = stackalloc (int X, int Y)[] { (0, 0), (1, 0), (2, 0), (2, 1), (2, 2) };

        var count = Path2.Simplify(path);

        Assert.Equal(new[] { (0, 0), (2, 0), (2, 2) }, path[..count].ToArray());
    }

    [Fact]
    public void Simplify_OfShortPath_KeepsIt()
    {
        Span<(int X, int Y)> path = stackalloc (int X, int Y)[] { (0, 0), (1, 1) };

        Assert.Equal(2, Path2.Simplify(path));
    }

    [Fact]
    public void PullString_OverEmptyGrid_KeepsEndsOnly()
    {
        Span<(int X, int Y)> path = stackalloc (int X, int Y)[] { (0, 0), (1, 0), (2, 0), (2, 1), (2, 2) };

        var count = Path2.PullString(path, default(NoObstacle));

        Assert.Equal(2, count);
        Assert.Equal(new[] { (0, 0), (2, 2) }, path[..count].ToArray());
    }

    [Fact]
    public void PullString_AroundObstacle_KeepsTheCorner()
    {
        Span<(int X, int Y)> path = stackalloc (int X, int Y)[] { (0, 0), (1, 0), (2, 0), (2, 1), (2, 2) };

        var count = Path2.PullString(path, new WallAt((1, 1)));

        Assert.True(count > 2);
        Assert.Equal((0, 0), path[0]);
        Assert.Equal((2, 2), path[count - 1]);
    }

    [Fact]
    public void Length_OfSegments()
    {
        ReadOnlySpan<(double X, double Y)> path = stackalloc (double X, double Y)[] { (0, 0), (3, 0), (3, 4) };

        Assert.Equal(7d, Path2.Length(path), 10);
    }

    [Fact]
    public void Length_OfSinglePoint_IsZero()
    {
        ReadOnlySpan<(double X, double Y)> path = stackalloc (double X, double Y)[] { (1, 1) };

        Assert.Equal(0d, Path2.Length(path));
    }

    [Theory]
    [InlineData(-1d, 0d, 0d)]
    [InlineData(0d, 0d, 0d)]
    [InlineData(2d, 2d, 0d)]
    [InlineData(4d, 3d, 1d)]
    [InlineData(100d, 3d, 4d)]
    public void PointAt(double distance, double expectedX, double expectedY)
    {
        ReadOnlySpan<(double X, double Y)> path = stackalloc (double X, double Y)[] { (0, 0), (3, 0), (3, 4) };

        var point = Path2.PointAt(path, distance);

        Assert.Equal(expectedX, point.X, 10);
        Assert.Equal(expectedY, point.Y, 10);
    }

    [Fact]
    public void PointAt_OfEmptyPath_Throws()
    {
        Assert.Throws<ArgumentException>(() => Path2.PointAt(ReadOnlySpan<(double X, double Y)>.Empty, 1d));
    }

    [Fact]
    public void ToPoints_AreCentersOfCells()
    {
        ReadOnlySpan<(int X, int Y)> path = stackalloc (int X, int Y)[] { (0, 0), (1, 0) };
        Span<(double X, double Y)> points = stackalloc (double X, double Y)[2];

        var count = Path2.ToPoints(path, 2d, points);

        Assert.Equal(2, count);
        Assert.Equal((1d, 1d), points[0]);
        Assert.Equal((3d, 1d), points[1]);
    }

    [Fact]
    public void Smooth_KeepsEndsAndCutsTheCorner()
    {
        ReadOnlySpan<(double X, double Y)> path = stackalloc (double X, double Y)[] { (0, 0), (2, 0), (2, 2) };
        Span<(double X, double Y)> points = stackalloc (double X, double Y)[Path2.SmoothPointCount(3)];

        var count = Path2.Smooth(path, points);

        Assert.Equal(4, count);
        Assert.Equal((0d, 0d), points[0]);
        Assert.Equal((2d, 2d), points[count - 1]);
        Assert.DoesNotContain((2d, 0d), points[..count].ToArray());
    }

    [Fact]
    public void Smooth_OfShortPath_KeepsIt()
    {
        ReadOnlySpan<(double X, double Y)> path = stackalloc (double X, double Y)[] { (0, 0), (1, 1) };
        Span<(double X, double Y)> points = stackalloc (double X, double Y)[Path2.SmoothPointCount(2)];

        Assert.Equal(2, Path2.Smooth(path, points));
        Assert.Equal((1d, 1d), points[1]);
    }

    [Theory]
    [InlineData(0, 0)]
    [InlineData(2, 2)]
    [InlineData(3, 4)]
    [InlineData(5, 8)]
    public void SmoothPointCount(int pathPointCount, int expected)
        => Assert.Equal(expected, Path2.SmoothPointCount(pathPointCount));
}
