using System.Numerics;

namespace SMath.Geometry3D
{
    /// <summary>
    /// Sphere.
    /// </summary>
    /// <remarks>
    /// <a href="https://en.wikipedia.org/wiki/Sphere">Wikipedia</a>
    /// <a href="https://mathworld.wolfram.com/Sphere.html">Wolfram Mathworld</a>
    /// </remarks>
    public static class Sphere
    {
        public static string SymmetryGroup => "O(3)";

        public static int EulerCharacteristic => 2;

        /// <summary>
        /// Radius of a sphere.
        /// </summary>
        public static class Radius
        {
            public static N FromSurfaceArea<N>(N surface)
                where N : IRootFunctions<N>
                => N.Sqrt(surface / (N.CreateChecked(4) * N.Pi));

            public static N FromVolume<N>(N volume)
                where N : IRootFunctions<N>
                => N.Cbrt(volume / (N.CreateChecked(4) / N.CreateChecked(3) * N.Pi));
        }

        /// <summary>
        /// Surface of a sphere.
        /// </summary>
        public static class Surface
        {
            /// <summary>
            /// Surface area of a sphere.
            /// </summary>
            public static class Area
            {
                public static N FromRadius<N>(N radius)
                    where N : IFloatingPointConstants<N>
                    => N.CreateChecked(4) * N.Pi * radius * radius;
            }
        }

        /// <summary>
        /// Ball region enclosed by a sphere.
        /// </summary>
        public static class Region
        {
            /// <summary>
            /// Enclosed volume of a sphere.
            /// </summary>
            public static class Volume
            {
                public static N FromRadius<N>(N radius)
                    where N : IFloatingPointConstants<N>
                    => N.CreateChecked(4) / N.CreateChecked(3) * N.Pi * radius * radius * radius;
            }
        }
    }
}