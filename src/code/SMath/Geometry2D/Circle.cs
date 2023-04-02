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

        /// <summary>
        /// Circular arc.
        /// </summary>
        /// <remarks>
        /// <a href="https://en.wikipedia.org/wiki/Circular_arc">wikipedia</a>
        /// <a href="https://mathworld.wolfram.com/Arc.html">mathworld</a>
        /// </remarks>
        public static class Arc
        {
            /// <summary>
            /// Length of a circular arc.
            /// </summary>
            public static class Length
            {
                public static N FromAngle<N>(N radius, N angle)
                    where N : IFloatingPoint<N>
                    => angle * radius;
            }
        }

        /// <summary>
        /// Chord of a circle.
        /// </summary>
        /// <remarks>
        /// <a href="https://en.wikipedia.org/wiki/Chord_(geometry)">wikipedia</a>
        /// <a href="https://mathworld.wolfram.com/Chord.html">mathworld</a>
        /// </remarks>
        public static class Chord
        {
            /// <summary>
            /// Length of a chord of a circle.
            /// </summary>
            public static class Length
            {
                public static N FromAngle<N>(N radius, N angle)
                    where N : IFloatingPoint<N>, ITrigonometricFunctions<N>
                    => radius * N.CreateChecked(2) * N.Sin(angle / N.CreateChecked(2));
            }

            /// <summary>
            /// Sagitta of circle determined by chord.
            /// </summary>
            public static class Sagitta
            {
                public static N FromAngle<N>(N radius, N angle)
                    where N : IFloatingPoint<N>, ITrigonometricFunctions<N>
                    => radius * (N.CreateChecked(1) - N.Cos(angle / N.CreateChecked(2)));
            }

            /// <summary>
            /// Apothem of circle determined by chord.
            /// </summary>
            public static class Apothem
            {
                public static N FromAngle<N>(N radius, N angle)
                    where N : IFloatingPoint<N>, ITrigonometricFunctions<N>
                    => radius * N.Cos(angle / N.CreateChecked(2));
            }
        }

        /// <summary>
        /// Circular sector.
        /// </summary>
        /// <remarks>
        /// <a href="https://en.wikipedia.org/wiki/Circular_sector">wikipedia</a>
        /// <a href="https://mathworld.wolfram.com/CircularSector.html">mathworld</a>
        /// </remarks>
        public static class Sector
        {
            /// <summary>
            /// Perimeter of a circular sector.
            /// </summary>
            public static class Perimeter
            {
                /// <summary>
                /// Length of perimeter of a circular sector.
                /// </summary>
                public static class Length
                {
                    public static N FromAngle<N>(N radius, N angle)
                        where N : IFloatingPoint<N>
                        => radius + radius + Arc.Length.FromAngle(radius, angle);

                    public static N FromArcLength<N>(N radius, N arcLength)
                        where N : IFloatingPoint<N>
                        => radius + radius + arcLength;
                }
            }

            /// <summary>
            /// Enclosed plane region of a circular sector.
            /// </summary>
            public static class Region
            {
                /// <summary>
                /// Enclosed plane region area of a circular sector.
                /// </summary>
                public static class Area
                {
                    public static N FromArcAngle<N>(N radius, N arcAngle)
                        where N : IFloatingPoint<N>
                        => radius * radius * arcAngle / N.CreateChecked(2);

                    public static N FromArcLength<N>(N radius, N length)
                        where N : IFloatingPoint<N>
                        => radius * length / N.CreateChecked(2);
                }
            }
        }

        /// <summary>
        /// Circular segment.
        /// </summary>
        /// <remarks>
        /// <a href="https://en.wikipedia.org/wiki/Circular_segment">wikipedia</a>
        /// <a href="https://mathworld.wolfram.com/CircularSegment.html">mathworld</a>
        /// </remarks>
        public static class Segment
        {
            /// <summary>
            /// Perimeter of a circular segment.
            /// </summary>
            public static class Perimeter
            {
                /// <summary>
                /// Length of perimeter of a circular segment.
                /// </summary>
                public static class Length
                {
                    public static N FromAngle<N>(N radius, N angle)
                        where N : IFloatingPoint<N>, ITrigonometricFunctions<N>
                        => Arc.Length.FromAngle(radius, angle) + Chord.Length.FromAngle(radius, angle);
                }
            }

            /// <summary>
            /// Enclosed plane region of a circular segment.
            /// </summary>
            public static class Region
            {
                /// <summary>
                /// Enclosed plane region area of a circular segment.
                /// </summary>
                public static class Area
                {
                    public static N FromAngle<N>(N radius, N angle)
                        where N : IFloatingPoint<N>, ITrigonometricFunctions<N>
                        => (radius * radius) / N.CreateChecked(2) * (angle - N.Sin(angle));
                }
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
    }
}
