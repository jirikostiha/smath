namespace Wayout.Mathematics.Geometry.D3
{
    using Wayout.Mathematics.Functions;

    /// <summary>
    /// Cube
    /// </summary>
    /// <remarks>
    /// <a href="https://en.wikipedia.org/wiki/Cube">wikipedia</a>
    /// </remarks>
    public static class Cube
    {
        public const int Vertices = Cuboid.Vertices;

        public const int Edges = Cuboid.Edges;

        public const int Faces = Cuboid.Faces;

        public const string SchlafliSymbol = "{4,3}";

        public static readonly double DihedralAngle = PI / 2; //90 degrees


        public static double SurfaceArea(double radius) => 4 * PI * Power2.f(radius);

        public static double EnclosedVolume(double radius) => Ball.Volume(radius);

    }
}
