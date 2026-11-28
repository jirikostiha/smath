using System;
using Xunit;

namespace SMath.Geometry4D;

public class GeometricVector4Tests
{
    [Fact]
    public void MagnitudeFromCartesian() => Assert.Equal(5d, GeometricVector4.Magnitude.FromCartesian((1d, 2d, 2d, 4d)), 12);

    [Fact]
    public void MagnitudeSquaredIsTheSquareOfTheMagnitude() => Assert.Equal(25d, GeometricVector4.MagnitudeSquared.FromCartesian((1d, 2d, 2d, 4d)));

    [Fact]
    public void CartesianFromCartesianVectorsSumsTheVectors() =>
        Assert.Equal((4d, 0d, 2d, 6d), GeometricVector4.Cartesian.FromCartesianVectors((1d, 2d, 3d, 4d), (3d, -2d, -1d, 2d)));

    [Fact]
    public void NormalizedHasTheUnitLength()
    {
        var vector = GeometricVector4.Cartesian.Normalized((1d, -2d, 0.5d, 3d));

        Assert.Equal(1d, GeometricVector4.Magnitude.FromCartesian(vector), 12);
    }

    [Fact]
    public void NormalizedOfZeroIsZero() => Assert.Equal((0d, 0d, 0d, 0d), GeometricVector4.Cartesian.Normalized((0d, 0d, 0d, 0d)));

    [Fact]
    public void DotProductOfPerpendicularVectorsIsZero() => Assert.Equal(0d, GeometricVector4.DotProduct.FromCartesian((1d, 2d, 0d, 0d), (-2d, 1d, 5d, 7d)));

    [Fact]
    public void DotProductFromCartesian() => Assert.Equal(20d, GeometricVector4.DotProduct.FromCartesian((1d, 2d, 3d, 4d), (4d, 3d, 2d, 1d)));

    [Fact]
    public void AngleOfPerpendicularVectorsIsAQuarterTurn() =>
        Assert.Equal(Math.PI / 2d, GeometricVector4.Angle.FromCartesian((0d, 0d, 3d, 0d), (0d, 0d, 0d, 2d)), 12);

    [Fact]
    public void AngleOfOppositeVectorsIsAHalfTurn() => Assert.Equal(Math.PI, GeometricVector4.Angle.FromCartesian((1d, 2d, 3d, 4d), (-1d, -2d, -3d, -4d)), 12);

    [Fact]
    public void AngleOfNearlyParallelVectorsStaysAccurate() =>
        Assert.Equal(1e-9d, GeometricVector4.Angle.FromCartesian((1d, 0d, 0d, 0d), (1d, 1e-9d, 0d, 0d)), 15);

    [Fact]
    public void AngleWithAZeroVectorIsZero() => Assert.Equal(0d, GeometricVector4.Angle.FromCartesian((0d, 0d, 0d, 0d), (1d, 2d, 3d, 4d)));

    [Fact]
    public void DistanceFromCartesian() => Assert.Equal(5d, GeometricVector4.Distance.FromCartesian((1d, 1d, 1d, 1d), (2d, 3d, 3d, 5d)), 12);

    [Fact]
    public void DirectionFromCartesian() => Assert.Equal((1d, 2d, 2d, 4d), GeometricVector4.Direction.FromCartesian((1d, 1d, 1d, 1d), (2d, 3d, 3d, 5d)));

    [Fact]
    public void ProjectionIsThePartParallelToTheDirection() =>
        Assert.Equal((0d, 0d, 0d, 4d), GeometricVector4.Projection.FromCartesian((1d, 2d, 3d, 4d), (0d, 0d, 0d, 2d)));

    [Fact]
    public void RejectionIsThePartPerpendicularToTheDirection() =>
        Assert.Equal((1d, 2d, 3d, 0d), GeometricVector4.Rejection.FromCartesian((1d, 2d, 3d, 4d), (0d, 0d, 0d, 2d)));

    [Fact]
    public void ProjectionAndRejectionOfAZeroDirection()
    {
        Assert.Equal((0d, 0d, 0d, 0d), GeometricVector4.Projection.FromCartesian((1d, 2d, 3d, 4d), (0d, 0d, 0d, 0d)));
        Assert.Equal((1d, 2d, 3d, 4d), GeometricVector4.Rejection.FromCartesian((1d, 2d, 3d, 4d), (0d, 0d, 0d, 0d)));
    }
}
