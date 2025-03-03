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
        yield return (++point.X, point.Y);
        yield return (point.X, ++point.Y);
        yield return (--point.X, point.Y);
        yield return (point.X, --point.Y);
    }

    /// <summary>
    /// Get the neighbors in diagonal directions.
    /// </summary>
    public static IEnumerable<(NInt X, NInt Y)> DiagonalNeighbors<NInt>((NInt X, NInt Y) point)
        where NInt : IBinaryInteger<NInt>
    {
        yield return (++point.X, ++point.Y);
        yield return (++point.X, --point.Y);
        yield return (--point.X, ++point.Y);
        yield return (--point.X, --point.Y);
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
        => N.Abs(point.X + point.Y);

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

            yield return (center.X - dx, center.Y + dy);
            if (dx != NInt.Zero) // zabránění duplicitního výpisu při dx == 0
                yield return (center.X + dx, center.Y + dy);
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

        for (var dy = -distance; dy <= distance; dy++)
        {
            var dx = distance - NInt.Abs(dy);

            var x1 = center.X - dx;
            var x2 = center.X + dx;
            var y = center.Y + dy;

            if (y >= bottomLimit.Y && y <= topLimit.Y)
            {
                if (x1 >= bottomLimit.X && x1 <= topLimit.X)
                    yield return (x1, y);
                if (x2 != x1 && x2 >= bottomLimit.X && x2 <= topLimit.X) // Zabránění duplikátům
                    yield return (x2, y);
            }
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
            var dxMax = distance - NInt.Abs(dy);
            for (var dx = -dxMax; dx <= dxMax; dx++)
            {
                yield return (center.X + dx, center.Y + dy);
            }
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

        for (var dy = -distance; dy <= distance; dy++)
        {
            var y = center.Y + dy;
            if (y < bottomLimit.Y || y > topLimit.Y)
                continue;

            var dxMax = distance - NInt.Abs(dy);
            for (var dx = -dxMax; dx <= dxMax; dx++)
            {
                var x = center.X + dx;
                if (x >= bottomLimit.X && x <= topLimit.X)
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

        if (minY >= bottomLimit.Y)
        {
            for (var x = NInt.Max(minX, bottomLimit.X); x <= NInt.Min(maxX, topLimit.X); x++)
                yield return (x, minY);
        }

        if (maxX <= topLimit.X)
        {
            for (var y = NInt.Max(minY + NInt.One, bottomLimit.Y); y < NInt.Min(maxY, topLimit.Y); y++)
                yield return (maxX, y);
        }

        if (maxY <= topLimit.Y)
        {
            for (var x = NInt.Min(maxX, topLimit.X); x >= NInt.Max(minX, bottomLimit.X); x--)
                yield return (x, maxY);
        }

        if (minX >= bottomLimit.X)
        {
            for (var y = NInt.Min(maxY - NInt.One, topLimit.Y); y > NInt.Max(minY, bottomLimit.Y); y--)
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
        if (distance < NInt.One)
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
        if (distance < NInt.One)
            yield break;

        var minX = NInt.Max(center.X - distance, bottomLimit.X);
        var maxX = NInt.Min(center.X + distance, topLimit.X);
        var minY = NInt.Max(center.Y - distance, bottomLimit.Y);
        var maxY = NInt.Min(center.Y + distance, topLimit.Y);

        for (var x = minX; x <= maxX; x++)
            for (var y = minY; y <= maxY; y++)
                yield return (x, y);
    }
}