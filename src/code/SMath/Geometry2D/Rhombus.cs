using System.Numerics;

namespace SMath.Geometry2D;

/// <summary>
/// Rhombus, the quadrilateral with four edges of equal length. Its diagonals are perpendicular
/// and bisect each other, and its opposite angles are equal.
/// </summary>
/// <remarks>
/// <a href="https://en.wikipedia.org/wiki/Rhombus">Wikipedia</a>
/// <a href="https://mathworld.wolfram.com/Rhombus.html">Wolfram Mathworld</a>
/// </remarks>
public static class Rhombus
{
    /// <summary> Count of vertices. </summary>
    public static N VertexCount<N>()
        where N : INumberBase<N>
        => N.CreateTruncating(4);

    /// <summary> Count of edges. </summary>
    public static N EdgeCount<N>()
        where N : INumberBase<N>
        => N.CreateTruncating(4);

    /// <summary> Count of diagonals. </summary>
    public static N DiagonalCount<N>()
        where N : INumberBase<N>
        => N.CreateTruncating(2);

    /// <summary> Sum of internal angles in rads. </summary>
    public static N InternalAngleSum<N>()
        where N : IFloatingPointConstants<N>
        => N.CreateChecked(2) * N.Pi;

    /// <summary> The internal angle adjacent to the given one, their sum is a straight angle. </summary>
    public static N AdjacentAngle<N>(N vertexAngle)
        where N : IFloatingPointConstants<N>
        => N.Pi - vertexAngle;

    /// <summary> Schläfli symbol </summary>
    public const string SchlafliSymbol = "{} + {}";

    /// <summary> Edge of a rhombus. </summary>
    public static class Edge
    {
        /// <summary> Length of an edge of a rhombus, half of the hypotenuse of its diagonals. </summary>
        public static N FromDiagonals<N>(N p, N q)
            where N : IRootFunctions<N>
            => PT.Hypotenuse(p, q) / N.CreateChecked(2);

        public static N FromHeightAndAngle<N>(N height, N vertexAngle)
            where N : ITrigonometricFunctions<N>
            => height / N.Sin(vertexAngle);
    }

    /// <summary> Diagonals of a rhombus. </summary>
    public static class Diagonal
    {
        /// <summary>
        /// Lengths of both diagonals of a rhombus. The first one subtends the given vertex angle,
        /// the second one the adjacent angle.
        /// </summary>
        public static (N Diagonal1, N Diagonal2) FromEdgeAndAngle<N>(N edgeLength, N vertexAngle)
            where N : ITrigonometricFunctions<N>
        {
            var two = N.CreateChecked(2);
            var halfAngle = vertexAngle / two;

            return (two * edgeLength * N.Sin(halfAngle), two * edgeLength * N.Cos(halfAngle));
        }

        /// <summary> Length of the other diagonal of a rhombus from its edge and one diagonal. </summary>
        public static N FromEdgeAndDiagonal<N>(N edgeLength, N diagonal)
            where N : IRootFunctions<N>
            => N.CreateChecked(2) * PT.Leg(edgeLength, diagonal / N.CreateChecked(2));
    }

    /// <summary> Height or altitude of a rhombus, the distance between two opposite edges. </summary>
    public static class Height
    {
        public static N FromEdgeAndAngle<N>(N edgeLength, N vertexAngle)
            where N : ITrigonometricFunctions<N>
            => edgeLength * N.Sin(vertexAngle);

        public static N FromDiagonals<N>(N p, N q)
            where N : IRootFunctions<N>
            => p * q / PT.Hypotenuse(p, q);
    }

    /// <summary> Inradius of a rhombus, the radius of the circle inscribed in it. </summary>
    public static class Inradius
    {
        public static N FromHeight<N>(N height)
            where N : INumberBase<N>
            => height / N.CreateChecked(2);

        public static N FromEdgeAndAngle<N>(N edgeLength, N vertexAngle)
            where N : ITrigonometricFunctions<N>
            => edgeLength * N.Sin(vertexAngle) / N.CreateChecked(2);

        public static N FromDiagonals<N>(N p, N q)
            where N : IRootFunctions<N>
            => p * q / (N.CreateChecked(2) * PT.Hypotenuse(p, q));
    }

    /// <summary>
    /// Perimeter or outline of a rhombus.
    /// </summary>
    public static class Perimeter
    {
        /// <summary>
        /// Length of the perimeter of a rhombus.
        /// </summary>
        public static class Length
        {
            public static N FromEdge<N>(N edgeLength)
                where N : INumberBase<N>
                => EdgeCount<N>() * edgeLength;

            public static N FromDiagonals<N>(N p, N q)
                where N : IRootFunctions<N>
                => N.CreateChecked(2) * PT.Hypotenuse(p, q);
        }
    }

    /// <summary>
    /// Enclosed plane region of a rhombus.
    /// </summary>
    public static class Region
    {
        /// <summary>
        /// Enclosed plane region area of a rhombus.
        /// </summary>
        public static class Area
        {
            public static N FromHeight<N>(N edgeLength, N height)
                where N : INumberBase<N>
                => edgeLength * height;

            public static N FromDiagonals<N>(N p, N q)
                where N : INumberBase<N>
                => p * q / N.CreateChecked(2);

            public static N FromEdgeAndAngle<N>(N edgeLength, N vertexAngle)
                where N : ITrigonometricFunctions<N>
                => edgeLength * edgeLength * N.Sin(vertexAngle);

            public static N FromHeightAndAngle<N>(N height, N vertexAngle)
                where N : ITrigonometricFunctions<N>
                => height * height / N.Sin(vertexAngle);

            public static N FromInradius<N>(N edgeLength, N inradius)
                where N : INumberBase<N>
                => N.CreateChecked(2) * edgeLength * inradius;
        }
    }
}
