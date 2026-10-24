using System.Numerics;

namespace SMath.Geometry3D;

/// <summary>Tetrahedron, the triangular pyramid and the three dimensional counterpart of a triangle.</summary>
/// <remarks>
/// <a href="https://en.wikipedia.org/wiki/Tetrahedron">Wikipedia</a>
/// <a href="https://mathworld.wolfram.com/Tetrahedron.html">Wolfram Mathworld</a>
/// </remarks>
public static class Tetrahedron
{
    /// <summary> Count of vertices. </summary>
    public static N VertexCount<N>()
        where N : INumberBase<N>
        => N.CreateTruncating(4);

    /// <summary> Count of edges. </summary>
    public static N EdgeCount<N>()
        where N : INumberBase<N>
        => N.CreateTruncating(6);

    /// <summary> Count of faces. </summary>
    public static N FaceCount<N>()
        where N : INumberBase<N>
        => N.CreateTruncating(4);

    /// <summary>Centroid of a tetrahedron, the average of its vertices.</summary>
    public static class Centroid
    {
        public static (N X, N Y, N Z) FromVertices<N>((N X, N Y, N Z) vertex1, (N X, N Y, N Z) vertex2, (N X, N Y, N Z) vertex3, (N X, N Y, N Z) vertex4)
            where N : INumberBase<N>
        {
            var four = N.CreateChecked(4);
            return ((vertex1.X + vertex2.X + vertex3.X + vertex4.X) / four,
                    (vertex1.Y + vertex2.Y + vertex3.Y + vertex4.Y) / four,
                    (vertex1.Z + vertex2.Z + vertex3.Z + vertex4.Z) / four);
        }
    }

    /// <summary>Region enclosed by a tetrahedron.</summary>
    public static class Region
    {
        /// <summary>Enclosed volume of a tetrahedron.</summary>
        public static class Volume
        {
            /// <summary>Volume of a tetrahedron given by its vertices, a sixth of the volume of the parallelepiped spanned by its edges from one vertex.</summary>
            public static N FromVertices<N>((N X, N Y, N Z) vertex1, (N X, N Y, N Z) vertex2, (N X, N Y, N Z) vertex3, (N X, N Y, N Z) vertex4)
                where N : INumberBase<N>
                => N.Abs(SignedFromVertices(vertex1, vertex2, vertex3, vertex4));

            /// <summary>
            /// Signed volume of a tetrahedron given by its vertices. Positive when the fourth vertex lies on the side
            /// the normal of the first three vertices points to by the right hand rule.
            /// </summary>
            public static N SignedFromVertices<N>((N X, N Y, N Z) vertex1, (N X, N Y, N Z) vertex2, (N X, N Y, N Z) vertex3, (N X, N Y, N Z) vertex4)
                where N : INumberBase<N>
                => GeometricVector3.TripleProduct.FromCartesian(
                    GeometricVector3.Direction.FromCartesian(vertex1, vertex2),
                    GeometricVector3.Direction.FromCartesian(vertex1, vertex3),
                    GeometricVector3.Direction.FromCartesian(vertex1, vertex4)) / N.CreateChecked(6);
        }
    }

    /// <summary>Regular tetrahedron, all faces are equilateral triangles.</summary>
    public static class Regular
    {
        /// <summary> Schläfli symbol </summary>
        public const string SchlafliSymbol = "{3,3}";

        /// <summary> Dihedral angle in rads, arccos(1/3). </summary>
        public static N DihedralAngle<N>()
            where N : ITrigonometricFunctions<N>
            => N.Acos(N.One / N.CreateChecked(3));

        /// <summary> Edge of a regular tetrahedron. </summary>
        public static class Edge
        {
            public static N FromHeight<N>(N height)
                where N : IRootFunctions<N>
                => height * N.Sqrt(N.CreateChecked(3) / N.CreateChecked(2));

            public static N FromCircumradius<N>(N circumradius)
                where N : IRootFunctions<N>
                => N.CreateChecked(4) * circumradius / N.Sqrt(N.CreateChecked(6));

            public static N FromInradius<N>(N inradius)
                where N : IRootFunctions<N>
                => N.CreateChecked(12) * inradius / N.Sqrt(N.CreateChecked(6));

            public static N FromSurfaceArea<N>(N surfaceArea)
                where N : IRootFunctions<N>
                => N.Sqrt(surfaceArea / N.Sqrt(N.CreateChecked(3)));

            public static N FromVolume<N>(N volume)
                where N : IRootFunctions<N>
                => N.Cbrt(N.CreateChecked(6) * N.Sqrt(N.CreateChecked(2)) * volume);
        }

        /// <summary> Height of a regular tetrahedron, the distance between a vertex and the opposite face. </summary>
        public static class Height
        {
            public static N FromEdge<N>(N edgeLength)
                where N : IRootFunctions<N>
                => edgeLength * N.Sqrt(N.CreateChecked(2) / N.CreateChecked(3));
        }

        /// <summary> Circumradius of a regular tetrahedron, the distance between its center and a vertex. </summary>
        public static class Circumradius
        {
            public static N FromEdge<N>(N edgeLength)
                where N : IRootFunctions<N>
                => edgeLength * N.Sqrt(N.CreateChecked(6)) / N.CreateChecked(4);
        }

        /// <summary> Midradius of a regular tetrahedron, the distance between its center and the middle of an edge. </summary>
        public static class Midradius
        {
            public static N FromEdge<N>(N edgeLength)
                where N : IRootFunctions<N>
                => edgeLength / N.Sqrt(N.CreateChecked(8));
        }

        /// <summary> Inradius of a regular tetrahedron, the distance between its center and a face. </summary>
        public static class Inradius
        {
            public static N FromEdge<N>(N edgeLength)
                where N : IRootFunctions<N>
                => edgeLength * N.Sqrt(N.CreateChecked(6)) / N.CreateChecked(12);
        }

        /// <summary>Surface of a regular tetrahedron.</summary>
        public static class Surface
        {
            /// <summary>Surface area of a regular tetrahedron, four equilateral triangles.</summary>
            public static class Area
            {
                public static N FromEdge<N>(N edgeLength)
                    where N : IRootFunctions<N>
                    => N.Sqrt(N.CreateChecked(3)) * edgeLength * edgeLength;
            }
        }

        /// <summary>Region enclosed by a regular tetrahedron.</summary>
        public static class Region
        {
            /// <summary>Enclosed volume of a regular tetrahedron.</summary>
            public static class Volume
            {
                public static N FromEdge<N>(N edgeLength)
                    where N : IRootFunctions<N>
                    => edgeLength * edgeLength * edgeLength / (N.CreateChecked(6) * N.Sqrt(N.CreateChecked(2)));
            }
        }
    }
}
