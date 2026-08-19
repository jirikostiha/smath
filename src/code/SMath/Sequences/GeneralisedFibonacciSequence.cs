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
    public static string PlainTextFormula => "a(n) = a(n - 1) + a(n - 2)";

    /// <summary>
    /// Term at the one based position <paramref name="n"/>.
    /// </summary>
    /// <exception cref="ArgumentOutOfRangeException"> Position is not a positive number. </exception>
    public static N Term<N>(N first, N second, int n)
        where N : INumberBase<N>
    {
        if (n <= 0)
            throw new ArgumentOutOfRangeException(nameof(n), "Position has to be a positive number.");

        var previous = first;
        var current = second;
        for (int i = 1; i < n; i++)
            (previous, current) = (current, previous + current);

        return previous;
    }

    /// <summary>
    /// Enumerate the first <paramref name="count"/> terms.
    /// </summary>
    public static IEnumerable<N> Terms<N>(N first, N second, int count)
        where N : INumberBase<N>
    {
        var previous = first;
        var current = second;
        for (int n = 1; n <= count; n++)
        {
            yield return previous;
            (previous, current) = (current, previous + current);
        }
    }

    /// <summary>
    /// Write the first <paramref name="count"/> terms into a destination buffer.
    /// Allocation free alternative to the enumerable overload.
    /// </summary>
    /// <returns> Count of written terms. </returns>
    /// <exception cref="ArgumentException"> Destination is too short. </exception>
    public static int Terms<N>(N first, N second, int count, Span<N> destination)
        where N : INumberBase<N>
    {
        if (count <= 0)
            return 0;

        if (destination.Length < count)
            throw new ArgumentException("Destination is too short.", nameof(destination));

        var previous = first;
        var current = second;
        for (int n = 1; n <= count; n++)
        {
            destination[n - 1] = previous;
            (previous, current) = (current, previous + current);
        }

        return count;
    }
}
