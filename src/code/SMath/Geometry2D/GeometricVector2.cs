using SMath.Functions1;
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
        /// Vector magnitude/length/size/scalar.
        /// </summary>
        public static class Magnitude
        {
            /// <summary>
            /// Get magnitude of a vector determined by cartesian coordinate system.
            /// </summary>
            public static N FromCartesian<N>(N x, N y)
                where N : IRootFunctions<N>
                => PT.Hypotenuse(x, y);

            /// <summary>
            /// Get magnitude of a vector determined by cartesian coordinate system.
            /// </summary>
            public static N FromCartesian<N>((N X, N Y) vector)
                where N : IRootFunctions<N>
                => PT.Hypotenuse(vector.X, vector.Y);

            //spise sum -> magnitude
            /// <summary>
            /// Magnitude of sum of two vectors.
            /// </summary>
            public static class Sum
            {
                public static N SumLength<N>(N magnitude1, N magnitude2, N angle)
                    where N : ITrigonometricFunctions<N>, IRootFunctions<N>
                    => PT.Cosine(magnitude1, magnitude2, -angle);

                /// <summary>
                /// Magnitude of sum of two polar vectors.
                /// </summary>
                public static N SumLength<N>((N Magnitude, N Angle) vector1, (N Magnitude, N Angle) vector2)
                    where N : ITrigonometricFunctions<N>, IRootFunctions<N>
                    => PT.Cosine(vector1.Magnitude, vector2.Magnitude, vector1.Angle - vector2.Angle);
            }
        }

        /// <summary>
        /// X-component of vector.
        /// </summary>
        public static class X
        {
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
            /// Get y-component of vector determined by polar coordinate system.
            /// </summary>
            public static N FromPolar<N>(N magnitude, N φ1)
                where N : ITrigonometricFunctions<N>
                => magnitude * N.Sin(φ1);
        }

        /// <summary> 
        /// Polar angle. Angle from x-axis to y-axis. 
        /// </summary>
        public static class PolarAngle
        {
            public static N FromCartesian<N>(N x, N y)
                where N : ITrigonometricFunctions<N>
                => N.Atan(y / x);

            public static N FromCartesian<N>((N X, N Y) vector)
                where N : ITrigonometricFunctions<N>
                => N.Atan(vector.Y / vector.X);
        }

        public static class Cartesian
        {
            public static (N X, N Y) FromPolar<N>(N magnitude, N φ1)
                where N : ITrigonometricFunctions<N>
                => (X.FromPolar(magnitude, φ1), Y.FromPolar(magnitude, φ1));

            //from two vectors (sum/add)

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

            public static (N X, N Y) Kvadrantized<N>(N x, N y)
                where N : INumberBase<N>
                => (x / N.Abs(x), y / N.Abs(y));

            public static (N X, N Y) Kvadrantized<N>((N X, N Y) vector)
                where N : INumberBase<N>
                => (vector.X / N.Abs(vector.X), vector.Y / N.Abs(vector.Y));
        }

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

            public static (N Magnitude, N Φ1) Normalized<N>(N Magnitude, N polarAngle)
                where N : INumberBase<N>
                => (N.One, polarAngle);
        }

        /// <summary>
        /// First normal vector of an input vector. It is the first one in circular direction from (+)x-axis to (+)y-axis.
        /// </summary>
        /// <remarks> 
        /// <a href="https://en.wikipedia.org/wiki/Normal_(geometry)">wikipedia</a>
        /// </remarks>
        public static class Normal1
        {
            public static (N X, N Y) FromCartesian<N>(N x, N y)
                where N : IUnaryNegationOperators<N, N>
                => (-y, x);

            public static (N X, N Y) FromCartesian<N>((N X, N Y) vector)
                where N : IUnaryNegationOperators<N, N>
                => (-vector.Y, vector.X);
        }

        /// <summary>
        /// Second normal vector of an input vector. It is the second one in circular direction from (+)x-axis to (+)y-axis.
        /// </summary>
        /// <remarks> 
        /// <a href="https://en.wikipedia.org/wiki/Normal_(geometry)">wikipedia</a>
        /// </remarks>
        public static class Normal2
        {
            public static (N X, N Y) FromCartesian<N>(N x, N y)
                where N : IUnaryNegationOperators<N, N>
                => (y, -x);

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
        /// Direction from one to the other vector.
        /// </summary>
        public static class Direction
        {
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

        /// <summary> Polar angle of sum of two polar vectors. </summary>
        /// <remarks> https://math.stackexchange.com/questions/1365622/adding-two-polar-vectors </remarks>
        //public static double SumΦ1(double v1Length, double v2Length, double v1φ1, double v2φ1)
        //    => v1φ1 + Atan2(v2Length * Sin(v2φ1 - v1φ1), v1Length + v2Length * Cos(v2φ1 - v1φ1));


    }
}
