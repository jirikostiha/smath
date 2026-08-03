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

    [Fact]
    public void NormalVector()
    {
        Assert.Equal((0d, 1d), GeometricVector2.Normal1.FromCartesian(1d, 0d));
        Assert.Equal((-1d, 1d), GeometricVector2.Normal1.FromCartesian(1d, 1d));

        Assert.Equal((0d, -1d), GeometricVector2.Normal2.FromCartesian(1d, 0d));
        Assert.Equal((1d, -1d), GeometricVector2.Normal2.FromCartesian(1d, 1d));
    }
}
