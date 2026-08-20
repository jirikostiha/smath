namespace SMath.Geometry3D;

using System;
using Xunit;

public class SphericalShellTests
{
    [Fact]
    public void SurfaceAreas()
    {
        Assert.Equal(4d * Math.PI, SphericalShell.InnerSurfaceArea(1d));
        Assert.Equal(16d * Math.PI, SphericalShell.OuterSurfaceArea(2d));
        Assert.Equal(20d * Math.PI, SphericalShell.TotalSurfaceArea(1d, 2d));
        Assert.Equal(20d * Math.PI, SphericalShell.Surface.Area.Total(1d, 2d));
    }

    [Fact]
    public void Volume()
    {
        var expectedVolume = (4d / 3d) * Math.PI * (8d - 1d);
        Assert.Equal(expectedVolume, SphericalShell.Volume(1d, 2d), 5);
        Assert.Equal(expectedVolume, SphericalShell.Region.Volume.FromRadii(1d, 2d), 5);
    }

    [Fact]
    public void Thickness()
    {
        Assert.Equal(1d, SphericalShell.Thickness(1d, 2d));
    }
}
