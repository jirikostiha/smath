using System.Numerics;

namespace SMath.Geometry2D;

public static class Rectangle
{
    /// <summary> Count of vertices. </summary>
    public static N VertexCount<N>()
        where N : INumberBase<N>
        => N.CreateTruncating(4);

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

    /// <summary>
    /// Determine whether a point lies inside an axis-aligned rectangle
    /// defined by its origin (bottom-left corner) and size. Edges are inclusive.
    /// </summary>
    public static bool Contains<N>((N X, N Y) origin, (N X, N Y) size, (N X, N Y) point)
        where N : INumber<N>
    {
        var maxX = origin.X + size.X;
        var maxY = origin.Y + size.Y;

        return point.X >= origin.X && point.X <= maxX
            && point.Y >= origin.Y && point.Y <= maxY;
    }

    /// <summary>
    /// Determine whether a point lies inside an axis-aligned rectangle
    /// defined by two opposite corners. Edges are inclusive and the corners may be given in any order.
    /// </summary>
    public static bool ContainsByCorners<N>((N X, N Y) corner1, (N X, N Y) corner2, (N X, N Y) point)
        where N : INumber<N>
    {
        var minX = N.Min(corner1.X, corner2.X);
        var maxX = N.Max(corner1.X, corner2.X);
        var minY = N.Min(corner1.Y, corner2.Y);
        var maxY = N.Max(corner1.Y, corner2.Y);

        return point.X >= minX && point.X <= maxX
            && point.Y >= minY && point.Y <= maxY;
    }

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

    /// <summary>
    /// Determine the origin (bottom-left corner) and size of an inner rectangle obtained by
    /// repeatedly descending into quadrants of an axis-aligned rectangle.
    /// <para>
    /// Each quadrant is identified by a two-digit code <c>XY</c> where <c>X</c> is the column
    /// (1 = left, 2 = right) and <c>Y</c> is the row (1 = bottom, 2 = top), matching
    /// <see cref="Quadrant11Center{N}"/>, <see cref="Quadrant21Center{N}"/>,
    /// <see cref="Quadrant12Center{N}"/> and <see cref="Quadrant22Center{N}"/>.
    /// Every code halves the rectangle in both dimensions and moves the origin into the
    /// selected quadrant; each subsequent code selects a quadrant of the previous inner rectangle.
    /// </para>
    /// An empty sequence returns the original rectangle unchanged.
    /// </summary>
    /// <param name="origin">Origin (bottom-left corner) of the outer rectangle.</param>
    /// <param name="size">Size of the outer rectangle.</param>
    /// <param name="quadrants">Quadrant codes (11, 21, 12 or 22), one per nesting level.</param>
    /// <exception cref="ArgumentOutOfRangeException">A code is not one of 11, 21, 12 or 22.</exception>
    public static ((N X, N Y) Origin, (N X, N Y) Size) Quadrant<N>(
        (N X, N Y) origin, (N X, N Y) size, params int[] quadrants)
        where N : INumber<N>
    {
        ArgumentNullException.ThrowIfNull(quadrants);

        var two = N.CreateTruncating(2);
        var currentOrigin = origin;
        var currentSize = size;

        foreach (var quadrant in quadrants)
        {
            var column = quadrant / 10;
            var row = quadrant % 10;

            if (column is < 1 or > 2 || row is < 1 or > 2)
                throw new ArgumentOutOfRangeException(nameof(quadrants),
                    quadrant, "Quadrant code must be one of 11, 21, 12 or 22.");

            currentSize = (currentSize.X / two, currentSize.Y / two);
            currentOrigin = (
                currentOrigin.X + N.CreateTruncating(column - 1) * currentSize.X,
                currentOrigin.Y + N.CreateTruncating(row - 1) * currentSize.Y);
        }

        return (currentOrigin, currentSize);
    }

    public static class Perimeter
    {
        public static N FromEdges<N>(N a, N b)
            where N : INumberBase<N>
            => N.CreateTruncating(2) * (a + b);

        public static class Points
        {
            public static IEnumerable<(N X, N Y)> Indexes<N>(int count, N a, N b)
                where N : INumberBase<N>, IModulusOperators<N, N, N>, IComparisonOperators<N, N, bool>
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
