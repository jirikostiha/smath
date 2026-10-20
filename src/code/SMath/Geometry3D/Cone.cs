using System.Numerics;

namespace SMath.Geometry3D;

/// <summary>
/// Right circular cone.
/// </summary>
/// <remarks>
/// <a href="https://en.wikipedia.org/wiki/Cone">Wikipedia</a>
/// <a href="https://mathworld.wolfram.com/Cone.html">Wolfram Mathworld</a>
/// </remarks>
public static class Cone
{
    /// <summary>
    /// Height of a cone, the distance between its apex and base.
    /// </summary>
    public static class Height
    {
        public static N FromVolume<N>(N volume, N radius)
            where N : IFloatingPointConstants<N>
            => N.CreateChecked(3) * volume / (N.Pi * radius * radius);
    }

    /// <summary>
    /// Slant height of a cone, the distance between its apex and the rim of its base.
    /// </summary>
    public static class SlantHeight
    {
        public static N FromRadius<N>(N radius, N height)
            where N : IRootFunctions<N>
            => PT.Hypotenuse(radius, height);
    }

    /// <summary>
    /// Half of the apex angle, the angle between the axis and the lateral surface, in rads.
    /// </summary>
    public static class HalfAngle
    {
        public static N FromRadius<N>(N radius, N height)
            where N : IFloatingPointIeee754<N>
            => N.Atan2(radius, height);
    }

    /// <summary>
    /// Surface of a cone.
    /// </summary>
    public static class Surface
    {
        /// <summary>
        /// Surface area of a cone, the base included.
        /// </summary>
        public static class Area
        {
            public static N FromRadius<N>(N radius, N height)
                where N : IRootFunctions<N>
                => N.Pi * radius * (radius + SlantHeight.FromRadius(radius, height));
        }

        /// <summary>
        /// Lateral surface area of a cone, the base excluded.
        /// </summary>
        public static class LateralArea
        {
            public static N FromRadius<N>(N radius, N height)
                where N : IRootFunctions<N>
                => N.Pi * radius * SlantHeight.FromRadius(radius, height);
        }
    }

    /// <summary>
    /// Region enclosed by a cone.
    /// </summary>
    public static class Region
    {
        /// <summary>
        /// Enclosed volume of a cone, a third of the cylinder with the same base and height.
        /// </summary>
        public static class Volume
        {
            public static N FromRadius<N>(N radius, N height)
                where N : IFloatingPointConstants<N>
                => Cylinder.Region.Volume.FromRadius(radius, height) / N.CreateChecked(3);
        }
    }
}
