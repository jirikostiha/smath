using System.Numerics;
using System.Runtime.CompilerServices;
using static SMath.Geometry2D.GeometricVector2;

namespace SMath.Geometry2D;

/// <summary>
/// Euclidean or geometric vector with 2 components.
/// </summary>
/// <remarks>
/// <a href="https://en.wikipedia.org/wiki/Euclidean_vector">Wikipedia</a>
/// </remarks>
public static class GeometricVector2
{
    /// <summary>
    /// Magnitude/length/size/scalar of vector.
    /// </summary>
    public static class Magnitude
    {
        /// <summary>
        /// Calculate magnitude of a vector determined in Cartesian coordinate system.
        /// </summary>
        public static N FromCartesian<N>(N x, N y)
            where N : IRootFunctions<N>
            => PT.Hypotenuse(x, y);

        /// <summary>
        /// Calculate magnitude of a vector determined in Cartesian coordinate system.
        /// </summary>
        public static N FromCartesian<N>((N X, N Y) vector)
            where N : IRootFunctions<N>
            => PT.Hypotenuse(vector.X, vector.Y);

        /// <summary>
        /// Calculate magnitude of vectors determined in Cartesian coordinate system.
        /// </summary>
        public static N FromCartesianVectors<N>(params (N X, N Y)[] vectors)
            where N : IRootFunctions<N>
            => FromCartesian(Cartesian.FromCartesianVectors(vectors));

        /// <summary>
        /// Calculate magnitude of two vectors determined in polar coordinate system.
        /// </summary>
        public static N FromTwoPolarVectors<N>(N magnitude1, N magnitude2, N angle)
            where N : ITrigonometricFunctions<N>, IRootFunctions<N>
            => PT.Cosine(magnitude1, magnitude2, -angle);

        /// <summary>
        /// Calculate magnitude of sum of two vectors determined in polar coordinate system.
        /// </summary>
        public static N FromPolarVectors<N>((N Magnitude, N Angle) vector1, (N Magnitude, N Angle) vector2)
            where N : ITrigonometricFunctions<N>, IRootFunctions<N>
            => PT.Cosine(vector1.Magnitude, vector2.Magnitude, vector1.Angle - vector2.Angle);
    }

    /// <summary>
    /// X-component of vector.
    /// </summary>
    public static class X
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static N FromPolar<N>(N magnitude, N φ1)
            where N : ITrigonometricFunctions<N>
            => magnitude * N.Cos(φ1);
    }

    /// <summary>
    /// Y-component of vector.
    /// </summary>
    public static class Y
    {
        /// <summary>
        /// Calculate y-component of vector determined in polar coordinate system.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static N FromPolar<N>(N magnitude, N φ1)
            where N : ITrigonometricFunctions<N>
            => magnitude * N.Sin(φ1);
    }

    /// <summary>
    /// Polar angle. Angle from x-axis to y-axis.
    /// </summary>
    public static class PolarAngle
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static N FromCartesian<N>(N x, N y)
            where N : ITrigonometricFunctions<N>
            => N.Atan(y / x);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static N FromCartesian<N>((N X, N Y) vector)
            where N : ITrigonometricFunctions<N>
            => N.Atan(vector.Y / vector.X);
    }

    /// <summary>
    /// Vector in Cartesian coordinate system.
    /// </summary>
    public static class Cartesian
    {
        public static (N X, N Y) FromPolar<N>(N magnitude, N φ1)
            where N : ITrigonometricFunctions<N>
            => (X.FromPolar(magnitude, φ1), Y.FromPolar(magnitude, φ1));

        /// <summary>
        /// Vector summation determined by n Cartesian vectors.
        /// </summary>
        public static (N X, N Y) FromCartesianVectors<N>(params (N X, N Y)[] vectors)
            where N : IRootFunctions<N>
            => (Summation.Eval(vectors.Select(v => v.X)), Summation.Eval(vectors.Select(v => v.Y)));

        public static (N X, N Y) Normalized<N>(N x, N y)
            where N : IRootFunctions<N>
        {
            var magnitude = Magnitude.FromCartesian(x, y);
            return (x / magnitude, y / magnitude);
        }

        public static (N X, N Y) Normalized<N>((N X, N Y) vector)
            where N : IRootFunctions<N>
        {
            var magnitude = Magnitude.FromCartesian(vector);
            return (vector.X / magnitude, vector.Y / magnitude);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (N X, N Y) Kvadrantized<N>(N x, N y)
            where N : INumberBase<N>
            => (x / N.Abs(x), y / N.Abs(y));

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (N X, N Y) Kvadrantized<N>((N X, N Y) vector)
            where N : INumberBase<N>
            => (vector.X / N.Abs(vector.X), vector.Y / N.Abs(vector.Y));
    }

    /// <summary>
    /// Vector in polar coordinate system.
    /// </summary>
    public static class Polar
    {
        public static (N Magnitude, N Φ1) FromCartesian<N>(N x, N y)
            where N : IRootFunctions<N>, ITrigonometricFunctions<N>
            => (Magnitude.FromCartesian(x, y),
                PolarAngle.FromCartesian(x, y));

        public static (N Magnitude, N Φ1) FromCartesian<N>((N X, N Y) vector)
            where N : IRootFunctions<N>, ITrigonometricFunctions<N>
            => (Magnitude.FromCartesian(vector.X, vector.Y),
                PolarAngle.FromCartesian(vector.X, vector.Y));

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (N Magnitude, N Φ1) Normalized<N>(N magnitude, N polarAngle)
            where N : INumberBase<N>
            => (N.One, polarAngle);
    }

    /// <summary>
    /// First normal vector of an input vector. It is the first one in circular direction from (+)x-axis to (+)y-axis.
    /// </summary>
    /// <remarks>
    /// <a href="https://en.wikipedia.org/wiki/Normal_(geometry)">Wikipedia</a>
    /// </remarks>
    public static class Normal1
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (N X, N Y) FromCartesian<N>(N x, N y)
            where N : IUnaryNegationOperators<N, N>
            => (-y, x);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (N X, N Y) FromCartesian<N>((N X, N Y) vector)
            where N : IUnaryNegationOperators<N, N>
            => (-vector.Y, vector.X);
    }

    /// <summary>
    /// Second normal vector of an input vector. It is the second one in circular direction from (+)x-axis to (+)y-axis.
    /// </summary>
    /// <remarks>
    /// <a href="https://en.wikipedia.org/wiki/Normal_(geometry)">Wikipedia</a>
    /// </remarks>
    public static class Normal2
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (N X, N Y) FromCartesian<N>(N x, N y)
            where N : IUnaryNegationOperators<N, N>
            => (y, -x);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (N X, N Y) FromCartesian<N>((N X, N Y) vector)
            where N : IUnaryNegationOperators<N, N>
            => (vector.Y, -vector.X);
    }

    /// <summary>
    /// Distance of two vectors.
    /// </summary>
    public static class Distance
    {
        public static N FromCartesian<N>((N X, N Y) vector1, (N X, N Y) vector2)
            where N : IRootFunctions<N>
            => PT.Hypotenuse(vector1.X - vector2.X, vector1.Y - vector2.Y);

        public static N FromPolar<N>((N Radius, N Angle) vector1, (N Radius, N Angle) vector2)
            where N : IRootFunctions<N>, ITrigonometricFunctions<N>
            => N.Sqrt(vector1.Radius * vector1.Radius + vector2.Radius * vector2.Radius
                - N.CreateChecked(2) * vector1.Radius * vector2.Radius * N.Cos(vector2.Angle - vector1.Angle));
    }

    /// <summary>
    /// Direction from one to the other vector. It is not normalized.
    /// </summary>
    public static class Direction
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (N X, N Y) FromCartesian<N>((N X, N Y) fromVector, (N X, N Y) toVector)
            where N : ISubtractionOperators<N, N, N>
            => (toVector.X - fromVector.X, toVector.Y - fromVector.Y);
    }

    /// <summary>
    /// Dot product or scalar product.
    /// </summary>
    public static class DotProduct
    {
        public static N FromCartesian<N>((N X, N Y) vector1, (N X, N Y) vector2)
            where N : IAdditionOperators<N, N, N>, IMultiplyOperators<N, N, N>
            => vector1.X * vector2.X + vector1.Y * vector2.Y;

        public static N FromPolar<N>(N length1, N length2, N angle)
            where N : ITrigonometricFunctions<N>
            => length1 * length2 * N.Cos(angle);
    }

    /// <summary>
    /// Cross product or vector product.
    /// In 2D the result is magnitude in 3rd dimension.
    /// </summary>
    public static class CrossProduct
    {
        public static N FromCartesian<N>((N X, N Y) vector1, (N X, N Y) vector2)
            where N : ISubtractionOperators<N, N, N>, IMultiplyOperators<N, N, N>
            => (vector1.X * vector2.Y) - (vector1.Y * vector2.X);
    }

    public static class Reflection
    {
        public static class ToX
        {
            /// <summary>
            /// Reflect or mirror vector to a line parallel to x-axis.
            /// </summary>
            /// <param name="vector"> Vector to mirror. </param>
            /// <param name="y"> Y coordinate of a mirror line. </param>
            /// <returns> Vector with mirrored coordinates. </returns>
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static (N X, N Y) FromCartesian<N>((N X, N Y) vector, N y)
                where N : INumberBase<N>
                => new(vector.X, vector.Y - N.CreateChecked(2) * (vector.Y - y));
        }

        public static class ToY
        {
            /// <summary>
            /// Reflect or mirror vector to a line parallel to y-axis.
            /// </summary>
            /// <param name="vector"> Vector to mirror. </param>
            /// <param name="x"> X coordinate of a mirror line. </param>
            /// <returns> Vector with mirrored coordinates. </returns>
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static (N X, N Y) FromCartesian<N>((N X, N Y) vector, N x)
                where N : INumberBase<N>
                => new(vector.X - N.CreateChecked(2) * (vector.X - x), vector.Y);
        }

        public static class ToLine
        {
            /// <summary>
            /// Mirror vector to line determined by origin and direction vector.
            /// </summary>
            /// <param name="vector"> Vector to mirror. </param>
            /// <param name="directionVector"> Direction vector of mirror line. </param>
            /// <returns> Vector with mirrored coordinates. </returns>
            public static (N X, N Y) FromCartesian<N>((N X, N Y) vector, (N X, N Y) directionVector)
                where N : INumberBase<N>
            {
                var normalizedDir =  directionVector.Normalized();
                float dotProduct = vector.X * normalizedDir.X + vector.Y * normalizedDir.Y;

                return new(
                    2 * dotProduct * normalizedDir.X - vector.X,
                    2 * dotProduct * normalizedDir.Y - vector.Y);
            }

            /// <summary>
            /// Mirror vector to line determined by one point and direction vector.
            /// </summary>
            /// <param name="vector"> Vector to mirror. </param>
            /// <param name="reflectionPoint"> Point of mirror line. </param>
            /// <param name="directionVector"> Direction vector of mirror line. </param>
            /// <returns> Vector with mirrored coordinates. </returns>
            public static (N X, N Y) FromCartesian<N>((N X, N Y) vector, (N X, N Y) reflectionPoint, (N X, N Y) directionVector)
                where N : INumberBase<N>
            {
                var normalizedDir = directionVector.Normalized();
                float dotProduct = (vector.X - mirrorPoint.X) * normalizedDir.X + (vector.Y - mirrorPoint.Y) * normalizedDir.Y;

                return new(
                    2 * (mirrorPoint.X + dotProduct * normalizedDir.X) - vector.X,
                    2 * (mirrorPoint.Y + dotProduct * normalizedDir.Y) - vector.Y);
            }

            /// <summary>
            /// Mirror vector to line determined by two points.
            /// </summary>
            /// <param name="vector"> Vector to mirror.</param>
            /// <param name="reflectionPointA"> First point of mirror line. </param>
            /// <param name="reflectionPointB"> Second point of mirror line. </param>
            /// <returns> Vector with mirrored coordinates. </returns>
            public static (N X, N Y) FromCartesian<N>((N X, N Y) vector, (N X, N Y) reflectionPointA, (N X, N Y) reflectionPointB)
                where N : INumberBase<N>
            {
                var dx = mirrorPointB.X - mirrorPointA.X;
                var dy = mirrorPointB.Y - mirrorPointA.Y;

                if (dx == 0 && dy == 0)
                {
                    return new(2 * mirrorPointA.X - vector.X, 2 * mirrorPointA.Y - vector.Y);
                }
                else
                {
                    float t = ((vector.X - mirrorPointA.X) * dx + (vector.Y - mirrorPointA.Y) * dy) / (dx * dx + dy * dy);

                    return new(
                        2 * (mirrorPointA.X + t * dx) - vector.X,
                        2 * (mirrorPointA.Y + t * dy) - vector.Y);
                }
            }
        }
    }
}
