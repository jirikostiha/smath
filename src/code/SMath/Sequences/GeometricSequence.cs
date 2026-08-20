using System.Numerics;
using System.Runtime.CompilerServices;

namespace SMath.Sequences;

/// <summary>
/// Geometric sequence or geometric progression.
/// </summary>
/// <remarks>
/// <a href="https://en.wikipedia.org/wiki/Geometric_progression">Wikipedia</a>
/// <a href="https://mathworld.wolfram.com/GeometricSequence.html">Wolfram Mathworld</a>
/// </remarks>
public static class GeometricSequence
{
    public static string PlainTextFormula => "a(n) = a(1) * r^(n - 1)";

    /// <summary>
    /// Term at the one based position <paramref name="n"/>.
    /// </summary>
    /// <exception cref="ArgumentOutOfRangeException"> Position is not a positive number. </exception>
    public static N Term<N>(N initial, N ratio, int n)
        where N : INumberBase<N>
    {
        if (n <= 0)
            throw new ArgumentOutOfRangeException(nameof(n), "Position has to be a positive number.");

        return initial * Power(ratio, n - 1);
    }

    /// <summary>
    /// Enumerate the first <paramref name="count"/> terms.
    /// </summary>
    public static IEnumerable<N> Terms<N>(N initial, N ratio, int count)
        where N : INumberBase<N>
    {
        for (int n = 1; n <= count; n++)
            yield return Term(initial, ratio, n);
    }

    /// <summary>
    /// Write the first <paramref name="count"/> terms into a destination buffer.
    /// Allocation free alternative to the enumerable overload.
    /// </summary>
    /// <returns> Count of written terms. </returns>
    /// <exception cref="ArgumentException"> Destination is too short. </exception>
    public static int Terms<N>(N initial, N ratio, int count, Span<N> destination)
        where N : INumberBase<N>
    {
        if (count <= 0)
            return 0;

        if (destination.Length < count)
            throw new ArgumentException("Destination is too short.", nameof(destination));

        for (int n = 1; n <= count; n++)
            destination[n - 1] = Term(initial, ratio, n);

        return count;
    }

    /// <summary>
    /// Raises a number to a non negative integer power.
    /// </summary>
    /// <remarks>
    /// Evaluated by squaring instead of by a repeated multiplication of the previous term,
    /// so that a rounding error does not accumulate along the sequence. Kept over
    /// <see cref="IPowerFunctions{TSelf}"/> to admit integer and decimal number types as well.
    /// </remarks>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static N Power<N>(N value, int exponent)
        where N : INumberBase<N>
    {
        var product = N.One;
        while (exponent > 0)
        {
            if ((exponent & 1) == 1)
                product *= value;

            exponent >>= 1;
            if (exponent > 0)
                value *= value;
        }

        return product;
    }
}
