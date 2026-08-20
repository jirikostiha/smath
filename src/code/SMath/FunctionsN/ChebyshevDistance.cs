using System.Numerics;

namespace SMath.FunctionsN;

/// <summary>
/// Chebyshev, chessboard or diagonal distance in n dimensions.
/// </summary>
/// <remarks>
/// It is the number of moves a king needs on a chessboard, therefore it is also
/// the number of steps of a diagonal walk in a grid.
/// The distance of an empty point is zero.
/// <a href="https://en.wikipedia.org/wiki/Chebyshev_distance">Wikipedia</a>
/// </remarks>
public static class ChebyshevDistance
{
    /// <summary>
    /// String representation of the function.
    /// </summary>
    public static string PlainTextFormula
        => "max(|xi|)";

    /// <summary>
    /// Chebyshev distance of a point and the origin.
    /// </summary>
    public static N Eval<N>(IEnumerable<N> point)
        where N : INumber<N>
    {
        var maximum = N.Zero;
        foreach (var coordinate in point)
            maximum = N.Max(maximum, N.Abs(coordinate));

        return maximum;
    }

    /// <summary>
    /// Chebyshev distance of a point and the origin.
    /// </summary>
    public static N Eval<N>(ReadOnlySpan<N> point)
        where N : INumber<N>
    {
        var maximum = N.Zero;
        for (int i = 0; i < point.Length; i++)
            maximum = N.Max(maximum, N.Abs(point[i]));

        return maximum;
    }

    /// <summary>
    /// Chebyshev distance of two points.
    /// </summary>
    public static N Eval<N>(IEnumerable<N> point1, IEnumerable<N> point2)
        where N : INumber<N>
    {
        var maximum = N.Zero;

        using (var enumerator1 = point1.GetEnumerator())
        using (var enumerator2 = point2.GetEnumerator())
        {
            while (true)
            {
                var moved1 = enumerator1.MoveNext();
                var moved2 = enumerator2.MoveNext();

                // one sequence ran out sooner than the other
                if (moved1 != moved2)
                    throw new ArgumentException("Inconsistent length of sequences.");

                if (!moved1)
                    break;

                maximum = N.Max(maximum, N.Abs(enumerator1.Current - enumerator2.Current));
            }
        }

        return maximum;
    }

    /// <summary>
    /// Chebyshev distance of two points.
    /// </summary>
    public static N Eval<N>(ReadOnlySpan<N> point1, ReadOnlySpan<N> point2)
        where N : INumber<N>
    {
        if (point1.Length != point2.Length)
            throw new ArgumentException("Inconsistent length of sequences.");

        var maximum = N.Zero;
        for (int i = 0; i < point1.Length; i++)
            maximum = N.Max(maximum, N.Abs(point1[i] - point2[i]));

        return maximum;
    }
}
