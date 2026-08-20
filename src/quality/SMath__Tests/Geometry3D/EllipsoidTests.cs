namespace SMath.Geometry3D;

using System;
using Xunit;

public class EllipsoidTests
{
    [Fact]
    public void Volume()
    {
        var expectedVolume = (4d / 3d) * Math.PI * 1d * 2d * 3d;
        Assert.Equal(expectedVolume, Ellipsoid.EnclosedVolume(1d, 2d, 3d), 5);
        Assert.Equal(expectedVolume, Ellipsoid.Region.Volume.FromRadii(1d, 2d, 3d), 5);
    }

    [Fact]
    public void SphereDegenerateSurfaceArea()
    {
        // When r1 = r2 = r3 = r, surface area equals 4 * pi * r^2
        var expected = 4d * Math.PI * 1d * 1d;
        Assert.Equal(expected, Ellipsoid.SurfaceArea(1d, 1d, 1d), 5);
        Assert.Equal(expected, Ellipsoid.Surface.Area.FromRadii(1d, 1d, 1d), 5);
    }

    [Fact]
    public void TriaxialSurfaceArea()
    {
        // For r1=1, r2=2, r3=3, Knud Thomsen's formula gives approx 48.8821
        var area = Ellipsoid.SurfaceArea(1d, 2d, 3d);
        Assert.InRange(area, 48.5, 49.2);
    }
}
