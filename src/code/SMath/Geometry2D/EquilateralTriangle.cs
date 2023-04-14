using System.Numerics;

namespace Wayout.Mathematics.Geometry.D2
{
    /// <summary>
    /// Equilateral triangle
    /// </summary>
    /// <remarks>
    /// <a href="https://en.wikipedia.org/wiki/Equilateral_triangle">wikipedia</a>
    /// </remarks>
    public static class EquilateralTriangle
    {
        public const double InternalAngle = PI / 3; // 60 degrees

        public static N Perimeter<N>(N edgeLength)
            where N : INumberBase<N>
            => Triangle.Edges * edgeLength;
    }
}
