using System.Numerics;

namespace SMath.Sequences
{
    /// <summary>
    /// Arithmetic sequence or arithmetic progression.
    /// </summary>
    /// <remarks>
    /// <a href="https://en.wikipedia.org/wiki/Arithmetic_progression">wikipedia</a>
    /// <a href="https://www.mathsisfun.com/algebra/sequences-sums-arithmetic.html">math is fun</a>
    /// </remarks>
    public static class ArithmeticSequence
    {
        /// <summary>
        /// Get specific term.
        /// </summary>
        public static N Term<N,NInt>(N initial, N diff, NInt n)
            where N : INumberBase<N>
            where NInt : IBinaryInteger<NInt>
            => initial + N.CreateChecked(n - NInt.One) * diff;

        public static IEnumerable<N> Terms<N, NInt>(N initial, N diff, NInt count)
            where N : INumberBase<N>
            where NInt : IBinaryInteger<NInt>, IComparisonOperators<NInt, NInt, bool>
        {
            for (NInt n = NInt.One; n <= count; n++)
                yield return Term(initial, diff, n);
        }
    }
}
