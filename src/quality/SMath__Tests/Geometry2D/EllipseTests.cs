using Xunit;
using static System.Math;

namespace SMath.Geometry2D;

public class EllipseTests
{
    [Fact]
    public void Eccentricity_Circle_IsZero()
    {
        // for a circle major == minor => eccentricity == 0
        Assert.Equal(0d, Ellipse.Eccentricity.FromRadius(2d, 2d), 6);
    }

    [Theory]
    // e = sqrt(1 - b^2/a^2)
    [InlineData(2d, 1d)]
    [InlineData(5d, 3d)]
    [InlineData(10d, 6d)]
    public void Eccentricity_FromRadius(double major, double minor)
    {
        var expected = Sqrt(1d - (minor * minor) / (major * major));
        Assert.Equal(expected, Ellipse.Eccentricity.FromRadius(major, minor), 6);
    }
}
