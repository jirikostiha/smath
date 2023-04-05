namespace SMath.Geometry2D
{
    using System;
    using System.Numerics;

    /// <summary>
    /// Euclidean or geometric vector with 2 components.
    /// </summary>
    /// <remarks>
    /// <a href="https://en.wikipedia.org/wiki/Euclidean_vector">wikipedia</a>
    /// </remarks>
    public static class GeometricVector2
    {
        /// <summary>
        /// Length or magnitude or scalar value of the vector in x1-x2 plane.
        /// </summary>
        public static N Magnitude<N>(N x1, N x2)
            where N : IRootFunctions<N>
            => PT.Hypotenuse(x1, x2);

        public static N Magnitude<N>((N X1, N X2) vector)
            where N : IRootFunctions<N>
            => PT.Hypotenuse(vector.X1, vector.X2);

        /// <summary> 
        /// Polar angle. Angle from x1 axis to x2 axis. 
        /// </summary>
        public static N Φ1<N>(N x1, N x2)
            where N : ITrigonometricFunctions<N>
            => N.Atan(x2 / x1);

        public static N X1<N>(N magnitude, N φ1)
            where N : ITrigonometricFunctions<N>
            => magnitude * N.Cos(φ1);

        public static N X2<N>(N magnitude, N φ1)
            where N : ITrigonometricFunctions<N>
            => magnitude * N.Sin(φ1);

        public static (N Magnitude, N Φ1) CartesianToPolar<N>(N x1, N x2)
            where N : IRootFunctions<N>, ITrigonometricFunctions<N>
            => (Magnitude(x1, x2), Φ1(x1, x2));

        public static (N X1, N X2) PolarToCartesian<N>(N magnitude, N φ1)
            where N : ITrigonometricFunctions<N>
            => (X1(magnitude, φ1), X2(magnitude, φ1));

        public static (N X1, N X2) Normalized<N>(N x1, N x2)
            where N : IRootFunctions<N>
        {
            var magnitude = Magnitude(x1, x2);
            return (x1 / magnitude, x2 / magnitude);
        }

        public static (N X1, N X2) Normalized<N>((N X1, N X2) vector)
            where N : IRootFunctions<N>
        {
            var magnitude = Magnitude(vector);
            return (vector.X1 / magnitude, vector.X2 / magnitude);
        }

        public static N Distance<N>(N v1x1, N v1x2, N v2x1, N v2x2)
            where N : IRootFunctions<N>
            => PT.Hypotenuse(v1x1 - v2x1, v1x2 - v2x2);

        public static N Distance<N>((N X1, N X2) vector1, (N X1, N X2) vector2)
            where N : IRootFunctions<N>
            => PT.Hypotenuse(vector1.X1 - vector2.X1, vector1.X2 - vector2.X2);

        public static (N X1, N X2) Direction<N>(N v1x1, N v1x2, N v2x1, N v2x2)
            where N : ISubtractionOperators<N, N, N>
            => (v2x1 - v1x1, v2x2 - v1x2);

        public static (N X1, N X2) Direction<N>((N X1, N X2) vector1, (N X1, N X2) vector2)
            where N : ISubtractionOperators<N, N, N>
            => (vector2.X1 - vector1.X1, vector2.X2 - vector1.X2);

        /// <summary> Calculate dot product or scalar product. </summary>
        //public static double DotProduct(double v1x1, double v1x2, double v2x1, double v2x2)
        //    => v1x1 * v2x1 + v1x2 * v2x2;

        /// <summary> Calculate dot product or scalar product. </summary>
        //public static double DotProduct(double v1Length, double v2Length, double angle)
        //    => v1Length * v2Length * Cos(angle);

        //public static (N X1, N X2) Kvadrantized<N>(N x1, N x2)
        //    where N : INumberBase<N>
        //    => (x1 / N.Abs(x1), x2 / N.Abs(x2));

        ///// <summary> Length of sum of two polar vectors. </summary>
        ///// <remarks> https://math.stackexchange.com/questions/1365622/adding-two-polar-vectors </remarks>
        //public static double SumLength(double v1Length, double v2Length, double v1φ1, double v2φ1)
        //    => Root2.f(Power2.f(v1Length) + Power2.f(v2Length) + 2 * v1Length * v2Length * Cos(v2φ1 - v1φ1));

        ///// <summary> Polar angle of sum of two polar vectors. </summary>
        ///// <remarks> https://math.stackexchange.com/questions/1365622/adding-two-polar-vectors </remarks>
        //public static double SumΦ1(double v1Length, double v2Length, double v1φ1, double v2φ1)
        //    => v1φ1 + Atan2(v2Length * Sin(v2φ1 - v1φ1), v1Length + v2Length * Cos(v2φ1 - v1φ1));

        /// <summary>
        /// First normal vector of an input vector. It is the first one in circular direction from (+)x-axis to (+)y-axis.
        /// </summary>
        /// <remarks> 
        /// <a href="https://en.wikipedia.org/wiki/Normal_(geometry)">wikipedia</a>
        /// </remarks>
        public static (N X1, N X2) Normal1<N>(N x1, N x2)
            where N : IUnaryNegationOperators<N, N>
            => (-x2, x1);

        /// <summary>
        /// First normal vector of an input vector. It is the first one in circular direction from (+)x-axis to (+)y-axis.
        /// </summary>
        /// <remarks> 
        /// <a href="https://en.wikipedia.org/wiki/Normal_(geometry)">wikipedia</a>
        /// </remarks>
        public static (N X1, N X2) Normal1<N>((N X1, N X2) vector)
            where N : IUnaryNegationOperators<N, N>
            => (-vector.X2, vector.X1);

        /// <summary>
        /// Second normal vector of an input vector. It is the second one in circular direction from (+)x-axis to (+)y-axis.
        /// </summary>
        /// <remarks> 
        /// <a href="https://en.wikipedia.org/wiki/Normal_(geometry)">wikipedia</a>
        /// </remarks>
        public static (N X1, N X2) Normal2<N>(N x1, N x2)
            where N : IUnaryNegationOperators<N, N>
            => (x2, -x1);

        /// <summary>
        /// Second normal vector of an input vector. It is the second one in circular direction from (+)x-axis to (+)y-axis.
        /// </summary>
        /// <remarks> 
        /// <a href="https://en.wikipedia.org/wiki/Normal_(geometry)">wikipedia</a>
        /// </remarks>
        public static (N X1, N X2) Normal2<N>((N X1, N X2) vector)
            where N : IUnaryNegationOperators<N, N>
            => (vector.X2, -vector.X1);
    }
}
