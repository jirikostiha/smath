using System.Numerics;
using System.Runtime.CompilerServices;

namespace SMath.Geometry3D;

/// <summary>
/// Geometric vector in 3D space.
/// Spherical coordinates follow the physics convention: the polar angle is measured from the +z axis,
/// the azimuth from the +x axis towards the +y axis.
/// </summary>
/// <remarks>
/// <a href="https://en.wikipedia.org/wiki/Euclidean_vector">Wikipedia</a>
/// <a href="https://en.wikipedia.org/wiki/Spherical_coordinate_system">Wikipedia</a>
/// </remarks>
public static class GeometricVector3
{
    /// <summary>
    /// Magnitude (length) of a vector.
    /// </summary>
    public static class Magnitude
    {
        public static N FromCartesian<N>(N x, N y, N z)
            where N : IRootFunctions<N>
            => PT.Hypotenuse(x, y, z);

        public static N FromCartesian<N>((N X, N Y, N Z) vector)
            where N : IRootFunctions<N>
            => PT.Hypotenuse(vector);

        /// <summary>
        /// Magnitude of the sum of n Cartesian vectors.
        /// </summary>
        public static N FromCartesianVectors<N>(params (N X, N Y, N Z)[] vectors)
            where N : IRootFunctions<N>
            => FromCartesian(Cartesian.FromCartesianVectors(vectors));
    }

    /// <summary>
    /// X component of a vector.
    /// </summary>
    public static class X
    {
        public static N FromSpherical<N>(N magnitude, N polarAngle, N azimuth)
            where N : ITrigonometricFunctions<N>
            => magnitude * N.Sin(polarAngle) * N.Cos(azimuth);
    }

    /// <summary>
    /// Y component of a vector.
    /// </summary>
    public static class Y
    {
        public static N FromSpherical<N>(N magnitude, N polarAngle, N azimuth)
            where N : ITrigonometricFunctions<N>
            => magnitude * N.Sin(polarAngle) * N.Sin(azimuth);
    }

    /// <summary>
    /// Z component of a vector.
    /// </summary>
    public static class Z
    {
        public static N FromSpherical<N>(N magnitude, N polarAngle)
            where N : ITrigonometricFunctions<N>
            => magnitude * N.Cos(polarAngle);
    }

    /// <summary>
    /// Polar angle (inclination) of a vector, measured from the +z axis, in the range [0, pi].
    /// </summary>
    public static class PolarAngle
    {
        public static N FromCartesian<N>(N x, N y, N z)
            where N : IFloatingPointIeee754<N>
            => N.Atan2(PT.Hypotenuse(x, y), z);

        public static N FromCartesian<N>((N X, N Y, N Z) vector)
            where N : IFloatingPointIeee754<N>
            => FromCartesian(vector.X, vector.Y, vector.Z);
    }

    /// <summary>
    /// Azimuth of a vector, measured in the xy plane from the +x axis towards the +y axis, in the range (-pi, pi].
    /// </summary>
    public static class Azimuth
    {
        public static N FromCartesian<N>(N x, N y)
            where N : IFloatingPointIeee754<N>
            => N.Atan2(y, x);

        public static N FromCartesian<N>((N X, N Y, N Z) vector)
            where N : IFloatingPointIeee754<N>
            => N.Atan2(vector.Y, vector.X);
    }

    /// <summary>
    /// Vector in Cartesian coordinate system.
    /// </summary>
    public static class Cartesian
    {
        public static (N X, N Y, N Z) FromSpherical<N>(N magnitude, N polarAngle, N azimuth)
            where N : ITrigonometricFunctions<N>
        {
            var (sinPolar, cosPolar) = N.SinCos(polarAngle);
            var (sinAzimuth, cosAzimuth) = N.SinCos(azimuth);
            // sin of the polar angle is shared by x and y
            N planar = magnitude * sinPolar;
            return (planar * cosAzimuth, planar * sinAzimuth, magnitude * cosPolar);
        }

        /// <summary>
        /// Vector summation determined by n Cartesian vectors.
        /// </summary>
        public static (N X, N Y, N Z) FromCartesianVectors<N>(params (N X, N Y, N Z)[] vectors)
            where N : INumberBase<N>
        {
            var x = N.Zero;
            var y = N.Zero;
            var z = N.Zero;
            for (int i = 0; i < vectors.Length; i++)
            {
                x += vectors[i].X;
                y += vectors[i].Y;
                z += vectors[i].Z;
            }

            return (x, y, z);
        }

        public static (N X, N Y, N Z) Normalized<N>(N x, N y, N z)
            where N : IRootFunctions<N>
        {
            var magnitude = Magnitude.FromCartesian(x, y, z);
            return magnitude == N.Zero ? (N.Zero, N.Zero, N.Zero) : (x / magnitude, y / magnitude, z / magnitude);
        }

        public static (N X, N Y, N Z) Normalized<N>((N X, N Y, N Z) vector)
            where N : IRootFunctions<N>
            => Normalized(vector.X, vector.Y, vector.Z);

        /// <summary>
        /// Vector with components reduced to their octant signs (+1, -1, or 0).
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (N X, N Y, N Z) Octantized<N>(N x, N y, N z)
            where N : INumberBase<N>
            => (Sign(x), Sign(y), Sign(z));

        /// <summary>
        /// Vector with components reduced to their octant signs (+1, -1, or 0).
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (N X, N Y, N Z) Octantized<N>((N X, N Y, N Z) vector)
            where N : INumberBase<N>
            => Octantized(vector.X, vector.Y, vector.Z);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static N Sign<N>(N value)
            where N : INumberBase<N>
            => N.IsZero(value) ? N.Zero : (N.IsNegative(value) ? -N.One : N.One);
    }

    /// <summary>
    /// Vector in spherical coordinate system.
    /// </summary>
    public static class Spherical
    {
        public static (N Magnitude, N PolarAngle, N Azimuth) FromCartesian<N>(N x, N y, N z)
            where N : IFloatingPointIeee754<N>
            => (Magnitude.FromCartesian(x, y, z), PolarAngle.FromCartesian(x, y, z), Azimuth.FromCartesian(x, y));

        public static (N Magnitude, N PolarAngle, N Azimuth) FromCartesian<N>((N X, N Y, N Z) vector)
            where N : IFloatingPointIeee754<N>
            => FromCartesian(vector.X, vector.Y, vector.Z);
    }

    /// <summary>
    /// Cross product or vector product.
    /// </summary>
    /// <remarks>
    /// <a href="https://en.wikipedia.org/wiki/Cross_product">Wikipedia</a>
    /// </remarks>
    public static class CrossProduct
    {
        public static (N X, N Y, N Z) FromCartesian<N>((N X, N Y, N Z) vector1, (N X, N Y, N Z) vector2)
            where N : ISubtractionOperators<N, N, N>, IMultiplyOperators<N, N, N>
            => ((vector1.Y * vector2.Z) - (vector1.Z * vector2.Y),
                (vector1.Z * vector2.X) - (vector1.X * vector2.Z),
                (vector1.X * vector2.Y) - (vector1.Y * vector2.X));
    }

    /// <summary>
    /// Dot product or scalar product.
    /// </summary>
    public static class DotProduct
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static N FromCartesian<N>((N X, N Y, N Z) vector1, (N X, N Y, N Z) vector2)
            where N : IAdditionOperators<N, N, N>, IMultiplyOperators<N, N, N>
            => (vector1.X * vector2.X) + (vector1.Y * vector2.Y) + (vector1.Z * vector2.Z);
    }

    /// <summary>
    /// Scalar triple product a · (b × c), the signed volume of the parallelepiped spanned by the vectors.
    /// </summary>
    /// <remarks>
    /// <a href="https://en.wikipedia.org/wiki/Triple_product">Wikipedia</a>
    /// </remarks>
    public static class TripleProduct
    {
        public static N FromCartesian<N>((N X, N Y, N Z) vector1, (N X, N Y, N Z) vector2, (N X, N Y, N Z) vector3)
            where N : IAdditionOperators<N, N, N>, ISubtractionOperators<N, N, N>, IMultiplyOperators<N, N, N>
            => DotProduct.FromCartesian(vector1, CrossProduct.FromCartesian(vector2, vector3));
    }

    /// <summary>
    /// Angle between two vectors in the range [0, pi]. Zero when either vector is zero.
    /// </summary>
    public static class Angle
    {
        /// <remarks>
        /// Computed as atan2(|a × b|, a · b), which stays accurate for nearly parallel vectors where acos loses precision.
        /// </remarks>
        public static N FromCartesian<N>((N X, N Y, N Z) vector1, (N X, N Y, N Z) vector2)
            where N : IFloatingPointIeee754<N>
            => N.Atan2(Magnitude.FromCartesian(CrossProduct.FromCartesian(vector1, vector2)), DotProduct.FromCartesian(vector1, vector2));
    }

    /// <summary>
    /// Normal vector of an input vector: perpendicular to it and of the same length.
    /// Space has a whole circle of such vectors; this one is also perpendicular to the coordinate axis
    /// the input leans on least, which keeps it far from degenerate. A zero vector has the zero vector.
    /// </summary>
    /// <remarks>
    /// <a href="https://en.wikipedia.org/wiki/Normal_(geometry)">Wikipedia</a>
    /// </remarks>
    public static class Normal
    {
        public static (N X, N Y, N Z) FromCartesian<N>((N X, N Y, N Z) vector)
            where N : INumber<N>, IRootFunctions<N>
        {
            N absX = N.Abs(vector.X);
            N absY = N.Abs(vector.Y);
            N absZ = N.Abs(vector.Z);
            (N X, N Y, N Z) axis = absX <= absY && absX <= absZ
                ? (N.One, N.Zero, N.Zero)
                : absY <= absZ ? (N.Zero, N.One, N.Zero) : (N.Zero, N.Zero, N.One);

            var cross = CrossProduct.FromCartesian(vector, axis);
            N length = Magnitude.FromCartesian(cross);
            if (!(length > N.Zero))
            {
                return (N.Zero, N.Zero, N.Zero);
            }

            N scale = Magnitude.FromCartesian(vector) / length;
            return (cross.X * scale, cross.Y * scale, cross.Z * scale);
        }
    }

    /// <summary>
    /// Distance of two vectors.
    /// </summary>
    public static class Distance
    {
        public static N FromCartesian<N>((N X, N Y, N Z) vector1, (N X, N Y, N Z) vector2)
            where N : IRootFunctions<N>
            => Point3.Distance(vector1, vector2);
    }

    /// <summary>
    /// Direction from one to the other vector. It is not normalized.
    /// </summary>
    public static class Direction
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (N X, N Y, N Z) FromCartesian<N>((N X, N Y, N Z) fromVector, (N X, N Y, N Z) toVector)
            where N : ISubtractionOperators<N, N, N>
            => (toVector.X - fromVector.X, toVector.Y - fromVector.Y, toVector.Z - fromVector.Z);
    }

    /// <summary>
    /// Projection of a vector onto another one.
    /// </summary>
    /// <remarks>
    /// <a href="https://en.wikipedia.org/wiki/Vector_projection">Wikipedia</a>
    /// </remarks>
    public static class Projection
    {
        /// <summary>
        /// Component of <paramref name="vector"/> parallel to <paramref name="onto"/>. Zero when <paramref name="onto"/> is zero.
        /// </summary>
        public static (N X, N Y, N Z) FromCartesian<N>((N X, N Y, N Z) vector, (N X, N Y, N Z) onto)
            where N : INumberBase<N>
        {
            var length2 = DotProduct.FromCartesian(onto, onto);
            if (length2 == N.Zero)
                return (N.Zero, N.Zero, N.Zero);

            var t = DotProduct.FromCartesian(vector, onto) / length2;
            return (onto.X * t, onto.Y * t, onto.Z * t);
        }
    }

    /// <summary>
    /// Reflection of a vector about a normal.
    /// </summary>
    /// <remarks>
    /// <a href="https://en.wikipedia.org/wiki/Reflection_(mathematics)">Wikipedia</a>
    /// </remarks>
    public static class Reflection
    {
        /// <summary>
        /// Reflect a vector about a unit normal of a plane. The normal is expected to be normalized.
        /// v' = v - 2*(v·n)*n
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (N X, N Y, N Z) FromCartesian<N>((N X, N Y, N Z) vector, (N X, N Y, N Z) unitNormal)
            where N : INumberBase<N>
        {
            var twoDot = (N.One + N.One) * DotProduct.FromCartesian(vector, unitNormal);
            return (vector.X - twoDot * unitNormal.X, vector.Y - twoDot * unitNormal.Y, vector.Z - twoDot * unitNormal.Z);
        }

        /// <summary>
        /// Reflect a point across a plane parallel to the xy plane at height <c>z</c>.
        /// </summary>
        public static class ToXY
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static (N X, N Y, N Z) FromCartesian<N>((N X, N Y, N Z) point, N z)
                where N : INumberBase<N>
                => (point.X, point.Y, N.CreateChecked(2) * z - point.Z);
        }

        /// <summary>
        /// Reflect a point across a plane parallel to the xz plane at ordinate <c>y</c>.
        /// </summary>
        public static class ToXZ
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static (N X, N Y, N Z) FromCartesian<N>((N X, N Y, N Z) point, N y)
                where N : INumberBase<N>
                => (point.X, N.CreateChecked(2) * y - point.Y, point.Z);
        }

        /// <summary>
        /// Reflect a point across a plane parallel to the yz plane at abscissa <c>x</c>.
        /// </summary>
        public static class ToYZ
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static (N X, N Y, N Z) FromCartesian<N>((N X, N Y, N Z) point, N x)
                where N : INumberBase<N>
                => (N.CreateChecked(2) * x - point.X, point.Y, point.Z);
        }
    }

    /// <summary>
    /// Rotation around an axis.
    /// </summary>
    /// <remarks>
    /// <a href="https://en.wikipedia.org/wiki/Rodrigues%27_rotation_formula">Wikipedia</a>
    /// </remarks>
    public static class Rotation
    {
        /// <summary>
        /// Rotate a vector around a unit axis through the origin by an angle in radians,
        /// counterclockwise when looking against the axis. The axis is expected to be normalized.
        /// v' = v cos θ + (k × v) sin θ + k (k · v)(1 - cos θ)
        /// </summary>
        public static (N X, N Y, N Z) FromCartesian<N>((N X, N Y, N Z) vector, (N X, N Y, N Z) unitAxis, N angle)
            where N : ITrigonometricFunctions<N>
        {
            var (sin, cos) = N.SinCos(angle);
            var cross = CrossProduct.FromCartesian(unitAxis, vector);
            var along = DotProduct.FromCartesian(unitAxis, vector) * (N.One - cos);
            return ((vector.X * cos) + (cross.X * sin) + (unitAxis.X * along),
                    (vector.Y * cos) + (cross.Y * sin) + (unitAxis.Y * along),
                    (vector.Z * cos) + (cross.Z * sin) + (unitAxis.Z * along));
        }

        /// <summary>
        /// Rotate a point around a unit axis through a center point by an angle in radians.
        /// </summary>
        public static (N X, N Y, N Z) FromCartesian<N>((N X, N Y, N Z) point, (N X, N Y, N Z) center, (N X, N Y, N Z) unitAxis, N angle)
            where N : ITrigonometricFunctions<N>
        {
            var (dx, dy, dz) = FromCartesian(Direction.FromCartesian(center, point), unitAxis, angle);
            return (center.X + dx, center.Y + dy, center.Z + dz);
        }
    }
}
