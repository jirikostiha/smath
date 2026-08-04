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

    [Theory]
    [InlineData(1d)]
    [InlineData(2.5d)]
    public void Perimeter_Circle_IsExact(double radius)
    {
        // with equal radii the ellipse is a circle, the approximation must degrade to 2*pi*r
        Assert.Equal(2d * PI * radius, Ellipse.Perimeter.Length.FromRadius(radius, radius), 9);
    }

    [Theory]
    // Regression: the perimeter used the coarse pi*(3*(a+b)/2 - sqrt(a*b)) approximation,
    // which is over 3% off for flat ellipses. Ramanujan's second approximation is used now.
    [InlineData(2d, 1d)]
    [InlineData(5d, 3d)]
    [InlineData(3d, 2d)]
    [InlineData(10d, 1d)]
    public void Perimeter_FromRadius_IsCloseToArcLength(double major, double minor)
    {
        var expected = EllipseArcLength(major, minor);
        var actual = Ellipse.Perimeter.Length.FromRadius(major, minor);

        // Ramanujan's second approximation stays well below 0.1% of relative error
        Assert.True(Abs(actual - expected) / expected < 0.001d,
            $"perimeter {actual} is too far from the reference {expected}");
    }

    [Fact]
    public void Perimeter_GrowsWithTheMinorRadius()
    {
        var flat = Ellipse.Perimeter.Length.FromRadius(4d, 1d);
        var round = Ellipse.Perimeter.Length.FromRadius(4d, 3d);

        Assert.True(flat < round);
    }

    /// <summary> Midpoint integration of the ellipse arc length, used as an independent reference. </summary>
    private static double EllipseArcLength(double major, double minor)
    {
        const int steps = 200_000;
        var delta = (PI / 2d) / steps;
        var sum = 0d;
        for (int i = 0; i < steps; i++)
        {
            var t = (i + 0.5d) * delta;
            sum += Sqrt(Pow(major * Sin(t), 2) + Pow(minor * Cos(t), 2));
        }

        return 4d * sum * delta;
    }
}
