using System;
using Xunit;

namespace SMath.Geometry2D;

public class RectangleQuadrantTests
{
    [Fact]
    public void Quadrant_NoCodes_ReturnsCenteredRectangle()
    {
        var (origin, size) = Rectangle.Quadrant((10d, 8d));

        // centered at (0,0): bottom-left corner at -size/2
        Assert.Equal((-5d, -4d), origin);
        Assert.Equal((10d, 8d), size);
    }

    [Theory]
    // quadrants numbered counter-clockwise from the upper right
    [InlineData(1, 0d, 0d)]   // top-right
    [InlineData(2, -5d, 0d)]  // top-left
    [InlineData(3, -5d, -4d)] // bottom-left
    [InlineData(4, 0d, -4d)]  // bottom-right
    public void Quadrant_SingleNumber_HalvesSizeAndPicksQuadrant(int quadrant, double x, double y)
    {
        var (origin, size) = Rectangle.Quadrant((10d, 8d), quadrant);

        Assert.Equal((x, y), origin);
        Assert.Equal((5d, 4d), size);
    }

    [Fact]
    public void Quadrant_NestedNumbers_DescendIntoEachInnerRectangle()
    {
        // top-right, then bottom-left of that, then top-right of that
        var (origin, size) = Rectangle.Quadrant((8d, 8d), 1, 3, 1);

        // start centered: origin (-4,-4), size (8,8)
        // 1 -> origin (0,0),  size (4,4)
        // 3 -> origin (0,0),  size (2,2)
        // 1 -> origin (1,1),  size (1,1)
        Assert.Equal((1d, 1d), origin);
        Assert.Equal((1d, 1d), size);
    }

    [Fact]
    public void Quadrant_InnerRectangleStaysWithinTheOuterOne()
    {
        var (origin, size) = Rectangle.Quadrant((4d, 4d), 2, 4);

        // 2 (top-left) -> origin (-2,0), size (2,2)
        // 4 (bottom-right of that) -> origin (-1,0), size (1,1)
        Assert.Equal((-1d, 0d), origin);
        Assert.Equal((1d, 1d), size);
        // fully inside the outer rectangle [-2,2] x [-2,2]
        Assert.True(origin.Item1 >= -2d && origin.Item1 + size.Item1 <= 2d);
        Assert.True(origin.Item2 >= -2d && origin.Item2 + size.Item2 <= 2d);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(5)]
    [InlineData(-1)]
    [InlineData(11)]
    public void Quadrant_InvalidNumber_Throws(int quadrant)
    {
        Assert.Throws<ArgumentOutOfRangeException>(
            () => Rectangle.Quadrant((1d, 1d), quadrant));
    }
}
