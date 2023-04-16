namespace Wayout.Mathematics.Geometry.D3
{
    using System;

    /// <summary>
    /// Ellipsoid
    /// </summary>
    /// <remarks>
    /// <a href="https://en.wikipedia.org/wiki/Ellipsoid">wikipedia</a>
    /// </remarks>
    public static class Ellipsoid
    {
        public static double SurfaceArea(double r1, double r2, double r3) => throw new NotImplementedException("todo");

        public static double EnclosedVolume(double r1, double r2, double r3) => 4/3 * PI * r1 * r2 * r3;
    }
}
