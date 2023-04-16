namespace Wayout.Mathematics.Geometry.D3
{
    using System;
    using Wayout.Mathematics.Functions;

    /// <summary>
    /// Spherical cap of a ball.
    /// </summary>
    /// <remarks>
    /// <a href="https://en.wikipedia.org/wiki/Spherical_cap">wikipedia</a>
    /// <a href="https://mathworld.wolfram.com/SphericalCap.html">wolfram</a>
    /// <a href="https://www.cuemath.com/measurement/volume-of-a-section-of-a-sphere/">cuemath</a>
    /// <a href="https://www.123calculus.com/en/spherical-cap-page-7-40-135.html">calculator 1</a>
    /// <a href="https://www.redcrab-software.com/en/Calculator/Spherical-Cap">calculator 2</a>
    /// </remarks>
    public static class BallCap
    {
        /// <summary> Surface area (without base) </summary>
        public static double SurfaceArea(double radius, double capHeight)
            => SphereCap.SurfaceArea(radius, capHeight) + D2.Disk.Area(SphereCap.CapRadius(radius, capHeight));

        /// <summary> Surface area (without base) </summary>
        public static double SurfaceAreaByCapRadius(double capHeight, double capRadius)
            => SphereCap.SurfaceAreaByCapRadius(capHeight, capRadius) + D2.Disk.Area(capRadius);

        /// <summary> Surface area (without base) </summary>
        public static double SurfaceAreaByPolarAngle(double radius, double polarAngle)
            => SphereCap.SurfaceAreaByPolarAngle(radius, polarAngle) + D2.Disk.Area(SphereCap.CapRadiusFromCapAngle(radius, polarAngle));

        public static double Volume(double radius, double capHeight)
            => PI * capHeight * capHeight * (3*radius - capHeight) / 3d;

        public static double VolumeByCapRadius(double capHeight, double capRadius)
            => PI * capHeight * (3*capRadius*capRadius + capHeight*capHeight) / 6d;

        public static double VolumeByPolarAngle(double radius, double polarAngle)
            => PI * Power3.f(radius) * (2 + Cos(polarAngle)) * Power2.f(1 - Cos(polarAngle)) / 3d;
    }
}
