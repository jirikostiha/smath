using System.Numerics;

namespace SMath.Geometry2D
{
    /// <summary>
    /// Euclidean or geometric vector with 2 components.
    /// </summary>
    /// <remarks>
    /// <a href="https://en.wikipedia.org/wiki/Euclidean_vector">wikipedia</a>
    /// </remarks>
    public static class GeometricVector2
    {
        /// <summary>
        /// Length or magnitude or scalar value of the vector in x-y plane.
        /// </summary>
        public static N Magnitude<N>(N x, N y)
            where N : IRootFunctions<N>
            => PT.Hypotenuse(x, y);

          /// <summary>
        /// Length or magnitude or scalar value of the vector in x-y plane.
        /// </summary>
        public static N Magnitude<N>((N X, N Y) vector)
            where N : IRootFunctions<N>
            => PT.Hypotenuse(vector.X, vector.Y);

        /// <summary> 
        /// Polar angle. Angle from x axis to y axis. 
        /// </summary>
        public static N Φ1<N>(N x, N y)
            where N : ITrigonometricFunctions<N>
            => N.Atan(y / x);

        public static N X<N>(N magnitude, N φ1)
            where N : ITrigonometricFunctions<N>
            => magnitude * N.Cos(φ1);

        public static N Y<N>(N magnitude, N φ1)
            where N : ITrigonometricFunctions<N>
            => magnitude * N.Sin(φ1);

        public static (N Magnitude, N Φ1) CartesianToPolar<N>(N x, N y)
            where N : IRootFunctions<N>, ITrigonometricFunctions<N>
            => (Magnitude(x, y), Φ1(x, y));

        public static (N X, N Y) PolarToCartesian<N>(N magnitude, N φ1)
            where N : ITrigonometricFunctions<N>
            => (X(magnitude, φ1), Y(magnitude, φ1));

        public static (N X, N Y) Normalized<N>(N x, N y)
            where N : IRootFunctions<N>
        {
            var magnitude = Magnitude(x, y);
            return (x / magnitude, y / magnitude);
        }

        public static (N X, N Y) Normalized<N>((N X, N Y) vector)
            where N : IRootFunctions<N>
        {
            var magnitude = Magnitude(vector);
            return (vector.X / magnitude, vector.Y / magnitude);
        }

        public static N Distance<N>(N vector1X, N vector1Y, N vector2X, N vector2Y)
            where N : IRootFunctions<N>
            => PT.Hypotenuse(vector1X - vector2X, vector1Y - vector2Y);

        public static N Distance<N>((N X, N Y) vector1, (N X, N Y) vector2)
            where N : IRootFunctions<N>
            => PT.Hypotenuse(vector1.X - vector2.X, vector1.Y - vector2.Y);

        public static (N X, N Y) Direction<N>(N fromX, N fromY, N toX, N toY)
            where N : ISubtractionOperators<N, N, N>
            => (toX - fromX, toY - fromY);

        public static (N X, N Y) Direction<N>((N X, N Y) vector1, (N X, N Y) vector2)
            where N : ISubtractionOperators<N, N, N>
            => (vector2.X - vector1.X, vector2.Y - vector1.Y);

        /// <summary> Dot product or scalar product. </summary>
        public static N DotProduct<N>(N vector1X, N vector1Y, N vector2X, N vector2Y)
            where N : IAdditionOperators<N, N, N>, IMultiplyOperators<N, N, N>
            => vector1X * vector2X + vector1Y * vector2Y;

        /// <summary>
        /// First normal vector of an input vector. It is the first one in circular direction from (+)x-axis to (+)y-axis.
        /// </summary>
        /// <remarks> 
        /// <a href="https://en.wikipedia.org/wiki/Normal_(geometry)">wikipedia</a>
        /// </remarks>
        public static (N X, N Y) Normal1<N>(N x, N y)
            where N : IUnaryNegationOperators<N, N>
            => (-y, x);

        /// <summary>
        /// First normal vector of an input vector. It is the first one in circular direction from (+)x-axis to (+)y-axis.
        /// </summary>
        /// <remarks> 
        /// <a href="https://en.wikipedia.org/wiki/Normal_(geometry)">wikipedia</a>
        /// </remarks>
        public static (N X, N Y) Normal1<N>((N X, N Y) vector)
            where N : IUnaryNegationOperators<N, N>
            => (-vector.Y, vector.X);

        /// <summary>
        /// Second normal vector of an input vector. It is the second one in circular direction from (+)x-axis to (+)y-axis.
        /// </summary>
        /// <remarks> 
        /// <a href="https://en.wikipedia.org/wiki/Normal_(geometry)">wikipedia</a>
        /// </remarks>
        public static (N X, N Y) Normal2<N>(N x, N y)
            where N : IUnaryNegationOperators<N, N>
            => (y, -x);

        /// <summary>
        /// Second normal vector of an input vector. It is the second one in circular direction from (+)x-axis to (+)y-axis.
        /// </summary>
        /// <remarks> 
        /// <a href="https://en.wikipedia.org/wiki/Normal_(geometry)">wikipedia</a>
        /// </remarks>
        public static (N X, N Y) Normal2<N>((N X, N Y) vector)
            where N : IUnaryNegationOperators<N, N>
            => (vector.Y, -vector.X);
    }
}
