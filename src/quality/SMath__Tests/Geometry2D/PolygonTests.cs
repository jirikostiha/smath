using SMath.Geometry2D;
using Xunit;

namespace SMath.Tests.Geometry2D;

public class PolygonTests
{
    [Fact]
    public void Regular_InternalAngleSum()
    {
        Assert.Equal(double.Pi, Polygon.Regular.InternalAngleSum(3.0), 5);
        Assert.Equal(2.0 * double.Pi, Polygon.Regular.InternalAngleSum(4.0), 5);
    }

    [Fact]
    public void Regular_InternalAngle()
    {
        Assert.Equal(double.Pi / 3.0, Polygon.Regular.InternalAngle(3.0), 5);
        Assert.Equal(double.Pi / 2.0, Polygon.Regular.InternalAngle(4.0), 5);
    }

    [Fact]
    public void Regular_ExternalAngleSum()
    {
        Assert.Equal(2.0 * double.Pi, Polygon.Regular.ExternalAngleSum<double>(), 5);
    }

    [Fact]
    public void Regular_ExternalAngle()
    {
        Assert.Equal(2.0 * double.Pi / 3.0, Polygon.Regular.ExternalAngle(3.0), 5);
    }
}
