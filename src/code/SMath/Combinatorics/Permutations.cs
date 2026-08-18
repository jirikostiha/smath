using System.Numerics;

namespace SMath.Combinatorics;

/// <summary>
/// Permutations without repetition.
/// Order does matter, e.g. {a,b}: a, b, ab, ba.
/// </summary>
/// <remarks>
/// <a href="https://en.wikipedia.org/wiki/Permutation">Wikipedia</a>
/// <a href="https://mathworld.wolfram.com/Permutation.html">Wolfram MathWorld</a>
/// </remarks>
public static class Permutations
{
    /// <summary>
    /// Counts all permutations of <paramref name="n"/> elements.
    /// </summary>
    public static NInt Count<NInt>(NInt n)
        where NInt : IBinaryInteger<NInt>, IComparisonOperators<NInt, NInt, bool>
    {
        if (n <= NInt.Zero)
            return NInt.Zero;

        var sum = NInt.Zero;
        for (var k = NInt.One; k <= n; k++)
            sum += Count(n, k);

        return sum;
    }

    /// <summary>
    /// Counts k-permutations of <paramref name="n"/>.
    /// </summary>
    /// <remarks>
    /// <a href="https://en.wikipedia.org/wiki/Permutation#k-permutations_of_n">Wikipedia</a>
    /// </remarks>
    public static NInt Count<NInt>(NInt n, NInt k)
        where NInt : IBinaryInteger<NInt>, IComparisonOperators<NInt, NInt, bool>
        => n >= k && k > NInt.Zero
            ? Factorial.Eval(n) / Factorial.Eval(n - k)
            : NInt.Zero;
}
