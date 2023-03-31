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

            public static class Point
            {
                public static N XFromAngle<N>(N radius, N angle)
                    where N : ITrigonometricFunctions<N>
                    => radius * N.Cos(angle);

                public static N YFromAngle<N>(N radius, N angle)
                    where N : ITrigonometricFunctions<N>
                    => radius * N.Sin(angle);

                public static (N X, N Y) FromAngle<N>(N radius, N angle)
                  where N : ITrigonometricFunctions<N>
                  => (XFromAngle(radius, angle), YFromAngle(radius, angle));
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
                        yield return Point.FromAngle(radius, angle);
                    }
                }
            }

            /// <summary>
            /// Determines if a point is a part of circle perimeter.
            /// </summary>
            /// <remarks>
            /// <a href="https://mathworld.wolfram.com/Circle-LineIntersection.html">mathword</a>
            /// </remarks>
            public static class Includes
            {
                public static bool Point<N>(N radius, (N X, N Y) point)
                    where N : IRootFunctions<N>
                    => PT.Hypotenuse(point.X, point.Y) == radius;

                public static bool Point<N>((N X, N Y) center, N radius, (N X, N Y) point)
                    where N : IRootFunctions<N>
                    => PT.Hypotenuse(point.X - center.X, point.Y - center.Y) == radius;
            }
        }

        public static class TangentLine
        {
            public static class Slope
            {
                public static N FromAngle<N>(N angle)
                    where N : ITrigonometricFunctions<N>
                    => N.Tan(angle);
            }

            public static (N A, N B, N C) FromAngle<N>(N radius, N angle)
                where N : ITrigonometricFunctions<N>
                => (
                    Perimeter.Point.XFromAngle(radius, angle),
                    Perimeter.Point.YFromAngle(radius, angle),
                    -(radius * radius));
        }

        /// <summary>
        /// Enclosed plane region of a circle.
        /// </summary>
        public static class Region
        {
            /// <summary>
            /// Enclosed plane region area of a circle.
            /// </summary>
            public static class Area
            {
                public static N FromRadius<N>(N radius)
                    where N : IFloatingPoint<N>
                    => N.Pi * radius * radius;
            }

            public static class Includes
            {
                public static bool Point<N>(N radius, (N X, N Y) point)
                    where N : IRootFunctions<N>, IComparisonOperators<N, N, bool>
                    => PT.Hypotenuse(point.X, point.Y) <= radius;

                public static bool Point<N>((N X, N Y) center, N radius, (N X, N Y) point)
                    where N : IRootFunctions<N>, IComparisonOperators<N, N, bool>
                    => PT.Hypotenuse(point.X - center.X, point.Y - center.Y) <= radius;
            }
        }
    }
}
