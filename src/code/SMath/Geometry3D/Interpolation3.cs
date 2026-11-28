using System.Numerics;

using Scalar = SMath.Geometry2D.Interpolation.Linear;

namespace SMath.Geometry3D;

/// <summary> Interpolation of points and directions in three dimensions. </summary>
/// <remarks> <a href="https://en.wikipedia.org/wiki/Interpolation">Wikipedia</a> </remarks>
public static class Interpolation3
{
    /// <summary> Linear interpolation. </summary>
    /// <remarks> <a href="https://en.wikipedia.org/wiki/Linear_interpolation">Wikipedia</a> </remarks>
    public static class Linear
    {
        /// <summary> Point at a parameter between two points. Parameter 0 gives the first, 1 the second point. </summary>
        public static (N X, N Y, N Z) Point<N>((N X, N Y, N Z) from, (N X, N Y, N Z) to, N t)
            where N : INumberBase<N>
            => (Scalar.Eval(from.X, to.X, t), Scalar.Eval(from.Y, to.Y, t), Scalar.Eval(from.Z, to.Z, t));
    }

    /// <summary> Spherical linear interpolation, constant angular speed along the arc between two directions. </summary>
    /// <remarks> <a href="https://en.wikipedia.org/wiki/Slerp">Wikipedia</a> </remarks>
    public static class Spherical
    {
        /// <summary>
        /// Unit vector at a parameter between two unit vectors along the shortest arc, <c>cos(t * angle) * from + sin(t * angle) * u</c>
        /// where <c>u</c> is the unit vector perpendicular to <paramref name="from"/> towards <paramref name="to"/>.
        /// Parameter 0 gives the first, 1 the second vector. The vectors are expected to be normalized.
        /// Opposite vectors have no unique arc, the one through the normal of the first vector is taken.
        /// </summary>
        public static (N X, N Y, N Z) Eval<N>((N X, N Y, N Z) from, (N X, N Y, N Z) to, N t)
            where N : IFloatingPointIeee754<N>
        {
            var u = GeometricVector3.Cartesian.Normalized(GeometricVector3.Rejection.FromCartesian(to, from));
            if (GeometricVector3.MagnitudeSquared.FromCartesian(u) == N.Zero)
            {
                u = GeometricVector3.Normal.FromCartesian(from);
            }

            var (sin, cos) = N.SinCos(t * GeometricVector3.Angle.FromCartesian(from, to));
            return ((from.X * cos) + (u.X * sin), (from.Y * cos) + (u.Y * sin), (from.Z * cos) + (u.Z * sin));
        }
    }
}
