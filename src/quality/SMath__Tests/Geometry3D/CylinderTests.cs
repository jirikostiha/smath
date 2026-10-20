using Xunit;

namespace SMath.Geometry3D;

public class CylinderTests
{
    [Fact]
    public void Measures()
    {
        Assert.Equal(2d * Math.PI * 2d * 5d, Cylinder.Surface.Area.FromRadius(2d, 3d), 12);
        Assert.Equal(2d * Math.PI * 2d * 3d, Cylinder.Surface.LateralArea.FromRadius(2d, 3d), 12);
        Assert.Equal(Math.PI * 4d * 3d, Cylinder.Region.Volume.FromRadius(2d, 3d), 12);
    }

    [Fact]
    public void RadiusAndHeightInvertTheVolume()
    {
        var volume = Cylinder.Region.Volume.FromRadius(2d, 3d);

        Assert.Equal(2d, Cylinder.Radius.FromVolume(volume, 3d), 12);
        Assert.Equal(3d, Cylinder.Height.FromVolume(volume, 2d), 12);
    }
}
