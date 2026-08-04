using System.Runtime.CompilerServices;
using SMath.Geometry2D;

namespace SMath.Grid2D;

/// <summary>
/// Uniform two dimensional grid of cells mapped onto a row-major one dimensional buffer.
/// </summary>
/// <remarks>
/// Cell (0,0) is the first cell of the buffer, cell index = y * width + x.
/// All members are allocation free unless they return an <see cref="IEnumerable{T}"/>.
/// </remarks>
public static class Grid
{
    /// <summary> Count of cells of a grid. </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int CellCount(int width, int height)
        => width * height;

    /// <summary> Buffer index of a cell. </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int Index(int x, int y, int width)
        => (y * width) + x;

    /// <summary> Buffer index of a cell. </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int Index((int X, int Y) cell, int width)
        => (cell.Y * width) + cell.X;

    /// <summary> Cell of a buffer index. </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static (int X, int Y) Cell(int index, int width)
        => (index % width, index / width);

    /// <summary> X coordinate of a buffer index. </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int X(int index, int width)
        => index % width;

    /// <summary> Y coordinate of a buffer index. </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int Y(int index, int width)
        => index / width;

    /// <summary> Determine if a cell lies inside of a grid. </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool Contains(int x, int y, int width, int height)
        => (uint)x < (uint)width && (uint)y < (uint)height;

    /// <summary> Determine if a cell lies inside of a grid. </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool Contains((int X, int Y) cell, int width, int height)
        => (uint)cell.X < (uint)width && (uint)cell.Y < (uint)height;

    /// <summary> Determine if a buffer index lies inside of a grid. </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool ContainsIndex(int index, int width, int height)
        => (uint)index < (uint)(width * height);

    /// <summary> Wrap a coordinate into range [0, length) as on a torus shaped map. </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int Wrap(int value, int length)
    {
        var rest = value % length;
        return rest < 0 ? rest + length : rest;
    }

    /// <summary> Wrap a cell into a grid as on a torus shaped map. </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static (int X, int Y) Wrap((int X, int Y) cell, int width, int height)
        => (Wrap(cell.X, width), Wrap(cell.Y, height));

    /// <summary> Clamp a cell to the closest cell of a grid. </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static (int X, int Y) Clamp((int X, int Y) cell, int width, int height)
        => (Math.Clamp(cell.X, 0, width - 1), Math.Clamp(cell.Y, 0, height - 1));

    /// <summary>
    /// Write buffer indexes of the existing neighbors in axes directions into a buffer.
    /// Order is right, up, left, down.
    /// </summary>
    /// <returns> Count of written neighbors. Maximum is 4. </returns>
    public static int Neighbors4(int x, int y, int width, int height, Span<int> neighbors)
    {
        var index = (y * width) + x;
        var count = 0;

        if (x + 1 < width)
            neighbors[count++] = index + 1;
        if (y + 1 < height)
            neighbors[count++] = index + width;
        if (x > 0)
            neighbors[count++] = index - 1;
        if (y > 0)
            neighbors[count++] = index - width;

        return count;
    }

    /// <summary>
    /// Write buffer indexes of the existing neighbors in axes directions into a buffer.
    /// Order is right, up, left, down.
    /// </summary>
    /// <returns> Count of written neighbors. Maximum is 4. </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int Neighbors4(int index, int width, int height, Span<int> neighbors)
        => Neighbors4(index % width, index / width, width, height, neighbors);

    /// <summary>
    /// Write buffer indexes of the existing neighbors in axes and diagonal directions into a buffer.
    /// Axial neighbors are written first.
    /// </summary>
    /// <returns> Count of written neighbors. Maximum is 8. </returns>
    public static int Neighbors8(int x, int y, int width, int height, Span<int> neighbors)
    {
        var count = Neighbors4(x, y, width, height, neighbors);

        var index = (y * width) + x;
        var hasRight = x + 1 < width;
        var hasLeft = x > 0;
        var hasUp = y + 1 < height;
        var hasDown = y > 0;

        if (hasRight && hasUp)
            neighbors[count++] = index + width + 1;
        if (hasLeft && hasUp)
            neighbors[count++] = index + width - 1;
        if (hasLeft && hasDown)
            neighbors[count++] = index - width - 1;
        if (hasRight && hasDown)
            neighbors[count++] = index - width + 1;

        return count;
    }

    /// <summary>
    /// Write buffer indexes of the existing neighbors in axes and diagonal directions into a buffer.
    /// Axial neighbors are written first.
    /// </summary>
    /// <returns> Count of written neighbors. Maximum is 8. </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int Neighbors8(int index, int width, int height, Span<int> neighbors)
        => Neighbors8(index % width, index / width, width, height, neighbors);

    /// <summary>
    /// Write the existing neighbors in axes directions into a buffer.
    /// Order is right, up, left, down.
    /// </summary>
    /// <returns> Count of written neighbors. Maximum is 4. </returns>
    public static int Neighbors4(int x, int y, int width, int height, Span<(int X, int Y)> neighbors)
    {
        var count = 0;

        if (x + 1 < width)
            neighbors[count++] = (x + 1, y);
        if (y + 1 < height)
            neighbors[count++] = (x, y + 1);
        if (x > 0)
            neighbors[count++] = (x - 1, y);
        if (y > 0)
            neighbors[count++] = (x, y - 1);

        return count;
    }

    /// <summary>
    /// Write the existing neighbors in axes and diagonal directions into a buffer.
    /// Axial neighbors are written first.
    /// </summary>
    /// <returns> Count of written neighbors. Maximum is 8. </returns>
    public static int Neighbors8(int x, int y, int width, int height, Span<(int X, int Y)> neighbors)
    {
        var count = Neighbors4(x, y, width, height, neighbors);

        var hasRight = x + 1 < width;
        var hasLeft = x > 0;
        var hasUp = y + 1 < height;
        var hasDown = y > 0;

        if (hasRight && hasUp)
            neighbors[count++] = (x + 1, y + 1);
        if (hasLeft && hasUp)
            neighbors[count++] = (x - 1, y + 1);
        if (hasLeft && hasDown)
            neighbors[count++] = (x - 1, y - 1);
        if (hasRight && hasDown)
            neighbors[count++] = (x + 1, y - 1);

        return count;
    }

    /// <summary> All cells of a grid in row-major order. </summary>
    public static IEnumerable<(int X, int Y)> Cells(int width, int height)
    {
        for (var y = 0; y < height; y++)
            for (var x = 0; x < width; x++)
                yield return (x, y);
    }

    /// <summary> All cells of a rectangular region, both corners are included. </summary>
    public static IEnumerable<(int X, int Y)> Region((int X, int Y) corner1, (int X, int Y) corner2)
    {
        var minX = Math.Min(corner1.X, corner2.X);
        var maxX = Math.Max(corner1.X, corner2.X);
        var minY = Math.Min(corner1.Y, corner2.Y);
        var maxY = Math.Max(corner1.Y, corner2.Y);

        for (var y = minY; y <= maxY; y++)
            for (var x = minX; x <= maxX; x++)
                yield return (x, y);
    }

    /// <summary>
    /// Cells ordered by rings of increasing Chebyshev distance from the center.
    /// Center is the first one. Useful for searching of the closest matching cell.
    /// </summary>
    public static IEnumerable<(int X, int Y)> Spiral((int X, int Y) center, int radius)
    {
        yield return center;

        for (var r = 1; r <= radius; r++)
            foreach (var cell in Point2.CoordinatesAtChebyshevDistance(center, r))
                yield return cell;
    }

    /// <summary>
    /// Cells of a grid ordered by rings of increasing Chebyshev distance from the center.
    /// Center is the first one. Useful for searching of the closest matching cell.
    /// </summary>
    public static IEnumerable<(int X, int Y)> Spiral((int X, int Y) center, int radius, int width, int height)
    {
        if (Contains(center, width, height))
            yield return center;

        var bottomLimit = (0, 0);
        var topLimit = (width - 1, height - 1);

        for (var r = 1; r <= radius; r++)
            foreach (var cell in Point2.CoordinatesAtChebyshevDistance(center, r, bottomLimit, topLimit))
                yield return cell;
    }
}
