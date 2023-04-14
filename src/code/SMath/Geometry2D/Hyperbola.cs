namespace Wayout.Mathematics.Geometry.D2
{
    using Functions;

    /// <summary>
    /// Hyperbola
    /// </summary>
    /// <remarks>
    /// <a href="https://en.wikipedia.org/wiki/Hyperbola">wikipedia</a>
    /// </remarks>
    public static class Hyperbola
    {
        public static N Eccentricity<N>(N r1, N r2)
            where N : INumberBase<N>
            => Root2.f(1 + Power2.f(r2) / Power2.f(r1));
    }
}
