using System.Numerics;

namespace SMath.Sequences;

/// <summary>
/// Generalised Fibonacci sequence with arbitrary first two terms.
/// </summary>
/// <remarks>
/// <a href="https://en.wikipedia.org/wiki/Generalizations_of_Fibonacci_numbers">Wikipedia</a>
/// </remarks>
public static class GeneralisedFibonacciSequence
{
    /// <summary>
    /// Enumerate the first <paramref name="count"/> terms given the two initial terms
    /// <paramref name="a"/> and <paramref name="b"/>.
    /// </summary>
    public static IEnumerable<N> Terms<N, NInt>(N a, N b, NInt count)
        where N : INumberBase<N>, IComparisonOperators<N, N, bool>
        where NInt : IBinaryInteger<NInt>
    {
        yield return a;
        if (count == NInt.One)
            yield break;

        yield return b;
        if (count == NInt.CreateChecked(2))
            yield break;

        var n = N.CreateChecked(3);
        var limit = N.CreateChecked(count);
        while (n <= limit)
        {
            var current = a + b;
            yield return current;

            a = b;
            b = current;
            n++;
        }
    }
}
