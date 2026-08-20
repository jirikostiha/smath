using System.Numerics;

namespace SMath.Geometry2D;

/// <summary>
/// Square, the regular quadrilateral. It is both a rectangle with equal edges and a rhombus
/// with right angles, the properties shared with a rectangle are delegated to it.
/// </summary>
/// <remarks>
/// <a href="https://en.wikipedia.org/wiki/Square">Wikipedia</a>
/// <a href="https://mathworld.wolfram.com/Square.html">Wolfram Mathworld</a>
/// </remarks>
public static class Square
{
    /// <summary> Count of vertices. </summary>
    public static N VertexCount<N>()
        where N : INumberBase<N>
        => Rectangle.VertexCount<N>();

    /// <summary> Count of edges. </summary>
    public static N EdgeCount<N>()
        where N : INumberBase<N>
        => Rectangle.EdgeCount<N>();

    /// <summary> Count of diagonals. </summary>
    public static N DiagonalCount<N>()
        where N : INumberBase<N>
        => N.CreateTruncating(2);

    /// <summary> 90 degrees in rads. </summary>
    public static N InternalAngle<N>()
        where N : ITrigonometricFunctions<N>
        => Rectangle.InternalAngle<N>();

    /// <summary> Sum of internal angles in rads. </summary>
    public static N InternalAngleSum<N>()
        where N : IFloatingPointConstants<N>
        => N.CreateChecked(2) * N.Pi;

    /// <summary> 90 degrees in rads, the angle an edge subtends in the center. </summary>
    public static N CentralAngle<N>()
        where N : IFloatingPointConstants<N>
        => N.Pi / N.CreateChecked(2);

    /// <summary> Schläfli symbol </summary>
    public const string SchlafliSymbol = "{4}";

    /// <summary> Bottom left vertex of the unit square. </summary>
    public static (N X, N Y) Vertex1<N>()
        where N : IFloatingPoint<N>
        => Rectangle.Vertex1<N>();

    /// <summary> Bottom right vertex of the unit square. </summary>
    public static (N X, N Y) Vertex2<N>()
        where N : IFloatingPoint<N>
        => Rectangle.Vertex2<N>();

    /// <summary> Top right vertex of the unit square. </summary>
    public static (N X, N Y) Vertex3<N>()
        where N : IFloatingPoint<N>
        => Rectangle.Vertex3<N>();

    /// <summary> Top left vertex of the unit square. </summary>
    public static (N X, N Y) Vertex4<N>()
        where N : IFloatingPoint<N>
        => Rectangle.Vertex4<N>();

    /// <summary> Center of the unit square. </summary>
    public static (N X, N Y) Center<N>()
        where N : IFloatingPoint<N>
        => Rectangle.Center<N>();

    /// <summary> Edge of a square. </summary>
    public static class Edge
    {
        public static N FromDiagonal<N>(N diagonal)
            where N : IRootFunctions<N>
            => diagonal / N.Sqrt(N.CreateChecked(2));

        public static N FromPerimeter<N>(N perimeter)
            where N : INumberBase<N>
            => perimeter / N.CreateChecked(4);

        public static N FromArea<N>(N area)
            where N : IRootFunctions<N>
            => N.Sqrt(area);

        public static N FromCircumradius<N>(N circumradius)
            where N : IRootFunctions<N>
            => N.Sqrt(N.CreateChecked(2)) * circumradius;

        public static N FromInradius<N>(N inradius)
            where N : INumberBase<N>
            => N.CreateChecked(2) * inradius;
    }

    /// <summary> Diagonal of a square. </summary>
    public static class Diagonal
    {
        public static N FromEdge<N>(N edgeLength)
            where N : IRootFunctions<N>
            => PT.Hypotenuse(edgeLength, edgeLength);

        public static N FromCircumradius<N>(N circumradius)
            where N : INumberBase<N>
            => N.CreateChecked(2) * circumradius;
    }

    /// <summary> Circumradius of a square, the distance between its center and a vertex. </summary>
    public static class Circumradius
    {
        public static N FromEdge<N>(N edgeLength)
            where N : IRootFunctions<N>
            => edgeLength * N.Sqrt(N.CreateChecked(2)) / N.CreateChecked(2);

        public static N FromDiagonal<N>(N diagonal)
            where N : INumberBase<N>
            => diagonal / N.CreateChecked(2);

        public static N FromInradius<N>(N inradius)
            where N : IRootFunctions<N>
            => inradius * N.Sqrt(N.CreateChecked(2));
    }

    /// <summary> Inradius or apothem of a square, the distance between its center and an edge. </summary>
    public static class Inradius
    {
        public static N FromEdge<N>(N edgeLength)
            where N : INumberBase<N>
            => edgeLength / N.CreateChecked(2);

        public static N FromCircumradius<N>(N circumradius)
            where N : IRootFunctions<N>
            => circumradius / N.Sqrt(N.CreateChecked(2));
    }

    /// <summary> Vertices of a square. </summary>
    public static class Vertices
    {
        /// <summary>
        /// Enumerating the vertices of a square centered in the origin from the +x axis
        /// towards the +y axis and beyond.
        /// </summary>
        public static IEnumerable<(N X, N Y)> FromCircumradius<N>(N circumradius)
            where N : IFloatingPoint<N>, ITrigonometricFunctions<N>
            => Polygon.Regular.Vertices.FromCircumradius(circumradius, 4);
    }

    /// <summary>
    /// Perimeter or outline of a square.
    /// </summary>
    public static class Perimeter
    {
        /// <summary>
        /// Length of the perimeter of a square.
        /// </summary>
        public static class Length
        {
            public static N FromEdge<N>(N edgeLength)
                where N : INumberBase<N>
                => EdgeCount<N>() * edgeLength;

            public static N FromDiagonal<N>(N diagonal)
                where N : IRootFunctions<N>
                => EdgeCount<N>() * Edge.FromDiagonal(diagonal);

            public static N FromCircumradius<N>(N circumradius)
                where N : IRootFunctions<N>
                => EdgeCount<N>() * Edge.FromCircumradius(circumradius);

            public static N FromInradius<N>(N inradius)
                where N : INumberBase<N>
                => N.CreateChecked(8) * inradius;
        }
    }

    /// <summary>
    /// Enclosed plane region of a square.
    /// </summary>
    public static class Region
    {
        /// <summary>
        /// Enclosed plane region area of a square.
        /// </summary>
        public static class Area
        {
            public static N FromEdge<N>(N edgeLength)
                where N : IMultiplyOperators<N, N, N>
                => edgeLength * edgeLength;

            public static N FromDiagonal<N>(N diagonal)
                where N : INumberBase<N>
                => diagonal * diagonal / N.CreateChecked(2);

            public static N FromCircumradius<N>(N circumradius)
                where N : INumberBase<N>
                => N.CreateChecked(2) * circumradius * circumradius;

            public static N FromInradius<N>(N inradius)
                where N : INumberBase<N>
                => N.CreateChecked(4) * inradius * inradius;

            public static N FromPerimeter<N>(N perimeter)
                where N : INumberBase<N>
                => perimeter * perimeter / N.CreateChecked(16);
        }
    }
}
