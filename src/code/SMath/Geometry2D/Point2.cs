using System.Numerics;

namespace SMath.GeometryD2
{
    /// <summary>
    /// Point in two dimensions.
    /// </summary>
    public static class Point2
    {
        /// <summary>
        /// Euclidean distance of point and origin.
        /// </summary>
        public static N Distance<N>((N X, N Y) point)
            where N : IRootFunctions<N>
            => PT.Hypotenuse(point);

        /// <summary>
        /// Euclidean distance of two points.
        /// </summary>
        public static N Distance<N>((N X, N Y) point1, (N X, N Y) point2)
            where N : IRootFunctions<N>
            => PT.Hypotenuse(point2.X - point1.X, point2.Y - point1.Y);

        /// <summary>
        /// Manhattan or taxicab distance of point and origin.
        /// </summary>
        public static N ManhattanDistance<N>((N X, N Y) point)
            where N : INumberBase<N>
            => point.X + point.Y;

        /// <summary>
        /// Manhattan or taxicab distance of two points.
        /// </summary>
        public static N ManhattanDistance<N>((N X, N Y) point1, (N X, N Y) point2)
            where N : INumberBase<N>
            => N.Abs(point1.X - point2.X) + N.Abs(point1.Y - point2.Y);
    }
}
