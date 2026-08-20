using System.Numerics;

namespace SMath.Geometry3D;

/// <summary>
/// Ellipsoid.
/// </summary>
/// <remarks>
/// <a href="https://en.wikipedia.org/wiki/Ellipsoid">Wikipedia</a>
/// <a href="https://mathworld.wolfram.com/Ellipsoid.html">Wolfram MathWorld</a>
/// </remarks>
public static class Ellipsoid
{
    /// <summary>
    /// Surface area of a triaxial ellipsoid using Knud Thomsen's approximation (p ≈ 1.6075).
    /// </summary>
    public static N SurfaceArea<N>(N r1, N r2, N r3)
        where N : IFloatingPointConstants<N>, IPowerFunctions<N>
        => Surface.Area.FromRadii(r1, r2, r3);

    /// <summary>
    /// Enclosed volume of an ellipsoid.
    /// </summary>
    public static N EnclosedVolume<N>(N r1, N r2, N r3)
        where N : IFloatingPointConstants<N>
        => Region.Volume.FromRadii(r1, r2, r3);

    /// <summary>
    /// Surface of an ellipsoid.
    /// </summary>
    public static class Surface
    {
        /// <summary>
        /// Surface area of an ellipsoid.
        /// </summary>
        /// <remarks>
        /// Calculated using Knud Thomsen's approximation formula 4 * pi * ((a^p * b^p + a^p * c^p + b^p * c^p) / 3)^(1/p)
        /// with p ≈ 1.6075, which has a maximum relative error below 1.061% and is exact for spheres.
        /// <a href="https://en.wikipedia.org/wiki/Ellipsoid#Surface_area">Wikipedia</a>
        /// </remarks>
        public static class Area
        {
            public static N FromRadii<N>(N r1, N r2, N r3)
                where N : IFloatingPointConstants<N>, IPowerFunctions<N>
            {
                var p = N.CreateChecked(1.6075);
                var p1 = N.Pow(r1, p);
                var p2 = N.Pow(r2, p);
                var p3 = N.Pow(r3, p);
                var sum = (p1 * p2) + (p1 * p3) + (p2 * p3);
                var avg = sum / N.CreateChecked(3);
                return N.CreateChecked(4) * N.Pi * N.Pow(avg, N.One / p);
            }
        }
    }

    /// <summary>
    /// Region enclosed by an ellipsoid.
    /// </summary>
    public static class Region
    {
        /// <summary>
        /// Enclosed volume of an ellipsoid.
        /// </summary>
        public static class Volume
        {
            public static N FromRadii<N>(N r1, N r2, N r3)
                where N : IFloatingPointConstants<N>
                => N.CreateChecked(4) / N.CreateChecked(3) * N.Pi * r1 * r2 * r3;
        }
    }
}
