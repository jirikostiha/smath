using System.Numerics;
using SMath.Expansions;

namespace SMath.Combinatorics;

/// <summary>
/// Combinations without repetition.
/// Order does not matter, e.g. {a,b}: a, b, ab.
/// </summary>
/// <remarks>
/// <a href="https://en.wikipedia.org/wiki/Combination">Wikipedia</a>
/// <a href="https://mathworld.wolfram.com/Combination.html">Wolfram Mathworld</a>
/// </remarks>
public static class Combinations
{
    /// <summary>
    /// Counts all non empty combinations (without repetition) of <paramref name="n"/> elements.
    /// </summary>
    /// <remarks> Sum of all k-combinations of n, 2^n - 1. </remarks>
    public static NInt Count<NInt>(NInt n)
        where NInt : IBinaryInteger<NInt>
        => n <= NInt.Zero
            ? NInt.Zero
            : (NInt.One << int.CreateChecked(n)) - NInt.One;

    /// <summary>
    /// Counts k-combinations of <paramref name="n"/>.
    /// </summary>
    public static NInt Count<NInt>(NInt n, NInt k)
        where NInt : IBinaryInteger<NInt>
        => n >= k && k > NInt.Zero
            ? BinomialCoefficient.Eval(n, k)
            : NInt.Zero;

    /// <summary>
    /// Enumerate all index pairs (i &lt; j) drawn from [0, n).
    /// </summary>
    public static IEnumerable<(NInt Index1, NInt Index2)> Tuple2<NInt>(NInt n)
        where NInt : IBinaryInteger<NInt>
    {
        for (var i = NInt.Zero; i < n - NInt.One; i++)
            for (var j = i + NInt.One; j < n; j++)
                yield return (i, j);
    }

    /// <summary>
    /// Enumerate all index triples (i &lt; j &lt; k) drawn from [0, n).
    /// </summary>
    public static IEnumerable<(NInt Index1, NInt Index2, NInt Index3)> Tuple3<NInt>(NInt n)
        where NInt : IBinaryInteger<NInt>
    {
        for (var i = NInt.Zero; i < n - NInt.One; i++)
            for (var j = i + NInt.One; j < n; j++)
                for (var k = j + NInt.One; k < n; k++)
                    yield return (i, j, k);
    }

    /// <summary>
    /// Enumerate all index quadruples (i &lt; j &lt; k &lt; l) drawn from [0, n).
    /// </summary>
    public static IEnumerable<(NInt Index1, NInt Index2, NInt Index3, NInt Index4)> Tuple4<NInt>(NInt n)
        where NInt : IBinaryInteger<NInt>
    {
        for (var i = NInt.Zero; i < n - NInt.One; i++)
            for (var j = i + NInt.One; j < n; j++)
                for (var k = j + NInt.One; k < n; k++)
                    for (var l = k + NInt.One; l < n; l++)
                        yield return (i, j, k, l);
    }

    /// <summary>
    /// Enumerate all k element index combinations drawn from [0, n), in lexicographic order.
    /// Indices within a combination ascend.
    /// </summary>
    /// <remarks>
    /// Generalization of <c>Tuple2</c> to <c>Tuple4</c> for a k known at run time.
    /// Every combination is a newly allocated array, the fixed size overloads allocate nothing.
    /// </remarks>
    public static IEnumerable<NInt[]> Tuples<NInt>(NInt n, NInt k)
        where NInt : IBinaryInteger<NInt>
    {
        if (k <= NInt.Zero || n < k)
            yield break;

        int size = int.CreateChecked(k);
        int lastIndex = int.CreateChecked(n) - 1;
        var indices = new int[size];
        for (int i = 0; i < size; i++)
            indices[i] = i;

        while (true)
        {
            var tuple = new NInt[size];
            for (int i = 0; i < size; i++)
                tuple[i] = NInt.CreateChecked(indices[i]);

            yield return tuple;

            // advance the rightmost index which has not reached its maximum yet
            int pivot = size - 1;
            while (pivot >= 0 && indices[pivot] == lastIndex - (size - 1 - pivot))
                pivot--;

            if (pivot < 0)
                yield break;

            indices[pivot]++;
            for (int i = pivot + 1; i < size; i++)
                indices[i] = indices[i - 1] + 1;
        }
    }
}
