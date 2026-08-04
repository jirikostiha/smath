using System.Runtime.CompilerServices;

namespace SMath.Grid2D;

/// <summary>
/// Directions of steps over cells of a two dimensional grid.
/// </summary>
/// <remarks>
/// Directions are ordered counterclockwise starting at the (+)x-axis,
/// axial and diagonal ones are interleaved so that
/// <c>index / 2</c> is the axial index and an even index is an axial direction.
/// </remarks>
public static class GridDirection
{
    /// <summary> Index of an unknown or missing direction. </summary>
    public const byte None = byte.MaxValue;

    /// <summary> Count of axial directions. </summary>
    public const int AxialCount = 4;

    /// <summary> Count of axial and diagonal directions. </summary>
    public const int Count = 8;

    private static readonly (int X, int Y)[] Deltas =
    {
        (1, 0),   // 0 east
        (1, 1),   // 1 north east
        (0, 1),   // 2 north
        (-1, 1),  // 3 north west
        (-1, 0),  // 4 west
        (-1, -1), // 5 south west
        (0, -1),  // 6 south
        (1, -1),  // 7 south east
    };

    /// <summary> All directions ordered counterclockwise from the (+)x-axis. </summary>
    public static ReadOnlySpan<(int X, int Y)> All
        => Deltas;

    /// <summary> Step of a direction. </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static (int X, int Y) Delta(int direction)
        => Deltas[direction];

    /// <summary> Determine if a direction is an axial one. </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsAxial(int direction)
        => (direction & 1) == 0;

    /// <summary> Direction of the opposite step. </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int Opposite(int direction)
        => (direction + 4) & 7;

    /// <summary> Direction rotated counterclockwise by a count of eighths of the full angle. </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int Rotated(int direction, int eighths)
        => (direction + eighths) & 7;

    /// <summary> Count of eighths of the full angle between two directions. It is 0 to 4. </summary>
    public static int Distance(int direction1, int direction2)
    {
        var difference = (direction1 - direction2) & 7;
        return Math.Min(difference, 8 - difference);
    }

    /// <summary>
    /// Direction of a step. Any non-zero step is snapped to the closest of the eight directions.
    /// </summary>
    /// <returns> Direction index or <see cref="None"/> for a zero step. </returns>
    public static byte OfStep(int dx, int dy)
    {
        if (dx == 0 && dy == 0)
            return None;

        var sx = Math.Sign(dx);
        var sy = Math.Sign(dy);

        // snap to an axis if the step is over twice longer in it
        var ax = Math.Abs(dx);
        var ay = Math.Abs(dy);
        if (ax > (ay << 1))
            sy = 0;
        else if (ay > (ax << 1))
            sx = 0;

        return OfUnitStep(sx, sy);
    }

    /// <summary>
    /// Direction of a step of unit length in both axes.
    /// </summary>
    /// <returns> Direction index or <see cref="None"/> for a zero step. </returns>
    public static byte OfUnitStep(int sx, int sy)
    {
        for (byte i = 0; i < Count; i++)
        {
            if (Deltas[i].X == sx && Deltas[i].Y == sy)
                return i;
        }

        return None;
    }

    /// <summary> Cell the step of a direction leads to. </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static (int X, int Y) Step((int X, int Y) cell, int direction)
    {
        var delta = Deltas[direction];
        return (cell.X + delta.X, cell.Y + delta.Y);
    }
}
