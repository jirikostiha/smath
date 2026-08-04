using SMath.Geometry3D;
using Xunit;

namespace SMath.Geometry3D;

public class Point3Tests
{
    [Theory]
    [InlineData(0, 0, 0, 1, 0, 0, 1)]
    [InlineData(0, 0, 0, 1, 1, 1, 3)]
    [InlineData(1, 1, 1, -1, -1, -1, 6)]
    public void ManhattanDistance(double x1, double y1, double z1, double x2, double y2, double z2, double distance)
    {
        Assert.Equal(distance, Point3.ManhattanDistance((x1, y1, z1), (x2, y2, z2)));
        Assert.Equal(distance, Point3.ManhattanDistance((x2 - x1, y2 - y1, z2 - z1)));
    }

    [Theory]
    [InlineData(0, 0, 0, 1, 0, 0, 1)]
    [InlineData(0, 0, 0, 1, 1, 1, 1)]
    [InlineData(1, 1, 1, -1, -1, -1, 2)]
    public void ChebyshevDistance(double x1, double y1, double z1, double x2, double y2, double z2, double distance)
    {
        Assert.Equal(distance, Point3.ChebyshevDistance((x1, y1, z1), (x2, y2, z2)));
        Assert.Equal(distance, Point3.ChebyshevDistance((x2 - x1, y2 - y1, z2 - z1)));
    }

    [Theory]
    [InlineData(0, 0, 0, 1, 0, 0, 1, 1)]
    [InlineData(0, 0, 0, 1, 1, 1, 1, 3)]
    [InlineData(1, 1, 1, -1, -1, -1, 1, 6)]
    public void MinkowskiDistance(double x1, double y1, double z1, double x2, double y2, double z2, double r, double distance)
    {
        Assert.Equal(distance, Point3.MinkowskiDistance((x1, y1, z1), (x2, y2, z2), r));
        Assert.Equal(distance, Point3.MinkowskiDistance((x2 - x1, y2 - y1, z2 - z1), r));
    }

    [Theory]
    // sum over the axes of |a-b| / (|a|+|b|)
    [InlineData(1, 2, 3, 1, 2, 3, 0)]   // identical points
    [InlineData(0, 0, 0, 1, 1, 1, 3)]   // origin against a unit point, every term is 1
    [InlineData(1, 2, 4, 3, 6, 12, 1.5)] // 2/4 + 4/8 + 8/16
    public void CanberraDistance(double x1, double y1, double z1, double x2, double y2, double z2, double distance)
    {
        Assert.Equal(distance, Point3.CanberraDistance((x1, y1, z1), (x2, y2, z2)), 6);
    }

    [Theory]
    // Regression: a term with a zero numerator and a zero denominator used to evaluate
    // to NaN, so a single coordinate that both points share as zero poisoned the sum.
    // Such a term is defined to contribute zero.
    [InlineData(0, 0, 0, 0, 0, 0, 0)] // both in the origin
    [InlineData(1, 0, 0, 3, 0, 0, 0.5)] // only the x axis contributes
    [InlineData(0, 5, 0, 0, 5, 0, 0)] // identical, off the origin, with zero coordinates
    public void CanberraDistance_SharedZeroCoordinates_ContributeZero(
        double x1, double y1, double z1, double x2, double y2, double z2, double distance)
    {
        Assert.Equal(distance, Point3.CanberraDistance((x1, y1, z1), (x2, y2, z2)), 6);
    }
}
