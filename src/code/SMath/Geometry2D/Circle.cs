using System.Numerics;

namespace SMath.Geometry2D
{
    /// <summary>
    /// Circle shape
    /// </summary>
    /// <remarks>
    /// <a href="https://en.wikipedia.org/wiki/Circle">wikipedia</a>
    /// </remarks>
    public static class Circle
    {
        public static string SymetryGroup => "O(2)";

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
            public static class Lenght
            {
                public static N FromRadius<N>(N radius) 
                    where N : IFloatingPoint<N>
                    => N.CreateChecked(2) * N.Pi * radius;
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

        public static class Arc
        {
            public static class Length
            {
                public static N FromAngle<N>(N radius, N angle)
                    where N : IFloatingPoint<N>
                    => angle * radius;
            }
        }

        public static class Chord
        {
            public static class Length
            {
                public static N FromAngle<N>(N radius, N angle)
                    where N : IFloatingPoint<N>, ITrigonometricFunctions<N>
                    => radius * N.CreateChecked(2) * N.Sin(angle / N.CreateChecked(2));
            }
        }

        public static class Region
        {
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
                    => default;

                public static bool Point<N>((N X, N Y) center, N radius, (N X, N Y) point)
                    where N : IFloatingPoint<N>
                    => default;

                public static bool LineSegment<N>(N radius, (N X, N Y) a, (N X, N Y) b)
                    where N : IFloatingPoint<N>
                    => default;

                public static bool LineSegment<N>((N X, N Y) center, N radius, (N X, N Y) a, (N X, N Y) b)
                    where N : IFloatingPoint<N>
                    => default;

                public static bool Circle<N>(N radius, (N X, N Y) center2, N radius2)
                    where N : IFloatingPoint<N>
                    => default;

                public static bool Circle<N>((N X, N Y) center, N radius, (N X, N Y) center2, N radius2)
                    where N : IFloatingPoint<N>
                    => default;
            }
        }

        public static class Sector
        {
            public static class Perimeter
            {
                public static class Lenght
                {
                    public static N FromArcAngle<N>(N radius, N arcAngle)
                        where N : IFloatingPoint<N>
                        => radius + radius + Arc.Length.FromAngle(radius, arcAngle);

                    public static N FromArcLength<N>(N radius, N arcLenght)
                        where N : IFloatingPoint<N>
                        => radius + radius + arcLenght;
                }
            }

            public static class Region
            {
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

        public static class Segment
        {
            public static class Region
            {
                public static class Area
                { }
            }
        }
    }
}
