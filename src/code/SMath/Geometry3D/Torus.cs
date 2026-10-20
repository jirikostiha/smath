using System.Numerics;

namespace SMath.Geometry3D;

/// <summary>
/// Ring torus around the z axis. The major radius is the distance from the axis to the center of the tube,
/// the minor radius is the radius of the tube.
/// </summary>
/// <remarks>
/// <a href="https://en.wikipedia.org/wiki/Torus">Wikipedia</a>
/// <a href="https://mathworld.wolfram.com/Torus.html">Wolfram Mathworld</a>
/// </remarks>
public static class Torus
{
    public static int EulerCharacteristic => 0;

    public static string PlainTextEquation => "(sqrt(x^2 + y^2) - R)^2 + z^2 = r^2";

    /// <summary>
    /// Surface of a torus.
    /// </summary>
    public static class Surface
    {
        /// <summary>
        /// Surface area of a torus, 4 pi^2 R r.
        /// </summary>
        public static class Area
        {
            public static N FromRadii<N>(N majorRadius, N minorRadius)
                where N : IFloatingPointConstants<N>
                => N.CreateChecked(4) * N.Pi * N.Pi * majorRadius * minorRadius;
        }

        /// <summary>
        /// Point on the surface of a torus centered in origin.
        /// </summary>
        public static class Point
        {
            /// <param name="majorRadius"> Distance from the axis to the center of the tube. </param>
            /// <param name="minorRadius"> Radius of the tube. </param>
            /// <param name="toroidalAngle"> Angle around the z axis from the +x axis. </param>
            /// <param name="poloidalAngle"> Angle around the tube from its outer equator towards +z. </param>
            public static (N X, N Y, N Z) FromAngles<N>(N majorRadius, N minorRadius, N toroidalAngle, N poloidalAngle)
                where N : ITrigonometricFunctions<N>
            {
                var distance = majorRadius + (minorRadius * N.Cos(poloidalAngle));
                return (distance * N.Cos(toroidalAngle), distance * N.Sin(toroidalAngle), minorRadius * N.Sin(poloidalAngle));
            }
        }
    }

    /// <summary>
    /// Region enclosed by a torus.
    /// </summary>
    public static class Region
    {
        /// <summary>
        /// Enclosed volume of a torus, 2 pi^2 R r^2.
        /// </summary>
        public static class Volume
        {
            public static N FromRadii<N>(N majorRadius, N minorRadius)
                where N : IFloatingPointConstants<N>
                => N.CreateChecked(2) * N.Pi * N.Pi * majorRadius * minorRadius * minorRadius;
        }

        /// <summary>
        /// Relations of a torus region and another geometric object.
        /// </summary>
        public static class And
        {
            /// <summary>
            /// Relations of a torus region and a point.
            /// </summary>
            public static class Point
            {
                /// <summary>
                /// Determines whether a point lies inside a region of a torus centered in origin (surface included).
                /// </summary>
                public static class Intersection
                {
                    public static bool FromRadii<N>(N majorRadius, N minorRadius, (N X, N Y, N Z) point)
                        where N : IRootFunctions<N>, IComparisonOperators<N, N, bool>
                    {
                        var ring = PT.Hypotenuse(point.X, point.Y) - majorRadius;
                        return (ring * ring) + (point.Z * point.Z) <= minorRadius * minorRadius;
                    }
                }
            }
        }
    }
}
