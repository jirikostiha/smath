using System.Numerics;

namespace SMath.Geometry3D;

/// <summary>
/// Spherical cap of a ball (solid segment with base).
/// </summary>
/// <remarks>
/// <a href="https://en.wikipedia.org/wiki/Spherical_cap">Wikipedia</a>
/// </remarks>
public static class BallCap
{
    /// <summary> Surface area (with base) </summary>
    public static N SurfaceArea<N>(N radius, N capHeight)
        where N : IRootFunctions<N>, IFloatingPointConstants<N>
        => Surface.Area.FromCapHeight(radius, capHeight);

    /// <summary> Surface area (with base) </summary>
    public static N SurfaceAreaByCapRadius<N>(N capHeight, N capRadius)
        where N : IFloatingPointConstants<N>
        => Surface.Area.FromCapRadius(capHeight, capRadius);

    /// <summary> Surface area (with base) </summary>
    public static N SurfaceAreaByPolarAngle<N>(N radius, N polarAngle)
        where N : IFloatingPointConstants<N>, ITrigonometricFunctions<N>
        => Surface.Area.FromPolarAngle(radius, polarAngle);

    public static N Volume<N>(N radius, N capHeight)
        where N : IFloatingPointConstants<N>
        => Region.Volume.FromCapHeight(radius, capHeight);

    public static N VolumeByCapRadius<N>(N capHeight, N capRadius)
        where N : IFloatingPointConstants<N>
        => Region.Volume.FromCapRadius(capHeight, capRadius);

    public static N VolumeByPolarAngle<N>(N radius, N polarAngle)
        where N : IFloatingPointConstants<N>, ITrigonometricFunctions<N>
        => Region.Volume.FromPolarAngle(radius, polarAngle);

    /// <summary>
    /// Surface of a ball cap (including base).
    /// </summary>
    public static class Surface
    {
        /// <summary>
        /// Surface area of a ball cap (including base).
        /// </summary>
        public static class Area
        {
            public static N FromCapHeight<N>(N radius, N capHeight)
                where N : IRootFunctions<N>, IFloatingPointConstants<N>
            {
                var cr = SphereCap.CapRadius.FromCapHeight(radius, capHeight);
                return SphereCap.Surface.Area.FromCapHeight(radius, capHeight) + N.Pi * cr * cr;
            }

            public static N FromCapRadius<N>(N capHeight, N capRadius)
                where N : IFloatingPointConstants<N>
                => SphereCap.Surface.Area.FromCapRadius(capHeight, capRadius) + N.Pi * capRadius * capRadius;

            public static N FromPolarAngle<N>(N radius, N polarAngle)
                where N : IFloatingPointConstants<N>, ITrigonometricFunctions<N>
            {
                var cr = SphereCap.CapRadius.FromCapAngle(radius, polarAngle);
                return SphereCap.Surface.Area.FromPolarAngle(radius, polarAngle) + N.Pi * cr * cr;
            }
        }
    }

    /// <summary>
    /// Region enclosed by a ball cap.
    /// </summary>
    public static class Region
    {
        /// <summary>
        /// Enclosed volume of a ball cap.
        /// </summary>
        public static class Volume
        {
            public static N FromCapHeight<N>(N radius, N capHeight)
                where N : IFloatingPointConstants<N>
                => N.Pi * capHeight * capHeight * (N.CreateChecked(3) * radius - capHeight) / N.CreateChecked(3);

            public static N FromCapRadius<N>(N capHeight, N capRadius)
                where N : IFloatingPointConstants<N>
                => N.Pi * capHeight * (N.CreateChecked(3) * capRadius * capRadius + capHeight * capHeight) / N.CreateChecked(6);

            public static N FromPolarAngle<N>(N radius, N polarAngle)
                where N : IFloatingPointConstants<N>, ITrigonometricFunctions<N>
            {
                var cos = N.Cos(polarAngle);
                var oneMinusCos = N.One - cos;
                return N.Pi * radius * radius * radius * (N.CreateChecked(2) + cos) * oneMinusCos * oneMinusCos / N.CreateChecked(3);
            }
        }
    }
}
