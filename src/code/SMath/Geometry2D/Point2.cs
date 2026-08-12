using System.Numerics;
using System.Runtime.CompilerServices;

namespace SMath.Geometry2D;

/// <summary>
/// Point in two dimensions.
/// </summary>
public static class Point2
{
    /// <summary>
    /// Get the neighbors in axes directions.
    /// </summary>
    public static IEnumerable<(NInt X, NInt Y)> AxialNeighbors<NInt>((NInt X, NInt Y) point)
        where NInt : IBinaryInteger<NInt>
    {
        yield return (point.X + NInt.One, point.Y);
        yield return (point.X, point.Y + NInt.One);
        yield return (point.X - NInt.One, point.Y);
        yield return (point.X, point.Y - NInt.One);
    }

    /// <summary>
    /// Get the neighbors in diagonal directions.
    /// </summary>
    public static IEnumerable<(NInt X, NInt Y)> DiagonalNeighbors<NInt>((NInt X, NInt Y) point)
        where NInt : IBinaryInteger<NInt>
    {
        yield return (point.X + NInt.One, point.Y + NInt.One);
        yield return (point.X + NInt.One, point.Y - NInt.One);
        yield return (point.X - NInt.One, point.Y + NInt.One);
        yield return (point.X - NInt.One, point.Y - NInt.One);
    }

    /// <summary>
    /// Euclidean distance of the point and origin.
    /// </summary>
    /// <remarks>
    /// <a href="https://en.wikipedia.org/wiki/Euclidean_distance">Wikipedia</a>
    /// </remarks>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static N Distance<N>((N X, N Y) point)
        where N : IRootFunctions<N>
        => PT.Hypotenuse(point);

    /// <summary>
    /// Euclidean distance of two points.
    /// </summary>
    /// <remarks>
    /// <a href="https://en.wikipedia.org/wiki/Euclidean_distance">Wikipedia</a>
    /// </remarks>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static N Distance<N>((N X, N Y) point1, (N X, N Y) point2)
        where N : IRootFunctions<N>
        => PT.Hypotenuse(point2.X - point1.X, point2.Y - point1.Y);

    /// <summary>
    /// Manhattan or taxicab distance of point and origin.
    /// </summary>
    /// <remarks>
    /// <a href="https://en.wikipedia.org/wiki/Taxicab_geometry">Wikipedia</a>
    /// </remarks>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static N ManhattanDistance<N>((N X, N Y) point)
        where N : INumberBase<N>
        => N.Abs(point.X) + N.Abs(point.Y);

    /// <summary>
    /// Manhattan or taxicab distance of two points.
    /// </summary>
    /// <remarks>
    /// <a href="https://en.wikipedia.org/wiki/Taxicab_geometry">Wikipedia</a>
    /// </remarks>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static N ManhattanDistance<N>((N X, N Y) point1, (N X, N Y) point2)
        where N : INumberBase<N>
        => N.Abs(point1.X - point2.X) + N.Abs(point1.Y - point2.Y);

    /// <summary>
    /// Chebyshev distance of point and origin
    /// </summary>
    /// <remarks>
    /// <a href="https://en.wikipedia.org/wiki/Chebyshev_distance">Wikipedia</a>
    /// </remarks>
    public static N ChebyshevDistance<N>((N X, N Y) point)
        where N : INumber<N>
        => N.Max(N.Abs(point.X), N.Abs(point.Y));

    /// <summary>
    /// Chebyshev distance of two points.
    /// </summary>
    /// <remarks>
    /// <a href="https://en.wikipedia.org/wiki/Chebyshev_distance">Wikipedia</a>
    /// </remarks>
    public static N ChebyshevDistance<N>((N X, N Y) point1, (N X, N Y) point2)
        where N : INumber<N>
        => N.Max(N.Abs(point1.X - point2.X), N.Abs(point1.Y - point2.Y));

    /// <summary>
    /// Minkowski distance of point and origin.
    /// </summary>
    /// <remarks>
    /// <a href="https://en.wikipedia.org/wiki/Minkowski_distance">Wikipedia</a>
    /// </remarks>
    public static N MinkowskiDistance<N>((N X, N Y) point, N r)
        where N : IPowerFunctions<N>
        => N.Pow(N.Pow(N.Abs(point.X), r) + N.Pow(N.Abs(point.Y), r), N.One / r);

    /// <summary>
    /// Minkowski distance of two points.
    /// </summary>
    /// <remarks>
    /// <a href="https://en.wikipedia.org/wiki/Minkowski_distance">Wikipedia</a>
    /// </remarks>
    public static N MinkowskiDistance<N>((N X, N Y) point1, (N X, N Y) point2, N r)
        where N : IPowerFunctions<N>
        => N.Pow(N.Pow(N.Abs(point1.X - point2.X), r) + N.Pow(N.Abs(point1.Y - point2.Y), r), N.One / r);

    /// <summary>
    /// Get all coordinates at exact Manhattan or taxicab distance from the center point.
    /// </summary>
    /// <remarks>
    /// <a href="https://en.wikipedia.org/wiki/Taxicab_geometry">Wikipedia</a>
    /// </remarks>
    public static IEnumerable<(NInt X, NInt Y)> CoordinatesAtManhattanDistance<NInt>((NInt X, NInt Y) center, NInt distance)
        where NInt : IBinaryInteger<NInt>
    {
        if (distance < NInt.One)
            yield break;

        for (var dy = -distance; dy <= distance; dy++)
        {
            var dx = distance - NInt.Abs(dy);
            var y = center.Y + dy;

            yield return (center.X - dx, y);
            if (dx != NInt.Zero) // dx == 0 would yield the very same coordinate twice
                yield return (center.X + dx, y);
        }
    }

    /// <summary>
    /// Get all coordinates at exact Manhattan or taxicab distance from the center point limited by bounds.
    /// </summary>
    /// <remarks>
    /// <a href="https://en.wikipedia.org/wiki/Taxicab_geometry">Wikipedia</a>
    /// </remarks>
    public static IEnumerable<(NInt X, NInt Y)> CoordinatesAtManhattanDistance<NInt>((NInt X, NInt Y) center, NInt distance,
        (NInt X, NInt Y) bottomLimit, (NInt X, NInt Y) topLimit)
        where NInt : IBinaryInteger<NInt>
    {
        if (distance < NInt.One)
            yield break;

        // the limits clamp the traversed range, so no coordinate outside of them is ever visited
        var maxY = NInt.Min(center.Y + distance, topLimit.Y);
        for (var y = NInt.Max(center.Y - distance, bottomLimit.Y); y <= maxY; y++)
        {
            var dx = distance - NInt.Abs(y - center.Y);

            var x1 = center.X - dx;
            var x2 = center.X + dx;

            if (x1 >= bottomLimit.X && x1 <= topLimit.X)
                yield return (x1, y);
            if (x2 != x1 && x2 >= bottomLimit.X && x2 <= topLimit.X) // skip the duplicate at dx == 0
                yield return (x2, y);
        }
    }

    /// <summary>
    /// Get all coordinates up to the Manhattan or taxicab distance from the center point.
    /// Center is included.
    /// </summary>
    /// <remarks>
    /// <a href="https://en.wikipedia.org/wiki/Taxicab_geometry">Wikipedia</a>
    /// </remarks>
    public static IEnumerable<(NInt X, NInt Y)> CoordinatesUpToManhattanDistance<NInt>((NInt X, NInt Y) center, NInt distance)
       where NInt : IBinaryInteger<NInt>
    {
        for (var dy = -distance; dy <= distance; dy++)
        {
            var y = center.Y + dy;
            var dxMax = distance - NInt.Abs(dy);

            // the bound is hoisted out of the loop condition, an iterator holds its locals
            // in fields and would reevaluate the expression for every single coordinate
            var maxX = center.X + dxMax;
            for (var x = center.X - dxMax; x <= maxX; x++)
                yield return (x, y);
        }
    }

    /// <summary>
    /// Get all coordinates up to the Manhattan or taxicab distance from the center point limited by bounds.
    /// Center is included.
    /// </summary>
    /// <remarks>
    /// <a href="https://en.wikipedia.org/wiki/Taxicab_geometry">Wikipedia</a>
    /// </remarks>
    public static IEnumerable<(NInt X, NInt Y)> CoordinatesUpToManhattanDistance<NInt>(
        (NInt X, NInt Y) center, NInt distance, (NInt X, NInt Y) bottomLimit, (NInt X, NInt Y) topLimit)
        where NInt : IBinaryInteger<NInt>
    {
        if (distance < NInt.Zero)
            yield break;

        // the limits clamp the traversed ranges, so every visited coordinate is also yielded
        var maxY = NInt.Min(center.Y + distance, topLimit.Y);
        for (var y = NInt.Max(center.Y - distance, bottomLimit.Y); y <= maxY; y++)
        {
            var dxMax = distance - NInt.Abs(y - center.Y);

            var maxX = NInt.Min(center.X + dxMax, topLimit.X);
            for (var x = NInt.Max(center.X - dxMax, bottomLimit.X); x <= maxX; x++)
                yield return (x, y);
        }
    }

    /// <summary>
    /// Get all coordinates whose Manhattan or taxicab distance from the center point
    /// falls within the inclusive range [<paramref name="minDistance"/>, <paramref name="maxDistance"/>].
    /// </summary>
    /// <remarks>
    /// <a href="https://en.wikipedia.org/wiki/Taxicab_geometry">Wikipedia</a>
    /// </remarks>
    public static IEnumerable<(NInt X, NInt Y)> CoordinatesInManhattanDistanceRange<NInt>(
        (NInt X, NInt Y) center, NInt minDistance, NInt maxDistance)
        where NInt : IBinaryInteger<NInt>
    {
        if (maxDistance < NInt.Zero || maxDistance < minDistance)
            yield break;

        var min = NInt.Max(minDistance, NInt.Zero);

        for (var dy = -maxDistance; dy <= maxDistance; dy++)
        {
            var y = center.Y + dy;
            var absDy = NInt.Abs(dy);
            var dxMax = maxDistance - absDy;
            var lowX = center.X - dxMax;
            var highX = center.X + dxMax;

            // the row is inside the minimum only where |dx| < inner, a contiguous gap around
            // the center, so the row is emitted as two runs instead of being filtered
            var inner = min - absDy;
            if (inner <= NInt.Zero)
            {
                for (var x = lowX; x <= highX; x++)
                    yield return (x, y);
            }
            else
            {
                var lowEnd = center.X - inner;
                for (var x = lowX; x <= lowEnd; x++)
                    yield return (x, y);

                var highStart = center.X + inner;
                for (var x = highStart; x <= highX; x++)
                    yield return (x, y);
            }
        }
    }

    /// <summary>
    /// Get all coordinates whose Manhattan or taxicab distance from the center point
    /// falls within the inclusive range [<paramref name="minDistance"/>, <paramref name="maxDistance"/>],
    /// limited by bounds.
    /// </summary>
    /// <remarks>
    /// <a href="https://en.wikipedia.org/wiki/Taxicab_geometry">Wikipedia</a>
    /// </remarks>
    public static IEnumerable<(NInt X, NInt Y)> CoordinatesInManhattanDistanceRange<NInt>(
        (NInt X, NInt Y) center, NInt minDistance, NInt maxDistance,
        (NInt X, NInt Y) bottomLimit, (NInt X, NInt Y) topLimit)
        where NInt : IBinaryInteger<NInt>
    {
        if (maxDistance < NInt.Zero || maxDistance < minDistance)
            yield break;

        var min = NInt.Max(minDistance, NInt.Zero);

        var maxY = NInt.Min(center.Y + maxDistance, topLimit.Y);
        for (var y = NInt.Max(center.Y - maxDistance, bottomLimit.Y); y <= maxY; y++)
        {
            var absDy = NInt.Abs(y - center.Y);
            var dxMax = maxDistance - absDy;
            var lowX = NInt.Max(center.X - dxMax, bottomLimit.X);
            var highX = NInt.Min(center.X + dxMax, topLimit.X);

            // the gap where the row is inside the minimum is contiguous, so the row is
            // emitted as two clamped runs instead of being filtered coordinate by coordinate
            var inner = min - absDy;
            if (inner <= NInt.Zero)
            {
                for (var x = lowX; x <= highX; x++)
                    yield return (x, y);
            }
            else
            {
                var lowEnd = NInt.Min(center.X - inner, highX);
                for (var x = lowX; x <= lowEnd; x++)
                    yield return (x, y);

                var highStart = NInt.Max(center.X + inner, lowX);
                for (var x = highStart; x <= highX; x++)
                    yield return (x, y);
            }
        }
    }

    /// <summary>
    /// Get all coordinates at exact Chebyshev distance from the center point.
    /// </summary>
    /// <remarks>
    /// <a href="https://en.wikipedia.org/wiki/Chebyshev_distance">Wikipedia</a>
    /// </remarks>
    public static IEnumerable<(NInt X, NInt Y)> CoordinatesAtChebyshevDistance<NInt>((NInt X, NInt Y) center, NInt distance)
        where NInt : IBinaryInteger<NInt>
    {
        if (distance < NInt.One)
            yield break;

        var minX = center.X - distance;
        var maxX = center.X + distance;
        var minY = center.Y - distance;
        var maxY = center.Y + distance;

        for (var x = minX; x <= maxX; x++)
            yield return (x, minY);

        for (var y = minY + NInt.One; y < maxY; y++)
            yield return (maxX, y);

        for (var x = maxX; x >= minX; x--)
            yield return (x, maxY);

        for (var y = maxY - NInt.One; y > minY; y--)
            yield return (minX, y);
    }

    /// <summary>
    /// Get all coordinates at exact Chebyshev distance from the center point limited by bounds.
    /// </summary>
    /// <remarks>
    /// <a href="https://en.wikipedia.org/wiki/Chebyshev_distance">Wikipedia</a>
    /// </remarks>
    public static IEnumerable<(NInt X, NInt Y)> CoordinatesAtChebyshevDistance<NInt>((NInt X, NInt Y) center, NInt distance,
        (NInt X, NInt Y) bottomLimit, (NInt X, NInt Y) topLimit)
        where NInt : IBinaryInteger<NInt>
    {
        if (distance < NInt.One)
            yield break;

        var minX = center.X - distance;
        var maxX = center.X + distance;
        var minY = center.Y - distance;
        var maxY = center.Y + distance;

        // horizontal edges carry the corners, vertical edges are emitted without them
        var edgeMinX = NInt.Max(minX, bottomLimit.X);
        var edgeMaxX = NInt.Min(maxX, topLimit.X);
        var edgeMinY = NInt.Max(minY + NInt.One, bottomLimit.Y);
        var edgeMaxY = NInt.Min(maxY - NInt.One, topLimit.Y);

        // bottom edge
        if (minY >= bottomLimit.Y && minY <= topLimit.Y)
        {
            for (var x = edgeMinX; x <= edgeMaxX; x++)
                yield return (x, minY);
        }

        // right edge
        if (maxX >= bottomLimit.X && maxX <= topLimit.X)
        {
            for (var y = edgeMinY; y <= edgeMaxY; y++)
                yield return (maxX, y);
        }

        // top edge
        if (maxY >= bottomLimit.Y && maxY <= topLimit.Y)
        {
            for (var x = edgeMaxX; x >= edgeMinX; x--)
                yield return (x, maxY);
        }

        // left edge
        if (minX >= bottomLimit.X && minX <= topLimit.X)
        {
            for (var y = edgeMaxY; y >= edgeMinY; y--)
                yield return (minX, y);
        }
    }

    /// <summary>
    /// Get all coordinates up to the Chebyshev distance from the center point.
    /// Center point is included.
    /// </summary>
    /// <remarks>
    /// <a href="https://en.wikipedia.org/wiki/Chebyshev_distance">Wikipedia</a>
    /// </remarks>
    public static IEnumerable<(NInt X, NInt Y)> CoordinatesUpToChebyshevDistance<NInt>((NInt X, NInt Y) center, NInt distance)
        where NInt : IBinaryInteger<NInt>
    {
        if (distance < NInt.Zero)
            yield break;

        var minX = center.X - distance;
        var maxX = center.X + distance;
        var minY = center.Y - distance;
        var maxY = center.Y + distance;

        for (var x = minX; x <= maxX; x++)
            for (var y = minY; y <= maxY; y++)
                yield return (x, y);
    }

    /// <summary>
    /// Get all coordinates up to the Chebyshev distance from the center point limited by bounds.
    /// Center point is included.
    /// </summary>
    /// <remarks>
    /// <a href="https://en.wikipedia.org/wiki/Chebyshev_distance">Wikipedia</a>
    /// </remarks>
    public static IEnumerable<(NInt X, NInt Y)> CoordinatesUpToChebyshevDistance<NInt>((NInt X, NInt Y) center, NInt distance,
        (NInt X, NInt Y) bottomLimit, (NInt X, NInt Y) topLimit)
        where NInt : IBinaryInteger<NInt>
    {
        if (distance < NInt.Zero)
            yield break;

        var minX = NInt.Max(center.X - distance, bottomLimit.X);
        var maxX = NInt.Min(center.X + distance, topLimit.X);
        var minY = NInt.Max(center.Y - distance, bottomLimit.Y);
        var maxY = NInt.Min(center.Y + distance, topLimit.Y);

        for (var x = minX; x <= maxX; x++)
            for (var y = minY; y <= maxY; y++)
                yield return (x, y);
    }

    /// <summary>
    /// Get all coordinates whose Chebyshev distance from the center point
    /// falls within the inclusive range [<paramref name="minDistance"/>, <paramref name="maxDistance"/>].
    /// </summary>
    /// <remarks>
    /// <a href="https://en.wikipedia.org/wiki/Chebyshev_distance">Wikipedia</a>
    /// </remarks>
    public static IEnumerable<(NInt X, NInt Y)> CoordinatesInChebyshevDistanceRange<NInt>(
        (NInt X, NInt Y) center, NInt minDistance, NInt maxDistance)
        where NInt : IBinaryInteger<NInt>
    {
        if (maxDistance < NInt.Zero || maxDistance < minDistance)
            yield break;

        var min = NInt.Max(minDistance, NInt.Zero);

        var minX = center.X - maxDistance;
        var maxX = center.X + maxDistance;
        var minY = center.Y - maxDistance;
        var maxY = center.Y + maxDistance;

        // the gap of a column that falls inside the minimum is the same for every column
        var gapEnd = center.Y - min;
        var gapStart = center.Y + min;

        for (var x = minX; x <= maxX; x++)
        {
            // once the column is at or beyond the minimum, all of it qualifies and not a
            // single coordinate of it needs its distance evaluated
            if (NInt.Abs(x - center.X) >= min)
            {
                for (var y = minY; y <= maxY; y++)
                    yield return (x, y);
            }
            else
            {
                // the column crosses the minimum, the part inside it is skipped as a whole
                for (var y = minY; y <= gapEnd; y++)
                    yield return (x, y);

                for (var y = gapStart; y <= maxY; y++)
                    yield return (x, y);
            }
        }
    }

    /// <summary>
    /// Get all coordinates whose Chebyshev distance from the center point
    /// falls within the inclusive range [<paramref name="minDistance"/>, <paramref name="maxDistance"/>],
    /// limited by bounds.
    /// </summary>
    /// <remarks>
    /// <a href="https://en.wikipedia.org/wiki/Chebyshev_distance">Wikipedia</a>
    /// </remarks>
    public static IEnumerable<(NInt X, NInt Y)> CoordinatesInChebyshevDistanceRange<NInt>(
        (NInt X, NInt Y) center, NInt minDistance, NInt maxDistance,
        (NInt X, NInt Y) bottomLimit, (NInt X, NInt Y) topLimit)
        where NInt : IBinaryInteger<NInt>
    {
        if (maxDistance < NInt.Zero || maxDistance < minDistance)
            yield break;

        var min = NInt.Max(minDistance, NInt.Zero);

        var minX = NInt.Max(center.X - maxDistance, bottomLimit.X);
        var maxX = NInt.Min(center.X + maxDistance, topLimit.X);
        var minY = NInt.Max(center.Y - maxDistance, bottomLimit.Y);
        var maxY = NInt.Min(center.Y + maxDistance, topLimit.Y);

        // the gap of a column that falls inside the minimum is the same for every column,
        // clamped to the limits so that the runs around it stay within them
        var gapEnd = NInt.Min(center.Y - min, maxY);
        var gapStart = NInt.Max(center.Y + min, minY);

        for (var x = minX; x <= maxX; x++)
        {
            if (NInt.Abs(x - center.X) >= min)
            {
                for (var y = minY; y <= maxY; y++)
                    yield return (x, y);
            }
            else
            {
                for (var y = minY; y <= gapEnd; y++)
                    yield return (x, y);

                for (var y = gapStart; y <= maxY; y++)
                    yield return (x, y);
            }
        }
    }

    /// <summary>
    /// Count of coordinates at exact Manhattan distance from a center point. Unbounded.
    /// </summary>
    /// <remarks> The taxicab circle has 4*distance coordinates, distance 0 gives the center only. </remarks>
    public static NInt ManhattanCircleCount<NInt>(NInt distance)
        where NInt : IBinaryInteger<NInt>
        => distance < NInt.Zero
            ? NInt.Zero
            : distance == NInt.Zero
                ? NInt.One
                : NInt.CreateChecked(4) * distance;

    /// <summary>
    /// Count of coordinates up to the Manhattan distance from a center point, center included. Unbounded.
    /// </summary>
    /// <remarks> The taxicab disk has 2*d^2 + 2*d + 1 coordinates. </remarks>
    public static NInt ManhattanDiskCount<NInt>(NInt distance)
        where NInt : IBinaryInteger<NInt>
        => distance < NInt.Zero
            ? NInt.Zero
            : NInt.CreateChecked(2) * distance * distance + NInt.CreateChecked(2) * distance + NInt.One;

    /// <summary>
    /// Count of coordinates at exact Chebyshev distance from a center point. Unbounded.
    /// </summary>
    /// <remarks> The Chebyshev ring has 8*distance coordinates, distance 0 gives the center only. </remarks>
    public static NInt ChebyshevRingCount<NInt>(NInt distance)
        where NInt : IBinaryInteger<NInt>
        => distance < NInt.Zero
            ? NInt.Zero
            : distance == NInt.Zero
                ? NInt.One
                : NInt.CreateChecked(8) * distance;

    /// <summary>
    /// Count of coordinates up to the Chebyshev distance from a center point, center included. Unbounded.
    /// </summary>
    /// <remarks> The Chebyshev square has (2*d + 1)^2 coordinates. </remarks>
    public static NInt ChebyshevSquareCount<NInt>(NInt distance)
        where NInt : IBinaryInteger<NInt>
    {
        if (distance < NInt.Zero)
            return NInt.Zero;

        var side = NInt.CreateChecked(2) * distance + NInt.One;
        return side * side;
    }

    /// <summary>
    /// Write all coordinates at exact Manhattan distance from the center point into a destination buffer.
    /// Allocation free alternative to the enumerable overload.
    /// </summary>
    /// <returns> Count of written coordinates. </returns>
    /// <exception cref="ArgumentException"> Destination is too short. </exception>
    public static int CoordinatesAtManhattanDistance<NInt>((NInt X, NInt Y) center, NInt distance,
        Span<(NInt X, NInt Y)> destination)
        where NInt : IBinaryInteger<NInt>
    {
        if (distance < NInt.One)
            return 0;

        var count = 0;
        for (var dy = -distance; dy <= distance; dy++)
        {
            var dx = distance - NInt.Abs(dy);
            var y = center.Y + dy;

            if (count >= destination.Length)
                throw new ArgumentException("Destination is too short.", nameof(destination));
            destination[count++] = (center.X - dx, y);

            if (dx != NInt.Zero) // dx == 0 would write the very same coordinate twice
            {
                if (count >= destination.Length)
                    throw new ArgumentException("Destination is too short.", nameof(destination));
                destination[count++] = (center.X + dx, y);
            }
        }

        return count;
    }

    /// <summary>
    /// Write all coordinates up to the Manhattan distance from the center point into a destination buffer.
    /// Center is included. Allocation free alternative to the enumerable overload.
    /// </summary>
    /// <returns> Count of written coordinates. </returns>
    /// <exception cref="ArgumentException"> Destination is too short. </exception>
    public static int CoordinatesUpToManhattanDistance<NInt>((NInt X, NInt Y) center, NInt distance,
        Span<(NInt X, NInt Y)> destination)
        where NInt : IBinaryInteger<NInt>
    {
        if (distance < NInt.Zero)
            return 0;

        var count = 0;
        for (var dy = -distance; dy <= distance; dy++)
        {
            var y = center.Y + dy;
            var dxMax = distance - NInt.Abs(dy);
            for (var dx = -dxMax; dx <= dxMax; dx++)
            {
                if (count >= destination.Length)
                    throw new ArgumentException("Destination is too short.", nameof(destination));

                destination[count++] = (center.X + dx, y);
            }
        }

        return count;
    }

    /// <summary>
    /// Write all coordinates up to the Manhattan distance from the center point, limited by bounds,
    /// into a destination buffer. Center is included. Allocation free alternative to the enumerable overload.
    /// </summary>
    /// <returns> Count of written coordinates. </returns>
    /// <exception cref="ArgumentException"> Destination is too short. </exception>
    public static int CoordinatesUpToManhattanDistance<NInt>((NInt X, NInt Y) center, NInt distance,
        (NInt X, NInt Y) bottomLimit, (NInt X, NInt Y) topLimit, Span<(NInt X, NInt Y)> destination)
        where NInt : IBinaryInteger<NInt>
    {
        if (distance < NInt.Zero)
            return 0;

        var count = 0;
        var maxY = NInt.Min(center.Y + distance, topLimit.Y);
        for (var y = NInt.Max(center.Y - distance, bottomLimit.Y); y <= maxY; y++)
        {
            var dxMax = distance - NInt.Abs(y - center.Y);

            var lowX = NInt.Max(center.X - dxMax, bottomLimit.X);
            var highX = NInt.Min(center.X + dxMax, topLimit.X);

            // the run length is known, so the capacity is checked once per run
            var runLength = highX >= lowX ? highX - lowX + NInt.One : NInt.Zero;
            if (NInt.CreateSaturating(destination.Length - count) < runLength)
                throw new ArgumentException("Destination is too short.", nameof(destination));

            for (var x = lowX; x <= highX; x++)
                destination[count++] = (x, y);
        }

        return count;
    }

    /// <summary>
    /// Write all coordinates whose Manhattan distance from the center point falls within the inclusive
    /// range [<paramref name="minDistance"/>, <paramref name="maxDistance"/>] into a destination buffer.
    /// Allocation free alternative to the enumerable overload.
    /// </summary>
    /// <remarks>
    /// Size the buffer with <c>ManhattanDiskCount(maxDistance) - ManhattanDiskCount(minDistance - 1)</c>.
    /// </remarks>
    /// <returns> Count of written coordinates. </returns>
    /// <exception cref="ArgumentException"> Destination is too short. </exception>
    public static int CoordinatesInManhattanDistanceRange<NInt>((NInt X, NInt Y) center,
        NInt minDistance, NInt maxDistance, Span<(NInt X, NInt Y)> destination)
        where NInt : IBinaryInteger<NInt>
    {
        if (maxDistance < NInt.Zero || maxDistance < minDistance)
            return 0;

        var min = NInt.Max(minDistance, NInt.Zero);

        // the disk up to the minimum is the part that is left out, so the count is known up front
        if (NInt.CreateSaturating(destination.Length) < ManhattanDiskCount(maxDistance) - ManhattanDiskCount(min - NInt.One))
            throw new ArgumentException("Destination is too short.", nameof(destination));

        var count = 0;
        for (var dy = -maxDistance; dy <= maxDistance; dy++)
        {
            var y = center.Y + dy;
            var absDy = NInt.Abs(dy);
            var dxMax = maxDistance - absDy;
            var lowX = center.X - dxMax;
            var highX = center.X + dxMax;

            var inner = min - absDy;
            if (inner <= NInt.Zero)
            {
                for (var x = lowX; x <= highX; x++)
                    destination[count++] = (x, y);
            }
            else
            {
                var lowEnd = center.X - inner;
                for (var x = lowX; x <= lowEnd; x++)
                    destination[count++] = (x, y);

                var highStart = center.X + inner;
                for (var x = highStart; x <= highX; x++)
                    destination[count++] = (x, y);
            }
        }

        return count;
    }

    /// <summary>
    /// Write all coordinates up to the Chebyshev distance from the center point into a destination buffer.
    /// Center is included. Allocation free alternative to the enumerable overload.
    /// </summary>
    /// <returns> Count of written coordinates. </returns>
    /// <exception cref="ArgumentException"> Destination is too short. </exception>
    public static int CoordinatesUpToChebyshevDistance<NInt>((NInt X, NInt Y) center, NInt distance,
        Span<(NInt X, NInt Y)> destination)
        where NInt : IBinaryInteger<NInt>
    {
        if (distance < NInt.Zero)
            return 0;

        var minX = center.X - distance;
        var maxX = center.X + distance;
        var minY = center.Y - distance;
        var maxY = center.Y + distance;

        var count = 0;
        for (var x = minX; x <= maxX; x++)
        {
            for (var y = minY; y <= maxY; y++)
            {
                if (count >= destination.Length)
                    throw new ArgumentException("Destination is too short.", nameof(destination));

                destination[count++] = (x, y);
            }
        }

        return count;
    }
}
