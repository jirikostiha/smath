using System.Numerics;

namespace SMath.Sequences;

/// <summary>
/// Arithmetic sequence or arithmetic progression.
/// </summary>
/// <remarks>
/// <a href="https://en.wikipedia.org/wiki/Arithmetic_progression">Wikipedia</a>
/// <a href="https://mathworld.wolfram.com/ArithmeticProgression.html">Wolfram Mathworld</a>
/// </remarks>
public static class ArithmeticSequence
{
    public static string PlainTextFormula => "a(n) = a(1) + (n - 1) * d";

    /// <summary>
    /// Term at the one based position <paramref name="n"/>.
    /// </summary>
    /// <exception cref="ArgumentOutOfRangeException"> Position is not a positive number. </exception>
    public static N Term<N>(N initial, N difference, int n)
        where N : INumberBase<N>
    {
        if (n <= 0)
            throw new ArgumentOutOfRangeException(nameof(n), "Position has to be a positive number.");

        return initial + N.CreateChecked(n - 1) * difference;
    }

    /// <summary>
    /// Enumerate the first <paramref name="count"/> terms.
    /// </summary>
    public static IEnumerable<N> Terms<N>(N initial, N difference, int count)
        where N : INumberBase<N>
    {
        for (int n = 1; n <= count; n++)
            yield return Term(initial, difference, n);
    }

    /// <summary>
    /// Write the first <paramref name="count"/> terms into a destination buffer.
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
