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
}
