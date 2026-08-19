using System.Numerics;

namespace SMath.Geometry2D;

/// <summary>
/// Regular hexagon, the regular polygon with six edges. Its circumradius equals the length
/// of its edge, so it is exactly composed of six equilateral triangles.
/// </summary>
/// <remarks>
/// <a href="https://en.wikipedia.org/wiki/Hexagon#Regular_hexagon">Wikipedia</a>
/// <a href="https://mathworld.wolfram.com/RegularHexagon.html">Wolfram Mathworld</a>
/// </remarks>
public static class RegularHexagon
{
    /// <summary> Count of vertices. </summary>
    public static N VertexCount<N>()
        where N : INumberBase<N>
        => N.CreateTruncating(6);

    /// <summary> Count of edges. </summary>
    public static N EdgeCount<N>()
        where N : INumberBase<N>
        => N.CreateTruncating(6);

    /// <summary> Count of diagonals. </summary>
    public static N DiagonalCount<N>()
        where N : INumberBase<N>
        => N.CreateTruncating(9);

    /// <summary> 120 degrees in rads. </summary>
    public static N InternalAngle<N>()
        where N : IFloatingPointConstants<N>
        => N.CreateChecked(2) * N.Pi / N.CreateChecked(3);

    /// <summary> 60 degrees in rads. </summary>
    public static N ExternalAngle<N>()
        where N : IFloatingPointConstants<N>
        => N.Pi / N.CreateChecked(3);

    /// <summary> 60 degrees in rads, the angle an edge subtends in the center. </summary>
    public static N CentralAngle<N>()
        where N : IFloatingPointConstants<N>
        => N.Pi / N.CreateChecked(3);

    /// <summary> Sum of internal angles in rads. </summary>
    public static N InternalAngleSum<N>()
        where N : IFloatingPointConstants<N>
        => N.CreateChecked(4) * N.Pi;

    /// <summary> Schläfli symbol </summary>
    public const string SchlafliSymbol = "{6}";

    /// <summary> Edge of a regular hexagon. </summary>
    public static class Edge
    {
        /// <summary> Length of an edge of a regular hexagon. It equals its circumradius. </summary>
        public static N FromCircumradius<N>(N circumradius)
            where N : INumberBase<N>
            => circumradius;

        /// <summary> Length of an edge of a regular hexagon from its inradius. </summary>
        public static N FromInradius<N>(N inradius)
            where N : IRootFunctions<N>
            => N.CreateChecked(2) * inradius / N.Sqrt(N.CreateChecked(3));
    }

    /// <summary> Circumradius of a regular hexagon, the distance between its center and a vertex. </summary>
    public static class Circumradius
    {
        /// <summary> Circumradius of a regular hexagon. It equals the length of its edge. </summary>
        public static N FromEdge<N>(N edgeLength)
            where N : INumberBase<N>
            => edgeLength;

        public static N FromInradius<N>(N inradius)
            where N : IRootFunctions<N>
            => N.CreateChecked(2) * inradius / N.Sqrt(N.CreateChecked(3));
    }

    /// <summary> Inradius or apothem of a regular hexagon, the distance between its center and an edge. </summary>
    public static class Inradius
    {
        public static N FromEdge<N>(N edgeLength)
            where N : IRootFunctions<N>
            => edgeLength * N.Sqrt(N.CreateChecked(3)) / N.CreateChecked(2);

        public static N FromCircumradius<N>(N circumradius)
            where N : IRootFunctions<N>
            => circumradius * N.Sqrt(N.CreateChecked(3)) / N.CreateChecked(2);
    }

    /// <summary> Diagonals of a regular hexagon. </summary>
    public static class Diagonal
    {
        /// <summary> Length of a minor, that is short, diagonal connecting two vertices with one vertex in between. </summary>
        public static class Minor
        {
            public static N FromEdge<N>(N edgeLength)
                where N : IRootFunctions<N>
                => edgeLength * N.Sqrt(N.CreateChecked(3));
        }

        /// <summary> Length of a major, that is long, diagonal connecting two opposite vertices. </summary>
        public static class Major
        {
            public static N FromEdge<N>(N edgeLength)
                where N : INumberBase<N>
                => N.CreateChecked(2) * edgeLength;
        }
    }

    /// <summary> Vertices of a regular hexagon. </summary>
    public static class Vertices
    {
        /// <summary>
        /// Enumerating the vertices of a regular hexagon centered in the origin from the +x axis
        /// towards the +y axis and beyond.
        /// </summary>
        public static IEnumerable<(N X, N Y)> FromCircumradius<N>(N circumradius)
            where N : IFloatingPoint<N>, ITrigonometricFunctions<N>
            => Polygon.Regular.Vertices.FromCircumradius(circumradius, 6);
    }

    /// <summary>
    /// Perimeter or outline of a regular hexagon.
    /// </summary>
    public static class Perimeter
    {
        /// <summary>
        /// Length of the perimeter of a regular hexagon.
        /// </summary>
        public static class Length
        {
            public static N FromEdge<N>(N edgeLength)
                where N : INumberBase<N>
                => EdgeCount<N>() * edgeLength;

            public static N FromCircumradius<N>(N circumradius)
                where N : INumberBase<N>
                => EdgeCount<N>() * circumradius;

            public static N FromInradius<N>(N inradius)
                where N : IRootFunctions<N>
                => EdgeCount<N>() * Edge.FromInradius(inradius);
        }
    }

    /// <summary>
    /// Enclosed plane region of a regular hexagon.
    /// </summary>
    public static class Region
    {
        /// <summary>
        /// Enclosed plane region area of a regular hexagon.
        /// </summary>
        public static class Area
        {
            public static N FromEdge<N>(N edgeLength)
                where N : IRootFunctions<N>
                => N.CreateChecked(3) * N.Sqrt(N.CreateChecked(3)) / N.CreateChecked(2)
                    * edgeLength * edgeLength;

            /// <summary> Area of a regular hexagon from its circumradius. It equals the area from its edge. </summary>
            public static N FromCircumradius<N>(N circumradius)
                where N : IRootFunctions<N>
                => FromEdge(circumradius);

            public static N FromInradius<N>(N inradius)
                where N : IRootFunctions<N>
                => N.CreateChecked(2) * N.Sqrt(N.CreateChecked(3)) * inradius * inradius;
        }
    }
}
