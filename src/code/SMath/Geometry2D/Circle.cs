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

        public static string PlainTextEquation => "(x - a)^2 + (y - b)^2 = r^2";

        /// <summary>
        /// Radius of a circle.
        /// </summary>
        public static class Radius
        {
            public static N FromCircumference<N>(N circumference)
                where N : IFloatingPointConstants<N>
                => circumference / (N.CreateChecked(2) * N.Pi);

            public static N FromArea<N>(N area)
                where N : IRootFunctions<N>
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
                    where N : IFloatingPointConstants<N>
                    => N.CreateChecked(2) * N.Pi * N.CreateChecked(radius);
            }

            public static class Points
            {
                /// <summary>
                /// Enumerating points on circle from the +x axis towards the +y axis and beyond.
                /// </summary>
                public static IEnumerable<(N X, N Y)> FromRadius<N>(N radius, int count)
                    where N : IFloatingPoint<N>, ITrigonometricFunctions<N>
                {
                    N deltaAngle = N.CreateChecked(2) * N.Pi / N.CreateChecked(count);
                    for (int i = 0; i < count; i++)
                    {
                        N angle = N.CreateChecked(i) * deltaAngle;
                        yield return (radius * N.Cos(angle), radius * N.Sin(angle));
                    }
                }
            }
        }
    }
}
