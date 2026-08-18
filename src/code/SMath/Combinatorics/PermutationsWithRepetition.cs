using System.Numerics;

namespace SMath.Combinatorics;

/// <summary>
/// Permutations with repetition.
/// Order does matter, e.g. {a,b}: a, b, aa, ab, ba, bb.
/// </summary>
/// <remarks>
/// <a href="https://en.wikipedia.org/wiki/Permutation#Permutations_with_repetition">Wikipedia</a>
/// </remarks>
public static class PermutationsWithRepetition
{
    /// <summary>
    /// Counts k-tuples drawn with repetition from <paramref name="n"/> elements (n^k).
    /// </summary>
    public static NInt Count<NInt>(NInt n, NInt k)
        where NInt : IBinaryInteger<NInt>, IComparisonOperators<NInt, NInt, bool>
        => n > NInt.Zero && k > NInt.Zero
            ? n.Pow(k)
            : NInt.Zero;
}
