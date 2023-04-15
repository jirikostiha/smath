using System;
using System.Numerics;

namespace SMath.Sequences
{
    /// <summary>
    /// 3x + 1 or Collatz conjecture sequence.
    /// </summary>
    /// <remarks>
    /// <a href="https://en.wikipedia.org/wiki/Collatz_conjecture">wikipedia</a>
    /// <a href="http://www.ericr.nl/wondrous/showsteps.html">calculator</a>
    /// </remarks>
    public static class CollatzConjecture
    {
        public static N NextTerm<N>(N value)
            where N : IBinaryInteger<N>
            => value % N.CreateChecked(2) == N.Zero 
                ? value / N.CreateChecked(2)          //even
                : value * N.CreateChecked(3) + N.One; // odd

        public static IEnumerable<N> Terms<N>(N initial, N count)
            where N : IBinaryInteger<N>
        {
            var value = initial;
            for (var n = N.One; n <= count; n++)
            {
                value = NextTerm(value);
                yield return value;
            }
        }
    }
}
