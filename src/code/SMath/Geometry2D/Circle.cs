using System.Numerics;

namespace SMath.Geometry2D
{
    /// <summary>
    /// Circle.
    /// </summary>
    /// <remarks>
    /// <a href="https://en.wikipedia.org/wiki/Circle">wikipedia</a>
    /// <a href="https://mathworld.wolfram.com/Circle.html">mathworld</a>
    /// </remarks>
    public static class Circle
    {
        public static string SymetryGroup => "O(2)";

        /// <summary>
        /// Radius of a circle.
        /// </summary>
        public static class Radius
        {
            public static N FromCircumference<N>(N circumference)
                where N : IFloatingPoint<N>
                => circumference / (N.CreateChecked(2) * N.Pi);
            
            public static N FromArea<N>(N area)
                where N : IFloatingPoint<N>, IRootFunctions<N>
                => N.Sqrt(area / N.Pi);
        }

        /// <summary>
        /// Perimeter or curve or outline of a circle.
        /// </summary>
        public static class Perimeter
        {
            /// <summary>
            /// Circumference of a circle.
            /// </summary>
            public static class Length
            {
                public static N FromRadius<N>(N radius)
                    where N : IFloatingPoint<N>
                    => N.CreateChecked(2) * N.Pi * N.CreateChecked(radius);
            }
        }
    }
}
