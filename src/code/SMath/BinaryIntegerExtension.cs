using System.Numerics;

namespace SMath
{
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
    }
}
