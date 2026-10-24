using System.Numerics;

namespace SMath.Geometry3D;

/// <summary>Line in 3D space, determined by two points. Space has no general form of a line, a plane takes that role.</summary>
/// <remarks><a href="https://en.wikipedia.org/wiki/Line_(geometry)">Wikipedia</a></remarks>
public static class Line3
{
    /// <summary>Line and ... investigation.</summary>
    public static class And
    {
        /// <summary>Line and point investigation.</summary>
        public static class Point
        {
            /// <summary>Projection of point to line.</summary>
            public static class Projection
            {
                /// <summary>Projection of a point to the line through two points. The first point when the line points coincide.</summary>
                public static (N X, N Y, N Z) FromPoints<N>((N X, N Y, N Z) linePoint1, (N X, N Y, N Z) linePoint2, (N X, N Y, N Z) point)
                    where N : INumberBase<N>
                    => At(linePoint1, linePoint2, Parameter(linePoint1, linePoint2, point));
            }

            /// <summary>Line and point distance.</summary>
            /// <remarks><a href="https://mathworld.wolfram.com/Point-LineDistance3-Dimensional.html">Wolfram Mathworld</a></remarks>
            public static class Distance
            {
                /// <summary>Distance of a point from the line through two points, |(p - p1) × (p2 - p1)| / |p2 - p1|.</summary>
                public static N FromPoints<N>((N X, N Y, N Z) linePoint1, (N X, N Y, N Z) linePoint2, (N X, N Y, N Z) point)
                    where N : IRootFunctions<N>
                {
                    var direction = GeometricVector3.Direction.FromCartesian(linePoint1, linePoint2);
                    var length = GeometricVector3.Magnitude.FromCartesian(direction);
                    var offset = GeometricVector3.Direction.FromCartesian(linePoint1, point);
                    return length == N.Zero
                        ? GeometricVector3.Magnitude.FromCartesian(offset)
                        : GeometricVector3.Magnitude.FromCartesian(GeometricVector3.CrossProduct.FromCartesian(offset, direction)) / length;
                }
            }

            /// <summary>Line and point intersection.</summary>
            public static class Intersection
            {
                /// <summary>Determine if the line through two points includes the point.</summary>
                public static bool FromPoints<N>((N X, N Y, N Z) linePoint1, (N X, N Y, N Z) linePoint2, (N X, N Y, N Z) point)
                    where N : INumberBase<N>
                {
                    var cross = GeometricVector3.CrossProduct.FromCartesian(
                        GeometricVector3.Direction.FromCartesian(linePoint1, point),
                        GeometricVector3.Direction.FromCartesian(linePoint1, linePoint2));
                    return N.IsZero(cross.X) && N.IsZero(cross.Y) && N.IsZero(cross.Z);
                }
            }
        }

        /// <summary>Line and line investigation.</summary>
        public static class Line
        {
            /// <summary>Shortest distance between two lines. Zero when they intersect.</summary>
            /// <remarks><a href="https://en.wikipedia.org/wiki/Skew_lines#Distance">Wikipedia</a></remarks>
            public static class Distance
            {
                public static N FromPoints<N>(
                    (N X, N Y, N Z) line1Point1, (N X, N Y, N Z) line1Point2,
                    (N X, N Y, N Z) line2Point1, (N X, N Y, N Z) line2Point2)
                    where N : IRootFunctions<N>
                {
                    var direction1 = GeometricVector3.Direction.FromCartesian(line1Point1, line1Point2);
                    var direction2 = GeometricVector3.Direction.FromCartesian(line2Point1, line2Point2);
                    var normal = GeometricVector3.CrossProduct.FromCartesian(direction1, direction2);
                    var length = GeometricVector3.Magnitude.FromCartesian(normal);
                    if (length == N.Zero) // parallel lines
                        return Point.Distance.FromPoints(line1Point1, line1Point2, line2Point1);

                    var offset = GeometricVector3.Direction.FromCartesian(line1Point1, line2Point1);
                    return N.Abs(GeometricVector3.DotProduct.FromCartesian(offset, normal)) / length;
                }
            }

            /// <summary>Pair of the closest points of two lines.</summary>
            public static class ClosestPoints
            {
                /// <returns>
                ///     null: the lines are parallel or a line is degenerated to a point
                ///     (point1, point2): the point of the first line closest to the second line and vice versa
                /// </returns>
                public static ((N X, N Y, N Z) Point1, (N X, N Y, N Z) Point2)? FromPoints<N>(
                    (N X, N Y, N Z) line1Point1, (N X, N Y, N Z) line1Point2,
                    (N X, N Y, N Z) line2Point1, (N X, N Y, N Z) line2Point2)
                    where N : INumberBase<N>
                {
                    var (a, b, c, e, f) = Coefficients(line1Point1, line1Point2, line2Point1, line2Point2);
                    var denominator = (a * e) - (b * b);
                    if (denominator == N.Zero)
                        return null;

                    var s = ((b * f) - (c * e)) / denominator;
                    var t = ((a * f) - (b * c)) / denominator;
                    return (At(line1Point1, line1Point2, s), At(line2Point1, line2Point2, t));
                }
            }
        }
    }

    /// <summary>
    /// Line Segment
    /// Determined by two points.
    /// </summary>
    public static class Segment
    {
        /// <summary>Length of a line segment.</summary>
        public static class Length
        {
            public static N FromPoints<N>((N X, N Y, N Z) point1, (N X, N Y, N Z) point2)
                where N : IRootFunctions<N>
                => GeometricVector3.Distance.FromCartesian(point1, point2);
        }

        /// <summary>Midpoint of a line segment.</summary>
        public static class Midpoint
        {
            public static (N X, N Y, N Z) FromPoints<N>((N X, N Y, N Z) point1, (N X, N Y, N Z) point2)
                where N : INumberBase<N>
                => At(point1, point2, N.One / N.CreateChecked(2));
        }

        /// <summary>Points on a line segment.</summary>
        public static class Points
        {
            /// <summary>Enumerating equally spaced points from the first to the second point, both included.</summary>
            public static IEnumerable<(N X, N Y, N Z)> FromPoints<N>((N X, N Y, N Z) point1, (N X, N Y, N Z) point2, int count)
                where N : INumberBase<N>
            {
                if (count == 1)
                {
                    yield return point1;
                    yield break;
                }

                var last = N.CreateChecked(count - 1);
                for (int i = 0; i < count; i++)
                    yield return At(point1, point2, N.CreateChecked(i) / last);
            }
        }

        /// <summary>Line segment and ... investigation.</summary>
        public static class And
        {
            /// <summary>Line segment and point investigation.</summary>
            public static class Point
            {
                /// <summary>Point of a line segment closest to a point.</summary>
                public static class ClosestPoint
                {
                    public static (N X, N Y, N Z) FromPoints<N>((N X, N Y, N Z) sp1, (N X, N Y, N Z) sp2, (N X, N Y, N Z) point)
                        where N : INumber<N>
                        => At(sp1, sp2, N.Clamp(Parameter(sp1, sp2, point), N.Zero, N.One));
                }

                /// <summary>Distance of line segment and point.</summary>
                public static class Distance
                {
                    public static N FromPoints<N>((N X, N Y, N Z) sp1, (N X, N Y, N Z) sp2, (N X, N Y, N Z) point)
                        where N : INumber<N>, IRootFunctions<N>
                        => GeometricVector3.Distance.FromCartesian(ClosestPoint.FromPoints(sp1, sp2, point), point);
                }

                /// <summary>Line segment and point intersection.</summary>
                public static class Intersection
                {
                    /// <summary>Determine if line segment includes the point.</summary>
                    public static bool FromPoints<N>((N X, N Y, N Z) sp1, (N X, N Y, N Z) sp2, (N X, N Y, N Z) point)
                        where N : INumber<N>
                    {
                        if (!Line3.And.Point.Intersection.FromPoints(sp1, sp2, point))
                            return false;

                        return point.X >= N.Min(sp1.X, sp2.X) && point.X <= N.Max(sp1.X, sp2.X)
                            && point.Y >= N.Min(sp1.Y, sp2.Y) && point.Y <= N.Max(sp1.Y, sp2.Y)
                            && point.Z >= N.Min(sp1.Z, sp2.Z) && point.Z <= N.Max(sp1.Z, sp2.Z);
                    }
                }
            }

            /// <summary>Line segment and line segment investigation.</summary>
            public static class Segment
            {
                /// <summary>Shortest distance between two line segments. Zero when they intersect.</summary>
                /// <remarks>Clamps the closest points of the two lines to the segments, Ericson, Real-Time Collision Detection, 5.1.9.</remarks>
                public static class Distance
                {
                    public static N FromPoints<N>(
                        (N X, N Y, N Z) segment1Point1, (N X, N Y, N Z) segment1Point2,
                        (N X, N Y, N Z) segment2Point1, (N X, N Y, N Z) segment2Point2)
                        where N : INumber<N>, IRootFunctions<N>
                    {
                        var (a, b, c, e, f) = Coefficients(segment1Point1, segment1Point2, segment2Point1, segment2Point2);

                        // a or e is zero when the segment degenerates to a point
                        N s = N.Zero;
                        N t = N.Zero;
                        if (a == N.Zero)
                        {
                            if (e != N.Zero)
                                t = N.Clamp(f / e, N.Zero, N.One);
                        }
                        else if (e == N.Zero)
                        {
                            s = N.Clamp(-c / a, N.Zero, N.One);
                        }
                        else
                        {
                            var denominator = (a * e) - (b * b);
                            // parallel segments: any s works, start from the first point
                            if (denominator != N.Zero)
                                s = N.Clamp(((b * f) - (c * e)) / denominator, N.Zero, N.One);

                            t = ((b * s) + f) / e;
                            if (t < N.Zero || t > N.One)
                            {
                                // the closest point of the second segment is its end, find the matching point of the first one again
                                t = t < N.Zero ? N.Zero : N.One;
                                s = N.Clamp(((t * b) - c) / a, N.Zero, N.One);
                            }
                        }

                        return GeometricVector3.Distance.FromCartesian(At(segment1Point1, segment1Point2, s), At(segment2Point1, segment2Point2, t));
                    }
                }
            }
        }
    }

    // dot products of the line directions d1, d2 and the offset r of their first points, named after Ericson,
    // Real-Time Collision Detection, 5.1.8: a = d1·d1, b = d1·d2, c = d1·r, e = d2·d2, f = d2·r
    private static (N A, N B, N C, N E, N F) Coefficients<N>(
        (N X, N Y, N Z) line1Point1, (N X, N Y, N Z) line1Point2,
        (N X, N Y, N Z) line2Point1, (N X, N Y, N Z) line2Point2)
        where N : INumberBase<N>
    {
        var d1 = GeometricVector3.Direction.FromCartesian(line1Point1, line1Point2);
        var d2 = GeometricVector3.Direction.FromCartesian(line2Point1, line2Point2);
        var r = GeometricVector3.Direction.FromCartesian(line2Point1, line1Point1);
        return (GeometricVector3.DotProduct.FromCartesian(d1, d1),
                GeometricVector3.DotProduct.FromCartesian(d1, d2),
                GeometricVector3.DotProduct.FromCartesian(d1, r),
                GeometricVector3.DotProduct.FromCartesian(d2, d2),
                GeometricVector3.DotProduct.FromCartesian(d2, r));
    }

    // parameter t of the projection of a point to the line p1 + t * (p2 - p1), zero for coinciding line points
    private static N Parameter<N>((N X, N Y, N Z) linePoint1, (N X, N Y, N Z) linePoint2, (N X, N Y, N Z) point)
        where N : INumberBase<N>
    {
        var direction = GeometricVector3.Direction.FromCartesian(linePoint1, linePoint2);
        var length2 = GeometricVector3.DotProduct.FromCartesian(direction, direction);
        return length2 == N.Zero
            ? N.Zero
            : GeometricVector3.DotProduct.FromCartesian(GeometricVector3.Direction.FromCartesian(linePoint1, point), direction) / length2;
    }

    // point p1 + t * (p2 - p1)
    internal static (N X, N Y, N Z) At<N>((N X, N Y, N Z) linePoint1, (N X, N Y, N Z) linePoint2, N t)
        where N : INumberBase<N>
        => (linePoint1.X + (t * (linePoint2.X - linePoint1.X)),
            linePoint1.Y + (t * (linePoint2.Y - linePoint1.Y)),
            linePoint1.Z + (t * (linePoint2.Z - linePoint1.Z)));
}
