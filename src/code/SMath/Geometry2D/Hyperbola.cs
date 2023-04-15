using System.Numerics;

namespace SMath.GeometryD2
{
    /// <summary>
    /// Hyperbola.
    /// </summary>
    /// <remarks>
    /// <a href="https://en.wikipedia.org/wiki/Hyperbola">wikipedia</a>
    /// </remarks>
    public static class Hyperbola
    {
        public static class Eccentricity
        {
            public static N FromRadius<N>(N majorRadius, N minorRadius)
                where N : IRootFunctions<N>
                => N.Sqrt(N.One + (minorRadius * minorRadius) / (majorRadius * majorRadius));
        }
    }
}
