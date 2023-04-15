using System;
using System.Collections.Generic;
using System.Numerics;

namespace SMath.Series
{
    /// <summary>
    /// Geometric series
    /// </summary>
    /// <remarks>
    /// <a href="https://en.wikipedia.org/wiki/Geometric_series">wikipedia</a>
    /// <a href="http://www.mathguide.com/lessons/SequenceGeometric.html">mathguide</a>
    /// </remarks>
    public static class GeometricSeries
    {
        public static N Term<N>(N initial, N ratio, N n)
            where N : IPowerFunctions<N>
            => ratio == N.One 
                ? initial 
                : initial * ((N.One - N.Pow(ratio, n)) / (N.One - ratio));

        public static IEnumerable<N> Terms<N, NInt>(N initial, N ratio, NInt count)
            where N : IPowerFunctions<N>, IComparisonOperators<N, N, bool>
            where NInt : IUnsignedNumber<NInt>
        {
            for (var n = N.One; n <= N.CreateChecked(count); n++)
                yield return Term(initial, ratio, n);
        }
    }
}
