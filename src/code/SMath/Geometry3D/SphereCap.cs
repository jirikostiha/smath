using System.Numerics;

namespace SMath.Geometry3D;

/// <summary>
/// Spherical cap of a sphere (curved surface without base).
/// </summary>
/// <remarks>
/// <a href="https://en.wikipedia.org/wiki/Spherical_cap">Wikipedia</a>
/// <a href="https://mathworld.wolfram.com/SphericalCap.html">Wolfram MathWorld</a>
/// </remarks>
public static class SphereCap
{
    public static N CapHeight<N>(N radius, N capRadius)
        where N : IRootFunctions<N>
        => Height.FromCapRadius(radius, capRadius);

    public static N CapHeightFromCapAngle<N>(N radius, N capAngle)
        where N : ITrigonometricFunctions<N>
        => Height.FromCapAngle(radius, capAngle);

    public static N CapRadiusFromCapHeight<N>(N radius, N capHeight)
        where N : IRootFunctions<N>
        => CapRadius.FromCapHeight(radius, capHeight);

    public static N CapRadiusFromCapAngle<N>(N radius, N capAngle)
        where N : ITrigonometricFunctions<N>
        => CapRadius.FromCapAngle(radius, capAngle);

    public static N RadiusFromCapHeightAndCapRadius<N>(N capHeight, N capRadius)
        where N : INumberBase<N>
        => Radius.FromCapHeightAndCapRadius(capHeight, capRadius);

    public static N CapAngleFromCapRadius<N>(N radius, N capRadius)
        where N : ITrigonometricFunctions<N>
        => CapAngle.FromCapRadius(radius, capRadius);

    public static N CapAngleFromCapHeight<N>(N radius, N capHeight)
        where N : ITrigonometricFunctions<N>
        => CapAngle.FromCapHeight(radius, capHeight);

    /// <summary> Surface area (without base) </summary>
    public static N SurfaceArea<N>(N radius, N capHeight)
        where N : IFloatingPointConstants<N>
        => Surface.Area.FromCapHeight(radius, capHeight);

    /// <summary> Surface area (without base) </summary>
    public static N SurfaceAreaByCapRadius<N>(N capHeight, N capRadius)
        where N : IFloatingPointConstants<N>
        => Surface.Area.FromCapRadius(capHeight, capRadius);

    /// <summary> Surface area (without base) </summary>
    public static N SurfaceAreaByPolarAngle<N>(N radius, N polarAngle)
        where N : IFloatingPointConstants<N>, ITrigonometricFunctions<N>
        => Surface.Area.FromPolarAngle(radius, polarAngle);

    /// <summary>
    /// Height of a spherical cap.
    /// </summary>
    public static class Height
    {
        public static N FromCapRadius<N>(N radius, N capRadius)
            where N : IRootFunctions<N>
            => radius - N.Sqrt((radius * radius) - (capRadius * capRadius));

        public static N FromCapAngle<N>(N radius, N capAngle)
            where N : ITrigonometricFunctions<N>
            => radius * (N.One - N.Cos(capAngle));
    }

    /// <summary>
    /// Base radius (cap radius) of a spherical cap.
    /// </summary>
    public static class CapRadius
    {
        public static N FromCapHeight<N>(N radius, N capHeight)
            where N : IRootFunctions<N>
            => N.Sqrt((radius * radius) - ((radius - capHeight) * (radius - capHeight)));

        public static N FromCapAngle<N>(N radius, N capAngle)
            where N : ITrigonometricFunctions<N>
            => N.Sin(capAngle) * radius;
    }

    /// <summary>
    /// Radius of the sphere containing the cap.
    /// </summary>
    public static class Radius
    {
        public static N FromCapHeightAndCapRadius<N>(N capHeight, N capRadius)
            where N : INumberBase<N>
            => ((capHeight * capHeight) + (capRadius * capRadius)) / (N.CreateChecked(2) * capHeight);
    }

    /// <summary>
    /// Cap angle (polar angle) of a spherical cap.
    /// </summary>
    public static class CapAngle
    {
        public static N FromCapRadius<N>(N radius, N capRadius)
            where N : ITrigonometricFunctions<N>
            => N.Asin(capRadius / radius);

        public static N FromCapHeight<N>(N radius, N capHeight)
            where N : ITrigonometricFunctions<N>
            => N.Acos(N.One - (capHeight / radius));
    }

    /// <summary>
    /// Surface of a spherical cap (excluding base).
    /// </summary>
    public static class Surface
    {
        /// <summary>
        /// Surface area of a spherical cap (excluding base).
        /// </summary>
        public static class Area
        {
            public static N FromCapHeight<N>(N radius, N capHeight)
                where N : IFloatingPointConstants<N>
                => N.CreateChecked(2) * N.Pi * radius * capHeight;

            public static N FromCapRadius<N>(N capHeight, N capRadius)
                where N : IFloatingPointConstants<N>
                => N.Pi * ((capHeight * capHeight) + (capRadius * capRadius));

            public static N FromPolarAngle<N>(N radius, N polarAngle)
                where N : IFloatingPointConstants<N>, ITrigonometricFunctions<N>
                => N.CreateChecked(2) * N.Pi * radius * radius * (N.One - N.Cos(polarAngle));
        }
    }
}
