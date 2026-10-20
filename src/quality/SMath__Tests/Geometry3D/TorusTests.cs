using Xunit;

namespace SMath.Geometry3D;

public class TorusTests
{
    [Fact]
    public void Measures()
    {
        Assert.Equal(4d * Math.PI * Math.PI * 3d, Torus.Surface.Area.FromRadii(3d, 1d), 12);
        Assert.Equal(2d * Math.PI * Math.PI * 3d * 4d, Torus.Region.Volume.FromRadii(3d, 2d), 12);
    }

    [Theory]
    [InlineData(0d, 0d)]
    [InlineData(1.3d, 2.1d)]
    [InlineData(-2d, 4d)]
    public void SurfacePointLiesOnTheSurface(double toroidalAngle, double poloidalAngle)
    {
        var (x, y, z) = Torus.Surface.Point.FromAngles(3d, 1d, toroidalAngle, poloidalAngle);

        double ring = Math.Sqrt((x * x) + (y * y)) - 3d;
        Assert.Equal(1d, (ring * ring) + (z * z), 12);
    }

    [Theory]
    [InlineData(3d, 0d, 0d, true)]     // center of the tube
    [InlineData(0d, -3.5d, 0.5d, true)]
    [InlineData(0d, 0d, 0d, false)]    // the hole
    [InlineData(3d, 0d, 1.5d, false)]
    public void RegionContainsPoint(double x, double y, double z, bool expected)
        => Assert.Equal(expected, Torus.Region.And.Point.Intersection.FromRadii(3d, 1d, (x, y, z)));
}
