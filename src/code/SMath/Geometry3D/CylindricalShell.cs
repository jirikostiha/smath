using System.Numerics;

namespace SMath.Geometry3D;

/// <summary>Cylindrical shell, a thick-walled tube: a right circular cylinder with a coaxial cylindrical hole through it.</summary>
/// <remarks><a href="https://en.wikipedia.org/wiki/Cylinder#Cylindrical_shells">Wikipedia</a></remarks>
public static class CylindricalShell
{
    /// <summary>Thickness of the wall of a cylindrical shell.</summary>
    public static class Thickness
    {
        public static N FromRadii<N>(N innerRadius, N outerRadius)
            where N : ISubtractionOperators<N, N, N>
            => Geometry2D.Annulus.Width.FromRadii(innerRadius, outerRadius);
    }

    /// <summary>Surface of a cylindrical shell.</summary>
    public static class Surface
    {
        /// <summary>Surface area of a cylindrical shell, the outer and the inner lateral surface and both annular ends.</summary>
        public static class Area
        {
            public static N FromRadii<N>(N innerRadius, N outerRadius, N height)
                where N : IFloatingPointConstants<N>
                => (Geometry2D.Annulus.Perimeter.Length.FromRadii(innerRadius, outerRadius) * height)
                    + (N.CreateChecked(2) * Geometry2D.Annulus.Region.Area.FromRadii(innerRadius, outerRadius));
        }
    }

    /// <summary>Region enclosed by a cylindrical shell.</summary>
    public static class Region
    {
        /// <summary>Enclosed volume of a cylindrical shell, the area of its annular base times its height.</summary>
        public static class Volume
        {
            public static N FromRadii<N>(N innerRadius, N outerRadius, N height)
                where N : IFloatingPointConstants<N>
                => Geometry2D.Annulus.Region.Area.FromRadii(innerRadius, outerRadius) * height;
        }
    }
}
