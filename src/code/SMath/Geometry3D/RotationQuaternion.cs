using System.Numerics;

using SMath.Geometry4D;

namespace SMath.Geometry3D;

/// <summary> 
/// Rotation in space as a unit quaternion <c>(X, Y, Z, W)</c> with <c>W</c> as the scalar part.
/// Unlike an axis and an angle it composes rotations and interpolates them without a singularity.
/// A quaternion and its negative are the same rotation.
/// </summary>
/// <remarks> <a href="https://en.wikipedia.org/wiki/Quaternions_and_spatial_rotation">Wikipedia</a> </remarks>
public static class RotationQuaternion
{
    /// <summary> Rotation around a unit axis by an angle in radians, counterclockwise when looking against the axis. The axis is expected to be normalized. </summary>
    public static (N X, N Y, N Z, N W) FromAxisAngle<N>((N X, N Y, N Z) unitAxis, N angle)
        where N : ITrigonometricFunctions<N>
    {
        var (sin, cos) = N.SinCos(angle / (N.One + N.One));
        return (unitAxis.X * sin, unitAxis.Y * sin, unitAxis.Z * sin, cos);
    }

    /// <summary> Rotation of the second quaternion followed by the rotation of the first one, the Hamilton product. </summary>
    public static (N X, N Y, N Z, N W) Multiply<N>((N X, N Y, N Z, N W) first, (N X, N Y, N Z, N W) second)
        where N : INumberBase<N>
        => ((first.W * second.X) + (first.X * second.W) + (first.Y * second.Z) - (first.Z * second.Y),
            (first.W * second.Y) - (first.X * second.Z) + (first.Y * second.W) + (first.Z * second.X),
            (first.W * second.Z) + (first.X * second.Y) - (first.Y * second.X) + (first.Z * second.W),
            (first.W * second.W) - (first.X * second.X) - (first.Y * second.Y) - (first.Z * second.Z));

    /// <summary> Opposite rotation. </summary>
    public static (N X, N Y, N Z, N W) Conjugate<N>((N X, N Y, N Z, N W) quaternion)
        where N : INumberBase<N>
        => (-quaternion.X, -quaternion.Y, -quaternion.Z, quaternion.W);

    /// <summary> Rotate a vector by a unit quaternion, <c>v' = v + w * t + u × t</c> with <c>t = 2 * (u × v)</c>. </summary>
    public static (N X, N Y, N Z) Rotate<N>((N X, N Y, N Z, N W) unitQuaternion, (N X, N Y, N Z) vector)
        where N : INumberBase<N>
    {
        var u = (unitQuaternion.X, unitQuaternion.Y, unitQuaternion.Z);
        var cross = GeometricVector3.CrossProduct.FromCartesian(u, vector);
        var t = (X: cross.X + cross.X, Y: cross.Y + cross.Y, Z: cross.Z + cross.Z);
        var ut = GeometricVector3.CrossProduct.FromCartesian(u, t);
        return (vector.X + (unitQuaternion.W * t.X) + ut.X,
                vector.Y + (unitQuaternion.W * t.Y) + ut.Y,
                vector.Z + (unitQuaternion.W * t.Z) + ut.Z);
    }

    /// <summary> Rotation at a parameter between two unit quaternions along the shortest arc. Parameter 0 gives the first, 1 the second rotation. </summary>
    public static (N X, N Y, N Z, N W) Slerp<N>((N X, N Y, N Z, N W) from, (N X, N Y, N Z, N W) to, N t)
        where N : IFloatingPointIeee754<N>
    {
        // the negative of the end is the same rotation; take the one that is nearer, then the arc is unique
        if (GeometricVector4.DotProduct.FromCartesian(from, to) < N.Zero)
        {
            to = (-to.X, -to.Y, -to.Z, -to.W);
        }

        var u = GeometricVector4.Cartesian.Normalized(GeometricVector4.Rejection.FromCartesian(to, from));
        var (sin, cos) = N.SinCos(t * GeometricVector4.Angle.FromCartesian(from, to));
        return ((from.X * cos) + (u.X * sin), (from.Y * cos) + (u.Y * sin), (from.Z * cos) + (u.Z * sin), (from.W * cos) + (u.W * sin));
    }
}
