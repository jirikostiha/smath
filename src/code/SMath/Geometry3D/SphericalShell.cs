using System.Numerics;

namespace SMath.Geometry3D;

/// <summary>
/// Spherical shell.
/// </summary>
/// <remarks>
/// <a href="https://en.wikipedia.org/wiki/Spherical_shell">Wikipedia</a>
/// </remarks>
public static class SphericalShell
{
    public static N OuterSurfaceArea<N>(N outerRadius)
        where N : IFloatingPointConstants<N>
        => Surface.Area.Outer(outerRadius);

    public static N InnerSurfaceArea<N>(N innerRadius)
        where N : IFloatingPointConstants<N>
        => Surface.Area.Inner(innerRadius);

    public static N TotalSurfaceArea<N>(N innerRadius, N outerRadius)
        where N : IFloatingPointConstants<N>
        => Surface.Area.Total(innerRadius, outerRadius);

    public static N Thickness<N>(N innerRadius, N outerRadius)
        where N : ISubtractionOperators<N, N, N>
        => outerRadius - innerRadius;

    public static N Volume<N>(N innerRadius, N outerRadius)
        where N : IFloatingPointConstants<N>
        => Region.Volume.FromRadii(innerRadius, outerRadius);

    /// <summary>
    /// Surface of a spherical shell.
    /// </summary>
    public static class Surface
    {
        /// <summary>
        /// Surface area of a spherical shell.
        /// </summary>
        public static class Area
        {
            public static N Outer<N>(N outerRadius)
                where N : IFloatingPointConstants<N>
                => Sphere.Surface.Area.FromRadius(outerRadius);

            public static N Inner<N>(N innerRadius)
                where N : IFloatingPointConstants<N>
                => Sphere.Surface.Area.FromRadius(innerRadius);

            public static N Total<N>(N innerRadius, N outerRadius)
                where N : IFloatingPointConstants<N>
                => Outer(outerRadius) + Inner(innerRadius);
        }
    }

    /// <summary>
    /// Region enclosed by a spherical shell.
    /// </summary>
    public static class Region
    {
        /// <summary>
        /// Enclosed volume of a spherical shell.
        /// </summary>
        public static class Volume
        {
            public static N FromRadii<N>(N innerRadius, N outerRadius)
                where N : IFloatingPointConstants<N>
                => Sphere.Region.Volume.FromRadius(outerRadius) - Sphere.Region.Volume.FromRadius(innerRadius);
        }
    }
}
