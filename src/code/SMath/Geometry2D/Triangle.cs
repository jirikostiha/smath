namespace Wayout.Mathematics.Geometry.D2
{
    /// <summary>
    /// Triangle
    /// </summary>
    /// <remarks>
    /// <a href="https://en.wikipedia.org/wiki/Triangle">wikipedia</a>
    /// https://en.wikipedia.org/wiki/Simplex pro ruzne dimenze
    /// https://en.wikipedia.org/wiki/Tetrahedron v 3D
    /// </remarks>
    public static class Triangle
    {
        public const int Vertices = 3;

        public const int Edges = 3;

        public const string SchlafliSymbol = "{3}";

        public static double Perimeter(double a, double b, double c) => a + b + c;
    }

    /// <summary>
    /// Right triangle
    /// </summary>
    /// <remarks>
    /// <a href="https://en.wikipedia.org/wiki/Right_triangle">wikipedia</a>
    /// </remarks>
    public static class RightTriangle
    {
        
    }

    /// <summary>
    /// 
    /// </summary>
    /// <remarks>
    /// https://en.wikipedia.org/wiki/Golden_triangle_(mathematics)
    /// </remarks>
    public static class GoldenTriangle
    {
        //pentagon vychazi z GoldenTriangles
    }
}
