using SMath.GeometryD2;
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
}
