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
        public static string SymmetryGroup => "O(2)";

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

            /// <summary>
            /// Point on the perimeter of a circle.
            /// </summary>
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

            /// <summary>
            /// Points on the perimeter of a circle.
            /// </summary>
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
            /// Circle perimeter and ... investigation.
            /// </summary>
            public static class And
            {
                /// <summary>
                /// Circle perimeter and point investigation.
                /// </summary>
                public static class OtherPoint
                {
                    /// <summary>
                    /// Circle perimeter and point distance.
                    /// </summary>
                    public static class Distance
                    {
                        /// <summary>
                        /// Calculates the distance of circle perimeter and point.
                        /// </summary>
                        public static N FromRadius<N>(N radius, (N X, N Y) point)
                            where N : IRootFunctions<N>
                            => N.Abs(GeometricVector2.Magnitude.FromCartesian(x, y) - radius);

                        /// <summary>
                        /// Calculates the distance of circle perimeter and point.
                        /// </summary>
                        public static N FromRadius<N>((N X, N Y) center, N radius, (N X, N Y) point)
                            where N : IRootFunctions<N>
                            => N.Abs(GeometricVector2.Magnitude.FromCartesian(center.X + x, center.Y + y) - radius);
                    }

                    /// <summary>
                    /// Circle perimeter and point intersection.
                    /// </summary>
                    public static class Intersection
                    {
                        public static bool FromRadius<N>(N radius, (N X, N Y) point)
                            where N : IRootFunctions<N>
                            => PT.Hypotenuse(point.X, point.Y) == radius;

                        public static bool Point<N>((N X, N Y) center, N radius, (N X, N Y) point)
                            where N : IRootFunctions<N>
                            => PT.Hypotenuse(point.X - center.X, point.Y - center.Y) == radius;
                    }
                }

                /// <summary>
                /// Circle perimeter and line investigation.
                /// </summary>
                public static class OtherLine
                {
                    /// <summary>
                    /// Circle perimeter and line distance.
                    /// </summary>
                    public static class Distance
                    {
                        /// <summary>
                        /// Calculates the distance of circle perimeter determined by radius from line determined in general form.
                        /// </summary>
                        public static N FromRadius<N>(N radius, (N A, N B, N C) line)
                            where N : INumberBase<N>
                        {
                            //var centerProjection = Geometry2D.Line.And.Point.Projection.FromGeneralForm(line, (0, 0));

                            //https://math.stackexchange.com/questions/1481904/distance-between-line-and-circle
                            var d = N.Abs(line.C) / PT.Hypotenuse(line.A, line.B);


                            //throw new NotImplementedException("todo");
                            return d;
                        }

                        /// <summary>
                        /// Calculates the distance of circle perimeter determined by center and radius from line determined in general form.
                        /// </summary>
                        public static N FromRadius<N>((N X, N Y) center, N radius, (N A, N B, N C) line)
                            where N : INumberBase<N>
                        {
                            var d = N.Abs(line.A * center.X + line.B * center.Y + line.C) / PT.Hypotenuse(line.A, line.B);

                            //=> throw new NotImplementedException("todo");
                            return d;
                        }
                    }

                    /// <summary>
                    /// Circle perimeter and line intersection.
                    /// </summary>
                    public static class Intersection
                    {
                        /// <summary>
                        /// Calculates the intersection of circle perimeter determined by radius with line determined in general form.
                        /// </summary>
                        /// <remarks>

                        /// </remarks>
                        public static ((N X, N Y) Point1, (N X, N Y) Point2)? FromRadius<N>(N radius, (N A, N B, N C) line)
                            where N : IRootFunctions<N>
                            => throw new NotImplementedException("todo");
                    }
                }

                /// <summary>
                /// Circle perimeter and circle investigation.
                /// </summary>
                public static class OtherCircle
                {
                    /// <summary>
                    /// Circle perimeter and circle distance.
                    /// </summary>
                    public static class Distance
                    {
                        /// <summary>
                        /// Calculates the distance of circle perimeter and circle.
                        /// </summary>
                        public static N FromRadius<N>(N radius, (N X, N Y) otherCenter, N otherRadius)
                            where N : IRootFunctions<N>
                        {
                            var centerDistance = PT.Hypotenuse(otherCenter.X, otherCenter.Y);
                            var d = centerDistance - radius - otherRadius;

                            return d > 0
                                ? d
                                : centerDistance + otherRadius < radius
                                    ? 0
                                    : centerDistance - otherRadius;

                            //throw new NotImplementedException("todo");
                        }

                        /// <summary>
                        /// Calculates the distance of circle perimeter and circle.
                        /// </summary>
                        public static N FromRadius<N>((N X, N Y) center, N radius, (N X, N Y) otherCenter, N otherRadius)
                            where N : IRootFunctions<N>
                        {
                            var centerDistance = PT.Hypotenuse(center.X - otherCenter.X, center.Y - otherCenter.Y);
                            var d = centerDistance - radius - otherRadius;

                            return d > N.Zero
                                ? d
                                : centerDistance + otherRadius < radius
                                    ? N.Zero
                                    : centerDistance - otherRadius; //todo
                        }
                    }

                    /// <summary>
                    /// Circle perimeter and circle perimeter intersection.
                    /// </summary>
                    public static class Intersection
                    {
                        /// <summary>
                        /// Calculates the intersection of circle perimeter determined by radius with other circle perimeter determined by center and radius.
                        /// </summary>
                        /// <remarks>
                        /// <a href="https://en.wikipedia.org/wiki/Intersection_(geometry)#Two_circles">Wikipedia</a>
                        /// <a href="https://mathworld.wolfram.com/Circle-CircleIntersection.html">Wolfram Mathworld</a>
                        /// </remarks>
                        public static ((N X, N Y) Point1, (N X, N Y) Point2)? FromRadius<N>(N radius, (N X, N Y) otherCenter, N otherRadius)
                            where N : IRootFunctions<N>
                            => throw new NotImplementedException("todo");

                        /// <summary>
                        /// Calculates the intersection of circle perimeter determined by center and radius with other circle perimeter determined by center and radius.
                        /// </summary>
                        /// <remarks>
                        /// <a href="https://en.wikipedia.org/wiki/Intersection_(geometry)#Two_circles">Wikipedia</a>
                        /// <a href="https://mathworld.wolfram.com/Circle-CircleIntersection.html">Wolfram Mathworld</a>
                        /// </remarks>
                        public static ((N X, N Y) Point1, (N X, N Y) Point2) FromRadius<N>((N X, N Y) center, N radius, (N X, N Y) otherCenter, N otherRadius)
                            where N : IRootFunctions<N>
                            => throw new NotImplementedException("todo");
                    }
                }
            }
        }

        /// <summary>
        /// Plane region enclosed by a circle.
        /// </summary>
        public static class Region
        {
            /// <summary>
            /// Area of a plane region enclosed by circle.
            /// </summary>
            /// <remarks>
            /// <a href="https://en.wikipedia.org/wiki/Area_of_a_circle">wikipedia</a>
            /// </remarks>
            public static class Area
            {
                public static N FromRadius<N>(N radius)
                    where N : IFloatingPoint<N>
                    => N.Pi * radius * radius;
            }

            /// <summary>
            /// Circle region and ... investigation.
            /// </summary>
            public static class And
            {
                /// <summary>
                /// Circle region and point investigation.
                /// </summary>
                public static class OtherPoint
                {
                    /// <summary>
                    /// Circle region and point distance.
                    /// </summary>
                    public static class Distance
                    {
                        /// <summary>
                        /// Calculates the distance of circle region and point.
                        /// </summary>
                        public static N FromRadius<N>(N radius, (N X, N Y) point)
                            where N : IRootFunctions<N>
                            => N.Max(PT.Hypotenuse(point.X, point.Y) - radius, N.Zero);

                        /// <summary>
                        /// Calculates the distance of circle region and point.
                        /// </summary>
                        public static N FromRadius<N>((N X, N Y) center, N radius, (N X, N Y) point)
                            where N : IRootFunctions<N>
                            => N.Max(PT.Hypotenuse(point.X - center.X, point.Y - center.Y) - radius, N.Zero);
                    }

                    /// <summary>
                    /// Circle region and point intersection.
                    /// </summary>
                    public static class Intersection
                    {
                        public static bool FromRadius<N>(N radius, (N X, N Y) point)
                            where N : IRootFunctions<N>, IComparisonOperators<N, N, bool>
                            => PT.Hypotenuse(point.X, point.Y) <= radius;

                        public static bool FromRadius<N>((N X, N Y) center, N radius, (N X, N Y) point)
                            where N : IRootFunctions<N>, IComparisonOperators<N, N, bool>
                            => PT.Hypotenuse(point.X - center.X, point.Y - center.Y) <= radius;
                    }
                }

                /// <summary>
                /// Circle region and circle investigation.
                /// </summary>
                public static class OtherCircle
                {
                    /// <summary>
                    /// Circle region and circle distance.
                    /// </summary>
                    public static class Distance
                    {
                        /// <summary>
                        /// Calculates the distance of circle region and circle.
                        /// </summary>
                        public static N FromRadius<N>(N radius, (N X, N Y) point)
                            where N : IRootFunctions<N>
                            => throw new NotImplementedException("todo");
                    }
                }
            }
        }

        /// <summary>
        /// Tangent line of a circle.
        /// </summary>
        public static class TangentLine
        {
            /// <summary>
            /// Get tangent line of a circle determined by center in origin and angle.
            /// </summary>
            public static (N A, N B, N C) FromAngle<N>(N radius, N angle)
                where N : ITrigonometricFunctions<N>
                => (
                    Perimeter.Point.XFromAngle(radius, angle),
                    Perimeter.Point.YFromAngle(radius, angle),
                    -(radius * radius));
        }

        /// <summary>
        /// Common point(s) of a circle and it's tangent line.
        /// </summary>
        public static class TangentPoint
        {
            public static ((N X, N Y) Point1, (N X, N Y) Point2)? FromPoint<N>(N radius, (N X, N Y) point)
                where N : IRootFunctions<N>, IComparisonOperators<N, N, bool>
            {
                // distance from center to outer point
                var d = PT.Hypotenuse(point.X, point.Y);

                if (d < radius || radius <= N.Zero)
                    return default;

                if (radius == d)
                    return (point, point);

                var a = radius * radius / d;
                var q = radius * N.Sqrt((d * d) - (radius * radius)) / d;
                var cpNormalized = GeometricVector2.Cartesian.Normalized(point);
                var cpNormal = GeometricVector2.Normal1.FromCartesian(cpNormalized);
                var vaX = cpNormalized.X * a;
                var vaY = cpNormalized.Y * a;

                return (
                    (vaX + cpNormal.X * q, vaY + cpNormal.Y * q),
                    (vaX - cpNormal.X * q, vaY - cpNormal.Y * q));
            }
        }

        /// <summary>
        /// Arc of a circle.
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
            /// The length of a chord of a circle.
            /// </summary>
            public static class Length
            {
                public static N FromAngle<N>(N radius, N angle)
                    where N : IFloatingPoint<N>, ITrigonometricFunctions<N>
                    => radius * N.CreateChecked(2) * N.Sin(angle / N.CreateChecked(2));
            }

            /// <summary>
            /// Sagitta of a circle determined by chord.
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
        /// Sector of a circle or circular sector.
        /// </summary>
        /// <remarks>
        /// <a href="https://en.wikipedia.org/wiki/Circular_sector">wikipedia</a>
        /// <a href="https://mathworld.wolfram.com/CircularSector.html">mathworld</a>
        /// </remarks>
        public static class Sector
        {
            /// <summary>
            /// Perimeter of the circular sector.
            /// </summary>
            public static class Perimeter
            {
                /// <summary>
                /// Length of the perimeter (circumference) of the circular sector.
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
            /// Plane region enclosed by a circular sector.
            /// </summary>
            public static class Region
            {
                /// <summary>
                /// The area of plane region enclosed by a circular sector.
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
        /// Segment of a circle or circular segment.
        /// </summary>
        /// <remarks>
        /// <a href="https://en.wikipedia.org/wiki/Circular_segment">wikipedia</a>
        /// <a href="https://mathworld.wolfram.com/CircularSegment.html">mathworld</a>
        /// </remarks>
        public static class Segment
        {
            /// <summary>
            /// Radius of a circular segment.
            /// </summary>
            public static class Radius
            {
                public static N FromSagittaAndChord<N>(N sagitta, N chordLength)
                    where N : INumberBase<N>
                    => (sagitta / N.CreateChecked(2)) + (chordLength * chordLength / (N.CreateChecked(8) * sagitta));
            }

            /// <summary>
            /// Sagitta of a circular segment.
            /// </summary>
            public static class Sagitta
            {
                public static N FromAngle<N>(N radius, N angle)
                    where N : ITrigonometricFunctions<N>
                    => N.CreateChecked(2) * radius * N.Sin(angle / N.CreateChecked(2));
            }

            /// <summary>
            /// Apothem of a circular segment.
            /// </summary>
            public static class Apothem
            {
                public static N FromAngle<N>(N radius, N angle)
                    where N : ITrigonometricFunctions<N>
                    => radius * N.Cos(angle / N.CreateChecked(2));
            }

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
                /// The area of plane region enclosed by a circular segment.
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
