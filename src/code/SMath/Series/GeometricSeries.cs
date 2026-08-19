using System.Numerics;
using SMath.Sequences;

namespace SMath.Series;

/// <summary>
/// Geometric series, the partial sums of a geometric sequence.
/// </summary>
/// <remarks>
/// <a href="https://en.wikipedia.org/wiki/Geometric_series">Wikipedia</a>
/// <a href="https://mathworld.wolfram.com/GeometricSeries.html">Wolfram Mathworld</a>
/// </remarks>
public static class GeometricSeries
{
    public static string PlainTextFormula => "s(n) = a(1) * (r^n - 1) / (r - 1)";

    /// <summary>
    /// Sum of the first <paramref name="n"/> terms of <see cref="GeometricSequence"/>.
    /// </summary>
    public static N Term<N>(N initial, N ratio, int n)
        where N : INumberBase<N>
    {
        if (n <= 0)
            return N.Zero;

        // the closed form divides by zero for the ratio of one, the sum is a plain multiple then
        if (ratio == N.One)
            return N.CreateChecked(n) * initial;

        // the quotient is exact in integer arithmetic and is taken first to keep the range wide,
        // both differences are written so that they do not go negative for an unsigned number type
        return initial * ((GeometricSequence.Power(ratio, n) - N.One) / (ratio - N.One));
    }

    /// <summary>
    /// Enumerate the first <paramref name="count"/> partial sums.
    /// </summary>
    public static IEnumerable<N> Terms<N>(N initial, N ratio, int count)
        where N : INumberBase<N>
    {
        for (int n = 1; n <= count; n++)
            yield return Term(initial, ratio, n);
    }

    /// <summary>
    /// Write the first <paramref name="count"/> partial sums into a destination buffer.
    /// Allocation free alternative to the enumerable overload.
    /// </summary>
    /// <returns> Count of written terms. </returns>
    /// <exception cref="ArgumentException"> Destination is too short. </exception>
    public static int Terms<N>(N initial, N ratio, int count, Span<N> destination)
        where N : INumberBase<N>
    {
        if (count <= 0)
            return 0;

        if (destination.Length < count)
            throw new ArgumentException("Destination is too short.", nameof(destination));

        for (int n = 1; n <= count; n++)
            destination[n - 1] = Term(initial, ratio, n);

        return count;
    }

    /// <summary>
    /// Sum of all terms of an infinite geometric series.
    /// </summary>
    /// <remarks> The series converges for an absolute value of the ratio lower than one only. </remarks>
    public static N Limit<N>(N initial, N ratio)
        where N : INumberBase<N>
        => initial / (N.One - ratio);
}
