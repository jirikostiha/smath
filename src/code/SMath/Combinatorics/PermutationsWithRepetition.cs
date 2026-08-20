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
    /// Counts k-tuples drawn with repetition from <paramref name="n"/> elements.
    /// </summary>
    /// <remarks> n^k. </remarks>
    public static NInt Count<NInt>(NInt n, NInt k)
        where NInt : IBinaryInteger<NInt>
        => n > NInt.Zero && k > NInt.Zero
            ? n.Pow(k)
            : NInt.Zero;

    /// <summary>
    /// Enumerate all k element index tuples drawn with repetition from [0, n),
    /// in lexicographic order.
    /// </summary>
    /// <remarks> Every tuple is a newly allocated array. </remarks>
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

            // odometer step, the rightmost index which has not reached its maximum yet
            int pivot = size - 1;
            while (pivot >= 0 && indices[pivot] == lastIndex)
                pivot--;

            if (pivot < 0)
                yield break;

            indices[pivot]++;
            for (int i = pivot + 1; i < size; i++)
                indices[i] = 0;
        }
    }
}
