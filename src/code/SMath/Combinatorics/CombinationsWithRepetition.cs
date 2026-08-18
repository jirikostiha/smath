using System.Numerics;

namespace SMath.Combinatorics;

/// <summary>
/// Combinations with repetition.
/// Order does not matter, e.g. {a,b}: a, b, aa, ab, bb.
/// </summary>
/// <remarks>
/// <a href="https://en.wikipedia.org/wiki/Combination#Number_of_combinations_with_repetition">Wikipedia</a>
/// <a href="https://mathworld.wolfram.com/Multiset.html">Wolfram MathWorld</a>
/// </remarks>
public static class CombinationsWithRepetition
{
    /// <summary>
    /// Counts k-multisets drawn from <paramref name="n"/> elements.
    /// </summary>
    public static NInt Count<NInt>(NInt n, NInt k)
        where NInt : IBinaryInteger<NInt>, IComparisonOperators<NInt, NInt, bool>
        => n > NInt.Zero && k > NInt.Zero
            ? Factorial.Eval(n + k - NInt.One) / (Factorial.Eval(k) * Factorial.Eval(n - NInt.One))
            : NInt.Zero;
}
