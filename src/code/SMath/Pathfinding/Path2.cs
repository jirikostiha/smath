using System.Numerics;
using SMath.Geometry2D;
using SMath.Grid2D;

namespace SMath.Pathfinding;

/// <summary>
/// Post processing of a path of cells or points.
/// </summary>
public static class Path2
{
    /// <summary>
    /// Remove the cells lying inside of a straight run of a path of cells.
    /// Only the cells where the direction changes are kept.
    /// </summary>
    /// <returns> Count of the kept cells. Cells are written in place. </returns>
    public static int Simplify(Span<(int X, int Y)> path)
    {
        if (path.Length < 3)
            return path.Length;

        var count = 1;
        var previousDx = path[1].X - path[0].X;
        var previousDy = path[1].Y - path[0].Y;

        for (var i = 1; i < path.Length - 1; i++)
        {
            var dx = path[i + 1].X - path[i].X;
            var dy = path[i + 1].Y - path[i].Y;

            if (dx != previousDx || dy != previousDy)
                path[count++] = path[i];

            previousDx = dx;
            previousDy = dy;
        }

        path[count++] = path[^1];

        return count;
    }

    /// <summary>
    /// Shorten a path of cells by dropping every cell that can be skipped
    /// by a straight line of sight over the passable cells.
    /// </summary>
    /// <remarks>
    /// It is the greedy variant of the string pulling, linear in the count of cells
    /// and good enough to remove the staircase artifacts of a grid search.
    /// </remarks>
    /// <typeparam name="TObstacle"> Predicate matching the impassable cells. </typeparam>
    /// <returns> Count of the kept cells. Cells are written in place. </returns>
    public static int PullString<TObstacle>(Span<(int X, int Y)> path, TObstacle isObstacle)
        where TObstacle : IGridPredicate
    {
        if (path.Length < 3)
            return path.Length;

        var count = 1;
        var anchor = path[0];

        for (var i = 1; i < path.Length - 1; i++)
        {
            if (Raster.IsVisible(anchor, path[i + 1], isObstacle))
                continue;

            anchor = path[i];
            path[count++] = anchor;
        }

        path[count++] = path[^1];

        return count;
    }

    /// <summary>
    /// Length of a path of points.
    /// </summary>
    public static N Length<N>(ReadOnlySpan<(N X, N Y)> path)
        where N : IRootFunctions<N>
    {
        var length = N.Zero;

        for (var i = 1; i < path.Length; i++)
            length += Point2.Distance(path[i - 1], path[i]);

        return length;
    }

    /// <summary>
    /// Point at a distance along a path of points.
    /// </summary>
    /// <remarks>
    /// A distance out of the path is clamped to the closest end.
    /// Use it to move an agent along a path at a constant speed.
    /// </remarks>
    public static (N X, N Y) PointAt<N>(ReadOnlySpan<(N X, N Y)> path, N distance)
        where N : IRootFunctions<N>, IComparisonOperators<N, N, bool>
    {
        if (path.Length == 0)
            throw new ArgumentException("Path must contain at least one point.", nameof(path));
        if (distance <= N.Zero || path.Length == 1)
            return path[0];

        var walked = N.Zero;
        for (var i = 1; i < path.Length; i++)
        {
            var segmentLength = Point2.Distance(path[i - 1], path[i]);
            var remaining = distance - walked;

            if (remaining <= segmentLength)
            {
                return segmentLength > N.Zero
                    ? Interpolation.Linear.Point(path[i - 1], path[i], remaining / segmentLength)
                    : path[i];
            }

            walked += segmentLength;
        }

        return path[^1];
    }

    /// <summary>
    /// Convert a path of cells into a path of center points of a grid of a given cell size.
    /// </summary>
    /// <returns> Count of written points, it equals the count of cells. </returns>
    public static int ToPoints<N>(ReadOnlySpan<(int X, int Y)> path, N cellSize, Span<(N X, N Y)> points)
        where N : IFloatingPoint<N>
    {
        for (var i = 0; i < path.Length; i++)
            points[i] = Raster.CenterOfCell(path[i], cellSize);

        return path.Length;
    }

    /// <summary>
    /// Round the corners of a path of points by a Chaikin subdivision.
    /// </summary>
    /// <remarks>
    /// One pass replaces every inner point by two points at a quarter and three quarters
    /// of its neighboring segments. Both ends are kept.
    /// <a href="https://en.wikipedia.org/wiki/Subdivision_surface">Wikipedia</a>
    /// </remarks>
    /// <returns> Count of written points. It is 2*(n-1) for n over 2. </returns>
    public static int Smooth<N>(ReadOnlySpan<(N X, N Y)> path, Span<(N X, N Y)> points)
        where N : IFloatingPoint<N>
    {
        if (path.Length < 3)
        {
            path.CopyTo(points);
            return path.Length;
        }

        var quarter = N.One / N.CreateTruncating(4);
        var threeQuarters = N.CreateTruncating(3) * quarter;
        var count = 0;

        points[count++] = path[0];

        for (var i = 0; i < path.Length - 1; i++)
        {
            var from = path[i];
            var to = path[i + 1];

            if (i > 0)
            {
                points[count++] = ((threeQuarters * from.X) + (quarter * to.X),
                                   (threeQuarters * from.Y) + (quarter * to.Y));
            }

            if (i < path.Length - 2)
            {
                points[count++] = ((quarter * from.X) + (threeQuarters * to.X),
                                   (quarter * from.Y) + (threeQuarters * to.Y));
            }
        }

        points[count++] = path[^1];

        return count;
    }

    /// <summary> Count of points written by <see cref="Smooth{N}"/>. </summary>
    public static int SmoothPointCount(int pathPointCount)
        => pathPointCount < 3 ? pathPointCount : (2 * (pathPointCount - 1));
}
