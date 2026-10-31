using Xunit;

namespace SMath.Geometry2D;

public class AnnulusTests
{
    [Fact]
    public void Measures()
    {
        Assert.Equal(1d, Annulus.Width.FromRadii(2d, 3d));
        Assert.Equal(2d * Math.PI * 5d, Annulus.Perimeter.Length.FromRadii(2d, 3d), 12);
        Assert.Equal(5d * Math.PI, Annulus.Region.Area.FromRadii(2d, 3d), 12);
    }

    [Fact]
    public void AreaIsTheOuterDiscWithoutTheInnerDisc()
    {
        var expected = Circle.Region.Area.FromRadius(3d) - Circle.Region.Area.FromRadius(2d);

        Assert.Equal(expected, Annulus.Region.Area.FromRadii(2d, 3d), 12);
    }

    [Fact]
    public void AnnulusWithoutAHoleIsADisc()
    {
        Assert.Equal(Circle.Region.Area.FromRadius(3d), Annulus.Region.Area.FromRadii(0d, 3d), 12);
        Assert.Equal(Circle.Perimeter.Length.FromRadius(3d), Annulus.Perimeter.Length.FromRadii(0d, 3d), 12);
    }

    [Fact]
    public void NarrowAnnulusKeepsItsPrecision()
    {
        // radii exact in binary; pi (R^2 - r^2) would drop the w^2 term of R^2 = 1 + 2w + w^2 to rounding
        double width = Math.ScaleB(1d, -30);
        double expected = Math.PI * (2d + width) * width;

        Assert.Equal(expected, Annulus.Region.Area.FromRadii(1d, 1d + width), expected * 1e-15);
    }
}
