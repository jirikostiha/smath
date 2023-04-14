namespace Wayout.Mathematics.Geometry.D2
{
    using Wayout.Mathematics.Functions;

    /// <summary>
    /// Square plane
    /// </summary>
    /// <remarks>
    /// <a href="https://en.wikipedia.org/wiki/Square">wikipedia</a>
    /// </remarks>
    public static class SquarePlane
    {
        public static double AreaByEdge(double edgeLength) => Power2.f(edgeLength);

        public static double AreaByDiagonal(double diagonal) => Power2.f(diagonal) / 2;

        public static double AreaByCircumradius(double circumradius) => 2 * Power2.f(circumradius);

        public static double AreaByInradius(double inradius) => 4 * Power2.f(inradius);
    }
}
