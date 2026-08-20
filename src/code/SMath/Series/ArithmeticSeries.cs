using System.Numerics;
using SMath.Sequences;

namespace SMath.Series;

/// <summary>
/// Arithmetic series, the partial sums of an arithmetic sequence.
/// </summary>
/// <remarks>
/// <a href="https://en.wikipedia.org/wiki/Arithmetic_progression#Sum">Wikipedia</a>
/// <a href="https://mathworld.wolfram.com/ArithmeticSeries.html">Wolfram Mathworld</a>
/// </remarks>
public static class ArithmeticSeries
{
    public static string PlainTextFormula => "s(n) = n * (2 * a(1) + (n - 1) * d) / 2";

    /// <summary>
    /// Sum of the first <paramref name="n"/> terms of <see cref="ArithmeticSequence"/>.
    /// </summary>
    /// <remarks>
    /// The numerator is always even, so the closed form stays exact for integer number types too.
    /// </remarks>
    public static N Term<N>(N initial, N difference, int n)
        where N : INumberBase<N>
        => n <= 0
            ? N.Zero
            : N.CreateChecked(n)
                * (N.CreateChecked(2) * initial + N.CreateChecked(n - 1) * difference)
                / N.CreateChecked(2);

    /// <summary>
    /// Enumerate the first <paramref name="count"/> partial sums.
    /// </summary>
    public static IEnumerable<N> Terms<N>(N initial, N difference, int count)
        where N : INumberBase<N>
    {
        for (int n = 1; n <= count; n++)
            yield return Term(initial, difference, n);
    }

    /// <summary>
    /// Write the first <paramref name="count"/> partial sums into a destination buffer.
    /// Allocation free alternative to the enumerable overload.
    /// </summary>
    /// <returns> Count of written terms. </returns>
    /// <exception cref="ArgumentException"> Destination is too short. </exception>
    public static int Terms<N>(N initial, N difference, int count, Span<N> destination)
        where N : INumberBase<N>
    {
        if (count <= 0)
            return 0;

        // the count is known up front, so the capacity is checked once instead of on every write
        if (destination.Length < count)
            throw new ArgumentException("Destination is too short.", nameof(destination));

        for (int n = 1; n <= count; n++)
            destination[n - 1] = Term(initial, difference, n);

        return count;
    }
}
