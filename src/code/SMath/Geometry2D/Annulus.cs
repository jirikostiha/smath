using System.Numerics;

namespace SMath.Geometry2D;

/// <summary>Annulus, the plane region between two concentric circles.</summary>
/// <remarks>
/// <a href="https://en.wikipedia.org/wiki/Annulus_(mathematics)">Wikipedia</a>
/// <a href="https://mathworld.wolfram.com/Annulus.html">Wolfram Mathworld</a>
/// </remarks>
public static class Annulus
{
    /// <summary>Width of an annulus, the distance between its circles.</summary>
    public static class Width
    {
        public static N FromRadii<N>(N innerRadius, N outerRadius)
            where N : ISubtractionOperators<N, N, N>
            => outerRadius - innerRadius;
    }

    /// <summary>Perimeter of an annulus, both of its circles.</summary>
    public static class Perimeter
    {
        /// <summary>Total length of the inner and the outer circle.</summary>
        public static class Length
        {
            public static N FromRadii<N>(N innerRadius, N outerRadius)
                where N : IFloatingPointConstants<N>
                => Circle.Perimeter.Length.FromRadius(innerRadius + outerRadius);
        }
    }

    /// <summary>Enclosed plane region of an annulus.</summary>
    public static class Region
    {
        /// <summary>Enclosed plane region area of an annulus, pi (R^2 - r^2).</summary>
        /// <remarks> Evaluated as pi (R + r)(R - r), which keeps the precision of a narrow annulus. </remarks>
        public static class Area
        {
            public static N FromRadii<N>(N innerRadius, N outerRadius)
                where N : IFloatingPointConstants<N>
                => N.Pi * (outerRadius + innerRadius) * (outerRadius - innerRadius);
        }
    }
}
