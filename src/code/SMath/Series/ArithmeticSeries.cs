using System.Numerics;

namespace SMath.Series
{
    /// <summary>
    /// Arithmetic series.
    /// </summary>
    /// <remarks>
    /// <a href="https://en.wikipedia.org/wiki/Arithmetic_progression#Sum">wikipedia</a>
    /// <a href="https://www.mathsisfun.com/algebra/sequences-sums-arithmetic.html">math is fun</a>
    /// </remarks>
    public static class ArithmeticSeries
    {
        public static N Term<N, NInt>(N initial, N diff, NInt n)
            where N : INumberBase<N>
            where NInt : IUnsignedNumber<NInt>
            => N.CreateChecked(n / NInt.CreateChecked(2)) * (N.CreateChecked(2) * initial + N.CreateChecked(n - NInt.One) * diff);

        public static IEnumerable<N> Terms<N, NInt>(N initial, N diff, NInt count)
            where N : INumberBase<N>
            where NInt : IUnsignedNumber<NInt>, IComparisonOperators<NInt, NInt, bool>
        {
            for (var n = NInt.One; n <= count; n++)
                yield return Term(initial, diff, n);
        }
    }
}
