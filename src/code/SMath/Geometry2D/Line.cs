using System;
using System.Numerics;

namespace SMath.Geometry2D
{
    /// <summary>
    /// Line in two dimensional space.
    /// </summary>
    /// <remarks>
    /// Line is defined in general form by default.
    /// <a href="https://en.wikipedia.org/wiki/Line_(geometry)">wikipedia</a>
    /// <a href="https://www.123calculus.com/en/line-calculator-page-7-60-100.html">calculator 1</a>
    /// </remarks>
    public static class Line
    {
        /// <summary>
        /// X axis line in general form.
        /// </summary>
        public static (N A, N B, N C) XAxis<N>()
            where N : INumberBase<N>
            => (N.Zero, N.One, N.Zero);

        /// <summary>
        /// Y axis line in general form.
        /// </summary>
        public static (N A, N B, N C) YAxis<N>()
            where N : INumberBase<N>
            => (-N.One, N.Zero, N.Zero);

        /// <summary>
        /// Identity line y=x in general form.
        /// </summary>
        public static (N A, N B, N C) Identity<N>()
            where N : INumberBase<N>
            => (-N.One, N.One, N.Zero);

        public static string PlainTextEquation
            => "a*x + b*y + c = 0";

        /// <summary>
        /// Line in general form from two points in Cartesian coordinates.
        /// </summary>
        public static (N A, N B, N C) FromTwoPoints<N>((N X, N Y) point1, (N X, N Y) point2)
            where N : IRootFunctions<N>
        {
            // direction vector = P2 - P1
            var direction = GeometricVector2.Direction.FromCartesian(point1, point2);
            // normal vector
            var normal = GeometricVector2.Normal1.FromCartesian(direction);
            // calculate c constant
            var c = -normal.X * point1.X - normal.Y * point1.Y;

            return (normal.X, normal.Y, c);
        }

        /// <summary>
        /// Line in general form from slope and y-intercept.
        /// </summary>
        public static (N A, N B, N C) FromSlopeAndYIntercept<N>(N slope, N yintercept)
            where N : INumberBase<N>
            => (-slope, N.One, -yintercept);

        /// <summary>
        /// Line in general form recounted to have a=1.
        /// </summary>
        public static (N A, N B, N C) NormalizedX<N>((N A, N B, N C) line)
            where N : INumberBase<N>
            => (N.One, line.B / line.A, line.C / line.A);

        /// <summary>
        /// Line in general form recounted to have b=1.
        /// </summary>
        public static (N A, N B, N C) NormalizedY<N>((N A, N B, N C) line)
            where N : INumberBase<N>
            => (line.A / line.B, N.One, line.C / line.B);

        /// <summary>
        /// Slope of a line.
        /// </summary>
        public static class Slope
        {
            /// <summary>
            /// Slope of a line from line general form of line.
            /// </summary>
            public static N FromGeneralForm<N>((N A, N B, N C) line)
                where N : IUnaryNegationOperators<N, N>, IDivisionOperators<N, N, N>
                => -line.A / line.B;

            /// <summary>
            /// Slope of a line from line angle.
            /// </summary>
            public static N FromAngle<N>(N angle)
                where N : IFloatingPointIeee754<N>, ITrigonometricFunctions<N>
                => angle != N.Pi / N.CreateChecked(2)
                ? N.Tan(angle)
                : N.PositiveInfinity;
        }

        /// <summary>
        /// The x-intercept is the point at which the lines crosses the x-axis.
        /// </summary>
        public static class XIntercept
        {
            /// <summary>
            /// The x-intercept of a line in general form.
            /// </summary>
            public static N FromGeneralForm<N>((N A, N B, N C) line)
                where N : IUnaryNegationOperators<N, N>, IDivisionOperators<N, N, N>
                => -line.C / line.A;
        }

        /// <summary>
        /// The y-intercept is the point at which the lines crosses the y-axis.
        /// </summary>
        public static class YIntercept
        {
            /// <summary>
            /// The y-intercept of a line in general form.
            /// </summary>
            public static N GeneralForm<N>((N A, N B, N C) line)
                where N : IUnaryNegationOperators<N, N>, IDivisionOperators<N, N, N>
                => -line.C / line.B;
        }

        /// <summary>
        /// Normal line to a given line.
        /// </summary>
        public static class NormalLine
        {
            /// <summary>
            /// Slope of a normal line.
            /// </summary>
            public static class Slope
            {
                /// <summary>
                /// Get a slope of a normal line to line determined in general form.
                /// </summary>
                public static N FromGeneralForm<N>(N a, N b)
                    where N : IDivisionOperators<N, N, N>
                    => b / a;

                /// <summary>
                /// Get a slope of a normal line to line determined in general form.
                /// </summary>
                public static N FromGeneralForm<N>((N A, N B, N C) line)
                    where N : IDivisionOperators<N, N, N>
                    => line.B / line.A;
            }

            /// <summary>
            /// Get normal line in general form from line determined by general form.
            /// </summary>
            public static (N A, N B, N C) FromGeneralForm<N>((N A, N B, N C) line)
                where N : IUnaryNegationOperators<N, N>
                => (line.B, -line.A, line.C);
        }

        /// <summary>
        /// Line and ... investigation.
        /// </summary>
        public static class And
        {
            /// <summary>
            /// Line and point investigation.
            /// </summary>
            public static class Point
            {
                /// <summary>
                /// Projection of point to line.
                /// </summary>
                public static class Projection
                {
                    public static (N X, N Y) FromPoints<N>((N X, N Y) spoint1, (N X, N Y) spoint2, (N X, N Y) point)
                        where N : IRootFunctions<N>
                    {
                        var e1 = GeometricVector2.Direction.FromCartesian(spoint1, spoint2);
                        var e2 = GeometricVector2.Direction.FromCartesian(spoint1, point);
                        var dp = GeometricVector2.DotProduct.FromCartesian(e1, e2);
                        var len = GeometricVector2.Magnitude.FromCartesian(e1);
                        var len2 = len * len;
                        var projection = (spoint1.X + (dp * e1.X) / len2,
                                          spoint1.Y + (dp * e1.Y) / len2);

                        return projection;
                    }
                }

                /// <summary>
                /// Line and point distance.
                /// </summary>
                /// <remarks>
                /// <a href="https://en.wikipedia.org/wiki/Distance_from_a_point_to_a_line">wikipedia</a>
                /// </remarks>
                public static class Distance
                {
                    /// <summary>
                    /// Calculates the distance of line in general form and point.
                    /// </summary>
                    public static N FromGeneralForm<N>((N A, N B, N C) line, (N X, N Y) point)
                        where N : IRootFunctions<N>
                        => N.Abs(line.A * point.X + line.B * point.Y + line.C) / PT.Hypotenuse(line.A, line.B);

                    /// <summary>
                    /// Calculates distance of line determined by two points and point.
                    /// </summary>
                    public static N FromPoints<N>((N X, N Y) linePoint1, (N X, N Y) linePoint2, (N X, N Y) point)
                        where N : IRootFunctions<N>
                        => N.Abs((linePoint2.X - linePoint1.X) * (linePoint1.Y - point.Y)
                            - (linePoint1.X - point.X) * (linePoint2.Y - linePoint1.Y))
                            / PT.Hypotenuse(linePoint1.X - linePoint2.X, linePoint1.Y - linePoint2.Y);
                }

                /// <summary>
                /// Line and point intersection.
                /// </summary>
                public static class Intersection
                {
                    /// <summary>
                    /// Determine if line includes point.
                    /// </summary>
                    public static bool FromGeneralForm<N>((N A, N B, N C) line, (N X, N Y) point)
                        where N : INumberBase<N>
                        => line.A * point.X + line.B * point.Y + line.C == N.Zero;
                }
            }

            /// <summary>
            /// Line and line investigation.
            /// </summary>
            public static class Line
            {
                /// <summary>
                /// Line and line intersection.
                /// </summary>
                /// <remarks>
                /// <a href="https://en.wikipedia.org/wiki/Line%E2%80%93line_intersection">wikipedia</a>
                /// </remarks>
                public static class Intersection
                {
                    /// <summary>
                    /// Line intersects line.
                    /// </summary>
                    /// <returns>
                    ///     null: parallel or identical
                    ///     (x,y): intersection point
                    /// </returns>
                    public static (N X, N Y)? FromGeneralForm<N>((N A, N B, N C) line1, (N A, N B, N C) line2)
                        where N : INumberBase<N>
                    {
                        var determinant = Determinant.FromCells(line1.A, line1.B, line2.A, line2.B);
                        return determinant != N.Zero
                            ? ((line2.B * (-line1.C) - line1.B * (-line2.C)) / determinant,
                               (line1.A * (-line2.C) - line2.A * (-line1.C)) / determinant)
                            : null;
                    }
                }
            }
        }

        /// <summary>
        /// Ray.
        /// Determined by point and angle from x-axis.
        /// </summary>
        public static class Ray
        {
            /// <summary>
            /// Positive x-axis ray.
            /// </summary>
            public static ((N X, N Y) Point, N Angle) PositiveXAxis<N>()
                where N : IFloatingPointConstants<N>
                => ((N.Zero, N.Zero), N.Zero);

            /// <summary>
            /// Positive y-axis ray.
            /// </summary>
            public static ((N X, N Y) Point, N Angle) PositiveYAxis<N>()
                where N : IFloatingPointConstants<N>
                => ((N.Zero, N.Zero), N.Pi / N.CreateChecked(2));

            /// <summary>
            /// Positive identity ray.
            /// </summary>
            public static ((N X, N Y) Point, N Angle) PositiveIdentity<N>()
                where N : IFloatingPointConstants<N>
                => ((N.Zero, N.Zero), N.Pi / N.CreateChecked(4));

            /// <summary>
            /// Points on a ray.
            /// </summary>
            public static class Points
            {
                /// <summary>
                /// Get points on a ray determined by origin point and angle.
                /// </summary>
                public static IEnumerable<(N X, N Y)> Get<N>(N angle, N step, int count)
                    where N : ITrigonometricFunctions<N>
                {
                    for (int i = 1; i <= count; i++)
                        yield return (
                            Circle.Perimeter.Point.XFromAngle(N.CreateChecked(i) * step, angle),
                            Circle.Perimeter.Point.YFromAngle(N.CreateChecked(i) * step, angle));
                }
            }
        }

        /// <summary>
        /// Line Segment
        /// Determined by two points.
        /// </summary>
        public static class Segment
        {
            /// <summary>
            /// X unit segment.
            /// </summary>
            public static ((N X, N Y) Point1, (N X, N Y) Point2) XUnit<N>()
                where N : INumberBase<N>
                => ((N.Zero, N.Zero), (N.One, N.Zero));

            /// <summary>
            /// Y unit segment.
            /// </summary>
            public static ((N X, N Y) Point1, (N X, N Y) Point2) YUnit<N>()
                where N : INumberBase<N>
                => ((N.Zero, N.Zero), (N.Zero, N.One));

            /// <summary>
            /// Unit segment.
            /// </summary>
            public static ((N X, N Y) Point1, (N X, N Y) Point2) Unit<N>()
                where N : INumberBase<N>
                => ((N.Zero, N.Zero), (N.One, N.One));

            /// <summary>
            /// Length of a line segment.
            /// </summary>
            public static class Length
            {
                /// <summary>
                /// Length of a line segment determined by two points.
                /// </summary>
                public static N FromTwoPints<N>((N X, N Y) p1, (N X, N Y) p2)
                    where N : IRootFunctions<N>
                    => PT.Hypotenuse(p2.X - p1.X, p2.Y - p1.Y);
            }

            /// <summary>
            /// Slope of a line segment.
            /// </summary>
            public static class Slope
            {
                /// <summary>
                /// Get slope value from two points in 2D.
                /// </summary>
                /// <remarks>
                /// <a href="https://en.wikipedia.org/wiki/Slope">wikipedia</a>
                /// </remarks>
                public static N FromTwoPoints<N>((N X, N Y) point1, (N X, N Y) point2)
                    where N : ISubtractionOperators<N, N, N>, IDivisionOperators<N, N, N>
                    => (point2.Y - point1.Y) / (point2.X - point1.X);
            }

            /// <summary>
            /// Points on a segment line.
            /// </summary>
            public static class Points
            {
                /// <summary>
                /// Get n points on a line segment determined by two points.
                /// </summary>
                public static IEnumerable<(N X, N Y)> Get<N>((N X, N Y) point1, (N X, N Y) point2, int count)
                    where N : INumberBase<N>
                {
                    var xstep = (point2.X - point1.X) / N.CreateChecked(count + 1);
                    var ystep = (point2.Y - point1.Y) / N.CreateChecked(count + 1);
                    for (int i = 1; i <= count; i++)
                        yield return (
                            point1.X + N.CreateChecked(i) * xstep,
                            point1.Y + N.CreateChecked(i) * ystep);
                }
            }

            /// <summary>
            /// Parallel line segment to original segment.
            /// </summary>
            public static class Parallel
            {
                /// <summary>
                /// Get a point which is with s2p1 line segment parallel to (s1p1, s1p2).
                /// </summary>
                public static (N X, N Y) FromThreePoints<N>((N X, N Y) point1, (N X, N Y) point2, (N X, N Y) seedPoint)
                    where N : INumberBase<N>
                    => (seedPoint.X + point2.X - point1.X, seedPoint.Y + point2.Y - point1.Y);
            }

            /// <summary>
            /// Parallel line segments to original segment.
            /// </summary>
            public static class Parallels
            {
                /// <summary>
                /// Get line segment parallels to given line segment in seed direction.
                /// </summary>
                public static IEnumerable<((N X, N Y) Point1, (N X, N Y) Point2)> Get<N>((N X, N Y) basePoint,
                    (N X, N Y) direction, (N X, N Y) seedDirection, params (N Distance, N Length)[] @params)
                    where N : INumberBase<N>, IRootFunctions<N>
                    => Get(basePoint, direction, seedDirection, @params);

                /// <summary>
                /// Get line segment parallels to given line segment in seed direction.
                /// </summary>
                public static IEnumerable<((N X, N Y) Point1, (N X, N Y) Point2)> Get<N>((N X, N Y) basePoint,
                    (N X, N Y) direction, (N X, N Y) seedDirection, IList<(N Distance, N Length)> @params)
                    where N : INumberBase<N>, IRootFunctions<N>
                {
                    direction = GeometricVector2.Cartesian.Normalized(direction);
                    seedDirection = GeometricVector2.Cartesian.Normalized(seedDirection);

                    foreach (var param in @params)
                    {
                        yield return (
                            (basePoint.X + seedDirection.X * param.Distance,
                             basePoint.Y + seedDirection.Y * param.Distance),
                            (basePoint.X + seedDirection.X * param.Distance + direction.X * param.Length,
                             basePoint.Y + seedDirection.Y * param.Distance + direction.Y * param.Length));
                    }
                }
            }

            /// <summary>
            /// Line segment and ... investigation.
            /// </summary>
            public static class And
            {
                /// <summary>
                /// Line segment and point investigation.
                /// </summary>
                public static class Point
                {
                    /// <summary>
                    /// Distance of line segment and point.
                    /// <a href="https://en.wikipedia.org/wiki/Distance_from_a_point_to_a_line#Line_defined_by_two_points">wikipedia</a>
                    /// </summary>
                    public static class Distance
                    {
                        /// <summary>
                        /// Determine distance of line segment and point.
                        /// </summary>
                        public static N FromPoints<N>((N X, N Y) sp1, (N X, N Y) sp2, (N X, N Y) point)
                            where N : IRootFunctions<N>, IComparisonOperators<N, N, bool>
                        {

                            var p1ToP2Dir = GeometricVector2.Direction.FromCartesian(sp1, sp2);
                            var p1ToP2Length = GeometricVector2.Magnitude.FromCartesian(p1ToP2Dir);
                            var p1ToP3Dir = GeometricVector2.Direction.FromCartesian(sp1, point);

                            if (p1ToP2Length == N.Zero) // segment is point
                                return GeometricVector2.Magnitude.FromCartesian(p1ToP3Dir);

                            var u = (p1ToP3Dir.X * p1ToP2Dir.X + p1ToP3Dir.Y * p1ToP2Dir.Y) / (p1ToP2Dir.X * p1ToP2Dir.X + p1ToP2Dir.Y * p1ToP2Dir.Y);

                            if (u < N.Zero)
                            {
                                return GeometricVector2.Magnitude.FromCartesian(p1ToP3Dir);
                            }
                            else if (u > N.One)
                            {
                                return GeometricVector2.Distance.FromCartesian(sp2, point);
                            }

                            var x = sp1.X + u * p1ToP2Dir.X;
                            var y = sp1.Y + u * p1ToP2Dir.Y;
                            return GeometricVector2.Distance.FromCartesian((x, y), point);
                        }
                    }

                    /// <summary>
                    /// Line segment and point intersection.
                    /// </summary>
                    public static class Intersection
                    {
                        /// <summary>
                        /// Determine if line segment includes the point.
                        /// </summary>
                        public static bool FromPoints<N>((N X, N Y) sp1, (N X, N Y) sp2, (N X, N Y) point)
                            where N : INumberBase<N>, IComparisonOperators<N, N, bool>
                        {
                            var ab = GeometricVector2.Direction.FromCartesian(sp1, sp2);
                            var ac = GeometricVector2.Direction.FromCartesian(sp1, point);

                            var abc = GeometricVector2.CrossProduct.FromCartesian(ab, ac);
                            if (abc != N.Zero)
                                return false;

                            var kac = GeometricVector2.DotProduct.FromCartesian(ab, ac);
                            if (kac < N.Zero)
                                return false;
                            if (kac == N.Zero)
                                return true;

                            var kab = GeometricVector2.DotProduct.FromCartesian(ab, ab);
                            if (kac > kab)
                                return false;
                            if (kac == kab)
                                return true;

                            return true;
                        }
                    }
                }

                /// <summary>
                /// Line segment and line segment investigation.
                /// </summary>
                public static class Segment
                {
                    /// <summary>
                    /// Line segment and line segment intersection.
                    /// </summary>
                    /// <remarks>
                    /// <a href="https://en.wikipedia.org/wiki/Intersection_(geometry)#Two_line_segments">wikipedia</a>
                    /// </remarks>
                    public static class Intersection
                    {
                        public static (N X, N Y)? FromPoints<N>((N X, N Y) s1p1, (N X, N Y) s1p2, (N X, N Y) s2p1, (N X, N Y) s2p2)
                            where N : INumberBase<N>, IComparisonOperators<N, N, bool>
                        {
                            var s1Dir = GeometricVector2.Direction.FromCartesian(s1p1, s1p2);
                            var s2Dir = GeometricVector2.Direction.FromCartesian(s2p1, s2p2);

                            var denominator = s2Dir.Y * s1Dir.X - s2Dir.X * s1Dir.Y;
                            if (denominator == N.Zero)
                                return null;

                            var p3p1Dir = GeometricVector2.Direction.FromCartesian(s2p1, s1p1);
                            var numeratorA = s2Dir.X * p3p1Dir.Y - s2Dir.Y * p3p1Dir.X;
                            var numeratorB = s1Dir.X * p3p1Dir.Y - s1Dir.Y * p3p1Dir.X;

                            var ua = numeratorA / denominator;
                            var ub = numeratorB / denominator;
                            if (ua >= N.Zero && ua <= N.One && ub >= N.Zero && ub <= N.One)
                                return (s1p1.X + ua * s1Dir.X, s1p1.Y + ua * s1Dir.Y);

                            return null;
                        }
                    }
                }
            }
        }
    }
}
