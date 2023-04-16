namespace Wayout.Mathematics.Geometry.D3
{
    using System;
    using Wayout.Mathematics.Functions;

    /// <summary>
    /// Spherical shell
    /// </summary>
    /// <remarks>
    /// <a href="https://en.wikipedia.org/wiki/Spherical_shell">wikipedia</a>
    /// </remarks>
    public static class SphericalShell
    {
        public static double OuterSurfaceArea(double outerRadius) => Sphere.SurfaceArea(outerRadius);

        public static double InnerSurfaceArea(double innerRadius) => Sphere.SurfaceArea(innerRadius);

        public static double Volume(double innerRadius, double outerRadius) 
            => Ball.Volume(outerRadius) - Ball.Volume(innerRadius);
    }
}
