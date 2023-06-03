using System.Numerics;
using System.Runtime.CompilerServices;

namespace SMath.GeometryD2
{
    /// <summary>
    /// Point in two dimensions.
    /// </summary>
    public static class Point2
    {
        /// <summary>
        /// Euclidean distance of the point and origin.
        /// </summary>
        /// <remarks>
        /// <a href="https://en.wikipedia.org/wiki/Euclidean_distance">Wikipedia</a>
        /// </remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static N Distance<N>((N X, N Y) point)
            where N : IRootFunctions<N>
            => PT.Hypotenuse(point);

        /// <summary>
        /// Euclidean distance of two points.
        /// </summary>
        /// <remarks>
        /// <a href="https://en.wikipedia.org/wiki/Euclidean_distance">Wikipedia</a>
        /// </remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static N Distance<N>((N X, N Y) point1, (N X, N Y) point2)
            where N : IRootFunctions<N>
            => PT.Hypotenuse(point2.X - point1.X, point2.Y - point1.Y);

        /// <summary>
        /// Manhattan or taxicab distance of point and origin.
        /// </summary>
        /// <remarks>
        /// <a href="https://en.wikipedia.org/wiki/Minkowski_distance">Wikipedia</a>
        /// </remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static N ManhattanDistance<N>((N X, N Y) point)
            where N : INumberBase<N>
            => N.Abs(point.X + point.Y);

        /// <summary>
        /// Manhattan or taxicab distance of two points.
        /// </summary>
        /// <remarks>
        /// <a href="https://en.wikipedia.org/wiki/Minkowski_distance">Wikipedia</a>
        /// </remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static N ManhattanDistance<N>((N X, N Y) point1, (N X, N Y) point2)
            where N : INumberBase<N>
            => N.Abs(point1.X - point2.X) + N.Abs(point1.Y - point2.Y);

        /// <summary>
        /// Chebyshev distance of point and origin
        /// </summary>
        /// <remarks>
        /// <a href="https://en.wikipedia.org/wiki/Chebyshev_distance">Wikipedia</a>
        /// </remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static N ChebyshevDistance<N>((N X, N Y) point)
            where N : INumber<N>
            => N.Max(N.Abs(point.X), N.Abs(point.Y));

        /// <summary>
        /// Chebyshev distance of two points.
        /// </summary>
        /// <remarks>
        /// <a href="https://en.wikipedia.org/wiki/Chebyshev_distance">Wikipedia</a>
        /// </remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static N ChebyshevDistance<N>((N X, N Y) point1, (N X, N Y) point2)
            where N : INumber<N>
            => N.Max(N.Abs(point1.X - point2.X), N.Abs(point1.Y - point2.Y));

        /// <summary>
        /// Minkowski distance of point and origin
        /// </summary>
        /// <remarks>
        /// <a href="https://en.wikipedia.org/wiki/Minkowski_distance">Wikipedia</a>
        /// </remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static N MinkowskiDistance<N>((N X, N Y) point, N r)
            where N : IPowerFunctions<N>
            => N.Pow(N.Pow(N.Abs(point.X), r) + N.Pow(N.Abs(point.Y), r), N.One / r);

        /// <summary>
        /// Minkowski distance of two points.
        /// </summary>
        /// <remarks>
        /// <a href="https://en.wikipedia.org/wiki/Minkowski_distance">Wikipedia</a>
        /// </remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static N MinkowskiDistance<N>((N X, N Y) point1, (N X, N Y) point2, N r)
            where N : IPowerFunctions<N>
            => N.Pow(N.Pow(N.Abs(point1.X - point2.X), r) + N.Pow(N.Abs(point1.Y - point2.Y), r), N.One / r);
    }
}
