using System.Numerics;

namespace SMath.Sequences;

/// <summary>
/// Fibonacci sequence of Fibonacci numbers.
/// </summary>
/// <remarks>
/// <a href="https://en.wikipedia.org/wiki/Fibonacci_number">Wikipedia</a>
/// <a href="https://mathworld.wolfram.com/FibonacciNumber.html">Wolfram Mathworld</a>
/// </remarks>
public static class FibonacciSequence
{
    public static string PlainTextFormula => "a(n) = a(n - 1) + a(n - 2), a(1) = 0, a(2) = 1";

    /// <summary>
    /// Fibonacci number at the one based position <paramref name="n"/>, the first one being zero.
    /// </summary>
    /// <exception cref="ArgumentOutOfRangeException"> Position is not a positive number. </exception>
    public static N Term<N>(int n)
        where N : INumberBase<N>
    {
        if (n <= 0)
            throw new ArgumentOutOfRangeException(nameof(n), "Position has to be a positive number.");

        var previous = N.Zero;
        var current = N.One;
        for (int i = 1; i < n; i++)
            (previous, current) = (current, previous + current);

        return previous;
    }

    /// <summary>
    /// Enumerate the first <paramref name="count"/> Fibonacci numbers, starting from zero.
    /// </summary>
    public static IEnumerable<N> Terms<N>(int count)
        where N : INumberBase<N>
    {
        var previous = N.Zero;
        var current = N.One;
        for (int n = 1; n <= count; n++)
        {
            yield return previous;
            (previous, current) = (current, previous + current);
        }
    }

    /// <summary>
    /// Write the first <paramref name="count"/> Fibonacci numbers into a destination buffer.
    /// Allocation free alternative to the enumerable overload.
    /// </summary>
    /// <returns> Count of written terms. </returns>
    /// <exception cref="ArgumentException"> Destination is too short. </exception>
    public static int Terms<N>(int count, Span<N> destination)
        where N : INumberBase<N>
    {
        if (count <= 0)
            return 0;

        // the count is known up front, so the capacity is checked once instead of on every write
        if (destination.Length < count)
            throw new ArgumentException("Destination is too short.", nameof(destination));

        var previous = N.Zero;
        var current = N.One;
        for (int n = 1; n <= count; n++)
        {
            destination[n - 1] = previous;
            (previous, current) = (current, previous + current);
        }

        return count;
    }
}
