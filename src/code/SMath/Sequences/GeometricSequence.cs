using System.Numerics;

namespace SMath.Sequences
{
    /// <summary>
    /// Geometric sequence or geometric progression
    /// </summary>
    /// <remarks>
    /// <a href="https://en.wikipedia.org/wiki/Geometric_progression">wikipedia</a>
    /// </remarks>
    public static class GeometricSequence
    {
        /// <summary>
        /// Specific term.
        /// </summary>
        public static N Term<N, NInt>(N initial, N ratio, NInt n)
            where N : IPowerFunctions<N>
            where NInt : IUnsignedNumber<NInt>
            => initial * N.Pow(ratio, N.CreateChecked(n - NInt.One));

        public static IEnumerable<N> Terms<N, NInt>(N initial, N ratio, NInt count)
            where N : IPowerFunctions<N>
            where NInt : IUnsignedNumber<NInt>, IComparisonOperators<NInt, NInt, bool>
        {
            for (var n = NInt.One; n <= count; n++)
                yield return Term(initial, ratio, n);
        }
    }
}
