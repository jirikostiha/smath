namespace Wayout.Mathematics.Geometry.D3
{
    using Wayout.Mathematics.Functions;

    /// <summary>
    /// Cuboid
    /// </summary>
    /// <remarks>
    /// <a href="https://en.wikipedia.org/wiki/Cuboid">wikipedia</a>
    /// </remarks>
    public static class Cuboid
    {
        public const int Vertices = 8;

        public const int Edges = 12;

        public const int Faces = 6;

        public const string SchlafliSymbol = "{} × {} × {}";

        public static double SurfaceArea(double radius) => 4 * PI * Power2.f(radius);

        public static double EnclosedVolume(double radius) => Ball.Volume(radius);

    }
}
