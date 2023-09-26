using System.Numerics;

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
    {
        N x = N.Abs(number) ^ N.Abs(otherNumber);
        N setBits = N.Zero;

        while (x > N.Zero)
        {
            setBits += x & N.One;
            x >>= 1;
        }

        return setBits;
    }

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
}
