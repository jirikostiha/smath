using System.Numerics;

namespace SMath.Series;

/// <summary>
/// Arithmetic series (partial sums of an arithmetic sequence).
/// </summary>
/// <remarks>
/// <a href="https://en.wikipedia.org/wiki/Arithmetic_progression#Sum">Wikipedia</a>
/// <a href="https://www.mathsisfun.com/algebra/sequences-sums-arithmetic.html">Math is Fun</a>
/// </remarks>
public static class ArithmeticSeries
{
    /// <summary>
    /// Sum of the first <paramref name="n"/> terms: n * (2 * initial + (n - 1) * diff) / 2.
    /// </summary>
    public static N Term<N, NInt>(N initial, N diff, NInt n)
        where N : INumberBase<N>
        where NInt : IBinaryInteger<NInt>
        => N.CreateChecked(n) * (N.CreateChecked(2) * initial + N.CreateChecked(n - NInt.One) * diff)
            / N.CreateChecked(2);

    /// <summary>
    /// Enumerate the first <paramref name="count"/> partial sums.
    /// </summary>
    public static IEnumerable<N> Terms<N, NInt>(N initial, N diff, NInt count)
        where N : INumberBase<N>
        where NInt : IBinaryInteger<NInt>, IComparisonOperators<NInt, NInt, bool>
    {
        for (var n = NInt.One; n <= count; n++)
            yield return Term(initial, diff, n);
    }
}
