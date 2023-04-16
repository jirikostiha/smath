namespace Wayout.Mathematics.Geometry.D3
{
    using System;
    using Wayout.Mathematics.Functions;

    /// <summary>
    /// Spherical cap of a sphere.
    /// </summary>
    /// <remarks>
    /// <a href="https://en.wikipedia.org/wiki/Spherical_cap">wikipedia</a>
    /// <a href="https://mathworld.wolfram.com/SphericalCap.html">wolfram</a>
    /// <a href="https://www.cuemath.com/measurement/volume-of-a-section-of-a-sphere/">cuemath</a>
    /// <a href="https://www.123calculus.com/en/spherical-cap-page-7-40-135.html">calculator 1</a>
    /// <a href="https://www.redcrab-software.com/en/Calculator/Spherical-Cap">calculator 2</a>
    /// </remarks>
    public static class SphereCap
    {
        public static double CapHeight(double radius, double capRadius)
            => radius - PythagorasTheorem.Leg(radius, capRadius);

        public static double CapHeightFromCapAngle(double radius, double capAngle)
            => radius * (1 - Cos(capAngle));

        public static double CapRadius(double radius, double capHeight)
            => PythagorasTheorem.Leg(radius, radius - capHeight);

        public static double CapRadiusFromCapAngle(double radius, double capAngle)
            => Sin(capAngle) * radius;

        public static double Radius(double capHeight, double capRadius)
            => (Power2.f(capHeight) + Power2.f(capRadius)) / (2 * capHeight);

        public static double CapAngle(double radius, double capRadius)
            => Asin(capRadius / radius);

        public static double CapAngleFromCapHeight(double radius, double capHeight)
            => Acos(1 - capHeight / radius);

        /// <summary> Surface area (without base) </summary>
        public static double SurfaceArea(double radius, double capHeight)
            => 2 * PI * radius * capHeight;

        /// <summary> Surface area (without base) </summary>
        public static double SurfaceAreaByCapRadius(double capHeight, double capRadius)
            => PI * (capHeight * capHeight + capRadius * capRadius);

        /// <summary> Surface area (without base) </summary>
        public static double SurfaceAreaByPolarAngle(double radius, double polarAngle)
            => 2 * PI * radius * radius * (1 - Cos(polarAngle));
    }
}
