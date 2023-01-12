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

            /// <summary>
            /// Determines if a point is a part of circle perimeter.
            /// </summary>
            /// <remarks>
            /// <a href="https://mathworld.wolfram.com/Circle-LineIntersection.html">mathword</a>
            /// </remarks>
            public static class Includes
            {
                public static bool Point<N>((N X, N Y) center, N radius, (N X, N Y) point)
                    where N : IFloatingPoint<N>, IRootFunctions<N>
                    => PT.Hypotenuse(point.X - center.X, point.Y - center.Y) == radius;
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
                    where N : IFloatingPoint<N>
                    => throw new NotImplementedException("todo");

                public static bool Point<N>((N X, N Y) center, N radius, (N X, N Y) point)
                    where N : IFloatingPoint<N>
                    => throw new NotImplementedException("todo");

                public static bool LineSegment<N>(N radius, (N X, N Y) a, (N X, N Y) b)
                    where N : IFloatingPoint<N>
                    => throw new NotImplementedException("todo");

                public static bool LineSegment<N>((N X, N Y) center, N radius, (N X, N Y) a, (N X, N Y) b)
                    where N : IFloatingPoint<N>
                    => throw new NotImplementedException("todo");

                public static bool Circle<N>(N radius, (N X, N Y) center2, N radius2)
                    where N : IFloatingPoint<N>
                    => throw new NotImplementedException("todo");

                public static bool Circle<N>((N X, N Y) center, N radius, (N X, N Y) center2, N radius2)
                    where N : IFloatingPoint<N>
                    => throw new NotImplementedException("todo");
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
    }
}
