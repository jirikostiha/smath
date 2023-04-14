namespace Wayout.Mathematics.Geometry.D2
{
    using System.Collections.Generic;

    /// <summary>
    /// Square
    /// </summary>
    /// <remarks>
    /// <a href="https://en.wikipedia.org/wiki/Square">wikipedia</a>
    /// </remarks>
    public static class Square
    {
        public const int Vertices = Rectangle.VertexCount;

        public const int Edges = Rectangle.EdgeCount;

        public const string SchlafliSymbol = "{4}";

        public const double InternalAngle = Rectangle.InternalAngle;

        public static double Perimeter(double edge) => Edges * edge;

        /// <summary>
        /// Split perimeter to n segments and return coords of splitting points
        /// in ++ quadrant
        /// </summary>
        public static IEnumerable<(double X1, double X2)> Indexes(int count, double edgeLength = 1)
            => Rectangle.Perimeter.Points.Indexes(count, edgeLength, edgeLength);
    }
}
