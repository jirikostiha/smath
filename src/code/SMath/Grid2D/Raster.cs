using System.Numerics;

namespace SMath.Grid2D;

/// <summary>
/// Rasterization of geometric primitives into cells of a two dimensional grid.
/// </summary>
/// <remarks>
/// All algorithms are purely integer based and allocation free.
/// <a href="https://en.wikipedia.org/wiki/Bresenham%27s_line_algorithm">Wikipedia</a>
/// </remarks>
public static class Raster
{
    /// <summary>
    /// Count of cells of a Bresenham line between two cells. Both ends are included.
    /// </summary>
    public static int LineCellCount((int X, int Y) from, (int X, int Y) to)
        => Math.Max(Math.Abs(to.X - from.X), Math.Abs(to.Y - from.Y)) + 1;

    /// <summary>
    /// Cells of a line between two cells. Both ends are included.
    /// The line is 8-connected, it may pass diagonally between two cells.
    /// </summary>
    /// <remarks>
    /// <a href="https://en.wikipedia.org/wiki/Bresenham%27s_line_algorithm">Wikipedia</a>
    /// </remarks>
    public static IEnumerable<(int X, int Y)> Line((int X, int Y) from, (int X, int Y) to)
    {
        var dx = Math.Abs(to.X - from.X);
        var dy = -Math.Abs(to.Y - from.Y);
        var sx = from.X < to.X ? 1 : -1;
        var sy = from.Y < to.Y ? 1 : -1;
        var error = dx + dy;

        var x = from.X;
        var y = from.Y;

        while (true)
        {
            yield return (x, y);

            if (x == to.X && y == to.Y)
                yield break;

            var doubleError = error << 1;
            if (doubleError >= dy)
            {
                error += dy;
                x += sx;
            }

            if (doubleError <= dx)
            {
                error += dx;
                y += sy;
            }
        }
    }

    /// <summary>
    /// Write cells of a line between two cells into a buffer. Both ends are included.
    /// The line is 8-connected, it may pass diagonally between two cells.
    /// </summary>
    /// <returns> Count of written cells, see <see cref="LineCellCount"/>. </returns>
    public static int Line((int X, int Y) from, (int X, int Y) to, Span<(int X, int Y)> cells)
    {
        var dx = Math.Abs(to.X - from.X);
        var dy = -Math.Abs(to.Y - from.Y);
        var sx = from.X < to.X ? 1 : -1;
        var sy = from.Y < to.Y ? 1 : -1;
        var error = dx + dy;

        var x = from.X;
        var y = from.Y;
        var count = 0;

        while (true)
        {
            cells[count++] = (x, y);

            if (x == to.X && y == to.Y)
                return count;

            var doubleError = error << 1;
            if (doubleError >= dy)
            {
                error += dy;
                x += sx;
            }

            if (doubleError <= dx)
            {
                error += dx;
                y += sy;
            }
        }
    }

    /// <summary>
    /// Cells of a line between two cells. Both ends are included.
    /// The line is 4-connected, every two consecutive cells share an edge.
    /// It is the "supercover" variant suitable for line of sight of solid obstacles.
    /// </summary>
    public static IEnumerable<(int X, int Y)> ThickLine((int X, int Y) from, (int X, int Y) to)
    {
        var dx = Math.Abs(to.X - from.X);
        var dy = Math.Abs(to.Y - from.Y);
        var sx = from.X < to.X ? 1 : -1;
        var sy = from.Y < to.Y ? 1 : -1;

        var x = from.X;
        var y = from.Y;
        var error = dx - dy;

        yield return (x, y);

        for (var i = dx + dy; i > 0; i--)
        {
            if (error > 0)
            {
                x += sx;
                error -= (dy << 1);
            }
            else if (error < 0)
            {
                y += sy;
                error += (dx << 1);
            }
            else
            {
                // exact diagonal, step in both axes at once and skip one iteration
                x += sx;
                y += sy;
                error = error - (dy << 1) + (dx << 1);
                i--;
            }

            yield return (x, y);
        }
    }

    /// <summary>
    /// Determine if there is no cell matching the obstacle predicate between two cells.
    /// The start cell is never tested, the target cell is.
    /// </summary>
    /// <remarks>
    /// It is symmetric only up to the symmetry of the underlying Bresenham line.
    /// </remarks>
    public static bool IsVisible<TObstacle>((int X, int Y) from, (int X, int Y) to, TObstacle isObstacle)
        where TObstacle : IGridPredicate
    {
        var dx = Math.Abs(to.X - from.X);
        var dy = -Math.Abs(to.Y - from.Y);
        var sx = from.X < to.X ? 1 : -1;
        var sy = from.Y < to.Y ? 1 : -1;
        var error = dx + dy;

        var x = from.X;
        var y = from.Y;

        while (x != to.X || y != to.Y)
        {
            var doubleError = error << 1;
            if (doubleError >= dy)
            {
                error += dy;
                x += sx;
            }

            if (doubleError <= dx)
            {
                error += dx;
                y += sy;
            }

            if (isObstacle.Test(x, y))
                return false;
        }

        return true;
    }

    /// <summary>
    /// Find the last cell reachable from the start cell towards the target cell.
    /// The start cell is never tested, the target cell is.
    /// </summary>
    public static (int X, int Y) Cast<TObstacle>((int X, int Y) from, (int X, int Y) to, TObstacle isObstacle)
        where TObstacle : IGridPredicate
    {
        var dx = Math.Abs(to.X - from.X);
        var dy = -Math.Abs(to.Y - from.Y);
        var sx = from.X < to.X ? 1 : -1;
        var sy = from.Y < to.Y ? 1 : -1;
        var error = dx + dy;

        var x = from.X;
        var y = from.Y;

        while (x != to.X || y != to.Y)
        {
            var previousX = x;
            var previousY = y;

            var doubleError = error << 1;
            if (doubleError >= dy)
            {
                error += dy;
                x += sx;
            }

            if (doubleError <= dx)
            {
                error += dx;
                y += sy;
            }

            if (isObstacle.Test(x, y))
                return (previousX, previousY);
        }

        return to;
    }

    /// <summary>
    /// Cells of a midpoint circle outline.
    /// </summary>
    /// <remarks>
    /// <a href="https://en.wikipedia.org/wiki/Midpoint_circle_algorithm">Wikipedia</a>
    /// </remarks>
    public static IEnumerable<(int X, int Y)> Circle((int X, int Y) center, int radius)
    {
        if (radius < 0)
            yield break;

        if (radius == 0)
        {
            yield return center;
            yield break;
        }

        var x = radius;
        var y = 0;
        var error = 1 - radius;

        while (x >= y)
        {
            yield return (center.X + x, center.Y + y);
            yield return (center.X + y, center.Y + x);
            yield return (center.X - y, center.Y + x);
            yield return (center.X - x, center.Y + y);
            yield return (center.X - x, center.Y - y);
            yield return (center.X - y, center.Y - x);
            yield return (center.X + y, center.Y - x);
            yield return (center.X + x, center.Y - y);

            y++;
            if (error < 0)
            {
                error += (y << 1) + 1;
            }
            else
            {
                x--;
                error += ((y - x) << 1) + 1;
            }
        }
    }

    /// <summary>
    /// Cells of a filled circle ordered by rows.
    /// </summary>
    public static IEnumerable<(int X, int Y)> Disc((int X, int Y) center, int radius)
    {
        if (radius < 0)
            yield break;

        var squaredRadius = radius * radius;

        for (var dy = -radius; dy <= radius; dy++)
        {
            var dxMax = (int)Math.Sqrt(squaredRadius - (dy * dy));
            for (var dx = -dxMax; dx <= dxMax; dx++)
                yield return (center.X + dx, center.Y + dy);
        }
    }

    /// <summary>
    /// Cells of a triangle outline.
    /// </summary>
    public static IEnumerable<(int X, int Y)> Triangle((int X, int Y) vertex1, (int X, int Y) vertex2, (int X, int Y) vertex3)
        => Line(vertex1, vertex2)
            .Concat(Line(vertex2, vertex3).Skip(1))
            .Concat(Line(vertex3, vertex1).Skip(1));

    /// <summary>
    /// Cell of a point in a grid of a given cell size. Point is snapped to the cell it lies in.
    /// </summary>
    public static (int X, int Y) CellOfPoint<N>((N X, N Y) point, N cellSize)
        where N : IFloatingPoint<N>
        => (int.CreateTruncating(N.Floor(point.X / cellSize)),
            int.CreateTruncating(N.Floor(point.Y / cellSize)));

    /// <summary>
    /// Center point of a cell in a grid of a given cell size.
    /// </summary>
    public static (N X, N Y) CenterOfCell<N>((int X, int Y) cell, N cellSize)
        where N : IFloatingPoint<N>
    {
        var half = cellSize / N.CreateTruncating(2);
        return ((N.CreateTruncating(cell.X) * cellSize) + half,
                (N.CreateTruncating(cell.Y) * cellSize) + half);
    }
}
