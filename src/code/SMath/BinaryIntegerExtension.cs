using System.Numerics;
using System.Runtime.CompilerServices;

namespace SMath;

public static class BinaryIntegerExtension
{
    /// <summary>
    /// Hamming distance of two numbers.
    /// </summary>
    /// <remarks>
    /// <a href="https://en.wikipedia.org/wiki/Hamming_distance">Wikipedia</a>
    /// </remarks>
    public static N HammingDistanceTo<N>(this N number, N otherNumber)
        where N : IBinaryInteger<N>
        => N.PopCount(number ^ otherNumber);

    public static N ToGrayCode<N>(this N number)
        where N : IBinaryInteger<N>, IUnsignedNumber<N>
        => number ^ (number >> 1);

    public static N FromGrayCode<N>(this N gray)
        where N : IBinaryInteger<N>, IUnsignedNumber<N>
    {
        var n = gray;
        var result = n;

        while (n > N.Zero)
        {
            n >>= 1;
            result ^= n;
        }

        return result;
    }

    /// <summary>
    /// Greatest common divisor of two numbers.
    /// </summary>
    /// <remarks>
    /// Shared by the formulas which reduce a fraction before multiplying it out.
    /// <a href="https://en.wikipedia.org/wiki/Euclidean_algorithm">Wikipedia</a>
    /// </remarks>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static N GreatestCommonDivisor<N>(N number, N otherNumber)
        where N : IBinaryInteger<N>
    {
        number = N.Abs(number);
        otherNumber = N.Abs(otherNumber);
        while (otherNumber != N.Zero)
            (number, otherNumber) = (otherNumber, number % otherNumber);

        return number;
    }

    /// <summary>
    /// Raises <paramref name="number"/> to a non negative integer power <paramref name="exp"/>.
    /// </summary>
    /// <remarks>
    /// Evaluated by squaring, so the count of multiplications is logarithmic in the exponent.
    /// <a href="https://en.wikipedia.org/wiki/Exponentiation_by_squaring">Wikipedia</a>
    /// </remarks>
    /// <exception cref="ArgumentOutOfRangeException"> Exponent is negative. </exception>
    public static N Pow<N>(this N number, N exp)
        where N : IBinaryInteger<N>
    {
        if (N.IsNegative(exp))
            throw new ArgumentOutOfRangeException(nameof(exp), "Exponent cannot be negative.");

        var product = N.One;
        var factor = number;
        while (exp > N.Zero)
        {
            if (N.IsOddInteger(exp))
                product *= factor;

            exp >>= 1;
            if (exp > N.Zero)
                factor *= factor;
        }

        return product;
    }
}
