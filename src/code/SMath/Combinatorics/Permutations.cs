using System.Numerics;

namespace SMath.Combinatorics;

/// <summary>
/// Permutations without repetition.
/// Order does matter, e.g. {a,b}: a, b, ab, ba.
/// </summary>
/// <remarks>
/// <a href="https://en.wikipedia.org/wiki/Permutation">Wikipedia</a>
/// <a href="https://mathworld.wolfram.com/Permutation.html">Wolfram Mathworld</a>
/// </remarks>
public static class Permutations
{
    /// <summary>
    /// Counts all non empty permutations of <paramref name="n"/> elements.
    /// </summary>
    /// <remarks> Sum of all k-permutations of n. </remarks>
    public static NInt Count<NInt>(NInt n)
        where NInt : IBinaryInteger<NInt>
    {
        if (n <= NInt.Zero)
            return NInt.Zero;

        var sum = NInt.Zero;
        var falling = NInt.One;
        for (var k = NInt.One; k <= n; k++)
        {
            // every falling factorial follows from the previous one, so the sum takes n multiplications
            falling *= n - k + NInt.One;
            sum += falling;
        }

        return sum;
    }

    /// <summary>
    /// Counts k-permutations of <paramref name="n"/>.
    /// </summary>
    /// <remarks>
    /// Falling factorial of n and k.
    /// <a href="https://en.wikipedia.org/wiki/Permutation#k-permutations_of_n">Wikipedia</a>
    /// </remarks>
    public static NInt Count<NInt>(NInt n, NInt k)
        where NInt : IBinaryInteger<NInt>
        => n >= k && k > NInt.Zero
            ? Factorial.Falling.Eval(n, k)
            : NInt.Zero;

    /// <summary>
    /// Enumerate all k element index permutations drawn from [0, n), in lexicographic order.
    /// </summary>
    /// <remarks> Every permutation is a newly allocated array. </remarks>
    public static IEnumerable<NInt[]> Tuples<NInt>(NInt n, NInt k)
        where NInt : IBinaryInteger<NInt>
    {
        if (k <= NInt.Zero || n < k)
            yield break;

        int count = int.CreateChecked(n);
        int size = int.CreateChecked(k);
        var indices = new int[size];
        var used = new bool[count];
        indices[0] = -1;

        // depth first walk, every level picks the lowest index not taken by the levels above
        int level = 0;
        while (level >= 0)
        {
            if (indices[level] >= 0)
                used[indices[level]] = false;

            int next = indices[level] + 1;
            while (next < count && used[next])
                next++;

            if (next == count)
            {
                indices[level] = -1;
                level--;
                continue;
            }

            indices[level] = next;
            used[next] = true;

            if (level == size - 1)
            {
                var tuple = new NInt[size];
                for (int i = 0; i < size; i++)
                    tuple[i] = NInt.CreateChecked(indices[i]);

                yield return tuple;
            }
            else
            {
                level++;
                indices[level] = -1;
            }
        }
    }
}
