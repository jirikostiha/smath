using System.Numerics;

namespace SMath.Geometry3D;

/// <summary>
/// Plane in general form a*x + b*y + c*z + d = 0, the three dimensional counterpart of a line in general form.
/// (a, b, c) is a normal vector of the plane.
/// </summary>
/// <remarks>
/// <a href="https://en.wikipedia.org/wiki/Plane_(mathematics)">Wikipedia</a>
/// <a href="https://mathworld.wolfram.com/Plane.html">Wolfram Mathworld</a>
/// </remarks>
public static class Plane
{
    /// <summary>
    /// xy plane (z = 0) in general form.
    /// </summary>
    public static (N A, N B, N C, N D) XY<N>()
        where N : INumberBase<N>
        => (N.Zero, N.Zero, N.One, N.Zero);

    /// <summary>
    /// xz plane (y = 0) in general form.
    /// </summary>
    public static (N A, N B, N C, N D) XZ<N>()
        where N : INumberBase<N>
        => (N.Zero, N.One, N.Zero, N.Zero);

    /// <summary>
    /// yz plane (x = 0) in general form.
    /// </summary>
    public static (N A, N B, N C, N D) YZ<N>()
        where N : INumberBase<N>
        => (N.One, N.Zero, N.Zero, N.Zero);

    public static string PlainTextEquation
        => "a*x + b*y + c*z + d = 0";

    /// <summary>
    /// Plane in general form.
    /// </summary>
    public static class GeneralForm
    {
        /// <summary>
        /// Plane through a point with the given normal vector.
        /// </summary>
        public static (N A, N B, N C, N D) FromPointAndNormal<N>((N X, N Y, N Z) point, (N X, N Y, N Z) normal)
            where N : INumberBase<N>
            => (normal.X, normal.Y, normal.Z, -GeometricVector3.DotProduct.FromCartesian(normal, point));

        /// <summary>
        /// Plane through three points. The normal follows the right hand rule from the first through the second to the third point.
        /// </summary>
        /// <returns>
        ///     null: the points are collinear
        ///     (a,b,c,d): the plane
        /// </returns>
        public static (N A, N B, N C, N D)? FromPoints<N>((N X, N Y, N Z) point1, (N X, N Y, N Z) point2, (N X, N Y, N Z) point3)
            where N : INumberBase<N>
        {
            var normal = GeometricVector3.CrossProduct.FromCartesian(
                GeometricVector3.Direction.FromCartesian(point1, point2),
                GeometricVector3.Direction.FromCartesian(point1, point3));
            return N.IsZero(normal.X) && N.IsZero(normal.Y) && N.IsZero(normal.Z)
                ? null
                : FromPointAndNormal(point1, normal);
        }

        /// <summary>
        /// The same plane with a unit normal vector, so that the equation gives the signed distance of a point.
        /// </summary>
        public static (N A, N B, N C, N D) Normalized<N>((N A, N B, N C, N D) plane)
            where N : IRootFunctions<N>
        {
            var length = PT.Hypotenuse(plane.A, plane.B, plane.C);
            return (plane.A / length, plane.B / length, plane.C / length, plane.D / length);
        }
    }

    /// <summary>
    /// Normal vector of a plane.
    /// </summary>
    public static class Normal
    {
        public static (N X, N Y, N Z) FromGeneralForm<N>((N A, N B, N C, N D) plane)
            => (plane.A, plane.B, plane.C);
    }

    /// <summary>
    /// Plane and ... investigation.
    /// </summary>
    public static class And
    {
        /// <summary>
        /// Plane and point investigation.
        /// </summary>
        public static class Point
        {
            /// <summary>
            /// Signed distance of a point from a plane, positive on the side the normal points to.
            /// </summary>
            public static class SignedDistance
            {
                public static N FromGeneralForm<N>((N A, N B, N C, N D) plane, (N X, N Y, N Z) point)
                    where N : IRootFunctions<N>
                    => Evaluate(plane, point) / PT.Hypotenuse(plane.A, plane.B, plane.C);
            }

            /// <summary>
            /// Plane and point distance.
            /// </summary>
            /// <remarks>
            /// <a href="https://en.wikipedia.org/wiki/Distance_from_a_point_to_a_plane">Wikipedia</a>
            /// </remarks>
            public static class Distance
            {
                public static N FromGeneralForm<N>((N A, N B, N C, N D) plane, (N X, N Y, N Z) point)
                    where N : IRootFunctions<N>
                    => N.Abs(SignedDistance.FromGeneralForm(plane, point));
            }

            /// <summary>
            /// Orthogonal projection of a point onto a plane.
            /// </summary>
            public static class Projection
            {
                public static (N X, N Y, N Z) FromGeneralForm<N>((N A, N B, N C, N D) plane, (N X, N Y, N Z) point)
                    where N : INumberBase<N>
                    => Along(plane, point, N.One);
            }

            /// <summary>
            /// Reflection of a point across a plane.
            /// </summary>
            public static class Reflection
            {
                public static (N X, N Y, N Z) FromGeneralForm<N>((N A, N B, N C, N D) plane, (N X, N Y, N Z) point)
                    where N : INumberBase<N>
                    => Along(plane, point, N.CreateChecked(2));
            }

            /// <summary>
            /// Plane and point intersection.
            /// </summary>
            public static class Intersection
            {
                /// <summary>
                /// Determine if plane includes point.
                /// </summary>
                public static bool FromGeneralForm<N>((N A, N B, N C, N D) plane, (N X, N Y, N Z) point)
                    where N : INumberBase<N>
                    => Evaluate(plane, point) == N.Zero;
            }

            // moves the point against the normal by the given multiple of its distance from the plane
            private static (N X, N Y, N Z) Along<N>((N A, N B, N C, N D) plane, (N X, N Y, N Z) point, N multiple)
                where N : INumberBase<N>
            {
                var t = multiple * Evaluate(plane, point) / ((plane.A * plane.A) + (plane.B * plane.B) + (plane.C * plane.C));
                return (point.X - (t * plane.A), point.Y - (t * plane.B), point.Z - (t * plane.C));
            }
        }

        /// <summary>
        /// Plane and line investigation.
        /// </summary>
        public static class Line
        {
            /// <summary>
            /// Plane and line intersection.
            /// </summary>
            /// <remarks>
            /// <a href="https://en.wikipedia.org/wiki/Line%E2%80%93plane_intersection">Wikipedia</a>
            /// </remarks>
            public static class Intersection
            {
                /// <summary>
                /// Intersection of a plane and a line through two points.
                /// </summary>
                /// <returns>
                ///     null: parallel, lying in the plane, or the points coincide
                ///     (x,y,z): intersection point
                /// </returns>
                public static (N X, N Y, N Z)? FromPoints<N>((N A, N B, N C, N D) plane, (N X, N Y, N Z) linePoint1, (N X, N Y, N Z) linePoint2)
                    where N : INumberBase<N>
                {
                    var direction = GeometricVector3.Direction.FromCartesian(linePoint1, linePoint2);
                    var denominator = GeometricVector3.DotProduct.FromCartesian(Normal.FromGeneralForm(plane), direction);
                    if (denominator == N.Zero)
                        return null;

                    var t = -Evaluate(plane, linePoint1) / denominator;
                    return (linePoint1.X + (t * direction.X), linePoint1.Y + (t * direction.Y), linePoint1.Z + (t * direction.Z));
                }
            }
        }

        /// <summary>
        /// Plane and plane investigation.
        /// </summary>
        public static class Plane
        {
            /// <summary>
            /// Plane and plane intersection.
            /// </summary>
            /// <remarks>
            /// <a href="https://en.wikipedia.org/wiki/Plane%E2%80%93plane_intersection">Wikipedia</a>
            /// </remarks>
            public static class Intersection
            {
                /// <summary>
                /// Line shared by two planes.
                /// </summary>
                /// <returns>
                ///     null: parallel or identical
                ///     (point, direction): the point of the line closest to the origin and its direction, the cross product of the normals
                /// </returns>
                public static ((N X, N Y, N Z) Point, (N X, N Y, N Z) Direction)? FromGeneralForm<N>((N A, N B, N C, N D) plane1, (N A, N B, N C, N D) plane2)
                    where N : INumberBase<N>
                {
                    var normal1 = Normal.FromGeneralForm(plane1);
                    var normal2 = Normal.FromGeneralForm(plane2);
                    var direction = GeometricVector3.CrossProduct.FromCartesian(normal1, normal2);
                    var length2 = GeometricVector3.DotProduct.FromCartesian(direction, direction);
                    if (length2 == N.Zero)
                        return null;

                    // p = (h1 (n2 × u) + h2 (u × n1)) / |u|^2 with the planes written as n · x = h
                    var a = GeometricVector3.CrossProduct.FromCartesian(normal2, direction);
                    var b = GeometricVector3.CrossProduct.FromCartesian(direction, normal1);
                    var h1 = -plane1.D;
                    var h2 = -plane2.D;
                    return (((h1 * a.X + h2 * b.X) / length2, (h1 * a.Y + h2 * b.Y) / length2, (h1 * a.Z + h2 * b.Z) / length2), direction);
                }
            }
        }
    }

    // left side of the plane equation, zero for a point in the plane
    private static N Evaluate<N>((N A, N B, N C, N D) plane, (N X, N Y, N Z) point)
        where N : INumberBase<N>
        => (plane.A * point.X) + (plane.B * point.Y) + (plane.C * point.Z) + plane.D;
}
