using System;
using Xunit;

namespace SMath.Geometry2D;

public class RectangleQuadrantTests
{
    [Fact]
    public void Quadrant_NoCodes_ReturnsOriginalRectangle()
    {
        var (origin, size) = Rectangle.Quadrant((0d, 0d), (4d, 8d));

        Assert.Equal((0d, 0d), origin);
        Assert.Equal((4d, 8d), size);
    }

    [Theory]
    // XY code: X = column (1 left, 2 right), Y = row (1 bottom, 2 top)
    [InlineData(11, 0d, 0d)]
    [InlineData(21, 5d, 0d)]
    [InlineData(12, 0d, 4d)]
    [InlineData(22, 5d, 4d)]
    public void Quadrant_SingleCode_HalvesSizeAndPicksCorner(int code, double x, double y)
    {
        var (origin, size) = Rectangle.Quadrant((0d, 0d), (10d, 8d), code);

        Assert.Equal((x, y), origin);
        Assert.Equal((5d, 4d), size);
    }

    [Fact]
    public void Quadrant_RespectsNonZeroOuterOrigin()
    {
        var (origin, size) = Rectangle.Quadrant((2d, 3d), (10d, 8d), 22);

        Assert.Equal((7d, 7d), origin);
        Assert.Equal((5d, 4d), size);
    }

    [Fact]
    public void Quadrant_NestedCodes_DescendIntoEachInnerRectangle()
    {
        // top-right, then bottom-left of that, then top-right of that
        var (origin, size) = Rectangle.Quadrant((0d, 0d), (8d, 8d), 22, 11, 22);

        // 22 -> origin (4,4), size (4,4)
        // 11 -> origin (4,4), size (2,2)
        // 22 -> origin (5,5), size (1,1)
        Assert.Equal((5d, 5d), origin);
        Assert.Equal((1d, 1d), size);
    }

    [Fact]
    public void Quadrant_CenterOfInnerRectangleMatchesQuadrantCenterHelpers()
    {
        var (origin, size) = Rectangle.Quadrant((0d, 0d), (1d, 1d), 21);
        var center = (origin.X + size.X / 2d, origin.Y + size.Y / 2d);

        Assert.Equal(Rectangle.Quadrant21Center<double>(), center);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(31)]
    [InlineData(13)]
    [InlineData(1)]
    [InlineData(23)]
    public void Quadrant_InvalidCode_Throws(int code)
    {
        Assert.Throws<ArgumentOutOfRangeException>(
            () => Rectangle.Quadrant((0d, 0d), (1d, 1d), code));
    }
}
