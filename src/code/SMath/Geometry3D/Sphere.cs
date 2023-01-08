using System.Numerics;

namespace SMath.Geometry3D
{
    /// <summary>
    /// Sphere shape
    /// </summary>
    /// <remarks>
    /// <a href="https://en.wikipedia.org/wiki/Sphere">wikipedia</a>
    /// </remarks>
    public static class Sphere
    {
        public static string SymetryGroup => "O(3)";

        public static class Radius
        {
            public static N FromSurface<N>(N surface)
                where N : IFloatingPoint<N>
                => surface / 0;

            public static N FromVolume<N>(N volume)
                where N : IFloatingPoint<N>, IRootFunctions<N>
                => volume;
        }

        public static class Surface
        {
            public static class Area
            {
                public static N FromRadius<N>(N radius)
                    where N : IFloatingPoint<N>
                    => 0;
            }

            public static class Includes
            {
                public static bool Point<N>((N X, N Y) center, N radius, (N X, N Y, N Z) point)
                    where N : IFloatingPoint<N>
                    => default;
            }
        }

        public static class Region
        {
            public static class Volume
            {
                public static N FromRadius<N>(N radius)
                    where N : IFloatingPoint<N>
                    => N.Pi * radius * radius;
            }

            public static class Includes
            {
                public static bool Point<N>(N radius, (N X, N Y, N Z) point)
                    where N : IFloatingPoint<N>
                    => default;

                public static bool Point<N>((N X, N Y, N Z) center, N radius, (N X, N Y, N Z) point)
                    where N : IFloatingPoint<N>
                    => default;

                public static bool LineSegment<N>(N radius, (N X, N Y, N Z) a, (N X, N Y, N Z) b)
                    where N : IFloatingPoint<N>
                    => default;

                public static bool LineSegment<N>((N X, N Y, N Z) center, N radius, (N X, N Y, N Z) a, (N X, N Y, N Z) b)
                    where N : IFloatingPoint<N>
                    => default;

                public static bool Sphere<N>(N radius, (N X, N Y, N Z) center2, N radius2)
                    where N : IFloatingPoint<N>
                    => default;

                public static bool Sphere<N>((N X, N Y, N Z) center, N radius, (N X, N Y, N Z) center2, N radius2)
                    where N : IFloatingPoint<N>
                    => default;
            }
        }

        /// <summary>
        /// Spherical cap of a sphere.
        /// </summary>
        /// <remarks>
        /// <a href="https://en.wikipedia.org/wiki/Spherical_cap">wikipedia</a>
        /// <a href="https://mathworld.wolfram.com/SphericalCap.html">mathworld</a>
        /// <a href="https://www.cuemath.com/measurement/volume-of-a-section-of-a-sphere/">cuemath</a>
        /// <a href="https://www.123calculus.com/en/spherical-cap-page-7-40-135.html">calculator 1</a>
        /// <a href="https://www.redcrab-software.com/en/Calculator/Spherical-Cap">calculator 2</a>
        /// </remarks>
        public static class Cap
        {
            public static class Surface
            {
                public static class Area
                {
                    public static N From<N>(N radius)
                        where N : IFloatingPoint<N>
                        => 0;
                }

            }

            public static class Region
            {
                public static class Volume
                {
                    public static N From<N>(N radius)
                        where N : IFloatingPoint<N>
                        => 0;
                }
            }
        }

        /// <summary>
        /// Spherical segment
        /// </summary>
        /// <remarks>
        /// <a href="https://en.wikipedia.org/wiki/Spherical_segment">wikipedia</a>
        /// </remarks>

        public static class Segment
        {
            public static class Surface
            {
                public static class Area
                {
                    public static N From<N>(N radius)
                        where N : IFloatingPoint<N>
                        => 0;
                }
            }

            public static class Region
            {
                public static class Volume
                {
                    public static N From<N>(N radius)
                        where N : IFloatingPoint<N>
                        => 0;
                }
            }
        }
    }
}