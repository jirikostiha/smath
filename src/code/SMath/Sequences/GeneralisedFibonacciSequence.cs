using System.Numerics;

namespace SMath.Sequences
{
    /// <summary>
    /// Generalised fibonacci suquence
    /// </summary>
    /// <remarks>
    /// <a href="https://en.wikipedia.org/wiki/Generalizations_of_Fibonacci_numbers">wikipedia</a>
    /// </remarks>
    public static class GeneralisedFibonacciSequence
    {
        /// <summary>
        /// Specific term.
        /// </summary>
        public static N Term<N, NInt>(N a, N b, NInt n)
            where N : INumberBase<N>
            where NInt : IUnsignedNumber<NInt>
        {
            return default; //todo
        }

        public static IEnumerable<N> Terms<N, NInt>(N a, N b, NInt count)
            where N : INumberBase<N>, IComparisonOperators<N, N, bool>
            where NInt : IUnsignedNumber<NInt>
        {
            //double a = aInitial;
            yield return a;
            if (count == NInt.One) yield break;

            //double b = bInitial;
            yield return b;
            if (count == NInt.CreateChecked(2)) yield break;

            var n = N.CreateChecked(3);
            while (n <= N.CreateChecked(count))
            {
                N current = a + b;
                yield return current;

                a = b;
                b = current;
                n++;
            }
        }
    }
}
