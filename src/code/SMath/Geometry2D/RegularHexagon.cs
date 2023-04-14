namespace Wayout.Mathematics.Geometry.D2
{
    /// <summary>
    /// Regular hexagon
    /// </summary>
    /// <remarks>
    /// <a href="https://en.wikipedia.org/wiki/Hexagon#Regular_hexagon">wikipedia</a>
    /// </remarks>
    public static class RegularHexagon
    {
        public const int Vertices = 6;

        public const int Edges = 6;

        public const string SchlafliSymbol = "{6}, t{3}";

        public const double InternalAngle = 2 / 3 * PI; // 120 degrees

        public static double Perimeter(double edgeLength) => Edges * edgeLength;
    }
}
