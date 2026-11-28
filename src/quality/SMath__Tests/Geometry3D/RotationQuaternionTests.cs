using System;
using Xunit;

namespace SMath.Geometry3D;

public class RotationQuaternionTests
{
    private const int Precision = 12;

    private static readonly (double X, double Y, double Z) _axis = GeometricVector3.Cartesian.Normalized((1d, 2d, -0.5d));
    private static readonly (double X, double Y, double Z) _vector = (1d, -2d, 3d);

    private static void AssertClose((double X, double Y, double Z) expected, (double X, double Y, double Z) actual)
    {
        Assert.Equal(expected.X, actual.X, Precision);
        Assert.Equal(expected.Y, actual.Y, Precision);
        Assert.Equal(expected.Z, actual.Z, Precision);
    }

    [Fact]
    public void FromAxisAngleRotatesLikeTheRodriguesRotation()
    {
        var rotated = RotationQuaternion.Rotate(RotationQuaternion.FromAxisAngle(_axis, 0.7d), _vector);

        AssertClose(GeometricVector3.Rotation.FromCartesian(_vector, _axis, 0.7d), rotated);
    }

    [Fact]
    public void MultiplyAppliesTheSecondRotationFirst()
    {
        var first = RotationQuaternion.FromAxisAngle((0d, 0d, 1d), 0.4d);
        var second = RotationQuaternion.FromAxisAngle((1d, 0d, 0d), -1.1d);

        var composed = RotationQuaternion.Rotate(RotationQuaternion.Multiply(first, second), _vector);

        AssertClose(RotationQuaternion.Rotate(first, RotationQuaternion.Rotate(second, _vector)), composed);
    }

    [Fact]
    public void MultiplyWithTheConjugateIsTheIdentity()
    {
        var quaternion = RotationQuaternion.FromAxisAngle(_axis, 2.3d);

        var product = RotationQuaternion.Multiply(quaternion, RotationQuaternion.Conjugate(quaternion));

        Assert.Equal(0d, product.X, Precision);
        Assert.Equal(0d, product.Y, Precision);
        Assert.Equal(0d, product.Z, Precision);
        Assert.Equal(1d, product.W, Precision);
    }

    [Fact]
    public void SlerpAtTheEndsGivesTheEnds()
    {
        var from = RotationQuaternion.FromAxisAngle(_axis, 0.3d);
        var to = RotationQuaternion.FromAxisAngle(_axis, 1.9d);

        AssertClose(RotationQuaternion.Rotate(from, _vector), RotationQuaternion.Rotate(RotationQuaternion.Slerp(from, to, 0d), _vector));
        AssertClose(RotationQuaternion.Rotate(to, _vector), RotationQuaternion.Rotate(RotationQuaternion.Slerp(from, to, 1d), _vector));
    }

    [Fact]
    public void SlerpOfTwoRotationsAboutOneAxisHalvesTheAngle()
    {
        var identity = (0d, 0d, 0d, 1d);
        var to = RotationQuaternion.FromAxisAngle(_axis, 1.2d);

        var halfway = RotationQuaternion.Slerp(identity, to, 0.5d);

        AssertClose(GeometricVector3.Rotation.FromCartesian(_vector, _axis, 0.6d), RotationQuaternion.Rotate(halfway, _vector));
    }

    [Fact]
    public void SlerpToTheNegativeOfAQuaternionTakesTheSameWay()
    {
        var from = RotationQuaternion.FromAxisAngle(_axis, 0.2d);
        var to = RotationQuaternion.FromAxisAngle(_axis, 1.4d);
        var negated = (-to.X, -to.Y, -to.Z, -to.W);

        AssertClose(
            RotationQuaternion.Rotate(RotationQuaternion.Slerp(from, to, 0.3d), _vector),
            RotationQuaternion.Rotate(RotationQuaternion.Slerp(from, negated, 0.3d), _vector));
    }

    [Fact]
    public void SlerpOfEqualQuaternionsIsTheQuaternion()
    {
        var quaternion = RotationQuaternion.FromAxisAngle(_axis, 0.9d);

        var result = RotationQuaternion.Slerp(quaternion, quaternion, 0.4d);

        Assert.Equal(quaternion.X, result.X, Precision);
        Assert.Equal(quaternion.W, result.W, Precision);
    }
}
