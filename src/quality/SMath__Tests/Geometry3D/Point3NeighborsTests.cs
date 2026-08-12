using System.Linq;
using Xunit;

namespace SMath.Geometry3D;

public class Point3NeighborsTests
{
    [Fact]
    public void AxialNeighbors_ReturnsSixDistinctAxialNeighbors()
    {
        var neighbors = Point3.AxialNeighbors((0, 0, 0)).ToArray();

        Assert.Equal(6, neighbors.Length);
        Assert.Equal(6, neighbors.Distinct().Count());
        Assert.Contains((1, 0, 0), neighbors);
        Assert.Contains((-1, 0, 0), neighbors);
        Assert.Contains((0, 1, 0), neighbors);
        Assert.Contains((0, -1, 0), neighbors);
        Assert.Contains((0, 0, 1), neighbors);
        Assert.Contains((0, 0, -1), neighbors);
    }

    [Fact]
    public void AxialNeighbors_AreOffsetInExactlyOneAxis()
    {
        var neighbors = Point3.AxialNeighbors((3, 5, -2)).ToArray();

        Assert.Equal(6, neighbors.Length);
        Assert.All(neighbors, n => Assert.Equal(1, Point3.ManhattanDistance(n, (3, 5, -2))));
        // center should never be returned as its own neighbor
        Assert.DoesNotContain((3, 5, -2), neighbors);
    }

    [Fact]
    public void EdgeNeighbors_ReturnsTwelveDistinctEdgeNeighbors()
    {
        var neighbors = Point3.EdgeNeighbors((0, 0, 0)).ToArray();

        Assert.Equal(12, neighbors.Length);
        Assert.Equal(12, neighbors.Distinct().Count());
        // exactly two axes are offset, so Manhattan distance is 2 and Chebyshev distance is 1
        Assert.All(neighbors, n => Assert.Equal(2, Point3.ManhattanDistance(n, (0, 0, 0))));
        Assert.All(neighbors, n => Assert.Equal(1, Point3.ChebyshevDistance(n, (0, 0, 0))));
        Assert.Contains((1, 1, 0), neighbors);
        Assert.Contains((-1, 0, 1), neighbors);
        Assert.Contains((0, -1, -1), neighbors);
    }

    [Fact]
    public void DiagonalNeighbors_ReturnsEightDistinctDiagonalNeighbors()
    {
        var neighbors = Point3.DiagonalNeighbors((0, 0, 0)).ToArray();

        Assert.Equal(8, neighbors.Length);
        Assert.Equal(8, neighbors.Distinct().Count());
        // all three axes are offset, so Manhattan distance is 3 and Chebyshev distance is 1
        Assert.All(neighbors, n => Assert.Equal(3, Point3.ManhattanDistance(n, (0, 0, 0))));
        Assert.All(neighbors, n => Assert.Equal(1, Point3.ChebyshevDistance(n, (0, 0, 0))));
        Assert.Contains((1, 1, 1), neighbors);
        Assert.Contains((1, -1, 1), neighbors);
        Assert.Contains((-1, -1, -1), neighbors);
    }

    [Fact]
    public void DiagonalNeighbors_AreRelativeToTheGivenPoint()
    {
        var neighbors = Point3.DiagonalNeighbors((10, 20, 30)).ToArray();

        Assert.Equal(8, neighbors.Length);
        Assert.Contains((11, 21, 31), neighbors);
        Assert.Contains((9, 19, 29), neighbors);
        Assert.Contains((11, 19, 31), neighbors);
    }

    [Fact]
    public void AllNeighbors_FormTheChebyshevShellOfDistanceOne()
    {
        var center = (X: 2, Y: -1, Z: 4);

        var neighbors = Point3.AxialNeighbors(center)
            .Concat(Point3.EdgeNeighbors(center))
            .Concat(Point3.DiagonalNeighbors(center))
            .ToArray();

        Assert.Equal(26, neighbors.Length);
        Assert.Equal(26, neighbors.Distinct().Count());
        Assert.Equal(
            Point3.CoordinatesAtChebyshevDistance(center, 1).OrderBy(c => c).ToArray(),
            neighbors.OrderBy(c => c).ToArray());
    }
}
