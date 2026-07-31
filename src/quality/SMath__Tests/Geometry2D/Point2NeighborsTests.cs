using System.Linq;
using Xunit;

namespace SMath.Geometry2D;

public class Point2NeighborsTests
{
    [Fact]
    public void AxialNeighbors_ReturnsFourDistinctAxialNeighbors()
    {
        var neighbors = Point2.AxialNeighbors((0, 0)).ToArray();

        Assert.Equal(4, neighbors.Length);
        Assert.Contains((1, 0), neighbors);
        Assert.Contains((0, 1), neighbors);
        Assert.Contains((-1, 0), neighbors);
        Assert.Contains((0, -1), neighbors);
    }

    [Fact]
    public void AxialNeighbors_DoesNotMutateSharedState()
    {
        var neighbors = Point2.AxialNeighbors((3, 5)).ToArray();

        Assert.Equal(4, neighbors.Length);
        Assert.Contains((4, 5), neighbors);
        Assert.Contains((3, 6), neighbors);
        Assert.Contains((2, 5), neighbors);
        Assert.Contains((3, 4), neighbors);
        // origin should never be returned as an "axial" neighbor
        Assert.DoesNotContain((3, 5), neighbors);
    }

    [Fact]
    public void DiagonalNeighbors_ReturnsFourDistinctDiagonalNeighbors()
    {
        var neighbors = Point2.DiagonalNeighbors((0, 0)).ToArray();

        Assert.Equal(4, neighbors.Length);
        Assert.Contains((1, 1), neighbors);
        Assert.Contains((1, -1), neighbors);
        Assert.Contains((-1, 1), neighbors);
        Assert.Contains((-1, -1), neighbors);
    }

    [Fact]
    public void DiagonalNeighbors_DoesNotMutateSharedState()
    {
        var neighbors = Point2.DiagonalNeighbors((10, 20)).ToArray();

        Assert.Equal(4, neighbors.Length);
        Assert.Contains((11, 21), neighbors);
        Assert.Contains((11, 19), neighbors);
        Assert.Contains((9, 21), neighbors);
        Assert.Contains((9, 19), neighbors);
    }
}
