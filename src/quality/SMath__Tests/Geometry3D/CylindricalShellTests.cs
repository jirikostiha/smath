using Xunit;

namespace SMath.Geometry3D;

public class CylindricalShellTests
{
    [Fact]
    public void Measures()
    {
        Assert.Equal(1d, CylindricalShell.Thickness.FromRadii(2d, 3d));

        // lateral 2 pi (2 + 3) 4, ends 2 pi (9 - 4)
        Assert.Equal(50d * Math.PI, CylindricalShell.Surface.Area.FromRadii(2d, 3d, 4d), 12);
        Assert.Equal(20d * Math.PI, CylindricalShell.Region.Volume.FromRadii(2d, 3d, 4d), 12);
    }

    [Fact]
    public void VolumeIsTheOuterCylinderWithoutTheInnerCylinder()
    {
        var expected = Cylinder.Region.Volume.FromRadius(3d, 4d) - Cylinder.Region.Volume.FromRadius(2d, 4d);

        Assert.Equal(expected, CylindricalShell.Region.Volume.FromRadii(2d, 3d, 4d), 12);
    }

    [Fact]
    public void ShellWithoutAHoleIsACylinder()
    {
        Assert.Equal(Cylinder.Region.Volume.FromRadius(3d, 4d), CylindricalShell.Region.Volume.FromRadii(0d, 3d, 4d), 12);
        Assert.Equal(Cylinder.Surface.Area.FromRadius(3d, 4d), CylindricalShell.Surface.Area.FromRadii(0d, 3d, 4d), 12);
    }
}
