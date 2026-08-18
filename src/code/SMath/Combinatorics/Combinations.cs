using System.Numerics;
using SMath.Expansions;

namespace SMath.Combinatorics;

/// <summary>
/// Combinations without repetition.
/// Order does not matter, e.g. {a,b}: a, b, ab.
/// </summary>
/// <remarks>
/// <a href="https://en.wikipedia.org/wiki/Combination">Wikipedia</a>
/// <a href="https://mathworld.wolfram.com/Combination.html">Wolfram MathWorld</a>
/// </remarks>
public static class Combinations
{
    /// <summary>
    /// Counts all combinations (without repetition) of <paramref name="n"/> elements.
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
    /// Counts k-combinations of <paramref name="n"/>.
    /// </summary>
    public static NInt Count<NInt>(NInt n, NInt k)
        where NInt : IBinaryInteger<NInt>, IComparisonOperators<NInt, NInt, bool>
        => n >= k && k > NInt.Zero
            ? BinomialCoefficient.Eval(n, k)
            : NInt.Zero;

    /// <summary>
    /// Enumerate all index pairs (i &lt; j) drawn from [0, n).
    /// </summary>
    public static IEnumerable<(NInt Index1, NInt Index2)> Tuple2<NInt>(NInt n)
        where NInt : IBinaryInteger<NInt>, IComparisonOperators<NInt, NInt, bool>
    {
        for (var i = NInt.Zero; i < n - NInt.One; i++)
            for (var j = i + NInt.One; j < n; j++)
                yield return (i, j);
    }

    /// <summary>
    /// Enumerate all index triples (i &lt; j &lt; k) drawn from [0, n).
    /// </summary>
    public static IEnumerable<(NInt Index1, NInt Index2, NInt Index3)> Tuple3<NInt>(NInt n)
        where NInt : IBinaryInteger<NInt>, IComparisonOperators<NInt, NInt, bool>
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
        where NInt : IBinaryInteger<NInt>, IComparisonOperators<NInt, NInt, bool>
    {
        for (var i = NInt.Zero; i < n - NInt.One; i++)
            for (var j = i + NInt.One; j < n; j++)
                for (var k = j + NInt.One; k < n; k++)
                    for (var l = k + NInt.One; l < n; l++)
                        yield return (i, j, k, l);
    }
}
