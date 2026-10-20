using System.Numerics;

namespace SMath.Geometry3D;

/// <summary>
/// Cube, the regular hexahedron and the three dimensional counterpart of a square. It is a cuboid with equal edges,
/// the properties shared with a cuboid are delegated to it.
/// </summary>
/// <remarks>
/// <a href="https://en.wikipedia.org/wiki/Cube">Wikipedia</a>
/// <a href="https://mathworld.wolfram.com/Cube.html">Wolfram Mathworld</a>
/// </remarks>
public static class Cube
{
    /// <summary> Count of vertices. </summary>
    public static N VertexCount<N>()
        where N : INumberBase<N>
        => Cuboid.VertexCount<N>();

    /// <summary> Count of edges. </summary>
    public static N EdgeCount<N>()
        where N : INumberBase<N>
        => Cuboid.EdgeCount<N>();

    /// <summary> Count of faces. </summary>
    public static N FaceCount<N>()
        where N : INumberBase<N>
        => Cuboid.FaceCount<N>();

    /// <summary> 90 degrees in rads. </summary>
    public static N DihedralAngle<N>()
        where N : ITrigonometricFunctions<N>
        => Cuboid.DihedralAngle<N>();

    /// <summary> Schläfli symbol </summary>
    public const string SchlafliSymbol = "{4,3}";

    /// <summary> Vertex of the unit cube in the origin. </summary>
    public static (N X, N Y, N Z) Vertex1<N>()
        where N : IFloatingPoint<N>
        => Cuboid.Vertex1<N>();

    /// <summary> Vertex of the unit cube on the +x axis. </summary>
    public static (N X, N Y, N Z) Vertex2<N>()
        where N : IFloatingPoint<N>
        => Cuboid.Vertex2<N>();

    /// <summary> Vertex of the unit cube in the xy plane opposite to the origin. </summary>
    public static (N X, N Y, N Z) Vertex3<N>()
        where N : IFloatingPoint<N>
        => Cuboid.Vertex3<N>();

    /// <summary> Vertex of the unit cube on the +y axis. </summary>
    public static (N X, N Y, N Z) Vertex4<N>()
        where N : IFloatingPoint<N>
        => Cuboid.Vertex4<N>();

    /// <summary> Vertex of the unit cube on the +z axis. </summary>
    public static (N X, N Y, N Z) Vertex5<N>()
        where N : IFloatingPoint<N>
        => Cuboid.Vertex5<N>();

    /// <summary> Vertex of the unit cube above <see cref="Vertex2{N}"/>. </summary>
    public static (N X, N Y, N Z) Vertex6<N>()
        where N : IFloatingPoint<N>
        => Cuboid.Vertex6<N>();

    /// <summary> Vertex of the unit cube opposite to the origin. </summary>
    public static (N X, N Y, N Z) Vertex7<N>()
        where N : IFloatingPoint<N>
        => Cuboid.Vertex7<N>();

    /// <summary> Vertex of the unit cube above <see cref="Vertex4{N}"/>. </summary>
    public static (N X, N Y, N Z) Vertex8<N>()
        where N : IFloatingPoint<N>
        => Cuboid.Vertex8<N>();

    /// <summary> Center of the unit cube. </summary>
    public static (N X, N Y, N Z) Center<N>()
        where N : IFloatingPoint<N>
        => Cuboid.Center<N>();

    /// <summary> Edge of a cube. </summary>
    public static class Edge
    {
        public static N FromFaceDiagonal<N>(N faceDiagonal)
            where N : IRootFunctions<N>
            => faceDiagonal / N.Sqrt(N.CreateChecked(2));

        public static N FromSpaceDiagonal<N>(N spaceDiagonal)
            where N : IRootFunctions<N>
            => spaceDiagonal / N.Sqrt(N.CreateChecked(3));

        public static N FromSurfaceArea<N>(N surfaceArea)
            where N : IRootFunctions<N>
            => N.Sqrt(surfaceArea / N.CreateChecked(6));

        public static N FromVolume<N>(N volume)
            where N : IRootFunctions<N>
            => N.Cbrt(volume);

        public static N FromCircumradius<N>(N circumradius)
            where N : IRootFunctions<N>
            => N.CreateChecked(2) * circumradius / N.Sqrt(N.CreateChecked(3));

        public static N FromMidradius<N>(N midradius)
            where N : IRootFunctions<N>
            => N.Sqrt(N.CreateChecked(2)) * midradius;

        public static N FromInradius<N>(N inradius)
            where N : INumberBase<N>
            => N.CreateChecked(2) * inradius;
    }

    /// <summary> Diagonal of a face of a cube. </summary>
    public static class FaceDiagonal
    {
        public static N FromEdge<N>(N edgeLength)
            where N : IRootFunctions<N>
            => PT.Hypotenuse(edgeLength, edgeLength);
    }

    /// <summary> Space diagonal of a cube, it joins opposite vertices through the center. </summary>
    public static class SpaceDiagonal
    {
        public static N FromEdge<N>(N edgeLength)
            where N : IRootFunctions<N>
            => Cuboid.Diagonal.FromEdges(edgeLength, edgeLength, edgeLength);

        public static N FromCircumradius<N>(N circumradius)
            where N : INumberBase<N>
            => N.CreateChecked(2) * circumradius;
    }

    /// <summary> Circumradius of a cube, the distance between its center and a vertex. </summary>
    public static class Circumradius
    {
        public static N FromEdge<N>(N edgeLength)
            where N : IRootFunctions<N>
            => edgeLength * N.Sqrt(N.CreateChecked(3)) / N.CreateChecked(2);

        public static N FromSpaceDiagonal<N>(N spaceDiagonal)
            where N : INumberBase<N>
            => spaceDiagonal / N.CreateChecked(2);

        public static N FromInradius<N>(N inradius)
            where N : IRootFunctions<N>
            => inradius * N.Sqrt(N.CreateChecked(3));
    }

    /// <summary> Midradius of a cube, the distance between its center and the middle of an edge. </summary>
    public static class Midradius
    {
        public static N FromEdge<N>(N edgeLength)
            where N : IRootFunctions<N>
            => edgeLength / N.Sqrt(N.CreateChecked(2));
    }

    /// <summary> Inradius of a cube, the distance between its center and a face. </summary>
    public static class Inradius
    {
        public static N FromEdge<N>(N edgeLength)
            where N : INumberBase<N>
            => edgeLength / N.CreateChecked(2);

        public static N FromCircumradius<N>(N circumradius)
            where N : IRootFunctions<N>
            => circumradius / N.Sqrt(N.CreateChecked(3));
    }

    /// <summary>
    /// Surface of a cube.
    /// </summary>
    public static class Surface
    {
        /// <summary>
        /// Surface area of a cube.
        /// </summary>
        public static class Area
        {
            public static N FromEdge<N>(N edgeLength)
                where N : INumberBase<N>
                => Cuboid.Surface.Area.FromEdges(edgeLength, edgeLength, edgeLength);

            public static N FromSpaceDiagonal<N>(N spaceDiagonal)
                where N : INumberBase<N>
                => N.CreateChecked(2) * spaceDiagonal * spaceDiagonal;
        }
    }

    /// <summary>
    /// Region enclosed by a cube.
    /// </summary>
    public static class Region
    {
        /// <summary>
        /// Enclosed volume of a cube.
        /// </summary>
        public static class Volume
        {
            public static N FromEdge<N>(N edgeLength)
                where N : INumberBase<N>
                => Cuboid.Region.Volume.FromEdges(edgeLength, edgeLength, edgeLength);

            public static N FromSurfaceArea<N>(N surfaceArea)
                where N : IRootFunctions<N>
                => FromEdge(Edge.FromSurfaceArea(surfaceArea));
        }
    }
}
