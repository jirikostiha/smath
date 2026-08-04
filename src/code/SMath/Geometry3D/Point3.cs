using System.Numerics;
using System.Runtime.CompilerServices;

namespace SMath.Geometry3D;

/// <summary>
/// Point in three dimensions.
/// </summary>
public static class Point3
{
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
    public static N ChebyshevDistance<N>((N X, N Y, N Z) point)
        where N : INumber<N>
        => N.Max(N.Max(N.Abs(point.X), N.Abs(point.Y)), N.Abs(point.Z));

    /// <summary>
    /// Chebyshev distance of two points.
    /// </summary>
    /// <remarks>
    /// <a href="https://en.wikipedia.org/wiki/Chebyshev_distance">Wikipedia</a>
    /// </remarks>
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
}
