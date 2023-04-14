namespace Wayout.Mathematics.Geometry.D2
{
    using Functions;

    /// <summary>
    /// Equilateral triangle plane
    /// </summary>
    /// <remarks>
    /// <a href="https://en.wikipedia.org/wiki/Equilateral_triangle">wikipedia</a>
    /// </remarks>
    public static class EquilateralTrianglePlane
    {
        public static N Area<N>(N a)
            where N : INumberBase<N>
            => Root2.f(3) * Power2.f(a) / 4;
    }
}
