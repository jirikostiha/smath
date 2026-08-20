using System.Numerics;
using SMath.Expansions;

namespace SMath.Combinatorics;

/// <summary>
/// Combinations with repetition.
/// Order does not matter, e.g. {a,b}: a, b, aa, ab, bb.
/// </summary>
/// <remarks>
/// <a href="https://en.wikipedia.org/wiki/Combination#Number_of_combinations_with_repetition">Wikipedia</a>
/// <a href="https://mathworld.wolfram.com/Multiset.html">Wolfram Mathworld</a>
/// </remarks>
public static class CombinationsWithRepetition
{
    /// <summary>
    /// Counts k-multisets drawn from <paramref name="n"/> elements.
    /// </summary>
    /// <remarks> C(n + k - 1, k). </remarks>
    public static NInt Count<NInt>(NInt n, NInt k)
        where NInt : IBinaryInteger<NInt>
        => !NInt.IsNegative(n) && !NInt.IsNegative(k) && (n > NInt.Zero || k == NInt.Zero)
            ? (k == NInt.Zero ? NInt.One : BinomialCoefficient.Eval(n + k - NInt.One, k))
            : NInt.Zero;

    /// <summary>
    /// Enumerate all k element index multisets drawn from [0, n), in lexicographic order.
    /// Indices within a multiset do not descend.
    /// </summary>
    /// <remarks> Every multiset is a newly allocated array. </remarks>
    public static IEnumerable<NInt[]> Tuples<NInt>(NInt n, NInt k)
        where NInt : IBinaryInteger<NInt>
    {
        if (n <= NInt.Zero || k <= NInt.Zero)
            yield break;

        int size = int.CreateChecked(k);
        int lastIndex = int.CreateChecked(n) - 1;
        var indices = new int[size];

        while (true)
        {
            var tuple = new NInt[size];
            for (int i = 0; i < size; i++)
                tuple[i] = NInt.CreateChecked(indices[i]);

            yield return tuple;

            // advance the rightmost index which has not reached its maximum yet
            int pivot = size - 1;
            while (pivot >= 0 && indices[pivot] == lastIndex)
                pivot--;

            if (pivot < 0)
                yield break;

            indices[pivot]++;
            for (int i = pivot + 1; i < size; i++)
                indices[i] = indices[pivot];
        }
    }
}
