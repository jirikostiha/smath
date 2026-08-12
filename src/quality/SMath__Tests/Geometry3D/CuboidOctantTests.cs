using System;
using System.Linq;
using Xunit;

namespace SMath.Geometry3D;

public class CuboidOctantTests
{
    [Fact]
    public void Octant_NoCodes_ReturnsCenteredCuboid()
    {
        var (origin, size) = Cuboid.Octant((10d, 8d, 6d), ReadOnlySpan<int>.Empty);

        // centered at (0,0,0): lowest corner at -size/2
        Assert.Equal((-5d, -4d, -3d), origin);
        Assert.Equal((10d, 8d, 6d), size);
    }

    [Theory]
    // 1..4 are the quadrants of the upper half, 5..8 the same ones of the lower half
    [InlineData(1, 0d, 0d, 0d)]     // top, front-right
    [InlineData(2, -5d, 0d, 0d)]    // top, front-left
    [InlineData(3, -5d, -4d, 0d)]   // top, back-left
    [InlineData(4, 0d, -4d, 0d)]    // top, back-right
    [InlineData(5, 0d, 0d, -3d)]    // bottom, front-right
    [InlineData(6, -5d, 0d, -3d)]   // bottom, front-left
    [InlineData(7, -5d, -4d, -3d)]  // bottom, back-left
    [InlineData(8, 0d, -4d, -3d)]   // bottom, back-right
    public void Octant_SingleNumber_HalvesSizeAndPicksOctant(int octant, double x, double y, double z)
    {
        var (origin, size) = Cuboid.Octant((10d, 8d, 6d), new[] { octant });

        Assert.Equal((x, y, z), origin);
        Assert.Equal((5d, 4d, 3d), size);
    }

    [Fact]
    public void Octant_NestedNumbers_DescendIntoEachInnerCuboid()
    {
        // top front-right, then bottom back-left of that, then top front-right of that
        var (origin, size) = Cuboid.Octant((8d, 8d, 8d), new[] { 1, 7, 1 });

        // start centered: origin (-4,-4,-4), size (8,8,8)
        // 1 -> origin (0,0,0),  size (4,4,4)
        // 7 -> origin (0,0,0),  size (2,2,2)
        // 1 -> origin (1,1,1),  size (1,1,1)
        Assert.Equal((1d, 1d, 1d), origin);
        Assert.Equal((1d, 1d, 1d), size);
    }

    [Fact]
    public void Octant_InnerCuboidStaysWithinTheOuterOne()
    {
        var (origin, size) = Cuboid.Octant((4d, 4d, 4d), new[] { 2, 8 });

        // 2 (top, front-left) -> origin (-2,0,0), size (2,2,2)
        // 8 (bottom, back-right of that) -> origin (-1,0,0), size (1,1,1)
        Assert.Equal((-1d, 0d, 0d), origin);
        Assert.Equal((1d, 1d, 1d), size);
        // fully inside the outer cuboid [-2,2]^3
        Assert.True(origin.Item1 >= -2d && origin.Item1 + size.Item1 <= 2d);
        Assert.True(origin.Item2 >= -2d && origin.Item2 + size.Item2 <= 2d);
        Assert.True(origin.Item3 >= -2d && origin.Item3 + size.Item3 <= 2d);
    }

    [Fact]
    public void Octant_EveryOctantOfTheSameLevelIsInsideTheOuterCuboid()
    {
        foreach (var octant in Enumerable.Range(1, 8))
        {
            var (origin, size) = Cuboid.Octant((2d, 2d, 2d), new[] { octant });

            Assert.Equal((1d, 1d, 1d), size);
            Assert.True(Cuboid.Contains((-1d, -1d, -1d), (2d, 2d, 2d), origin));
            Assert.True(Cuboid.Contains(
                (-1d, -1d, -1d), (2d, 2d, 2d),
                (origin.Item1 + size.Item1, origin.Item2 + size.Item2, origin.Item3 + size.Item3)));
        }
    }

    [Fact]
    public void Octant_SpanMatchesEnumerable()
    {
        int[] path = [1, 8, 5, 3, 7, 2];

        var fromSpan = Cuboid.Octant((16d, 12d, 8d), path);
        var fromEnumerable = Cuboid.Octant((16d, 12d, 8d), path.AsEnumerable());

        Assert.Equal(fromEnumerable.Origin, fromSpan.Origin);
        Assert.Equal(fromEnumerable.Size, fromSpan.Size);
    }

    [Fact]
    public void Octant_NullEnumerable_Throws()
        => Assert.Throws<ArgumentNullException>(
            () => Cuboid.Octant((1d, 1d, 1d), (System.Collections.Generic.IEnumerable<int>)null!));

    [Theory]
    [InlineData(0)]
    [InlineData(9)]
    [InlineData(-1)]
    [InlineData(11)]
    public void Octant_InvalidNumber_Throws(int octant)
    {
        // both overloads reject an out-of-range octant number
        Assert.Throws<ArgumentOutOfRangeException>(
            () => Cuboid.Octant((1d, 1d, 1d), new[] { octant }));
        Assert.Throws<ArgumentOutOfRangeException>(
            () => Cuboid.Octant((1d, 1d, 1d), new[] { octant }.AsEnumerable()));
    }
}
