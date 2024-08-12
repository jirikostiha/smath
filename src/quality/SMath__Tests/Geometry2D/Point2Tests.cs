using SMath.GeometryD2;
using System.Linq;
using Xunit;

namespace SMath.Geometry2D;

public class Point2Tests
{
    [Theory]
    [InlineData(0, 0, 1, 0, 1)]
    [InlineData(0, 0, 1, 1, 2)]
    [InlineData(1, 1, -1, -1, 4)]
    public void ManhattanDistance(double x1, double y1, double x2, double y2, double distance)
    {
        Assert.Equal(distance, Point2.ManhattanDistance((x1, y1), (x2, y2)));
        Assert.Equal(distance, Point2.ManhattanDistance((x2 - x1, y2 - y1)));
    }

    [Theory]
    [InlineData(0, 0, 1, 0, 1)]
    [InlineData(0, 0, 1, 1, 1)]
    [InlineData(1, 1, -1, -1, 2)]
    public void ChebyshevDistance(double x1, double y1, double x2, double y2, double distance)
    {
        Assert.Equal(distance, Point2.ChebyshevDistance((x1, y1), (x2, y2)));
        Assert.Equal(distance, Point2.ChebyshevDistance((x2 - x1, y2 - y1)));
    }

    [Theory]
    [InlineData(0, 0, 1, 0, 1, 1)]
    [InlineData(0, 0, 1, 1, 1, 2)]
    [InlineData(1, 1, -1, -1, 1, 4)]
    public void MinkowskiDistance(double x1, double y1, double x2, double y2, double r, double distance)
    {
        Assert.Equal(distance, Point2.MinkowskiDistance((x1, y1), (x2, y2), r));
        Assert.Equal(distance, Point2.MinkowskiDistance((x2 - x1, y2 - y1), r));
    }

    [Fact]
    public void CoordinatesAtManhattanDistance()
    {
        var coords = Point2.CoordinatesAtManhattanDistance((1, 1), 2).ToArray();

        Assert.Equal(8, coords.Length);
        Assert.Equal((1, -1), coords[0]);
        Assert.Equal((2, 0), coords[1]);
        Assert.Equal((3, 1), coords[2]);
        Assert.Equal((2, 2), coords[3]);
        Assert.Equal((1, 3), coords[4]);
        Assert.Equal((0, 2), coords[5]);
    }

    [Fact]
    public void CoordinatesAtManhattanDistanceWithBottomLimitSmallerThanDistance_OnlyHigherCoords()
    {
        var coords = Point2.CoordinatesAtManhattanDistance((1, 1), 2, (0, 0), (5, 5)).ToArray();

        Assert.Equal(6, coords.Length);
        Assert.Equal((2, 0), coords[0]);
        Assert.Equal((3, 1), coords[1]);
        Assert.Equal((2, 2), coords[2]);
        Assert.Equal((1, 3), coords[3]);
        Assert.Equal((0, 2), coords[4]);
        Assert.Equal((0, 0), coords[5]);
    }

    [Fact]
    public void CoordinatesAtManhattanDistanceWithLimitsSmallerThanDistance_FourCoords()
    {
        var coords = Point2.CoordinatesAtManhattanDistance((1, 1), 2, (0, 0), (2, 2)).ToArray();

        Assert.Equal(4, coords.Length);
        Assert.Equal((2, 0), coords[0]);
        Assert.Equal((2, 2), coords[1]);
        Assert.Equal((0, 2), coords[2]);
        Assert.Equal((0, 0), coords[3]);
    }

    //[Fact]
    [Fact]
    public void CoordinatesUpToManhattanDistance()
    {
        var coords = Point2.CoordinatesUpToManhattanDistance((1, 1), 2).ToArray();

        Assert.Equal(25, coords.Length);
    }

    //[Fact]
    [Fact]
    public void CoordinatesUpToManhattanDistanceWithLimits()
    {
        var coords = Point2.CoordinatesUpToManhattanDistance((1, 1), 2, (0, -1), (2, 2)).ToArray();

        Assert.Equal(12, coords.Length);
    }

    [Fact]
    public void CoordinatesUpToManhattanDistanceWithLimits_Square()
    {
        var coords = Point2.CoordinatesUpToManhattanDistance((1, 1), 2, (0, 0), (2, 2)).ToArray();

        Assert.Equal(9, coords.Length);
    }

    [Fact]
    public void CoordinatesAtChebyshevDistance()
    {
        var coords = Point2.CoordinatesAtChebyshevDistance((1, 1), 2).ToArray();

        Assert.Equal(16, coords.Length);
        Assert.Equal((-1, -1), coords[0]);
        Assert.Equal((0, -1), coords[1]);
        Assert.Equal((3, -1), coords[4]);
        Assert.Equal((3, 0), coords[5]);
        Assert.Equal((3, 1), coords[6]);
        Assert.Equal((-1, 0), coords[15]);
    }

    [Fact]
    public void CoordinatesAtChebyshevDistanceWithBottomLimitSmallerThanDistance_OnlyHigherCoords()
    {
        var coords = Point2.CoordinatesAtChebyshevDistance((1, 1), 2, (0, 0), (5, 5)).ToArray();

        Assert.Equal(7, coords.Length);
        Assert.Equal((3, 0), coords[0]);
        Assert.Equal((0, 3), coords[6]);
    }

    [Fact]
    public void CoordinatesAtChebyshevDistanceWithLimitsSmallerThanDistance_NoCoords()
    {
        var coords = Point2.CoordinatesAtChebyshevDistance((1, 1), 2, (0, 0), (2, 2)).ToArray();

        Assert.Empty(coords);
    }

    [Fact]
    public void CoordinatesUpToChebyshevDistance()
    {
        var coords = Point2.CoordinatesUpToChebyshevDistance((1, 1), 2).ToArray();

        Assert.Equal(25, coords.Length);
    }

    [Fact]
    public void CoordinatesUpToChebyshevDistanceWithLimits()
    {
        var coords = Point2.CoordinatesUpToChebyshevDistance((1, 1), 2, (0, -1), (2, 2)).ToArray();

        Assert.Equal(12, coords.Length);
    }
}
