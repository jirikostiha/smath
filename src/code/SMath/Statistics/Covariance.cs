using System.Numerics;

namespace SMath.Statistics
{
    /// <summary>
    /// Covariance.
    /// </summary>
    /// <remarks>
    /// <a href="https://en.wikipedia.org/wiki/Covariance">Wikipedia</a>
    /// <a href="https://mathworld.wolfram.com/Covariance.html">Wolfram Mathworld</a>
    /// </remarks>
    public static class Covariance
    {
        public static double Eval<N>(IEnumerable<N> aSequence, IEnumerable<N> bSequence)
            where N : INumberBase<N>
            => Eval(aSequence, bSequence, out long _);

        public static double Eval<N>(IEnumerable<N> aSequence, IEnumerable<N> bSequence, out long count)
            where N : INumberBase<N>
        {
            var sumS1 = N.Zero;
            var sumS2 = N.Zero;
            var sumS1S2 = N.Zero;
            count = 0;

            using (var aEnumerator = aSequence.GetEnumerator())
            using (var bEnumerator = bSequence.GetEnumerator())
            {
                while (aEnumerator.MoveNext() && bEnumerator.MoveNext())
                {
                    sumS1 += aEnumerator.Current;
                    sumS2 += bEnumerator.Current;
                    sumS1S2 += aEnumerator.Current * bEnumerator.Current;
                    count++;
                }
            }

            return (double.CreateChecked(sumS1S2) - double.CreateChecked(sumS1 * sumS2) / double.CreateChecked(count)) / double.CreateChecked(count - 1);
        }
    }
}
