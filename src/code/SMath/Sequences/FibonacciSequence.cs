using System.Numerics;

namespace SMath.Sequences
{    
    /// <summary>
    /// Fibonacci sequence of fibonacci number
    /// </summary>
    /// <remarks>
    /// <a href="https://en.wikipedia.org/wiki/Fibonacci_number">wikipedia</a>
    /// </remarks>
    public static class FibonacciSequence
    {
        //public static double Term(long n) => 0; //todo does formula exist?

        public static IEnumerable<N> Terms<N>(N count)
            where N : IUnsignedNumber<N>, IComparisonOperators<N, N, bool>
        {
            var a = N.Zero;
            yield return a;
            if (count == N.One) yield break;

            var b = N.One;
            yield return b;
            if (count == N.CreateChecked(2)) yield break;

            var n = N.CreateChecked(3);
            while (n <= count)
            {
                var current = a + b;
                yield return current;

                a = b;
                b = current;
                n++;
            }
        }
    }
}
