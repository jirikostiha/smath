using System.Numerics;

namespace SMath.Geometry3D;

/// <summary>
/// Sphere.
/// Angles on the surface follow the physics convention: the polar angle is measured from the +z axis,
/// the azimuth from the +x axis towards the +y axis.
/// </summary>
/// <remarks>
/// <a href="https://en.wikipedia.org/wiki/Sphere">Wikipedia</a>
/// <a href="https://mathworld.wolfram.com/Sphere.html">Wolfram Mathworld</a>
/// </remarks>
public static class Sphere
{
    public static string SymmetryGroup => "O(3)";

    public static int EulerCharacteristic => 2;

    public static string PlainTextEquation => "(x - a)^2 + (y - b)^2 + (z - c)^2 = r^2";

    /// <summary>Radius of a sphere.</summary>
    public static class Radius
    {
        public static N FromSurfaceArea<N>(N surface)
            where N : IRootFunctions<N>
            => N.Sqrt(surface / (N.CreateChecked(4) * N.Pi));

        public static N FromVolume<N>(N volume)
            where N : IRootFunctions<N>
            => N.Cbrt(volume / (N.CreateChecked(4) / N.CreateChecked(3) * N.Pi));
    }

    /// <summary>Surface of a sphere.</summary>
    public static class Surface
    {
        /// <summary>Surface area of a sphere.</summary>
        public static class Area
        {
            public static N FromRadius<N>(N radius)
                where N : IFloatingPointConstants<N>
                => N.CreateChecked(4) * N.Pi * radius * radius;
        }

        /// <summary>Point on the surface of a sphere centered in origin.</summary>
        public static class Point
        {
            public static N XFromAngles<N>(N radius, N polarAngle, N azimuth)
                where N : ITrigonometricFunctions<N>
                => GeometricVector3.X.FromSpherical(radius, polarAngle, azimuth);

            public static N YFromAngles<N>(N radius, N polarAngle, N azimuth)
                where N : ITrigonometricFunctions<N>
                => GeometricVector3.Y.FromSpherical(radius, polarAngle, azimuth);

            public static N ZFromAngles<N>(N radius, N polarAngle)
                where N : ITrigonometricFunctions<N>
                => GeometricVector3.Z.FromSpherical(radius, polarAngle);

            public static (N X, N Y, N Z) FromAngles<N>(N radius, N polarAngle, N azimuth)
                where N : ITrigonometricFunctions<N>
                => GeometricVector3.Cartesian.FromSpherical(radius, polarAngle, azimuth);
        }

        /// <summary>Points on the surface of a sphere centered in origin.</summary>
        public static class Points
        {
            /// <summary>Enumerating nearly evenly spread points on a golden angle spiral from the +z pole to the -z pole.</summary>
            public static IEnumerable<(N X, N Y, N Z)> FromRadius<N>(N radius, int count)
                where N : IFloatingPointIeee754<N>
                => SphereCap.Surface.Points.Spiral(radius, N.Pi, count);
        }

        /// <summary>Relations of a sphere surface and another geometric object.</summary>
        public static class And
        {
            /// <summary>Relations of a sphere surface and a point.</summary>
            public static class Point
            {
                /// <summary>Shortest distance between a sphere surface and a point.</summary>
                public static class Distance
                {
                    /// <summary>Distance between a surface of a sphere centered in origin and a point.</summary>
                    public static N FromRadius<N>(N radius, (N X, N Y, N Z) point)
                        where N : IRootFunctions<N>
                        => FromRadius((N.Zero, N.Zero, N.Zero), radius, point);

                    /// <summary>Distance between a surface of a sphere centered in <paramref name="center"/> and a point.</summary>
                    public static N FromRadius<N>((N X, N Y, N Z) center, N radius, (N X, N Y, N Z) point)
                        where N : IRootFunctions<N>
                        => N.Abs(GeometricVector3.Distance.FromCartesian(point, center) - radius);
                }

                /// <summary>Determines whether a point lies on a sphere surface.</summary>
                public static class Intersection
                {
                    /// <summary>Determines whether a point lies on a surface of a sphere centered in origin.</summary>
                    public static bool FromRadius<N>(N radius, (N X, N Y, N Z) point)
                        where N : INumberBase<N>
                        => GeometricVector3.DotProduct.FromCartesian(point, point) == radius * radius;

                    /// <summary>Determines whether a point lies on a surface of a sphere centered in <paramref name="center"/>.</summary>
                    public static bool FromRadius<N>((N X, N Y, N Z) center, N radius, (N X, N Y, N Z) point)
                        where N : INumberBase<N>
                        => FromRadius(radius, GeometricVector3.Direction.FromCartesian(center, point));
                }
            }

            /// <summary>Relations of a sphere surface and a plane.</summary>
            public static class Plane
            {
                /// <summary>Shortest distance between a sphere surface and a plane. Zero when the plane cuts the sphere.</summary>
                public static class Distance
                {
                    /// <summary>Distance between a surface of a sphere centered in origin and a plane in general form.</summary>
                    public static N FromRadius<N>(N radius, (N A, N B, N C, N D) plane)
                        where N : IRootFunctions<N>, IComparisonOperators<N, N, bool>
                        => FromRadius((N.Zero, N.Zero, N.Zero), radius, plane);

                    /// <summary>Distance between a surface of a sphere centered in <paramref name="center"/> and a plane in general form.</summary>
                    public static N FromRadius<N>((N X, N Y, N Z) center, N radius, (N A, N B, N C, N D) plane)
                        where N : IRootFunctions<N>, IComparisonOperators<N, N, bool>
                    {
                        var gap = Geometry3D.Plane.And.Point.Distance.FromGeneralForm(plane, center) - radius;
                        return NonNegative(gap);
                    }
                }

                /// <summary>Circle in which a plane cuts a sphere surface.</summary>
                /// <remarks><a href="https://en.wikipedia.org/wiki/Spherical_cap">Wikipedia</a></remarks>
                public static class Intersection
                {
                    /// <summary>
                    /// Circle in which a plane in general form cuts a surface of a sphere centered in origin.
                    /// The circle lies in the plane, so the plane normal is its axis.
                    /// </summary>
                    /// <returns>
                    ///     null: the plane misses the sphere;
                    ///     zero radius: the plane is tangent;
                    ///     (center, radius): the circle.
                    /// </returns>
                    public static ((N X, N Y, N Z) Center, N Radius)? FromRadius<N>(N radius, (N A, N B, N C, N D) plane)
                        where N : IRootFunctions<N>, IComparisonOperators<N, N, bool>
                        => FromRadius((N.Zero, N.Zero, N.Zero), radius, plane);

                    /// <summary>
                    /// Circle in which a plane in general form cuts a surface of a sphere centered in <paramref name="center"/>.
                    /// The circle lies in the plane, so the plane normal is its axis.
                    /// </summary>
                    /// <returns>
                    ///     null: the plane misses the sphere;
                    ///     zero radius: the plane is tangent;
                    ///     (center, radius): the circle.
                    /// </returns>
                    public static ((N X, N Y, N Z) Center, N Radius)? FromRadius<N>((N X, N Y, N Z) center, N radius, (N A, N B, N C, N D) plane)
                        where N : IRootFunctions<N>, IComparisonOperators<N, N, bool>
                    {
                        var distance = Geometry3D.Plane.And.Point.Distance.FromGeneralForm(plane, center);
                        if (distance > radius)
                            return null;

                        var circleRadius2 = (radius * radius) - (distance * distance);
                        return (Geometry3D.Plane.And.Point.Projection.FromGeneralForm(plane, center),
                            CircleRadius(circleRadius2));
                    }
                }
            }

            /// <summary>Relations of a sphere surface and another sphere.</summary>
            public static class Sphere
            {
                /// <summary>Shortest distance between two sphere surfaces. Zero when the surfaces intersect.</summary>
                public static class Distance
                {
                    /// <summary>Distance between a surface of a sphere centered in origin and another sphere.</summary>
                    public static N FromRadius<N>(N radius, (N X, N Y, N Z) otherCenter, N otherRadius)
                        where N : IRootFunctions<N>, IComparisonOperators<N, N, bool>
                        => FromRadius((N.Zero, N.Zero, N.Zero), radius, otherCenter, otherRadius);

                    /// <summary>Distance between a surface of a sphere centered in <paramref name="center"/> and another sphere.</summary>
                    public static N FromRadius<N>((N X, N Y, N Z) center, N radius, (N X, N Y, N Z) otherCenter, N otherRadius)
                        where N : IRootFunctions<N>, IComparisonOperators<N, N, bool>
                    {
                        var centerDistance = GeometricVector3.Distance.FromCartesian(otherCenter, center);
                        // gap when the spheres are separated, and gap when one sphere is nested in the other
                        var outer = centerDistance - radius - otherRadius;
                        var inner = N.Abs(radius - otherRadius) - centerDistance;
                        var gap = outer > inner ? outer : inner;
                        return NonNegative(gap);
                    }
                }

                /// <summary>Circle in which two sphere surfaces intersect.</summary>
                /// <remarks><a href="https://mathworld.wolfram.com/Sphere-SphereIntersection.html">Wolfram Mathworld</a></remarks>
                public static class Intersection
                {
                    /// <summary>Circle in which a surface of a sphere centered in origin and another sphere intersect.</summary>
                    /// <returns>
                    ///     null: the spheres do not meet, one contains the other, or they coincide;
                    ///     zero radius: the spheres are tangent;
                    ///     (center, radius): the circle, its axis is the line through the sphere centers.
                    /// </returns>
                    public static ((N X, N Y, N Z) Center, N Radius)? FromRadius<N>(N radius, (N X, N Y, N Z) otherCenter, N otherRadius)
                        where N : IRootFunctions<N>, IComparisonOperators<N, N, bool>
                        => FromRadius((N.Zero, N.Zero, N.Zero), radius, otherCenter, otherRadius);

                    /// <summary>Circle in which a surface of a sphere centered in <paramref name="center"/> and another sphere intersect.</summary>
                    /// <returns>
                    ///     null: the spheres do not meet, one contains the other, or they coincide;
                    ///     zero radius: the spheres are tangent;
                    ///     (center, radius): the circle, its axis is the line through the sphere centers.
                    /// </returns>
                    public static ((N X, N Y, N Z) Center, N Radius)? FromRadius<N>((N X, N Y, N Z) center, N radius, (N X, N Y, N Z) otherCenter, N otherRadius)
                        where N : IRootFunctions<N>, IComparisonOperators<N, N, bool>
                    {
                        var direction = GeometricVector3.Direction.FromCartesian(center, otherCenter);
                        var centerDistance = GeometricVector3.Magnitude.FromCartesian(direction);
                        if (centerDistance == N.Zero
                            || centerDistance > radius + otherRadius
                            || centerDistance < N.Abs(radius - otherRadius))
                            return null;

                        // distance from the center to the plane of the circle along the center-to-center direction
                        var a = ((centerDistance * centerDistance) + (radius * radius) - (otherRadius * otherRadius))
                            / (N.CreateChecked(2) * centerDistance);
                        var circleRadius2 = (radius * radius) - (a * a);
                        var t = a / centerDistance;
                        return (Line3.At(center, otherCenter, t), CircleRadius(circleRadius2));
                    }
                }
            }
        }
    }

    /// <summary>Ball region enclosed by a sphere.</summary>
    public static class Region
    {
        /// <summary>Enclosed volume of a sphere.</summary>
        public static class Volume
        {
            public static N FromRadius<N>(N radius)
                where N : IFloatingPointConstants<N>
                => N.CreateChecked(4) / N.CreateChecked(3) * N.Pi * radius * radius * radius;
        }

        /// <summary>Relations of a ball region and another geometric object.</summary>
        public static class And
        {
            /// <summary>Relations of a ball region and a point.</summary>
            public static class Point
            {
                /// <summary>Shortest distance between a ball region and a point. Zero when the point lies inside the region.</summary>
                public static class Distance
                {
                    /// <summary>Distance between a region of a sphere centered in origin and a point.</summary>
                    public static N FromRadius<N>(N radius, (N X, N Y, N Z) point)
                        where N : IRootFunctions<N>, IComparisonOperators<N, N, bool>
                        => FromRadius((N.Zero, N.Zero, N.Zero), radius, point);

                    /// <summary>Distance between a region of a sphere centered in <paramref name="center"/> and a point.</summary>
                    public static N FromRadius<N>((N X, N Y, N Z) center, N radius, (N X, N Y, N Z) point)
                        where N : IRootFunctions<N>, IComparisonOperators<N, N, bool>
                    {
                        var gap = GeometricVector3.Distance.FromCartesian(point, center) - radius;
                        return NonNegative(gap);
                    }
                }

                /// <summary>Determines whether a point lies inside a ball region (surface included).</summary>
                public static class Intersection
                {
                    /// <summary>Determines whether a point lies inside a region of a sphere centered in origin.</summary>
                    public static bool FromRadius<N>(N radius, (N X, N Y, N Z) point)
                        where N : INumberBase<N>, IComparisonOperators<N, N, bool>
                        => GeometricVector3.DotProduct.FromCartesian(point, point) <= radius * radius;

                    /// <summary>Determines whether a point lies inside a region of a sphere centered in <paramref name="center"/>.</summary>
                    public static bool FromRadius<N>((N X, N Y, N Z) center, N radius, (N X, N Y, N Z) point)
                        where N : INumberBase<N>, IComparisonOperators<N, N, bool>
                        => FromRadius(radius, GeometricVector3.Direction.FromCartesian(center, point));
                }
            }

            /// <summary>Relations of a ball region and another sphere.</summary>
            public static class Sphere
            {
                /// <summary>Shortest distance between two ball regions. Zero when the regions overlap.</summary>
                public static class Distance
                {
                    /// <summary>Distance between a region of a sphere centered in origin and another sphere.</summary>
                    public static N FromRadius<N>(N radius, (N X, N Y, N Z) otherCenter, N otherRadius)
                        where N : IRootFunctions<N>, IComparisonOperators<N, N, bool>
                        => FromRadius((N.Zero, N.Zero, N.Zero), radius, otherCenter, otherRadius);

                    /// <summary>Distance between a region of a sphere centered in <paramref name="center"/> and another sphere.</summary>
                    public static N FromRadius<N>((N X, N Y, N Z) center, N radius, (N X, N Y, N Z) otherCenter, N otherRadius)
                        where N : IRootFunctions<N>, IComparisonOperators<N, N, bool>
                    {
                        var gap = GeometricVector3.Distance.FromCartesian(otherCenter, center) - radius - otherRadius;
                        return NonNegative(gap);
                    }
                }

                /// <summary>Volume shared by two ball regions, a lens of two spherical caps.</summary>
                /// <remarks><a href="https://mathworld.wolfram.com/Sphere-SphereIntersection.html">Wolfram Mathworld</a></remarks>
                public static class Overlap
                {
                    /// <summary>
                    /// Volume shared by two balls whose centers are <paramref name="centerDistance"/> apart.
                    /// Zero when they are apart, the smaller ball when it is nested in the other.
                    /// </summary>
                    public static N FromRadius<N>(N radius, N otherRadius, N centerDistance)
                        where N : IFloatingPointConstants<N>, IComparisonOperators<N, N, bool>
                    {
                        if (centerDistance >= radius + otherRadius)
                            return N.Zero;

                        if (centerDistance <= N.Abs(radius - otherRadius))
                            return Volume.FromRadius(radius < otherRadius ? radius : otherRadius);

                        // V = pi (R + r - d)^2 (d^2 + 2d (R + r) - 3 (R - r)^2) / (12 d)
                        var gap = radius + otherRadius - centerDistance;
                        var difference = radius - otherRadius;
                        return N.Pi * gap * gap
                            * ((centerDistance * centerDistance) + (N.CreateChecked(2) * centerDistance * (radius + otherRadius))
                                - (N.CreateChecked(3) * difference * difference))
                            / (N.CreateChecked(12) * centerDistance);
                    }
                }
            }
        }
    }

    /// <summary>Tangent plane of a sphere.</summary>
    public static class TangentPlane
    {
        /// <summary>Get tangent plane in general form of a sphere centered in origin at the surface point given by its angles.</summary>
        public static (N A, N B, N C, N D) FromAngles<N>(N radius, N polarAngle, N azimuth)
            where N : ITrigonometricFunctions<N>
        {
            var (x, y, z) = Surface.Point.FromAngles(radius, polarAngle, azimuth);
            return (x, y, z, -(radius * radius));
        }
    }

    // a gap that closes up is no gap
    private static N NonNegative<N>(N gap)
        where N : INumberBase<N>, IComparisonOperators<N, N, bool>
        => gap > N.Zero ? gap : N.Zero;

    // radius from its square, zero when rounding leaves the square slightly negative for a tangent contact
    private static N CircleRadius<N>(N radius2)
        where N : IRootFunctions<N>, IComparisonOperators<N, N, bool>
        => radius2 > N.Zero ? N.Sqrt(radius2) : N.Zero;
}
