using System.Numerics;
using System.Runtime.CompilerServices;

namespace SMath.Geometry3D;

/// <summary>
/// Point in three dimensions.
/// </summary>
public static class Point3
{
    /// <summary>
    /// Get the neighbors in axes directions. The six coordinates sharing a face with the point.
    /// </summary>
    public static IEnumerable<(NInt X, NInt Y, NInt Z)> AxialNeighbors<NInt>((NInt X, NInt Y, NInt Z) point)
        where NInt : IBinaryInteger<NInt>
    {
        yield return (point.X + NInt.One, point.Y, point.Z);
        yield return (point.X - NInt.One, point.Y, point.Z);
        yield return (point.X, point.Y + NInt.One, point.Z);
        yield return (point.X, point.Y - NInt.One, point.Z);
        yield return (point.X, point.Y, point.Z + NInt.One);
        yield return (point.X, point.Y, point.Z - NInt.One);
    }

    /// <summary>
    /// Get the neighbors in edge directions. The twelve coordinates sharing an edge with the point,
    /// that is the ones offset in exactly two axes.
    /// </summary>
    public static IEnumerable<(NInt X, NInt Y, NInt Z)> EdgeNeighbors<NInt>((NInt X, NInt Y, NInt Z) point)
        where NInt : IBinaryInteger<NInt>
    {
        // the four edges around each axis
        yield return (point.X + NInt.One, point.Y + NInt.One, point.Z);
        yield return (point.X + NInt.One, point.Y - NInt.One, point.Z);
        yield return (point.X - NInt.One, point.Y + NInt.One, point.Z);
        yield return (point.X - NInt.One, point.Y - NInt.One, point.Z);

        yield return (point.X + NInt.One, point.Y, point.Z + NInt.One);
        yield return (point.X + NInt.One, point.Y, point.Z - NInt.One);
        yield return (point.X - NInt.One, point.Y, point.Z + NInt.One);
        yield return (point.X - NInt.One, point.Y, point.Z - NInt.One);

        yield return (point.X, point.Y + NInt.One, point.Z + NInt.One);
        yield return (point.X, point.Y + NInt.One, point.Z - NInt.One);
        yield return (point.X, point.Y - NInt.One, point.Z + NInt.One);
        yield return (point.X, point.Y - NInt.One, point.Z - NInt.One);
    }

    /// <summary>
    /// Get the neighbors in diagonal directions. The eight coordinates sharing only a vertex with the
    /// point, that is the ones offset in all three axes.
    /// </summary>
    public static IEnumerable<(NInt X, NInt Y, NInt Z)> DiagonalNeighbors<NInt>((NInt X, NInt Y, NInt Z) point)
        where NInt : IBinaryInteger<NInt>
    {
        yield return (point.X + NInt.One, point.Y + NInt.One, point.Z + NInt.One);
        yield return (point.X + NInt.One, point.Y + NInt.One, point.Z - NInt.One);
        yield return (point.X + NInt.One, point.Y - NInt.One, point.Z + NInt.One);
        yield return (point.X + NInt.One, point.Y - NInt.One, point.Z - NInt.One);
        yield return (point.X - NInt.One, point.Y + NInt.One, point.Z + NInt.One);
        yield return (point.X - NInt.One, point.Y + NInt.One, point.Z - NInt.One);
        yield return (point.X - NInt.One, point.Y - NInt.One, point.Z + NInt.One);
        yield return (point.X - NInt.One, point.Y - NInt.One, point.Z - NInt.One);
    }

    /// <summary>
    /// Euclidean distance of the point and origin.
    /// </summary>
    /// <remarks>
    /// <a href="https://en.wikipedia.org/wiki/Euclidean_distance">Wikipedia</a>
    /// </remarks>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static N Distance<N>((N X, N Y, N Z) point)
        where N : IRootFunctions<N>
        => PT.Hypotenuse(point);

    /// <summary>
    /// Euclidean distance of two points.
    /// </summary>
    /// <remarks>
    /// <a href="https://en.wikipedia.org/wiki/Euclidean_distance">Wikipedia</a>
    /// </remarks>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static N Distance<N>((N X, N Y, N Z) point1, (N X, N Y, N Z) point2)
        where N : IRootFunctions<N>
        => PT.Hypotenuse(point2.X - point1.X, point2.Y - point1.Y, point2.Z - point1.Z);

    /// <summary>
    /// Manhattan or taxicab distance of point and origin.
    /// </summary>
    /// <remarks>
    /// <a href="https://en.wikipedia.org/wiki/Taxicab_geometry">Wikipedia</a>
    /// </remarks>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static N ManhattanDistance<N>((N X, N Y, N Z) point)
        where N : INumberBase<N>
        => N.Abs(point.X) + N.Abs(point.Y) + N.Abs(point.Z);

    /// <summary>
    /// Manhattan or taxicab distance of two points.
    /// </summary>
    /// <remarks>
    /// <a href="https://en.wikipedia.org/wiki/Taxicab_geometry">Wikipedia</a>
    /// </remarks>
    public static N ManhattanDistance<N>((N X, N Y, N Z) point1, (N X, N Y, N Z) point2)
        where N : INumberBase<N>
        => N.Abs(point1.X - point2.X) + N.Abs(point1.Y - point2.Y) + N.Abs(point1.Z - point2.Z);

    /// <summary>
    /// Chebyshev distance of point and origin
    /// </summary>
    /// <remarks>
    /// <a href="https://en.wikipedia.org/wiki/Chebyshev_distance">Wikipedia</a>
    /// </remarks>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static N ChebyshevDistance<N>((N X, N Y, N Z) point)
        where N : INumber<N>
        => N.Max(N.Max(N.Abs(point.X), N.Abs(point.Y)), N.Abs(point.Z));

    /// <summary>
    /// Chebyshev distance of two points.
    /// </summary>
    /// <remarks>
    /// <a href="https://en.wikipedia.org/wiki/Chebyshev_distance">Wikipedia</a>
    /// </remarks>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static N ChebyshevDistance<N>((N X, N Y, N Z) point1, (N X, N Y, N Z) point2)
        where N : INumber<N>
        => N.Max(N.Max(N.Abs(point1.X - point2.X), N.Abs(point1.Y - point2.Y)), N.Abs(point1.Z - point2.Z));

    /// <summary>
    /// Minkowski distance of point and origin.
    /// </summary>
    /// <remarks>
    /// <a href="https://en.wikipedia.org/wiki/Minkowski_distance">Wikipedia</a>
    /// </remarks>
    public static N MinkowskiDistance<N>((N X, N Y, N Z) point, N r)
        where N : IPowerFunctions<N>
        => N.Pow(N.Pow(N.Abs(point.X), r) + N.Pow(N.Abs(point.Y), r) + N.Pow(N.Abs(point.Z), r), N.One / r);

    /// <summary>
    /// Minkowski distance of two points.
    /// </summary>
    /// <remarks>
    /// <a href="https://en.wikipedia.org/wiki/Minkowski_distance">Wikipedia</a>
    /// </remarks>
    public static N MinkowskiDistance<N>((N X, N Y, N Z) point1, (N X, N Y, N Z) point2, N r)
        where N : IPowerFunctions<N>
        => N.Pow(
              N.Pow(N.Abs(point1.X - point2.X), r)
            + N.Pow(N.Abs(point1.Y - point2.Y), r)
            + N.Pow(N.Abs(point1.Z - point2.Z), r),
            N.One / r);

    // not implemented yet: Canberra distance of point and origin
    // https://en.wikipedia.org/wiki/Canberra_distance

    /// <summary>
    /// Canberra distance of two points.
    /// </summary>
    /// <remarks>
    /// A term whose numerator and denominator are both zero contributes zero,
    /// otherwise any coordinate shared as zero would poison the whole sum with NaN.
    /// <a href="https://en.wikipedia.org/wiki/Canberra_distance">Wikipedia</a>
    /// </remarks>
    public static N CanberraDistance<N>((N X, N Y, N Z) point1, (N X, N Y, N Z) point2)
        where N : INumberBase<N>
        => CanberraTerm(point1.X, point2.X)
         + CanberraTerm(point1.Y, point2.Y)
         + CanberraTerm(point1.Z, point2.Z);

    private static N CanberraTerm<N>(N a, N b)
        where N : INumberBase<N>
    {
        var denominator = N.Abs(a) + N.Abs(b);
        return N.IsZero(denominator)
            ? N.Zero
            : N.Abs(a - b) / denominator;
    }

    // not implemented yet: Bray–Curtis dissimilarity of point and origin, and of two points
    // https://en.wikipedia.org/wiki/Bray%E2%80%93Curtis_dissimilarity

    /// <summary>
    /// Get all coordinates at exact Manhattan or taxicab distance from the center point.
    /// They form the surface of an octahedron.
    /// </summary>
    /// <remarks>
    /// <a href="https://en.wikipedia.org/wiki/Taxicab_geometry">Wikipedia</a>
    /// </remarks>
    public static IEnumerable<(NInt X, NInt Y, NInt Z)> CoordinatesAtManhattanDistance<NInt>(
        (NInt X, NInt Y, NInt Z) center, NInt distance)
        where NInt : IBinaryInteger<NInt>
    {
        if (distance < NInt.One)
            yield break;

        for (var dz = -distance; dz <= distance; dz++)
        {
            var z = center.Z + dz;
            var radius = distance - NInt.Abs(dz); // taxicab circle radius within the layer

            for (var dy = -radius; dy <= radius; dy++)
            {
                var dx = radius - NInt.Abs(dy);
                var y = center.Y + dy;

                yield return (center.X - dx, y, z);
                if (dx != NInt.Zero) // dx == 0 would yield the very same coordinate twice
                    yield return (center.X + dx, y, z);
            }
        }
    }

    /// <summary>
    /// Get all coordinates at exact Manhattan or taxicab distance from the center point limited by bounds.
    /// </summary>
    /// <remarks>
    /// <a href="https://en.wikipedia.org/wiki/Taxicab_geometry">Wikipedia</a>
    /// </remarks>
    public static IEnumerable<(NInt X, NInt Y, NInt Z)> CoordinatesAtManhattanDistance<NInt>(
        (NInt X, NInt Y, NInt Z) center, NInt distance,
        (NInt X, NInt Y, NInt Z) bottomLimit, (NInt X, NInt Y, NInt Z) topLimit)
        where NInt : IBinaryInteger<NInt>
    {
        if (distance < NInt.One)
            yield break;

        // the limits clamp the traversed range, so no coordinate outside of them is ever visited
        var maxZ = NInt.Min(center.Z + distance, topLimit.Z);
        for (var z = NInt.Max(center.Z - distance, bottomLimit.Z); z <= maxZ; z++)
        {
            var radius = distance - NInt.Abs(z - center.Z);

            var maxY = NInt.Min(center.Y + radius, topLimit.Y);
            for (var y = NInt.Max(center.Y - radius, bottomLimit.Y); y <= maxY; y++)
            {
                var dx = radius - NInt.Abs(y - center.Y);
                var x1 = center.X - dx;
                var x2 = center.X + dx;

                if (x1 >= bottomLimit.X && x1 <= topLimit.X)
                    yield return (x1, y, z);
                if (x2 != x1 && x2 >= bottomLimit.X && x2 <= topLimit.X) // skip the duplicate at dx == 0
                    yield return (x2, y, z);
            }
        }
    }

    /// <summary>
    /// Get all coordinates up to the Manhattan or taxicab distance from the center point.
    /// They form a solid octahedron, center is included.
    /// </summary>
    /// <remarks>
    /// <a href="https://en.wikipedia.org/wiki/Taxicab_geometry">Wikipedia</a>
    /// </remarks>
    public static IEnumerable<(NInt X, NInt Y, NInt Z)> CoordinatesUpToManhattanDistance<NInt>(
        (NInt X, NInt Y, NInt Z) center, NInt distance)
        where NInt : IBinaryInteger<NInt>
    {
        if (distance < NInt.Zero)
            yield break;

        for (var dz = -distance; dz <= distance; dz++)
        {
            var z = center.Z + dz;
            var radius = distance - NInt.Abs(dz);

            for (var dy = -radius; dy <= radius; dy++)
            {
                var y = center.Y + dy;
                var dxMax = radius - NInt.Abs(dy);

                // the bounds are hoisted out of the loop condition, an iterator holds its locals
                // in fields and would reevaluate the expression for every single coordinate
                var highX = center.X + dxMax;
                for (var x = center.X - dxMax; x <= highX; x++)
                    yield return (x, y, z);
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
    public static IEnumerable<(NInt X, NInt Y, NInt Z)> CoordinatesUpToManhattanDistance<NInt>(
        (NInt X, NInt Y, NInt Z) center, NInt distance,
        (NInt X, NInt Y, NInt Z) bottomLimit, (NInt X, NInt Y, NInt Z) topLimit)
        where NInt : IBinaryInteger<NInt>
    {
        if (distance < NInt.Zero)
            yield break;

        // the limits clamp the traversed ranges, so every visited coordinate is also yielded
        var maxZ = NInt.Min(center.Z + distance, topLimit.Z);
        for (var z = NInt.Max(center.Z - distance, bottomLimit.Z); z <= maxZ; z++)
        {
            var radius = distance - NInt.Abs(z - center.Z);

            var maxY = NInt.Min(center.Y + radius, topLimit.Y);
            for (var y = NInt.Max(center.Y - radius, bottomLimit.Y); y <= maxY; y++)
            {
                var dxMax = radius - NInt.Abs(y - center.Y);

                var maxX = NInt.Min(center.X + dxMax, topLimit.X);
                for (var x = NInt.Max(center.X - dxMax, bottomLimit.X); x <= maxX; x++)
                    yield return (x, y, z);
            }
        }
    }

    /// <summary>
    /// Get all coordinates whose Manhattan or taxicab distance from the center point
    /// falls within the inclusive range [<paramref name="minDistance"/>, <paramref name="maxDistance"/>].
    /// </summary>
    /// <remarks>
    /// <a href="https://en.wikipedia.org/wiki/Taxicab_geometry">Wikipedia</a>
    /// </remarks>
    public static IEnumerable<(NInt X, NInt Y, NInt Z)> CoordinatesInManhattanDistanceRange<NInt>(
        (NInt X, NInt Y, NInt Z) center, NInt minDistance, NInt maxDistance)
        where NInt : IBinaryInteger<NInt>
    {
        if (maxDistance < NInt.Zero || maxDistance < minDistance)
            yield break;

        var min = NInt.Max(minDistance, NInt.Zero);

        for (var dz = -maxDistance; dz <= maxDistance; dz++)
        {
            var z = center.Z + dz;
            var absDz = NInt.Abs(dz);
            var radius = maxDistance - absDz;

            for (var dy = -radius; dy <= radius; dy++)
            {
                var y = center.Y + dy;
                var absDy = NInt.Abs(dy);
                var dxMax = radius - absDy;
                var lowX = center.X - dxMax;
                var highX = center.X + dxMax;

                // the row is inside the minimum only where |dx| < inner, a contiguous gap around
                // the center, so the row is emitted as two runs instead of being filtered
                var inner = min - absDz - absDy;
                if (inner <= NInt.Zero)
                {
                    for (var x = lowX; x <= highX; x++)
                        yield return (x, y, z);
                }
                else
                {
                    var lowEnd = center.X - inner;
                    for (var x = lowX; x <= lowEnd; x++)
                        yield return (x, y, z);

                    var highStart = center.X + inner;
                    for (var x = highStart; x <= highX; x++)
                        yield return (x, y, z);
                }
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
    public static IEnumerable<(NInt X, NInt Y, NInt Z)> CoordinatesInManhattanDistanceRange<NInt>(
        (NInt X, NInt Y, NInt Z) center, NInt minDistance, NInt maxDistance,
        (NInt X, NInt Y, NInt Z) bottomLimit, (NInt X, NInt Y, NInt Z) topLimit)
        where NInt : IBinaryInteger<NInt>
    {
        if (maxDistance < NInt.Zero || maxDistance < minDistance)
            yield break;

        var min = NInt.Max(minDistance, NInt.Zero);

        var maxZ = NInt.Min(center.Z + maxDistance, topLimit.Z);
        for (var z = NInt.Max(center.Z - maxDistance, bottomLimit.Z); z <= maxZ; z++)
        {
            var absDz = NInt.Abs(z - center.Z);
            var radius = maxDistance - absDz;

            var maxY = NInt.Min(center.Y + radius, topLimit.Y);
            for (var y = NInt.Max(center.Y - radius, bottomLimit.Y); y <= maxY; y++)
            {
                var absDy = NInt.Abs(y - center.Y);
                var dxMax = radius - absDy;
                var lowX = NInt.Max(center.X - dxMax, bottomLimit.X);
                var highX = NInt.Min(center.X + dxMax, topLimit.X);

                // the gap where the row is inside the minimum is contiguous, so the row is
                // emitted as two clamped runs instead of being filtered coordinate by coordinate
                var inner = min - absDz - absDy;
                if (inner <= NInt.Zero)
                {
                    for (var x = lowX; x <= highX; x++)
                        yield return (x, y, z);
                }
                else
                {
                    var lowEnd = NInt.Min(center.X - inner, highX);
                    for (var x = lowX; x <= lowEnd; x++)
                        yield return (x, y, z);

                    var highStart = NInt.Max(center.X + inner, lowX);
                    for (var x = highStart; x <= highX; x++)
                        yield return (x, y, z);
                }
            }
        }
    }

    /// <summary>
    /// Get all coordinates at exact Chebyshev distance from the center point.
    /// They form the surface of a cube.
    /// </summary>
    /// <remarks>
    /// <a href="https://en.wikipedia.org/wiki/Chebyshev_distance">Wikipedia</a>
    /// </remarks>
    public static IEnumerable<(NInt X, NInt Y, NInt Z)> CoordinatesAtChebyshevDistance<NInt>(
        (NInt X, NInt Y, NInt Z) center, NInt distance)
        where NInt : IBinaryInteger<NInt>
    {
        if (distance < NInt.One)
            yield break;

        var minX = center.X - distance;
        var maxX = center.X + distance;
        var minY = center.Y - distance;
        var maxY = center.Y + distance;
        var minZ = center.Z - distance;
        var maxZ = center.Z + distance;

        for (var z = minZ; z <= maxZ; z++)
        {
            if (z == minZ || z == maxZ)
            {
                // the caps are filled squares
                for (var x = minX; x <= maxX; x++)
                    for (var y = minY; y <= maxY; y++)
                        yield return (x, y, z);
            }
            else
            {
                // the layers in between contribute their perimeter only
                for (var x = minX; x <= maxX; x++)
                    yield return (x, minY, z);

                for (var y = minY + NInt.One; y < maxY; y++)
                    yield return (maxX, y, z);

                for (var x = maxX; x >= minX; x--)
                    yield return (x, maxY, z);

                for (var y = maxY - NInt.One; y > minY; y--)
                    yield return (minX, y, z);
            }
        }
    }

    /// <summary>
    /// Get all coordinates at exact Chebyshev distance from the center point limited by bounds.
    /// </summary>
    /// <remarks>
    /// <a href="https://en.wikipedia.org/wiki/Chebyshev_distance">Wikipedia</a>
    /// </remarks>
    public static IEnumerable<(NInt X, NInt Y, NInt Z)> CoordinatesAtChebyshevDistance<NInt>(
        (NInt X, NInt Y, NInt Z) center, NInt distance,
        (NInt X, NInt Y, NInt Z) bottomLimit, (NInt X, NInt Y, NInt Z) topLimit)
        where NInt : IBinaryInteger<NInt>
    {
        if (distance < NInt.One)
            yield break;

        var minX = center.X - distance;
        var maxX = center.X + distance;
        var minY = center.Y - distance;
        var maxY = center.Y + distance;
        var minZ = center.Z - distance;
        var maxZ = center.Z + distance;

        // horizontal edges carry the corners, vertical edges are emitted without them
        var edgeMinX = NInt.Max(minX, bottomLimit.X);
        var edgeMaxX = NInt.Min(maxX, topLimit.X);
        var edgeMinY = NInt.Max(minY + NInt.One, bottomLimit.Y);
        var edgeMaxY = NInt.Min(maxY - NInt.One, topLimit.Y);
        var capMinY = NInt.Max(minY, bottomLimit.Y);
        var capMaxY = NInt.Min(maxY, topLimit.Y);

        var lastZ = NInt.Min(maxZ, topLimit.Z);
        for (var z = NInt.Max(minZ, bottomLimit.Z); z <= lastZ; z++)
        {
            if (z == minZ || z == maxZ)
            {
                for (var x = edgeMinX; x <= edgeMaxX; x++)
                    for (var y = capMinY; y <= capMaxY; y++)
                        yield return (x, y, z);
            }
            else
            {
                // bottom edge
                if (minY >= bottomLimit.Y && minY <= topLimit.Y)
                {
                    for (var x = edgeMinX; x <= edgeMaxX; x++)
                        yield return (x, minY, z);
                }

                // right edge
                if (maxX >= bottomLimit.X && maxX <= topLimit.X)
                {
                    for (var y = edgeMinY; y <= edgeMaxY; y++)
                        yield return (maxX, y, z);
                }

                // top edge
                if (maxY >= bottomLimit.Y && maxY <= topLimit.Y)
                {
                    for (var x = edgeMaxX; x >= edgeMinX; x--)
                        yield return (x, maxY, z);
                }

                // left edge
                if (minX >= bottomLimit.X && minX <= topLimit.X)
                {
                    for (var y = edgeMaxY; y >= edgeMinY; y--)
                        yield return (minX, y, z);
                }
            }
        }
    }

    /// <summary>
    /// Get all coordinates up to the Chebyshev distance from the center point.
    /// They form a solid cube, center point is included.
    /// </summary>
    /// <remarks>
    /// <a href="https://en.wikipedia.org/wiki/Chebyshev_distance">Wikipedia</a>
    /// </remarks>
    public static IEnumerable<(NInt X, NInt Y, NInt Z)> CoordinatesUpToChebyshevDistance<NInt>(
        (NInt X, NInt Y, NInt Z) center, NInt distance)
        where NInt : IBinaryInteger<NInt>
    {
        if (distance < NInt.Zero)
            yield break;

        var minX = center.X - distance;
        var maxX = center.X + distance;
        var minY = center.Y - distance;
        var maxY = center.Y + distance;
        var minZ = center.Z - distance;
        var maxZ = center.Z + distance;

        for (var x = minX; x <= maxX; x++)
            for (var y = minY; y <= maxY; y++)
                for (var z = minZ; z <= maxZ; z++)
                    yield return (x, y, z);
    }

    /// <summary>
    /// Get all coordinates up to the Chebyshev distance from the center point limited by bounds.
    /// Center point is included.
    /// </summary>
    /// <remarks>
    /// <a href="https://en.wikipedia.org/wiki/Chebyshev_distance">Wikipedia</a>
    /// </remarks>
    public static IEnumerable<(NInt X, NInt Y, NInt Z)> CoordinatesUpToChebyshevDistance<NInt>(
        (NInt X, NInt Y, NInt Z) center, NInt distance,
        (NInt X, NInt Y, NInt Z) bottomLimit, (NInt X, NInt Y, NInt Z) topLimit)
        where NInt : IBinaryInteger<NInt>
    {
        if (distance < NInt.Zero)
            yield break;

        var minX = NInt.Max(center.X - distance, bottomLimit.X);
        var maxX = NInt.Min(center.X + distance, topLimit.X);
        var minY = NInt.Max(center.Y - distance, bottomLimit.Y);
        var maxY = NInt.Min(center.Y + distance, topLimit.Y);
        var minZ = NInt.Max(center.Z - distance, bottomLimit.Z);
        var maxZ = NInt.Min(center.Z + distance, topLimit.Z);

        for (var x = minX; x <= maxX; x++)
            for (var y = minY; y <= maxY; y++)
                for (var z = minZ; z <= maxZ; z++)
                    yield return (x, y, z);
    }

    /// <summary>
    /// Get all coordinates whose Chebyshev distance from the center point
    /// falls within the inclusive range [<paramref name="minDistance"/>, <paramref name="maxDistance"/>].
    /// </summary>
    /// <remarks>
    /// <a href="https://en.wikipedia.org/wiki/Chebyshev_distance">Wikipedia</a>
    /// </remarks>
    public static IEnumerable<(NInt X, NInt Y, NInt Z)> CoordinatesInChebyshevDistanceRange<NInt>(
        (NInt X, NInt Y, NInt Z) center, NInt minDistance, NInt maxDistance)
        where NInt : IBinaryInteger<NInt>
    {
        if (maxDistance < NInt.Zero || maxDistance < minDistance)
            yield break;

        var min = NInt.Max(minDistance, NInt.Zero);

        var minX = center.X - maxDistance;
        var maxX = center.X + maxDistance;
        var minY = center.Y - maxDistance;
        var maxY = center.Y + maxDistance;
        var minZ = center.Z - maxDistance;
        var maxZ = center.Z + maxDistance;

        // the gap of a row that falls inside the minimum is the same for every row
        var gapEnd = center.Z - min;
        var gapStart = center.Z + min;

        for (var x = minX; x <= maxX; x++)
        {
            // once one axis is at or beyond the minimum, the whole slice or row qualifies and
            // not a single coordinate of it needs its distance evaluated
            var xOutside = NInt.Abs(x - center.X) >= min;

            for (var y = minY; y <= maxY; y++)
            {
                if (xOutside || NInt.Abs(y - center.Y) >= min)
                {
                    for (var z = minZ; z <= maxZ; z++)
                        yield return (x, y, z);
                }
                else
                {
                    // the row crosses the minimum, the part inside it is skipped as a whole
                    for (var z = minZ; z <= gapEnd; z++)
                        yield return (x, y, z);

                    for (var z = gapStart; z <= maxZ; z++)
                        yield return (x, y, z);
                }
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
    public static IEnumerable<(NInt X, NInt Y, NInt Z)> CoordinatesInChebyshevDistanceRange<NInt>(
        (NInt X, NInt Y, NInt Z) center, NInt minDistance, NInt maxDistance,
        (NInt X, NInt Y, NInt Z) bottomLimit, (NInt X, NInt Y, NInt Z) topLimit)
        where NInt : IBinaryInteger<NInt>
    {
        if (maxDistance < NInt.Zero || maxDistance < minDistance)
            yield break;

        var min = NInt.Max(minDistance, NInt.Zero);

        var minX = NInt.Max(center.X - maxDistance, bottomLimit.X);
        var maxX = NInt.Min(center.X + maxDistance, topLimit.X);
        var minY = NInt.Max(center.Y - maxDistance, bottomLimit.Y);
        var maxY = NInt.Min(center.Y + maxDistance, topLimit.Y);
        var minZ = NInt.Max(center.Z - maxDistance, bottomLimit.Z);
        var maxZ = NInt.Min(center.Z + maxDistance, topLimit.Z);

        // the gap of a row that falls inside the minimum is the same for every row,
        // clamped to the limits so that the runs around it stay within them
        var gapEnd = NInt.Min(center.Z - min, maxZ);
        var gapStart = NInt.Max(center.Z + min, minZ);

        for (var x = minX; x <= maxX; x++)
        {
            var xOutside = NInt.Abs(x - center.X) >= min;

            for (var y = minY; y <= maxY; y++)
            {
                if (xOutside || NInt.Abs(y - center.Y) >= min)
                {
                    for (var z = minZ; z <= maxZ; z++)
                        yield return (x, y, z);
                }
                else
                {
                    for (var z = minZ; z <= gapEnd; z++)
                        yield return (x, y, z);

                    for (var z = gapStart; z <= maxZ; z++)
                        yield return (x, y, z);
                }
            }
        }
    }

    /// <summary>
    /// Count of coordinates at exact Manhattan distance from a center point. Unbounded.
    /// </summary>
    /// <remarks> The taxicab sphere, an octahedron surface, has 4*d^2 + 2 coordinates, distance 0 gives the center only. </remarks>
    public static NInt ManhattanSphereCount<NInt>(NInt distance)
        where NInt : IBinaryInteger<NInt>
        => distance < NInt.Zero
            ? NInt.Zero
            : distance == NInt.Zero
                ? NInt.One
                : NInt.CreateChecked(4) * distance * distance + NInt.CreateChecked(2);

    /// <summary>
    /// Count of coordinates up to the Manhattan distance from a center point, center included. Unbounded.
    /// </summary>
    /// <remarks> The taxicab ball, a solid octahedron, has (2*d + 1) * (2*d^2 + 2*d + 3) / 3 coordinates. </remarks>
    public static NInt ManhattanBallCount<NInt>(NInt distance)
        where NInt : IBinaryInteger<NInt>
    {
        if (distance < NInt.Zero)
            return NInt.Zero;

        var two = NInt.CreateChecked(2);
        return (two * distance + NInt.One) * (two * distance * distance + two * distance + NInt.CreateChecked(3))
            / NInt.CreateChecked(3);
    }

    /// <summary>
    /// Count of coordinates at exact Chebyshev distance from a center point. Unbounded.
    /// </summary>
    /// <remarks> The Chebyshev shell, a cube surface, has 24*d^2 + 2 coordinates, distance 0 gives the center only. </remarks>
    public static NInt ChebyshevShellCount<NInt>(NInt distance)
        where NInt : IBinaryInteger<NInt>
        => distance < NInt.Zero
            ? NInt.Zero
            : distance == NInt.Zero
                ? NInt.One
                : NInt.CreateChecked(24) * distance * distance + NInt.CreateChecked(2);

    /// <summary>
    /// Count of coordinates up to the Chebyshev distance from a center point, center included. Unbounded.
    /// </summary>
    /// <remarks> The Chebyshev cube has (2*d + 1)^3 coordinates. </remarks>
    public static NInt ChebyshevCubeCount<NInt>(NInt distance)
        where NInt : IBinaryInteger<NInt>
    {
        if (distance < NInt.Zero)
            return NInt.Zero;

        var side = NInt.CreateChecked(2) * distance + NInt.One;
        return side * side * side;
    }

    /// <summary>
    /// Write all coordinates at exact Manhattan distance from the center point into a destination buffer.
    /// Allocation free alternative to the enumerable overload.
    /// </summary>
    /// <returns> Count of written coordinates. </returns>
    /// <exception cref="ArgumentException"> Destination is too short. </exception>
    public static int CoordinatesAtManhattanDistance<NInt>((NInt X, NInt Y, NInt Z) center, NInt distance,
        Span<(NInt X, NInt Y, NInt Z)> destination)
        where NInt : IBinaryInteger<NInt>
    {
        if (distance < NInt.One)
            return 0;

        // the count is known up front, so the capacity is checked once instead of on every write
        if (NInt.CreateSaturating(destination.Length) < ManhattanSphereCount(distance))
            throw new ArgumentException("Destination is too short.", nameof(destination));

        var count = 0;
        for (var dz = -distance; dz <= distance; dz++)
        {
            var z = center.Z + dz;
            var radius = distance - NInt.Abs(dz);

            for (var dy = -radius; dy <= radius; dy++)
            {
                var dx = radius - NInt.Abs(dy);
                var y = center.Y + dy;

                destination[count++] = (center.X - dx, y, z);

                if (dx != NInt.Zero) // dx == 0 would write the very same coordinate twice
                    destination[count++] = (center.X + dx, y, z);
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
    public static int CoordinatesUpToManhattanDistance<NInt>((NInt X, NInt Y, NInt Z) center, NInt distance,
        Span<(NInt X, NInt Y, NInt Z)> destination)
        where NInt : IBinaryInteger<NInt>
    {
        if (distance < NInt.Zero)
            return 0;

        if (NInt.CreateSaturating(destination.Length) < ManhattanBallCount(distance))
            throw new ArgumentException("Destination is too short.", nameof(destination));

        var count = 0;
        for (var dz = -distance; dz <= distance; dz++)
        {
            var z = center.Z + dz;
            var radius = distance - NInt.Abs(dz);

            for (var dy = -radius; dy <= radius; dy++)
            {
                var y = center.Y + dy;
                var dxMax = radius - NInt.Abs(dy);
                for (var x = center.X - dxMax; x <= center.X + dxMax; x++)
                    destination[count++] = (x, y, z);
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
    public static int CoordinatesUpToManhattanDistance<NInt>((NInt X, NInt Y, NInt Z) center, NInt distance,
        (NInt X, NInt Y, NInt Z) bottomLimit, (NInt X, NInt Y, NInt Z) topLimit,
        Span<(NInt X, NInt Y, NInt Z)> destination)
        where NInt : IBinaryInteger<NInt>
    {
        if (distance < NInt.Zero)
            return 0;

        var count = 0;
        var maxZ = NInt.Min(center.Z + distance, topLimit.Z);
        for (var z = NInt.Max(center.Z - distance, bottomLimit.Z); z <= maxZ; z++)
        {
            var radius = distance - NInt.Abs(z - center.Z);

            var maxY = NInt.Min(center.Y + radius, topLimit.Y);
            for (var y = NInt.Max(center.Y - radius, bottomLimit.Y); y <= maxY; y++)
            {
                var dxMax = radius - NInt.Abs(y - center.Y);

                var lowX = NInt.Max(center.X - dxMax, bottomLimit.X);
                var highX = NInt.Min(center.X + dxMax, topLimit.X);

                // the run length is known, so the capacity is checked once per run
                var runLength = highX >= lowX ? highX - lowX + NInt.One : NInt.Zero;
                if (NInt.CreateSaturating(destination.Length - count) < runLength)
                    throw new ArgumentException("Destination is too short.", nameof(destination));

                for (var x = lowX; x <= highX; x++)
                    destination[count++] = (x, y, z);
            }
        }

        return count;
    }

    /// <summary>
    /// Write all coordinates whose Manhattan distance from the center point falls within the inclusive
    /// range [<paramref name="minDistance"/>, <paramref name="maxDistance"/>] into a destination buffer.
    /// Allocation free alternative to the enumerable overload.
    /// </summary>
    /// <remarks>
    /// Size the buffer with <c>ManhattanBallCount(maxDistance) - ManhattanBallCount(minDistance - 1)</c>.
    /// </remarks>
    /// <returns> Count of written coordinates. </returns>
    /// <exception cref="ArgumentException"> Destination is too short. </exception>
    public static int CoordinatesInManhattanDistanceRange<NInt>((NInt X, NInt Y, NInt Z) center,
        NInt minDistance, NInt maxDistance, Span<(NInt X, NInt Y, NInt Z)> destination)
        where NInt : IBinaryInteger<NInt>
    {
        if (maxDistance < NInt.Zero || maxDistance < minDistance)
            return 0;

        var min = NInt.Max(minDistance, NInt.Zero);

        // the ball up to the minimum is the part that is left out, so the count is known up front
        if (NInt.CreateSaturating(destination.Length) < ManhattanBallCount(maxDistance) - ManhattanBallCount(min - NInt.One))
            throw new ArgumentException("Destination is too short.", nameof(destination));

        var count = 0;
        for (var dz = -maxDistance; dz <= maxDistance; dz++)
        {
            var z = center.Z + dz;
            var absDz = NInt.Abs(dz);
            var radius = maxDistance - absDz;

            for (var dy = -radius; dy <= radius; dy++)
            {
                var y = center.Y + dy;
                var absDy = NInt.Abs(dy);
                var dxMax = radius - absDy;

                var inner = min - absDz - absDy;
                if (inner <= NInt.Zero)
                {
                    for (var x = center.X - dxMax; x <= center.X + dxMax; x++)
                        destination[count++] = (x, y, z);
                }
                else
                {
                    for (var x = center.X - dxMax; x <= center.X - inner; x++)
                        destination[count++] = (x, y, z);

                    for (var x = center.X + inner; x <= center.X + dxMax; x++)
                        destination[count++] = (x, y, z);
                }
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
    public static int CoordinatesUpToChebyshevDistance<NInt>((NInt X, NInt Y, NInt Z) center, NInt distance,
        Span<(NInt X, NInt Y, NInt Z)> destination)
        where NInt : IBinaryInteger<NInt>
    {
        if (distance < NInt.Zero)
            return 0;

        if (NInt.CreateSaturating(destination.Length) < ChebyshevCubeCount(distance))
            throw new ArgumentException("Destination is too short.", nameof(destination));

        var minX = center.X - distance;
        var maxX = center.X + distance;
        var minY = center.Y - distance;
        var maxY = center.Y + distance;
        var minZ = center.Z - distance;
        var maxZ = center.Z + distance;

        var count = 0;
        for (var x = minX; x <= maxX; x++)
            for (var y = minY; y <= maxY; y++)
                for (var z = minZ; z <= maxZ; z++)
                    destination[count++] = (x, y, z);

        return count;
    }
}
