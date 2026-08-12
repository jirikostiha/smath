using System.Numerics;
using System.Runtime.CompilerServices;

namespace SMath.Geometry3D;

/// <summary>
/// Rectangular cuboid, the three dimensional counterpart of a rectangle.
/// </summary>
/// <remarks>
/// <a href="https://en.wikipedia.org/wiki/Cuboid">Wikipedia</a>
/// </remarks>
public static class Cuboid
{
    /// <summary> Count of vertices. </summary>
    public static N VertexCount<N>()
        where N : INumberBase<N>
        => N.CreateTruncating(8);

    /// <summary> Count of edges. </summary>
    public static N EdgeCount<N>()
        where N : INumberBase<N>
        => N.CreateTruncating(12);

    /// <summary> Count of faces. </summary>
    public static N FaceCount<N>()
        where N : INumberBase<N>
        => N.CreateTruncating(6);

    /// <summary> 90 degrees in rads. </summary>
    public static N DihedralAngle<N>()
        where N : ITrigonometricFunctions<N>
        => N.Pi / N.CreateTruncating(2);

    /// <summary> Schläfli symbol </summary>
    public const string SchlafliSymbol = "{} x {} x {}";

    /// <summary>
    /// Determine whether a point lies inside an axis-aligned cuboid
    /// defined by its origin (the corner with the lowest coordinates) and size. Faces are inclusive.
    /// </summary>
    public static bool Contains<N>((N X, N Y, N Z) origin, (N X, N Y, N Z) size, (N X, N Y, N Z) point)
        where N : INumber<N>
    {
        var maxX = origin.X + size.X;
        var maxY = origin.Y + size.Y;
        var maxZ = origin.Z + size.Z;

        return point.X >= origin.X && point.X <= maxX
            && point.Y >= origin.Y && point.Y <= maxY
            && point.Z >= origin.Z && point.Z <= maxZ;
    }

    /// <summary>
    /// Determine whether a point lies inside an axis-aligned cuboid
    /// defined by two opposite corners. Faces are inclusive and the corners may be given in any order.
    /// </summary>
    public static bool ContainsByCorners<N>((N X, N Y, N Z) corner1, (N X, N Y, N Z) corner2, (N X, N Y, N Z) point)
        where N : INumber<N>
    {
        var minX = N.Min(corner1.X, corner2.X);
        var maxX = N.Max(corner1.X, corner2.X);
        var minY = N.Min(corner1.Y, corner2.Y);
        var maxY = N.Max(corner1.Y, corner2.Y);
        var minZ = N.Min(corner1.Z, corner2.Z);
        var maxZ = N.Max(corner1.Z, corner2.Z);

        return point.X >= minX && point.X <= maxX
            && point.Y >= minY && point.Y <= maxY
            && point.Z >= minZ && point.Z <= maxZ;
    }

    public static (N X, N Y, N Z) Vertex1<N>()
        where N : IFloatingPoint<N>
        => (N.Zero, N.Zero, N.Zero);

    public static (N X, N Y, N Z) Vertex2<N>()
        where N : IFloatingPoint<N>
        => (N.One, N.Zero, N.Zero);

    public static (N X, N Y, N Z) Vertex3<N>()
        where N : IFloatingPoint<N>
        => (N.One, N.One, N.Zero);

    public static (N X, N Y, N Z) Vertex4<N>()
        where N : IFloatingPoint<N>
        => (N.Zero, N.One, N.Zero);

    public static (N X, N Y, N Z) Vertex5<N>()
        where N : IFloatingPoint<N>
        => (N.Zero, N.Zero, N.One);

    public static (N X, N Y, N Z) Vertex6<N>()
        where N : IFloatingPoint<N>
        => (N.One, N.Zero, N.One);

    public static (N X, N Y, N Z) Vertex7<N>()
        where N : IFloatingPoint<N>
        => (N.One, N.One, N.One);

    public static (N X, N Y, N Z) Vertex8<N>()
        where N : IFloatingPoint<N>
        => (N.Zero, N.One, N.One);

    public static (N X, N Y, N Z) Center<N>()
        where N : IFloatingPoint<N>
        => (N.CreateTruncating(0.5), N.CreateTruncating(0.5), N.CreateTruncating(0.5));

    public static (N X, N Y, N Z) Octant111Center<N>()
        where N : IFloatingPoint<N>
        => (N.CreateTruncating(0.25), N.CreateTruncating(0.25), N.CreateTruncating(0.25));

    public static (N X, N Y, N Z) Octant211Center<N>()
        where N : IFloatingPoint<N>
        => (N.CreateTruncating(0.75), N.CreateTruncating(0.25), N.CreateTruncating(0.25));

    public static (N X, N Y, N Z) Octant121Center<N>()
        where N : IFloatingPoint<N>
        => (N.CreateTruncating(0.25), N.CreateTruncating(0.75), N.CreateTruncating(0.25));

    public static (N X, N Y, N Z) Octant221Center<N>()
        where N : IFloatingPoint<N>
        => (N.CreateTruncating(0.75), N.CreateTruncating(0.75), N.CreateTruncating(0.25));

    public static (N X, N Y, N Z) Octant112Center<N>()
        where N : IFloatingPoint<N>
        => (N.CreateTruncating(0.25), N.CreateTruncating(0.25), N.CreateTruncating(0.75));

    public static (N X, N Y, N Z) Octant212Center<N>()
        where N : IFloatingPoint<N>
        => (N.CreateTruncating(0.75), N.CreateTruncating(0.25), N.CreateTruncating(0.75));

    public static (N X, N Y, N Z) Octant122Center<N>()
        where N : IFloatingPoint<N>
        => (N.CreateTruncating(0.25), N.CreateTruncating(0.75), N.CreateTruncating(0.75));

    public static (N X, N Y, N Z) Octant222Center<N>()
        where N : IFloatingPoint<N>
        => (N.CreateTruncating(0.75), N.CreateTruncating(0.75), N.CreateTruncating(0.75));

    /// <summary>
    /// Determine the origin (the corner with the lowest coordinates) and size of an inner cuboid
    /// obtained by repeatedly descending into octants of an axis-aligned cuboid centered at the
    /// origin (0, 0, 0).
    /// <para>
    /// Octants are numbered 1..8 in the usual mathematical order: 1..4 are the quadrants of the
    /// upper half (z &gt; 0) counter-clockwise starting from the upper right, 5..8 are the same
    /// quadrants of the lower half (z &lt; 0). Every number halves the cuboid in all three
    /// dimensions and moves into the selected octant; each subsequent number selects an octant
    /// of the previous inner cuboid.
    /// </para>
    /// An empty sequence returns the original centered cuboid (origin at <c>-size / 2</c>).
    /// </summary>
    /// <param name="size">Size of the outer cuboid, centered at (0, 0, 0).</param>
    /// <param name="octants">Octant numbers (1 to 8), one per nesting level.</param>
    /// <exception cref="ArgumentOutOfRangeException">A number is not in the range 1..8.</exception>
    /// <remarks>
    /// Reusable overload that accepts any sequence; it is enumerated exactly once.
    /// Prefer the <see cref="ReadOnlySpan{T}"/> overload on hot paths.
    /// </remarks>
    public static ((N X, N Y, N Z) Origin, (N X, N Y, N Z) Size) Octant<N>(
        (N X, N Y, N Z) size, IEnumerable<int> octants)
        where N : IFloatingPoint<N>
    {
        ArgumentNullException.ThrowIfNull(octants);

        var half = N.CreateTruncating(0.5);
        var scale = N.One;    // 2^-depth, ends up as the size scale
        var weightX = -half;  // origin.X == size.X * weightX (starts at the centered -size/2)
        var weightY = -half;
        var weightZ = -half;

        foreach (var octant in octants)
            Descend(octant, half, ref scale, ref weightX, ref weightY, ref weightZ);

        return Assemble(size, scale, weightX, weightY, weightZ);
    }

    /// <summary>
    /// Allocation-free, span based counterpart of
    /// <see cref="Octant{N}(ValueTuple{N, N, N}, IEnumerable{int})"/> for hot paths;
    /// the octant numbers are read straight from the span.
    /// </summary>
    /// <param name="size">Size of the outer cuboid, centered at (0, 0, 0).</param>
    /// <param name="octants">Octant numbers (1 to 8), one per nesting level.</param>
    /// <exception cref="ArgumentOutOfRangeException">A number is not in the range 1..8.</exception>
    public static ((N X, N Y, N Z) Origin, (N X, N Y, N Z) Size) Octant<N>(
        (N X, N Y, N Z) size, ReadOnlySpan<int> octants)
        where N : IFloatingPoint<N>
    {
        var half = N.CreateTruncating(0.5);
        var scale = N.One;
        var weightX = -half;
        var weightY = -half;
        var weightZ = -half;

        foreach (var octant in octants)
            Descend(octant, half, ref scale, ref weightX, ref weightY, ref weightZ);

        return Assemble(size, scale, weightX, weightY, weightZ);
    }

    /// <summary>
    /// Accumulate one descent step as dimensionless weights: halve the running
    /// <paramref name="scale"/> (a power of two, never a division) and add it to the
    /// axis weights of the selected octant. The size is applied once in <see cref="Assemble"/>.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static void Descend<N>(int octant, N half, ref N scale, ref N weightX, ref N weightY, ref N weightZ)
        where N : IFloatingPoint<N>
    {
        scale *= half;
        switch (octant)
        {
            case 1: weightX += scale; weightY += scale; weightZ += scale; break; // top, front-right
            case 2: weightY += scale; weightZ += scale; break;                   // top, front-left
            case 3: weightZ += scale; break;                                     // top, back-left
            case 4: weightX += scale; weightZ += scale; break;                   // top, back-right
            case 5: weightX += scale; weightY += scale; break;                   // bottom, front-right
            case 6: weightY += scale; break;                                     // bottom, front-left
            case 7: break;                                                       // bottom, back-left
            case 8: weightX += scale; break;                                     // bottom, back-right
            default:
                throw new ArgumentOutOfRangeException(nameof(octant), octant,
                    "Octant number must be in the range 1 to 8.");
        }
    }

    /// <summary> Scale the accumulated weights back to the outer cuboid's size. </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static ((N X, N Y, N Z) Origin, (N X, N Y, N Z) Size) Assemble<N>(
        (N X, N Y, N Z) size, N scale, N weightX, N weightY, N weightZ)
        where N : IFloatingPoint<N>
        => ((size.X * weightX, size.Y * weightY, size.Z * weightZ),
            (size.X * scale, size.Y * scale, size.Z * scale));

    /// <summary> Space diagonal of a cuboid. </summary>
    public static class Diagonal
    {
        public static N FromEdges<N>(N a, N b, N c)
            where N : IRootFunctions<N>
            => PT.Hypotenuse(a, b, c);
    }

    /// <summary>
    /// Surface of a cuboid.
    /// </summary>
    public static class Surface
    {
        /// <summary>
        /// Surface area of a cuboid.
        /// </summary>
        public static class Area
        {
            public static N FromEdges<N>(N a, N b, N c)
                where N : INumberBase<N>
                => N.CreateTruncating(2) * ((a * b) + (b * c) + (a * c));
        }
    }

    /// <summary>
    /// Region enclosed by a cuboid.
    /// </summary>
    public static class Region
    {
        /// <summary>
        /// Enclosed volume of a cuboid.
        /// </summary>
        public static class Volume
        {
            public static N FromEdges<N>(N a, N b, N c)
                where N : INumberBase<N>
                => a * b * c;
        }
    }
}
