using System;
using Xunit;

namespace SMath.Geometry3D;

public class Interpolation3Tests
{
    private const int Precision = 12;

    [Fact]
    public void LinearPointAtAParameterIsBetweenTheEnds()
    {
        Assert.Equal((2d, 0d, -1d), Interpolation3.Linear.Point((1d, -2d, 0d), (3d, 2d, -2d), 0.5d));
        Assert.Equal((1d, -2d, 0d), Interpolation3.Linear.Point((1d, -2d, 0d), (3d, 2d, -2d), 0d));
        Assert.Equal((3d, 2d, -2d), Interpolation3.Linear.Point((1d, -2d, 0d), (3d, 2d, -2d), 1d));
    }

    [Fact]
    public void SphericalHalfwayBetweenAxesIsTheirBisector()
    {
        var (x, y, z) = Interpolation3.Spherical.Eval((1d, 0d, 0d), (0d, 1d, 0d), 0.5d);

        Assert.Equal(Math.Sqrt(0.5d), x, Precision);
        Assert.Equal(Math.Sqrt(0.5d), y, Precision);
        Assert.Equal(0d, z, Precision);
    }

    [Theory]
    [InlineData(0d)]
    [InlineData(0.25d)]
    [InlineData(0.8d)]
    [InlineData(1d)]
    public void SphericalKeepsTheUnitLength(double t)
    {
        var from = GeometricVector3.Cartesian.Normalized((1d, 2d, 3d));
        var to = GeometricVector3.Cartesian.Normalized((-2d, 0.5d, 1d));

        var result = Interpolation3.Spherical.Eval(from, to, t);

        Assert.Equal(1d, GeometricVector3.Magnitude.FromCartesian(result), Precision);
    }

    [Fact]
    public void SphericalTurnsByAnAngleProportionalToTheParameter()
    {
        var from = GeometricVector3.Cartesian.Normalized((1d, 2d, 3d));
        var to = GeometricVector3.Cartesian.Normalized((-2d, 0.5d, 1d));

        var result = Interpolation3.Spherical.Eval(from, to, 0.3d);

        Assert.Equal(0.3d * GeometricVector3.Angle.FromCartesian(from, to), GeometricVector3.Angle.FromCartesian(from, result), Precision);
    }

    [Fact]
    public void SphericalOfOppositeVectorsGoesAroundThroughAPerpendicularUnitVector()
    {
        var from = GeometricVector3.Cartesian.Normalized((1d, 2d, 3d));
        var to = (X: -from.X, Y: -from.Y, Z: -from.Z);

        var halfway = Interpolation3.Spherical.Eval(from, to, 0.5d);
        var end = Interpolation3.Spherical.Eval(from, to, 1d);

        Assert.Equal(1d, GeometricVector3.Magnitude.FromCartesian(halfway), Precision);
        Assert.Equal(0d, GeometricVector3.DotProduct.FromCartesian(halfway, from), Precision);
        Assert.Equal(to.X, end.X, Precision);
        Assert.Equal(to.Y, end.Y, Precision);
        Assert.Equal(to.Z, end.Z, Precision);
    }

    [Fact]
    public void SphericalOfEqualVectorsIsTheVector()
    {
        var vector = GeometricVector3.Cartesian.Normalized((1d, 2d, 3d));

        var result = Interpolation3.Spherical.Eval(vector, vector, 0.6d);

        Assert.Equal(vector.X, result.X, Precision);
        Assert.Equal(vector.Y, result.Y, Precision);
        Assert.Equal(vector.Z, result.Z, Precision);
    }
}
