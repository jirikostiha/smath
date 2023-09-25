using System.Numerics;

namespace SMath.Statistics;

/// <summary>
/// Histogram.
/// </summary>
/// <remarks>
/// <a href="https://en.wikipedia.org/wiki/Histogram">wikipedia</a>
/// </remarks>
public static class Histogram
{
    /// <summary>
    /// Get histogram.
    /// bin bounds indices: ---<0--<1----<2---<
    /// </summary>
    /// <param name="numbers"> data </param>
    /// <param name="bounds"> exclusive bin bounds </param>
    /// <returns></returns>
    public static (N Bound, NInt Count)[] GetExclusive<N, NInt>(IEnumerable<N> numbers, IList<N> bounds)
        where N : INumberBase<N>, IComparisonOperators<N, N, bool>
        where NInt : IBinaryInteger<NInt>
    {
        var bins = new (N Bound, NInt Count)[bounds.Count + 1];
        // initialize bins
        for (int i = 0; i < bounds.Count; i++)
            bins[i] = (bounds[i], NInt.Zero);

        foreach (var number in numbers)
        {
            for (int i = 0; i < bins.Length; i++)
            {
                if (number < bins[i].Bound)
                {
                    bins[i] = (bins[i].Bound, bins[i].Count + NInt.One);
                    goto outerLoopEnd;
                }
            }
            bins[^1] = (bins[^1].Bound, bins[^1].Count + NInt.One);
outerLoopEnd:;
        }

        return bins;
    }

    public static (N Bound, NInt Count)[] GetInclusive<N, NInt>(IEnumerable<N> numbers, IList<N> bounds)
        where N : INumberBase<N>, IComparisonOperators<N, N, bool>
        where NInt : IBinaryInteger<NInt>
    {
        var bins = new (N Bound, NInt Count)[bounds.Count + 1];
        for (int i = 0; i < bounds.Count; i++)
            bins[i] = (bounds[i], NInt.Zero);

        foreach (var number in numbers)
        {
            for (int i = 0; i < bins.Length; i++)
            {
                if (number <= bins[i].Bound)
                {
                    bins[i] = (bins[i].Bound, bins[i].Count + NInt.One);
                    goto outerLoopEnd;
                }
            }
            bins[^1] = (bins[^1].Bound, bins[^1].Count + NInt.One);
outerLoopEnd:;
        }

        return bins;
    }
}
