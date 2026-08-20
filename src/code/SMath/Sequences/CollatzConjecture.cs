using System.Numerics;

namespace SMath.Sequences;

/// <summary>
/// 3x + 1 or Collatz conjecture sequence.
/// </summary>
/// <remarks>
/// <a href="https://en.wikipedia.org/wiki/Collatz_conjecture">Wikipedia</a>
/// <a href="https://mathworld.wolfram.com/CollatzProblem.html">Wolfram Mathworld</a>
/// <a href="http://www.ericr.nl/wondrous/showsteps.html">Calculator</a>
/// </remarks>
public static class CollatzConjecture
{
    public static string PlainTextFormula => "a(n+1) = a(n) / 2 for even a(n), 3 * a(n) + 1 for odd a(n)";

    /// <summary>
    /// Term following <paramref name="value"/>, a half of an even value, 3 * value + 1 of an odd one.
    /// </summary>
    public static N NextTerm<N>(N value)
        where N : IBinaryInteger<N>
        => N.IsEvenInteger(value)
            ? value / N.CreateChecked(2)
            : N.CreateChecked(3) * value + N.One;

    /// <summary>
    /// Enumerate <paramref name="count"/> terms following <paramref name="initial"/>.
    /// The initial value itself is not a part of the sequence.
    /// </summary>
    public static IEnumerable<N> Terms<N>(N initial, int count)
        where N : IBinaryInteger<N>
    {
        var value = initial;
        for (int n = 1; n <= count; n++)
        {
            value = NextTerm(value);
            yield return value;
        }
    }

    /// <summary>
    /// Write <paramref name="count"/> terms following <paramref name="initial"/> into a destination
    /// buffer. Allocation free alternative to the enumerable overload.
    /// </summary>
    /// <returns> Count of written terms. </returns>
    /// <exception cref="ArgumentException"> Destination is too short. </exception>
    public static int Terms<N>(N initial, int count, Span<N> destination)
        where N : IBinaryInteger<N>
    {
        if (count <= 0)
            return 0;

        // the count is known up front, so the capacity is checked once instead of on every write
        if (destination.Length < count)
            throw new ArgumentException("Destination is too short.", nameof(destination));

        var value = initial;
        for (int n = 1; n <= count; n++)
        {
            value = NextTerm(value);
            destination[n - 1] = value;
        }

        return count;
    }

    /// <summary>
    /// Total stopping time, the count of steps needed to reach one.
    /// </summary>
    /// <remarks>
    /// The conjecture that every positive number reaches one is not proven, the search
    /// is therefore unbounded.
    /// <a href="https://en.wikipedia.org/wiki/Collatz_conjecture#Statement_of_the_problem">Wikipedia</a>
    /// </remarks>
    /// <exception cref="ArgumentOutOfRangeException"> Initial value is not a positive number. </exception>
    public static int StoppingTime<N>(N initial)
        where N : IBinaryInteger<N>
    {
        if (initial <= N.Zero)
            throw new ArgumentOutOfRangeException(nameof(initial), "Initial value has to be a positive number.");

        var steps = 0;
        var value = initial;
        while (value > N.One)
        {
            value = NextTerm(value);
            steps++;
        }

        return steps;
    }
}
