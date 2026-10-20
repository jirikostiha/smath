using Xunit;

namespace SMath.Geometry3D;

public class ConeTests
{
    [Fact]
    public void Measures()
    {
        Assert.Equal(5d, Cone.SlantHeight.FromRadius(3d, 4d), 12);
        Assert.Equal(Math.Atan(3d / 4d), Cone.HalfAngle.FromRadius(3d, 4d), 12);
        Assert.Equal(Math.PI * 3d * 8d, Cone.Surface.Area.FromRadius(3d, 4d), 12);
        Assert.Equal(Math.PI * 3d * 5d, Cone.Surface.LateralArea.FromRadius(3d, 4d), 12);
        Assert.Equal(Math.PI * 9d * 4d / 3d, Cone.Region.Volume.FromRadius(3d, 4d), 12);
    }

    [Fact]
    public void HeightInvertsTheVolume() => Assert.Equal(4d, Cone.Height.FromVolume(Cone.Region.Volume.FromRadius(3d, 4d), 3d), 12);
}
