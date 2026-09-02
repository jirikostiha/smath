using System;
using Xunit;

namespace SMath.Geometry2D;

public class GeometricVector2Tests
{
    [Fact]
    public void Magnitude()
    {
        Assert.Equal(1, GeometricVector2.Magnitude.FromCartesian(1d, 0d));
        Assert.Equal(1, GeometricVector2.Magnitude.FromCartesian(0d, 1d));
        Assert.Equal(Math.Sqrt(2), GeometricVector2.Magnitude.FromCartesian(1d, 1d));
    }

    [Fact]
    public void Normalized()
    {
        Assert.Equal((1d, 0d), GeometricVector2.Cartesian.Normalized(1d, 0d));
        Assert.Equal((0d, 1d), GeometricVector2.Cartesian.Normalized(0d, 1d));
        Assert.Equal(1 / Math.Sqrt(2d), GeometricVector2.Cartesian.Normalized(1d, 1d).X, 0.00000001);
        Assert.Equal(1 / Math.Sqrt(2d), GeometricVector2.Cartesian.Normalized(1d, 1d).Y, 0.00000001);
        Assert.Equal(2 / Math.Sqrt(8d), GeometricVector2.Cartesian.Normalized(2d, 2d).Y, 0.00000001);
    }

    [Fact]
    public void Normalized_ZeroVector()
    {
        Assert.Equal((0d, 0d), GeometricVector2.Cartesian.Normalized(0d, 0d));
        Assert.Equal((0d, 0d), GeometricVector2.Cartesian.Normalized((0d, 0d)));
        Assert.Equal((0f, 0f), GeometricVector2.Cartesian.Normalized(0f, 0f));
        Assert.Equal((0f, 0f), GeometricVector2.Cartesian.Normalized((0f, 0f)));
    }

    [Theory]
    [InlineData(5d, 3d, 1d, 1d)]
    [InlineData(-5d, 3d, -1d, 1d)]
    [InlineData(-5d, -3d, -1d, -1d)]
    [InlineData(5d, -3d, 1d, -1d)]
    [InlineData(0d, 0d, 0d, 0d)]
    [InlineData(5d, 0d, 1d, 0d)]
    [InlineData(0d, -3d, 0d, -1d)]
    public void Quadrantized(double x, double y, double ex, double ey)
    {
        Assert.Equal((ex, ey), GeometricVector2.Cartesian.Quadrantized(x, y));
        Assert.Equal((ex, ey), GeometricVector2.Cartesian.Quadrantized((x, y)));
#pragma warning disable CS0618
        Assert.Equal((ex, ey), GeometricVector2.Cartesian.Kvadrantized(x, y));
        Assert.Equal((ex, ey), GeometricVector2.Cartesian.Kvadrantized((x, y)));
#pragma warning restore CS0618
    }

    [Fact]
    public void Quadrantized_IntVector()
    {
        Assert.Equal((1, -1), GeometricVector2.Cartesian.Quadrantized((10, -20)));
        Assert.Equal((0, 0), GeometricVector2.Cartesian.Quadrantized((0, 0)));
        Assert.Equal((-1, 1), GeometricVector2.Cartesian.Quadrantized(-100, 50));
    }

    [Fact]
    public void Quadrantized_NegativeZero()
    {
        Assert.Equal((0d, 0d), GeometricVector2.Cartesian.Quadrantized(-0.0, 0.0));
        Assert.Equal((0d, 0d), GeometricVector2.Cartesian.Quadrantized(0.0, -0.0));
        Assert.Equal((0d, 0d), GeometricVector2.Cartesian.Quadrantized(-0.0, -0.0));
    }

    [Fact]
    public void DirectionVector()
    {
        Assert.Equal((1, 0), GeometricVector2.Direction.FromCartesian((0, 0), (1d, 0)));
        Assert.Equal((0, 1), GeometricVector2.Direction.FromCartesian((0, 0), (0, 1d)));
        Assert.Equal((1, 1), GeometricVector2.Direction.FromCartesian((0, 0), (1d, 1d)));
    }

    [Fact]
    public void Distance()
    {
        Assert.Equal(1, GeometricVector2.Distance.FromCartesian((0, 0), (1d, 0)));
        Assert.Equal(1, GeometricVector2.Distance.FromCartesian((0, 0), (0, 1d)));
        Assert.Equal(Math.Sqrt(2), GeometricVector2.Distance.FromCartesian((0, 0), (1d, 1d)));
    }

    [Theory]
    // Regression: PolarAngle used Atan(y/x), which is wrong for any vector with
    // negative x (it collapses quadrants II/III onto IV/I). Atan2 is required.
    [InlineData(1d, 0d, 0d)]                 // +x axis
    [InlineData(0d, 1d, Math.PI / 2d)]       // +y axis
    [InlineData(1d, 1d, Math.PI / 4d)]       // quadrant I
    [InlineData(-1d, 0d, Math.PI)]           // -x axis (was 0 with Atan)
    [InlineData(-1d, -1d, -3d * Math.PI / 4d)] // quadrant III (was +pi/4 with Atan)
    [InlineData(1d, -1d, -Math.PI / 4d)]     // quadrant IV
    public void PolarAngle(double x, double y, double expected)
    {
        Assert.Equal(expected, GeometricVector2.PolarAngle.FromCartesian(x, y), 6);
        Assert.Equal(expected, GeometricVector2.PolarAngle.FromCartesian((x, y)), 6);
    }

    [Theory]
    // Regression: the polar magnitude used the law of cosines at the angle between
    // the vectors, which yields the magnitude of their difference, not of their sum.
    [InlineData(1d, 1d, 0d, 2d)]              // same direction
    [InlineData(1d, 1d, Math.PI, 0d)]         // opposite directions
    [InlineData(3d, 4d, Math.PI / 2d, 5d)]    // perpendicular
    [InlineData(1d, 1d, Math.PI / 2d, 1.4142135623730951d)]
    public void MagnitudeFromTwoPolarVectors_IsMagnitudeOfSum(double magnitude1, double magnitude2, double angle, double expected)
    {
        Assert.Equal(expected, GeometricVector2.Magnitude.FromTwoPolarVectors(magnitude1, magnitude2, angle), 6);
    }

    [Theory]
    [InlineData(2d, Math.PI / 6d, 3d, 2d * Math.PI / 3d)]
    [InlineData(1d, 0d, 1d, Math.PI / 2d)]
    [InlineData(5d, -Math.PI / 4d, 2d, Math.PI)]
    public void MagnitudeFromPolarVectors_EqualsCartesianSum(double magnitude1, double angle1, double magnitude2, double angle2)
    {
        var cartesian1 = GeometricVector2.Cartesian.FromPolar(magnitude1, angle1);
        var cartesian2 = GeometricVector2.Cartesian.FromPolar(magnitude2, angle2);
        var expected = GeometricVector2.Magnitude.FromCartesianVectors(cartesian1, cartesian2);

        Assert.Equal(
            expected,
            GeometricVector2.Magnitude.FromPolarVectors((magnitude1, angle1), (magnitude2, angle2)),
            6);
    }

    [Fact]
    public void NormalVector()
    {
        Assert.Equal((0d, 1d), GeometricVector2.Normal1.FromCartesian(1d, 0d));
        Assert.Equal((-1d, 1d), GeometricVector2.Normal1.FromCartesian(1d, 1d));

        Assert.Equal((0d, -1d), GeometricVector2.Normal2.FromCartesian(1d, 0d));
        Assert.Equal((1d, -1d), GeometricVector2.Normal2.FromCartesian(1d, 1d));
    }

    [Theory]
    [InlineData(1d, 0d, 0d, 1d, 0d)]                 // no rotation
    [InlineData(1d, 0d, Math.PI / 2d, 0d, 1d)]       // +x -> +y
    [InlineData(1d, 0d, Math.PI, -1d, 0d)]           // +x -> -x
    [InlineData(0d, 1d, Math.PI / 2d, -1d, 0d)]      // +y -> -x
    public void RotationAroundOrigin(double x, double y, double angle, double ex, double ey)
    {
        var (rx, ry) = GeometricVector2.Rotation.FromCartesian((x, y), angle);
        Assert.Equal(ex, rx, 9);
        Assert.Equal(ey, ry, 9);
    }

    [Theory]
    // Ported from the retired `vector` branch CartesianCoords.Rotate cases.
    [InlineData(0d, 0d, 0d, 0d, 0d, 0d, 0d)]         // zero point, zero center
    [InlineData(0d, 0d, 0d, 0d, Math.PI, 0d, 0d)]    // zero point, zero center
    [InlineData(1d, 0d, 0d, 0d, 0d, 1d, 0d)]         // around origin
    [InlineData(2d, 0d, 0d, 0d, Math.PI, -2d, 0d)]   // around origin
    [InlineData(4d, 4d, 0d, 0d, Math.PI, -4d, -4d)]  // around origin
    [InlineData(2d, 1d, 1d, 1d, Math.PI, 0d, 1d)]    // around Q1 point
    [InlineData(3d, -3d, 3d, 0d, Math.PI, 3d, 3d)]   // around Q4 point
    public void RotationAroundCenter(double x, double y, double cx, double cy, double angle, double ex, double ey)
    {
        var (rx, ry) = GeometricVector2.Rotation.FromCartesian((x, y), (cx, cy), angle);
        Assert.Equal(ex, rx, 9);
        Assert.Equal(ey, ry, 9);
    }

    [Theory]
    [InlineData(1d, 1d, 0d, 1d, -1d)]     // across x-axis
    [InlineData(1d, 1d, 2d, 1d, 3d)]      // across y = 2
    [InlineData(3d, -4d, -1d, 3d, 2d)]    // across y = -1
    public void ReflectionToX(double x, double y, double lineY, double ex, double ey)
    {
        var (rx, ry) = GeometricVector2.Reflection.ToX.FromCartesian((x, y), lineY);
        Assert.Equal(ex, rx, 9);
        Assert.Equal(ey, ry, 9);
    }

    [Theory]
    [InlineData(1d, 1d, 0d, -1d, 1d)]     // across y-axis
    [InlineData(1d, 1d, 2d, 3d, 1d)]      // across x = 2
    [InlineData(-4d, 3d, -1d, 2d, 3d)]    // across x = -1
    public void ReflectionToY(double x, double y, double lineX, double ex, double ey)
    {
        var (rx, ry) = GeometricVector2.Reflection.ToY.FromCartesian((x, y), lineX);
        Assert.Equal(ex, rx, 9);
        Assert.Equal(ey, ry, 9);
    }

    [Theory]
    [InlineData(1d, 1d, 1d, 0d, 1d, -1d)] // across x-axis direction
    [InlineData(1d, 1d, 0d, 1d, -1d, 1d)] // across y-axis direction
    [InlineData(1d, 0d, 1d, 1d, 0d, 1d)]  // across diagonal y = x
    [InlineData(2d, 3d, 0d, 0d, -2d, -3d)] // zero direction vector -> point reflection about origin
    [InlineData(-5d, 7d, 0d, 0d, 5d, -7d)]
    [InlineData(0d, 0d, 0d, 0d, 0d, 0d)]
    public void ReflectionToLine_ThroughOrigin(double x, double y, double dx, double dy, double ex, double ey)
    {
        var (rx, ry) = GeometricVector2.Reflection.ToLine.FromCartesian((x, y), (dx, dy));
        Assert.Equal(ex, rx, 9);
        Assert.Equal(ey, ry, 9);
    }

    [Fact]
    public void ReflectionToLine_ThroughOrigin_ZeroDirection_Decimal()
    {
        var result = GeometricVector2.Reflection.ToLine.FromCartesian((3m, -4m), (0m, 0m));
        Assert.Equal((-3m, 4m), result);
    }

    [Theory]
    [InlineData(2d, 3d, 0d, 0d, 1d, 0d, 2d, -3d)]  // across x-axis (two points)
    [InlineData(2d, 3d, 0d, 0d, 0d, 1d, -2d, 3d)]  // across y-axis (two points)
    [InlineData(1d, 0d, 0d, 0d, 1d, 1d, 0d, 1d)]   // across y = x
    [InlineData(3d, 3d, 1d, 1d, 1d, 1d, -1d, -1d)] // degenerate line -> point reflection about (1,1)
    public void ReflectionToLine_TwoPoints(double x, double y, double ax, double ay, double bx, double by, double ex, double ey)
    {
        var (rx, ry) = GeometricVector2.Reflection.ToLine.FromCartesian((x, y), (ax, ay), (bx, by));
        Assert.Equal(ex, rx, 9);
        Assert.Equal(ey, ry, 9);
    }
}
