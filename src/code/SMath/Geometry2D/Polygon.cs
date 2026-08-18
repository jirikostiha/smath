using System.Numerics;

namespace SMath.Geometry2D
{
    /// <summary>
    /// Polygon shape.
    /// </summary>
    /// <remarks>
    /// <a href="https://en.wikipedia.org/wiki/Polygon">wikipedia</a>
    /// </remarks>
    public static class Polygon
    {
        /// <summary>
        /// Regular polygon.
        /// </summary>
        public static class Regular
        {
            /// <summary>
            /// Sum of internal angles of a regular polygon.
            /// </summary>
            public static N InternalAngleSum<N>(N edges)
                where N : IFloatingPoint<N>
                => (edges - N.CreateChecked(2)) * N.Pi;

            /// <summary>
            /// Internal angle of a regular polygon.
            /// </summary>
            public static N InternalAngle<N>(N edges)
                where N : IFloatingPoint<N>
                => InternalAngleSum(edges) / edges;

            /// <summary>
            /// Sum of external angles of a regular polygon.
            /// </summary>
            public static N ExternalAngleSum<N>()
                where N : IFloatingPoint<N>
                => N.CreateChecked(2) * N.Pi;

            /// <summary>
            /// External angle of a regular polygon.
            /// </summary>
            public static N ExternalAngle<N>(N edges)
                where N : IFloatingPoint<N>
                => ExternalAngleSum<N>() / edges;
        }
    }
}
