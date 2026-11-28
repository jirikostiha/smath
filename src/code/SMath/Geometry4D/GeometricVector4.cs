using System.Numerics;
using System.Runtime.CompilerServices;

namespace SMath.Geometry4D;

/// <summary> Euclidean or geometric vector with 4 components. </summary>
/// <remarks> <a href="https://en.wikipedia.org/wiki/Four-dimensional_space">Wikipedia</a> </remarks>
public static class GeometricVector4
{
    /// <summary> Magnitude (length) of a vector. </summary>
    public static class Magnitude
    {
        public static N FromCartesian<N>((N X, N Y, N Z, N W) vector)
            where N : IRootFunctions<N>
            => PT.Hypotenuse(vector.X, vector.Y, vector.Z, vector.W);
    }

    /// <summary> Squared magnitude of a vector. It spares the root where only lengths are compared. </summary>
    public static class MagnitudeSquared
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static N FromCartesian<N>((N X, N Y, N Z, N W) vector)
            where N : INumberBase<N>
            => DotProduct.FromCartesian(vector, vector);
    }

    public static class Cartesian
    {
        /// <summary> Vector summation determined by n Cartesian vectors. </summary>
        public static (N X, N Y, N Z, N W) FromCartesianVectors<N>(params (N X, N Y, N Z, N W)[] vectors)
            where N : INumberBase<N>
        {
            var sum = (X: N.Zero, Y: N.Zero, Z: N.Zero, W: N.Zero);
            for (int i = 0; i < vectors.Length; i++)
            {
                sum = (sum.X + vectors[i].X, sum.Y + vectors[i].Y, sum.Z + vectors[i].Z, sum.W + vectors[i].W);
            }

            return sum;
        }

        /// <summary> Vector of the unit length. The zero vector has the zero vector. </summary>
        public static (N X, N Y, N Z, N W) Normalized<N>((N X, N Y, N Z, N W) vector)
            where N : IRootFunctions<N>
        {
            var magnitude = Magnitude.FromCartesian(vector);
            return magnitude == N.Zero
                ? (N.Zero, N.Zero, N.Zero, N.Zero)
                : (vector.X / magnitude, vector.Y / magnitude, vector.Z / magnitude, vector.W / magnitude);
        }
    }

    /// <summary> Dot product or scalar product. </summary>
    public static class DotProduct
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static N FromCartesian<N>((N X, N Y, N Z, N W) vector1, (N X, N Y, N Z, N W) vector2)
            where N : INumberBase<N>
            => (vector1.X * vector2.X) + (vector1.Y * vector2.Y) + (vector1.Z * vector2.Z) + (vector1.W * vector2.W);
    }

    /// <summary> Angle between two vectors, in the range [0, pi]. </summary>
    public static class Angle
    {
        /// <remarks>
        /// Computed as 2 * atan2(| |b| * a - |a| * b |, | |b| * a + |a| * b |), Kahan's form, which stays accurate for nearly parallel and
        /// opposite vectors where acos loses precision. Zero when either vector is zero, as in 3D.
        /// </remarks>
        public static N FromCartesian<N>((N X, N Y, N Z, N W) vector1, (N X, N Y, N Z, N W) vector2)
            where N : IFloatingPointIeee754<N>
        {
            N magnitude1 = Magnitude.FromCartesian(vector1);
            N magnitude2 = Magnitude.FromCartesian(vector2);
            var a = (vector1.X * magnitude2, vector1.Y * magnitude2, vector1.Z * magnitude2, vector1.W * magnitude2);
            var b = (vector2.X * magnitude1, vector2.Y * magnitude1, vector2.Z * magnitude1, vector2.W * magnitude1);
            return (N.One + N.One) * N.Atan2(Distance.FromCartesian(a, b), Magnitude.FromCartesian(Cartesian.FromCartesianVectors(a, b)));
        }
    }

    /// <summary> Distance of two vectors. </summary>
    public static class Distance
    {
        public static N FromCartesian<N>((N X, N Y, N Z, N W) vector1, (N X, N Y, N Z, N W) vector2)
            where N : IRootFunctions<N>
            => Magnitude.FromCartesian(Direction.FromCartesian(vector1, vector2));
    }

    /// <summary> Direction from one to the other vector. It is not normalized. </summary>
    public static class Direction
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (N X, N Y, N Z, N W) FromCartesian<N>((N X, N Y, N Z, N W) fromVector, (N X, N Y, N Z, N W) toVector)
            where N : ISubtractionOperators<N, N, N>
            => (toVector.X - fromVector.X, toVector.Y - fromVector.Y, toVector.Z - fromVector.Z, toVector.W - fromVector.W);
    }

    /// <summary> Projection of a vector onto another one. </summary>
    /// <remarks> <a href="https://en.wikipedia.org/wiki/Vector_projection">Wikipedia</a> </remarks>
    public static class Projection
    {
        /// <summary> Component of <paramref name="vector"/> parallel to <paramref name="onto"/>. Zero when <paramref name="onto"/> is zero. </summary>
        public static (N X, N Y, N Z, N W) FromCartesian<N>((N X, N Y, N Z, N W) vector, (N X, N Y, N Z, N W) onto)
            where N : INumberBase<N>
        {
            var length2 = MagnitudeSquared.FromCartesian(onto);
            if (length2 == N.Zero)
                return (N.Zero, N.Zero, N.Zero, N.Zero);

            var t = DotProduct.FromCartesian(vector, onto) / length2;
            return (onto.X * t, onto.Y * t, onto.Z * t, onto.W * t);
        }
    }

    /// <summary> Rejection of a vector from another one, the part of it that is not parallel to the other one. </summary>
    /// <remarks> <a href="https://en.wikipedia.org/wiki/Vector_projection#Vector_rejection">Wikipedia</a> </remarks>
    public static class Rejection
    {
        /// <summary> Component of <paramref name="vector"/> perpendicular to <paramref name="onto"/>. It is the vector itself when <paramref name="onto"/> is zero. </summary>
        public static (N X, N Y, N Z, N W) FromCartesian<N>((N X, N Y, N Z, N W) vector, (N X, N Y, N Z, N W) onto)
            where N : INumberBase<N>
        {
            var parallel = Projection.FromCartesian(vector, onto);
            return (vector.X - parallel.X, vector.Y - parallel.Y, vector.Z - parallel.Z, vector.W - parallel.W);
        }
    }
}
