using System.Numerics;
using System.Runtime.CompilerServices;

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

    /// <summary> Count of diagonals. </summary>
    public static N DiagonalCount<N>()
        where N : INumberBase<N>
        => N.CreateTruncating(2);

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
    /// repeatedly descending into quadrants of an axis-aligned rectangle centered at the origin (0, 0).
    /// <para>
    /// Quadrants are numbered 1..4 in the usual mathematical order, counter-clockwise starting
    /// from the upper right: 1 = top-right, 2 = top-left, 3 = bottom-left, 4 = bottom-right.
    /// Every number halves the rectangle in both dimensions and moves into the selected quadrant;
    /// each subsequent number selects a quadrant of the previous inner rectangle.
    /// </para>
    /// An empty sequence returns the original centered rectangle (origin at <c>-size / 2</c>).
    /// </summary>
    /// <param name="size">Size of the outer rectangle, centered at (0, 0).</param>
    /// <param name="quadrants">Quadrant numbers (1, 2, 3 or 4), one per nesting level.</param>
    /// <exception cref="ArgumentOutOfRangeException">A number is not in the range 1..4.</exception>
    /// <remarks>
    /// Reusable overload that accepts any sequence; it is enumerated exactly once.
    /// Prefer the <see cref="ReadOnlySpan{T}"/> overload on hot paths.
    /// </remarks>
    public static ((N X, N Y) Origin, (N X, N Y) Size) Quadrant<N>(
        (N X, N Y) size, IEnumerable<int> quadrants)
        where N : IFloatingPoint<N>
    {
        ArgumentNullException.ThrowIfNull(quadrants);

        var half = N.CreateTruncating(0.5);
        var scale = N.One;    // 2^-depth, ends up as the size scale
        var weightX = -half;  // origin.X == size.X * weightX (starts at the centered -size/2)
        var weightY = -half;

        foreach (var quadrant in quadrants)
            Descend(quadrant, half, ref scale, ref weightX, ref weightY);

        return Assemble(size, scale, weightX, weightY);
    }

    /// <summary>
    /// Allocation-free, span based counterpart of
    /// <see cref="Quadrant{N}(ValueTuple{N, N}, IEnumerable{int})"/> for hot paths;
    /// the quadrant numbers are read straight from the span.
    /// </summary>
    /// <param name="size">Size of the outer rectangle, centered at (0, 0).</param>
    /// <param name="quadrants">Quadrant numbers (1, 2, 3 or 4), one per nesting level.</param>
    /// <exception cref="ArgumentOutOfRangeException">A number is not in the range 1..4.</exception>
    public static ((N X, N Y) Origin, (N X, N Y) Size) Quadrant<N>(
        (N X, N Y) size, ReadOnlySpan<int> quadrants)
        where N : IFloatingPoint<N>
    {
        var half = N.CreateTruncating(0.5);
        var scale = N.One;
        var weightX = -half;
        var weightY = -half;

        foreach (var quadrant in quadrants)
            Descend(quadrant, half, ref scale, ref weightX, ref weightY);

        return Assemble(size, scale, weightX, weightY);
    }

    /// <summary>
    /// Accumulate one descent step as dimensionless weights: halve the running
    /// <paramref name="scale"/> (a power of two, never a division) and add it to the
    /// axis weights of the selected quadrant. The size is applied once in <see cref="Assemble"/>.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static void Descend<N>(int quadrant, N half, ref N scale, ref N weightX, ref N weightY)
        where N : IFloatingPoint<N>
    {
        scale *= half;
        switch (quadrant)
        {
            case 1: weightX += scale; weightY += scale; break; // top-right
            case 2: weightY += scale; break;                   // top-left
            case 3: break;                                     // bottom-left
            case 4: weightX += scale; break;                   // bottom-right
            default:
                throw new ArgumentOutOfRangeException(nameof(quadrant), quadrant,
                    "Quadrant number must be 1, 2, 3 or 4.");
        }
    }

    /// <summary> Scale the accumulated weights back to the outer rectangle's size. </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static ((N X, N Y) Origin, (N X, N Y) Size) Assemble<N>(
        (N X, N Y) size, N scale, N weightX, N weightY)
        where N : IFloatingPoint<N>
        => ((size.X * weightX, size.Y * weightY), (size.X * scale, size.Y * scale));

    /// <summary> Diagonal of a rectangle. </summary>
    public static class Diagonal
    {
        public static N FromEdges<N>(N a, N b)
            where N : IRootFunctions<N>
            => PT.Hypotenuse(a, b);
    }

    public static class Perimeter
    {
        public static N FromEdges<N>(N a, N b)
            where N : INumberBase<N>
            => N.CreateTruncating(2) * (a + b);

        public static class Length
        {
            public static N FromEdges<N>(N a, N b)
                where N : INumberBase<N>
                => Perimeter.FromEdges(a, b);
        }

        public static class Points
        {
            public static IEnumerable<(N X, N Y)> Indexes<N>(int count, N a, N b)
                where N : INumberBase<N>, IModulusOperators<N, N, N>, IComparisonOperators<N, N, bool>
            {
                if (count <= 0)
                    yield break;

                var perimeter = Perimeter.FromEdges(a, b);
                if (perimeter <= N.Zero)
                {
                    for (int i = 0; i < count; i++)
                        yield return (N.Zero, N.Zero);
                    yield break;
                }

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

            public static IEnumerable<(N X, N Y)> Indices<N>(int count, N a, N b)
                where N : INumberBase<N>, IModulusOperators<N, N, N>, IComparisonOperators<N, N, bool>
                => Indexes(count, a, b);
        }
    }

    /// <summary>
    /// Enclosed plane region of a rectangle.
    /// </summary>
    public static class Region
    {
        /// <summary>
        /// Enclosed plane region area of a rectangle.
        /// </summary>
        public static class Area
        {
            public static N FromEdges<N>(N a, N b)
                where N : IMultiplyOperators<N, N, N>
                => a * b;
        }
    }
}
