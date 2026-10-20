namespace SMath.Geometry3D;

using System;
using Xunit;

public class GeometricVector3Tests
{
    [Fact]
    public void MagnitudeFromCartesian() => Assert.Equal(7d, GeometricVector3.Magnitude.FromCartesian((2d, 3d, 6d)), 10);

    [Fact]
    public void CrossProductOfXAndYIsZ() => Assert.Equal((0d, 0d, 1d), GeometricVector3.CrossProduct.FromCartesian((1d, 0d, 0d), (0d, 1d, 0d)));

    [Fact]
    public void CrossProductIsAntiCommutative()
    {
        var forward = GeometricVector3.CrossProduct.FromCartesian((1d, 2d, 3d), (-4d, 0.5d, 2d));
        var backward = GeometricVector3.CrossProduct.FromCartesian((-4d, 0.5d, 2d), (1d, 2d, 3d));

        Assert.Equal((-backward.X, -backward.Y, -backward.Z), forward);
    }

    [Theory]
    [InlineData(1d, 0d, 0d)]
    [InlineData(0d, 1d, 0d)]
    [InlineData(0d, 0d, -3d)]
    [InlineData(1d, 2d, 3d)]
    [InlineData(-0.3d, 0.1d, 0.9d)]
    [InlineData(1e-20d, -2e-20d, 5e-21d)]
    public void NormalIsPerpendicularAndOfTheSameLength(double x, double y, double z)
    {
        var normal = GeometricVector3.Normal.FromCartesian((x, y, z));

        double length = GeometricVector3.Magnitude.FromCartesian((x, y, z));
        Assert.Equal(0d, ((normal.X * x) + (normal.Y * y) + (normal.Z * z)) / (length * length), 10);
        Assert.Equal(length, GeometricVector3.Magnitude.FromCartesian(normal), length * 1e-12);
    }

    [Fact]
    public void NormalOfZeroVectorIsZero() => Assert.Equal((0d, 0d, 0d), GeometricVector3.Normal.FromCartesian((0d, 0d, 0d)));

    [Fact]
    public void NormalIsPerpendicularToTheAxisTheVectorLeansOnLeast()
    {
        var normal = GeometricVector3.Normal.FromCartesian((3d, 0.5d, 2d));

        Assert.Equal(0d, normal.Y, 10);
    }

    [Fact]
    public void NormalWorksWithSinglePrecision()
    {
        var normal = GeometricVector3.Normal.FromCartesian((1f, 2f, 3f));

        Assert.Equal(0f, (normal.X * 1f) + (normal.Y * 2f) + (normal.Z * 3f), 5);
        Assert.Equal(MathF.Sqrt(14f), GeometricVector3.Magnitude.FromCartesian(normal), 5);
    }

    [Fact]
    public void SphericalRoundTrip()
    {
        var (magnitude, polar, azimuth) = GeometricVector3.Spherical.FromCartesian((1d, -2d, 2d));
        var (x, y, z) = GeometricVector3.Cartesian.FromSpherical(magnitude, polar, azimuth);

        Assert.Equal(3d, magnitude, 12);
        Assert.Equal(Math.Acos(2d / 3d), polar, 12);
        Assert.Equal(Math.Atan2(-2d, 1d), azimuth, 12);
        Assert.Equal(1d, x, 12);
        Assert.Equal(-2d, y, 12);
        Assert.Equal(2d, z, 12);
    }

    [Fact]
    public void ComponentsFromSphericalMatchTheVector()
    {
        var vector = GeometricVector3.Cartesian.FromSpherical(2d, 0.7d, -1.2d);

        Assert.Equal(vector.X, GeometricVector3.X.FromSpherical(2d, 0.7d, -1.2d), 12);
        Assert.Equal(vector.Y, GeometricVector3.Y.FromSpherical(2d, 0.7d, -1.2d), 12);
        Assert.Equal(vector.Z, GeometricVector3.Z.FromSpherical(2d, 0.7d), 12);
    }

    [Fact]
    public void SumAndMagnitudeOfVectors()
    {
        Assert.Equal((3d, 3d, 6d), GeometricVector3.Cartesian.FromCartesianVectors((1d, 2d, 3d), (2d, 1d, 3d)));
        Assert.Equal(7d, GeometricVector3.Magnitude.FromCartesianVectors((2d, 0d, 6d), (0d, 3d, 0d)), 12);
        Assert.Equal(7d, GeometricVector3.Magnitude.FromCartesian(2d, 3d, 6d), 12);
    }

    [Fact]
    public void NormalizedIsUnitOrZero()
    {
        Assert.Equal((2d / 7d, 3d / 7d, 6d / 7d), GeometricVector3.Cartesian.Normalized((2d, 3d, 6d)));
        Assert.Equal((0d, 0d, 0d), GeometricVector3.Cartesian.Normalized(0d, 0d, 0d));
    }

    [Fact]
    public void OctantizedKeepsTheSigns() => Assert.Equal((1d, 0d, -1d), GeometricVector3.Cartesian.Octantized((3d, 0d, -0.5d)));

    [Fact]
    public void DotAndTripleProduct()
    {
        Assert.Equal(11d, GeometricVector3.DotProduct.FromCartesian((1d, 2d, 3d), (4d, -1d, 3d)));
        Assert.Equal(1d, GeometricVector3.TripleProduct.FromCartesian((1d, 0d, 0d), (0d, 1d, 0d), (0d, 0d, 1d)));
        Assert.Equal(-1d, GeometricVector3.TripleProduct.FromCartesian((0d, 1d, 0d), (1d, 0d, 0d), (0d, 0d, 1d)));
    }

    [Theory]
    [InlineData(1d, 0d, 0d, 0d, 1d, 0d, Math.PI / 2)]
    [InlineData(1d, 1d, 0d, 2d, 2d, 0d, 0d)]
    [InlineData(1d, 0d, 0d, -3d, 0d, 0d, Math.PI)]
    public void AngleBetweenVectors(double x1, double y1, double z1, double x2, double y2, double z2, double expected)
        => Assert.Equal(expected, GeometricVector3.Angle.FromCartesian((x1, y1, z1), (x2, y2, z2)), 12);

    [Fact]
    public void DistanceAndDirection()
    {
        Assert.Equal(7d, GeometricVector3.Distance.FromCartesian((1d, 1d, 1d), (3d, 4d, 7d)), 12);
        Assert.Equal((2d, 3d, 6d), GeometricVector3.Direction.FromCartesian((1d, 1d, 1d), (3d, 4d, 7d)));
    }

    [Fact]
    public void ProjectionKeepsTheParallelPart()
    {
        Assert.Equal((3d, 0d, 0d), GeometricVector3.Projection.FromCartesian((3d, 4d, 5d), (2d, 0d, 0d)));
        Assert.Equal((0d, 0d, 0d), GeometricVector3.Projection.FromCartesian((3d, 4d, 5d), (0d, 0d, 0d)));
    }

    [Fact]
    public void ReflectionFlipsTheNormalComponent()
    {
        Assert.Equal((1d, 2d, -3d), GeometricVector3.Reflection.FromCartesian((1d, 2d, 3d), (0d, 0d, 1d)));
        Assert.Equal((1d, 2d, -1d), GeometricVector3.Reflection.ToXY.FromCartesian((1d, 2d, 3d), 1d));
        Assert.Equal((1d, 0d, 3d), GeometricVector3.Reflection.ToXZ.FromCartesian((1d, 2d, 3d), 1d));
        Assert.Equal((1d, 2d, 3d), GeometricVector3.Reflection.ToYZ.FromCartesian((1d, 2d, 3d), 1d));
    }

    [Fact]
    public void RotationAroundZMatchesThePlanarRotation()
    {
        var rotated = GeometricVector3.Rotation.FromCartesian((1d, 2d, 5d), (0d, 0d, 1d), 0.4d);
        var planar = Geometry2D.GeometricVector2.Rotation.FromCartesian((1d, 2d), 0.4d);

        Assert.Equal(planar.X, rotated.X, 12);
        Assert.Equal(planar.Y, rotated.Y, 12);
        Assert.Equal(5d, rotated.Z, 12);
    }

    [Fact]
    public void RotationAroundTheDiagonalCyclesTheAxes()
    {
        var axis = GeometricVector3.Cartesian.Normalized((1d, 1d, 1d));
        var rotated = GeometricVector3.Rotation.FromCartesian((2d, 1d, 1d), (1d, 1d, 1d), axis, 2d * Math.PI / 3d);

        Assert.Equal(1d, rotated.X, 12);
        Assert.Equal(2d, rotated.Y, 12);
        Assert.Equal(1d, rotated.Z, 12);
    }
}
