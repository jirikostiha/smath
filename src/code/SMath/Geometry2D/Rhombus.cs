namespace Wayout.Mathematics.Geometry.D2
{
    /// <summary>
    /// Rhombus
    /// </summary>
    /// <remarks>
    /// <a href="https://en.wikipedia.org/wiki/Rhombus">wikipedia</a>
    /// </remarks>
    public static class Rhombus
    {
        public const int Vertices = 4;

        public const int Edges = 4;

        public const string SchlafliSymbol = "{} + {}";

        public static double Perimeter(double edgeLength) => Edges * edgeLength;
    }
}
