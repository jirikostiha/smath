using System.Numerics;

namespace SMath.Geometry2D;

public static class Rectangle
{
    /// <summary> Count of vertices. </summary>
    public static N VertexCount<N>()
        where N : INumberBase<N>
        =>  N.CreateTruncating(4);

    /// <summary> Count of edges. </summary>
    public static N EdgeCount<N>()
        where N : INumberBase<N>
        => N.CreateTruncating(4);

    /// <summary> 90 degrees in rads. </summary>
    public static N InternalAngle<N>()
        where N : ITrigonometricFunctions<N>
        => N.Pi / N.CreateTruncating(2);

    /// <summary> Schläfli symbol </summary>
    public const string SchlafliSymbol = "{} x {}";

    public static (N X, N Y) Vertex1<N>()
        where N : IFloatingPoint<N>
        => (N.Zero, N.Zero);

    public static (N X, N Y) Vertex2<N>()
        where N : IFloatingPoint<N>
        => (N.One, N.Zero);

    public static (N X, N Y) Vertex3<N>()
        where N : IFloatingPoint<N>
        => (N.One, N.One);

    public static (N X, N Y) Vertex4<N>()
        where N : IFloatingPoint<N>
        => (N.Zero, N.One);

    public static (N X, N Y) Center<N>()
        where N : IFloatingPoint<N>
        => (N.CreateTruncating(0.5), N.CreateTruncating(0.5));

    public static (N X, N Y) Quadrant11Center<N>()
        where N : IFloatingPoint<N>
        => (N.CreateTruncating(0.25), N.CreateTruncating(0.25));

    public static (N X, N Y) Quadrant21Center<N>()
        where N : IFloatingPoint<N>
        => (N.CreateTruncating(0.75), N.CreateTruncating(0.25));

    public static (N X, N Y) Quadrant12Center<N>()
        where N : IFloatingPoint<N>
        => (N.CreateTruncating(0.25), N.CreateTruncating(0.75));

    public static (N X, N Y) Quadrant22Center<N>()
        where N : IFloatingPoint<N>
        => (N.CreateTruncating(0.75), N.CreateTruncating(0.75));

    public static class Perimeter
    {
        public static N FromEdges<N>(N a, N b)
            where N : INumberBase<N>
            => N.CreateTruncating(2) * (a + b);

        public static class Points
        {
            public static IEnumerable<(N X, N Y)> Indexes<N>(int count, N a, N b)
                where N : INumberBase<N>, IModulusOperators<N,N,N>, IComparisonOperators<N,N,bool>
            {
                var perimeter = Perimeter.FromEdges(a, b);
                var segmentIndexes = Line.Segment.Indices(count, perimeter);

                foreach (var index in segmentIndexes)
                {
                    var pos = index % perimeter;

                    if (pos < a)
                        yield return (pos, N.Zero); // bottom edge
                    else if (pos < a + b)
                        yield return (a, pos - a); // right edge
                    else if (pos < N.CreateTruncating(2) * a + b)
                        yield return (N.CreateTruncating(2) * a + b - pos, b); // top edge
                    else
                        yield return (N.Zero, perimeter - pos); // left edge
                }
            }
        }
    }
}
