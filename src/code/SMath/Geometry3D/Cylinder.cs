using System.Numerics;

namespace SMath.Geometry3D;

/// <summary>
/// Right circular cylinder.
/// </summary>
/// <remarks>
/// <a href="https://en.wikipedia.org/wiki/Cylinder">Wikipedia</a>
/// <a href="https://mathworld.wolfram.com/Cylinder.html">Wolfram Mathworld</a>
/// </remarks>
public static class Cylinder
{
    /// <summary>
    /// Radius of a cylinder base.
    /// </summary>
    public static class Radius
    {
        public static N FromVolume<N>(N volume, N height)
            where N : IRootFunctions<N>
            => Geometry2D.Circle.Radius.FromArea(volume / height);
    }

    /// <summary>
    /// Height of a cylinder, the distance between its bases.
    /// </summary>
    public static class Height
    {
        public static N FromVolume<N>(N volume, N radius)
            where N : IFloatingPoint<N>
            => volume / Geometry2D.Circle.Region.Area.FromRadius(radius);
    }

    /// <summary>
    /// Surface of a cylinder.
    /// </summary>
    public static class Surface
    {
        /// <summary>
        /// Surface area of a cylinder, both bases included.
        /// </summary>
        public static class Area
        {
            public static N FromRadius<N>(N radius, N height)
                where N : IFloatingPointConstants<N>
                => N.CreateChecked(2) * N.Pi * radius * (radius + height);
        }

        /// <summary>
        /// Lateral surface area of a cylinder, bases excluded.
        /// </summary>
        public static class LateralArea
        {
            public static N FromRadius<N>(N radius, N height)
                where N : IFloatingPointConstants<N>
                => Geometry2D.Circle.Perimeter.Length.FromRadius(radius) * height;
        }
    }

    /// <summary>
    /// Region enclosed by a cylinder.
    /// </summary>
    public static class Region
    {
        /// <summary>
        /// Enclosed volume of a cylinder.
        /// </summary>
        public static class Volume
        {
            public static N FromRadius<N>(N radius, N height)
                where N : IFloatingPoint<N>
                => Geometry2D.Circle.Region.Area.FromRadius(radius) * height;
        }
    }
}
