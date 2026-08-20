using System.Numerics;

namespace SMath.Geometry2D;

/// <summary>
/// Polygon, a plane figure bounded by a closed polyline.
/// Vertices are enumerated in order along the boundary, the closing edge from the last
/// vertex back to the first one is implicit.
/// </summary>
/// <remarks>
/// <a href="https://en.wikipedia.org/wiki/Polygon">Wikipedia</a>
/// <a href="https://mathworld.wolfram.com/Polygon.html">Wolfram Mathworld</a>
/// </remarks>
public static class Polygon
{
    /// <summary> Count of edges of a polygon with the given count of vertices. </summary>
    public static N EdgeCount<N>(N vertices)
        where N : INumberBase<N>
        => vertices;

    /// <summary> Count of vertices of a polygon with the given count of edges. </summary>
    public static N VertexCount<N>(N edges)
        where N : INumberBase<N>
        => edges;

    /// <summary> Count of diagonals of a convex polygon. </summary>
    public static N DiagonalCount<N>(N vertices)
        where N : INumberBase<N>
        => vertices * (vertices - N.CreateChecked(3)) / N.CreateChecked(2);

    /// <summary> Sum of internal angles of a simple polygon in rads. </summary>
    public static N InternalAngleSum<N>(N edges)
        where N : IFloatingPointConstants<N>
        => (edges - N.CreateChecked(2)) * N.Pi;

    /// <summary> Sum of external angles of a simple polygon in rads. </summary>
    public static N ExternalAngleSum<N>()
        where N : IFloatingPointConstants<N>
        => N.CreateChecked(2) * N.Pi;

    /// <summary>
    /// Determine whether a point lies inside a simple polygon by the even-odd rule.
    /// </summary>
    /// <param name="vertices"> Vertices in order along the boundary. </param>
    /// <param name="point"> Tested point. </param>
    /// <remarks>
    /// A point exactly on the boundary may be reported either way, the rule is exact for
    /// the interior and the exterior only.
    /// Reusable overload that accepts any sequence; it is enumerated exactly once.
    /// Prefer the <see cref="ReadOnlySpan{T}"/> overload on hot paths.
    /// </remarks>
    public static bool Contains<N>(IEnumerable<(N X, N Y)> vertices, (N X, N Y) point)
        where N : INumber<N>
    {
        ArgumentNullException.ThrowIfNull(vertices);

        var inside = false;
        var first = default((N X, N Y));
        var previous = default((N X, N Y));
        var hasVertex = false;

        foreach (var vertex in vertices)
        {
            if (!hasVertex)
            {
                first = vertex;
                hasVertex = true;
            }
            else
                inside ^= CrossesRay(previous, vertex, point);

            previous = vertex;
        }

        return hasVertex && (inside ^ CrossesRay(previous, first, point));
    }

    /// <summary>
    /// Allocation-free, span based counterpart of
    /// <see cref="Contains{N}(IEnumerable{ValueTuple{N, N}}, ValueTuple{N, N})"/> for hot paths.
    /// </summary>
    /// <param name="vertices"> Vertices in order along the boundary. </param>
    /// <param name="point"> Tested point. </param>
    public static bool Contains<N>(ReadOnlySpan<(N X, N Y)> vertices, (N X, N Y) point)
        where N : INumber<N>
    {
        var inside = false;

        for (int i = 0, j = vertices.Length - 1; i < vertices.Length; j = i++)
            inside ^= CrossesRay(vertices[j], vertices[i], point);

        return inside;
    }

    /// <summary>
    /// Determine whether the edge from <paramref name="from"/> to <paramref name="to"/> is crossed
    /// by the ray cast from the point towards +x.
    /// </summary>
    private static bool CrossesRay<N>((N X, N Y) from, (N X, N Y) to, (N X, N Y) point)
        where N : INumber<N>
        => (from.Y > point.Y) != (to.Y > point.Y)
            && point.X < (from.X - to.X) * (point.Y - to.Y) / (from.Y - to.Y) + to.X;

    /// <summary>
    /// Perimeter or outline of a polygon.
    /// </summary>
    public static class Perimeter
    {
        /// <summary>
        /// Length of the perimeter of a polygon.
        /// </summary>
        public static class Length
        {
            /// <summary>
            /// Length of the perimeter of a polygon, the sum of the lengths of all its edges.
            /// </summary>
            /// <param name="vertices"> Vertices in order along the boundary. </param>
            /// <remarks>
            /// Reusable overload that accepts any sequence; it is enumerated exactly once.
            /// Prefer the <see cref="ReadOnlySpan{T}"/> overload on hot paths.
            /// </remarks>
            public static N FromVertices<N>(IEnumerable<(N X, N Y)> vertices)
                where N : IRootFunctions<N>
            {
                ArgumentNullException.ThrowIfNull(vertices);

                var length = N.Zero;
                var first = default((N X, N Y));
                var previous = default((N X, N Y));
                var hasVertex = false;

                foreach (var vertex in vertices)
                {
                    if (!hasVertex)
                    {
                        first = vertex;
                        hasVertex = true;
                    }
                    else
                        length += Point2.Distance(previous, vertex);

                    previous = vertex;
                }

                return hasVertex
                    ? length + Point2.Distance(previous, first)
                    : N.Zero;
            }

            /// <summary>
            /// Allocation-free, span based counterpart of
            /// <see cref="FromVertices{N}(IEnumerable{ValueTuple{N, N}})"/> for hot paths.
            /// </summary>
            /// <param name="vertices"> Vertices in order along the boundary. </param>
            public static N FromVertices<N>(ReadOnlySpan<(N X, N Y)> vertices)
                where N : IRootFunctions<N>
            {
                var length = N.Zero;

                for (int i = 0, j = vertices.Length - 1; i < vertices.Length; j = i++)
                    length += Point2.Distance(vertices[j], vertices[i]);

                return length;
            }
        }
    }

    /// <summary>
    /// Enclosed plane region of a polygon.
    /// </summary>
    public static class Region
    {
        /// <summary>
        /// Enclosed plane region area of a polygon.
        /// </summary>
        public static class Area
        {
            /// <summary>
            /// Area of a simple polygon by the shoelace formula. The vertices may be given in
            /// either orientation, the area is never negative.
            /// </summary>
            /// <param name="vertices"> Vertices in order along the boundary. </param>
            /// <remarks>
            /// <a href="https://en.wikipedia.org/wiki/Shoelace_formula">Wikipedia</a>
            /// Reusable overload that accepts any sequence; it is enumerated exactly once.
            /// Prefer the <see cref="ReadOnlySpan{T}"/> overload on hot paths.
            /// </remarks>
            public static N FromVertices<N>(IEnumerable<(N X, N Y)> vertices)
                where N : INumberBase<N>
                => N.Abs(SignedFromVertices(vertices));

            /// <summary>
            /// Allocation-free, span based counterpart of
            /// <see cref="FromVertices{N}(IEnumerable{ValueTuple{N, N}})"/> for hot paths.
            /// </summary>
            /// <param name="vertices"> Vertices in order along the boundary. </param>
            public static N FromVertices<N>(ReadOnlySpan<(N X, N Y)> vertices)
                where N : INumberBase<N>
                => N.Abs(SignedFromVertices(vertices));

            /// <summary>
            /// Signed area of a simple polygon by the shoelace formula. It is positive for
            /// counter-clockwise and negative for clockwise oriented vertices.
            /// </summary>
            /// <param name="vertices"> Vertices in order along the boundary. </param>
            /// <remarks>
            /// Reusable overload that accepts any sequence; it is enumerated exactly once.
            /// Prefer the <see cref="ReadOnlySpan{T}"/> overload on hot paths.
            /// </remarks>
            public static N SignedFromVertices<N>(IEnumerable<(N X, N Y)> vertices)
                where N : INumberBase<N>
            {
                ArgumentNullException.ThrowIfNull(vertices);

                var doubleArea = N.Zero;
                var first = default((N X, N Y));
                var previous = default((N X, N Y));
                var hasVertex = false;

                foreach (var vertex in vertices)
                {
                    if (!hasVertex)
                    {
                        first = vertex;
                        hasVertex = true;
                    }
                    else
                        doubleArea += CrossProduct(previous, vertex);

                    previous = vertex;
                }

                return hasVertex
                    ? (doubleArea + CrossProduct(previous, first)) / N.CreateChecked(2)
                    : N.Zero;
            }

            /// <summary>
            /// Allocation-free, span based counterpart of
            /// <see cref="SignedFromVertices{N}(IEnumerable{ValueTuple{N, N}})"/> for hot paths.
            /// </summary>
            /// <param name="vertices"> Vertices in order along the boundary. </param>
            public static N SignedFromVertices<N>(ReadOnlySpan<(N X, N Y)> vertices)
                where N : INumberBase<N>
            {
                var doubleArea = N.Zero;

                for (int i = 0, j = vertices.Length - 1; i < vertices.Length; j = i++)
                    doubleArea += CrossProduct(vertices[j], vertices[i]);

                return doubleArea / N.CreateChecked(2);
            }
        }

        /// <summary>
        /// Centroid of the plane region enclosed by a polygon.
        /// </summary>
        /// <remarks>
        /// <a href="https://en.wikipedia.org/wiki/Centroid#Of_a_polygon">Wikipedia</a>
        /// </remarks>
        public static class Centroid
        {
            /// <summary>
            /// Centroid of a simple polygon. A degenerate polygon of zero area falls back to the
            /// arithmetic mean of its vertices.
            /// </summary>
            /// <param name="vertices"> Vertices in order along the boundary. </param>
            /// <exception cref="ArgumentException"> No vertex is given. </exception>
            /// <remarks>
            /// Reusable overload that accepts any sequence; it is enumerated exactly once.
            /// Prefer the <see cref="ReadOnlySpan{T}"/> overload on hot paths.
            /// </remarks>
            public static (N X, N Y) FromVertices<N>(IEnumerable<(N X, N Y)> vertices)
                where N : INumberBase<N>
            {
                ArgumentNullException.ThrowIfNull(vertices);

                var accumulator = new CentroidAccumulator<N>();
                var first = default((N X, N Y));
                var previous = default((N X, N Y));
                var hasVertex = false;

                foreach (var vertex in vertices)
                {
                    if (!hasVertex)
                    {
                        first = vertex;
                        hasVertex = true;
                    }
                    else
                        accumulator.Add(previous, vertex);

                    accumulator.Count(vertex);
                    previous = vertex;
                }

                if (!hasVertex)
                    throw new ArgumentException("Polygon must have at least one vertex.", nameof(vertices));

                accumulator.Add(previous, first);

                return accumulator.Result();
            }

            /// <summary>
            /// Allocation-free, span based counterpart of
            /// <see cref="FromVertices{N}(IEnumerable{ValueTuple{N, N}})"/> for hot paths.
            /// </summary>
            /// <param name="vertices"> Vertices in order along the boundary. </param>
            /// <exception cref="ArgumentException"> No vertex is given. </exception>
            public static (N X, N Y) FromVertices<N>(ReadOnlySpan<(N X, N Y)> vertices)
                where N : INumberBase<N>
            {
                if (vertices.IsEmpty)
                    throw new ArgumentException("Polygon must have at least one vertex.", nameof(vertices));

                var accumulator = new CentroidAccumulator<N>();

                for (int i = 0, j = vertices.Length - 1; i < vertices.Length; j = i++)
                {
                    accumulator.Add(vertices[j], vertices[i]);
                    accumulator.Count(vertices[i]);
                }

                return accumulator.Result();
            }
        }
    }

    /// <summary> Cross product of two points taken as vectors from the origin. </summary>
    private static N CrossProduct<N>((N X, N Y) point1, (N X, N Y) point2)
        where N : INumberBase<N>
        => (point1.X * point2.Y) - (point2.X * point1.Y);

    /// <summary>
    /// Running sums of the polygon centroid, kept in one place for the enumerable and the span overload.
    /// </summary>
    private struct CentroidAccumulator<N>
        where N : INumberBase<N>
    {
        private N _doubleArea = N.Zero;
        private N _weightedX = N.Zero;
        private N _weightedY = N.Zero;
        private N _sumX = N.Zero;
        private N _sumY = N.Zero;
        private N _vertexCount = N.Zero;

        public CentroidAccumulator()
        {
        }

        /// <summary> Accumulate one edge of the polygon. </summary>
        public void Add((N X, N Y) from, (N X, N Y) to)
        {
            var cross = CrossProduct(from, to);
            _doubleArea += cross;
            _weightedX += (from.X + to.X) * cross;
            _weightedY += (from.Y + to.Y) * cross;
        }

        /// <summary> Accumulate one vertex of the polygon, used by the degenerate fallback. </summary>
        public void Count((N X, N Y) vertex)
        {
            _sumX += vertex.X;
            _sumY += vertex.Y;
            _vertexCount += N.One;
        }

        /// <summary> Centroid of the accumulated polygon. </summary>
        public readonly (N X, N Y) Result()
        {
            if (N.IsZero(_doubleArea))
                return (_sumX / _vertexCount, _sumY / _vertexCount);

            var scale = N.CreateChecked(3) * _doubleArea;
            return (_weightedX / scale, _weightedY / scale);
        }
    }

    /// <summary>
    /// Regular polygon, equilateral and equiangular, centered in the origin.
    /// </summary>
    /// <remarks>
    /// <a href="https://en.wikipedia.org/wiki/Regular_polygon">Wikipedia</a>
    /// </remarks>
    public static class Regular
    {
        /// <summary> Schläfli symbol of a regular polygon with the given count of edges. </summary>
        public static string SchlafliSymbol(int edges)
            => FormattableString.Invariant($"{{{edges}}}");

        /// <summary> Internal angle of a regular polygon in rads. </summary>
        public static N InternalAngle<N>(N edges)
            where N : IFloatingPointConstants<N>
            => InternalAngleSum(edges) / edges;

        /// <summary> External angle of a regular polygon in rads. </summary>
        public static N ExternalAngle<N>(N edges)
            where N : IFloatingPointConstants<N>
            => ExternalAngleSum<N>() / edges;

        /// <summary> Central angle of a regular polygon in rads, the angle an edge subtends in the center. </summary>
        public static N CentralAngle<N>(N edges)
            where N : IFloatingPointConstants<N>
            => ExternalAngleSum<N>() / edges;

        /// <summary> Edge of a regular polygon. </summary>
        public static class Edge
        {
            /// <summary> Length of an edge of a regular polygon from its circumradius. </summary>
            public static N FromCircumradius<N>(N edges, N circumradius)
                where N : ITrigonometricFunctions<N>
                => N.CreateChecked(2) * circumradius * N.Sin(N.Pi / edges);

            /// <summary> Length of an edge of a regular polygon from its inradius. </summary>
            public static N FromInradius<N>(N edges, N inradius)
                where N : ITrigonometricFunctions<N>
                => N.CreateChecked(2) * inradius * N.Tan(N.Pi / edges);
        }

        /// <summary> Circumradius of a regular polygon, the distance between its center and a vertex. </summary>
        public static class Circumradius
        {
            public static N FromEdge<N>(N edges, N edgeLength)
                where N : ITrigonometricFunctions<N>
                => edgeLength / (N.CreateChecked(2) * N.Sin(N.Pi / edges));

            public static N FromInradius<N>(N edges, N inradius)
                where N : ITrigonometricFunctions<N>
                => inradius / N.Cos(N.Pi / edges);
        }

        /// <summary> Inradius or apothem of a regular polygon, the distance between its center and an edge. </summary>
        public static class Inradius
        {
            public static N FromEdge<N>(N edges, N edgeLength)
                where N : ITrigonometricFunctions<N>
                => edgeLength / (N.CreateChecked(2) * N.Tan(N.Pi / edges));

            public static N FromCircumradius<N>(N edges, N circumradius)
                where N : ITrigonometricFunctions<N>
                => circumradius * N.Cos(N.Pi / edges);
        }

        /// <summary> Vertices of a regular polygon. </summary>
        public static class Vertices
        {
            /// <summary>
            /// Enumerating the vertices of a regular polygon centered in the origin from the +x axis
            /// towards the +y axis and beyond.
            /// </summary>
            public static IEnumerable<(N X, N Y)> FromCircumradius<N>(N circumradius, int count)
                where N : IFloatingPoint<N>, ITrigonometricFunctions<N>
                => Circle.Perimeter.Points.FromRadius(circumradius, count);
        }

        /// <summary>
        /// Perimeter or outline of a regular polygon.
        /// </summary>
        public static class Perimeter
        {
            /// <summary>
            /// Length of the perimeter of a regular polygon.
            /// </summary>
            public static class Length
            {
                public static N FromEdge<N>(N edges, N edgeLength)
                    where N : INumberBase<N>
                    => edges * edgeLength;

                public static N FromCircumradius<N>(N edges, N circumradius)
                    where N : ITrigonometricFunctions<N>
                    => edges * Edge.FromCircumradius(edges, circumradius);

                public static N FromInradius<N>(N edges, N inradius)
                    where N : ITrigonometricFunctions<N>
                    => edges * Edge.FromInradius(edges, inradius);
            }
        }

        /// <summary>
        /// Enclosed plane region of a regular polygon.
        /// </summary>
        public static class Region
        {
            /// <summary>
            /// Enclosed plane region area of a regular polygon.
            /// </summary>
            public static class Area
            {
                public static N FromEdge<N>(N edges, N edgeLength)
                    where N : ITrigonometricFunctions<N>
                    => edges * edgeLength * edgeLength / (N.CreateChecked(4) * N.Tan(N.Pi / edges));

                public static N FromCircumradius<N>(N edges, N circumradius)
                    where N : ITrigonometricFunctions<N>
                    => edges * circumradius * circumradius
                        * N.Sin(N.CreateChecked(2) * N.Pi / edges) / N.CreateChecked(2);

                public static N FromInradius<N>(N edges, N inradius)
                    where N : ITrigonometricFunctions<N>
                    => edges * inradius * inradius * N.Tan(N.Pi / edges);

                /// <summary> Area of a regular polygon from its inradius and the length of its perimeter. </summary>
                public static N FromInradiusAndPerimeter<N>(N inradius, N perimeter)
                    where N : INumberBase<N>
                    => inradius * perimeter / N.CreateChecked(2);
            }
        }
    }
}
