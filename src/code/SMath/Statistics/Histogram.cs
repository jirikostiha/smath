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
    /// Get histogram. A number falls into the first bin whose bound it is strictly below.
    /// bin bounds indices: ---&lt;0--&lt;1----&lt;2---
    /// </summary>
    /// <param name="numbers"> data </param>
    /// <param name="bounds"> exclusive bin bounds, expected in ascending order </param>
    /// <returns>
    /// One bin more than there are bounds. The trailing bin collects everything at or above
    /// the last bound, it is not upper bounded and its <c>Bound</c> carries no meaning.
    /// </returns>
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
            // the last bin has no upper bound, everything above the last bound falls into it
            for (int i = 0; i < bounds.Count; i++)
            {
                if (number < bins[i].Bound)
                {
                    bins[i].Count += NInt.One;
                    goto outerLoopEnd;
                }
            }
            bins[^1].Count += NInt.One;
        outerLoopEnd:;
        }

        return bins;
    }

    /// <summary>
    /// Get histogram. A number falls into the first bin whose bound it does not exceed.
    /// bin bounds indices: ---0]--1]----2]---
    /// </summary>
    /// <param name="numbers"> data </param>
    /// <param name="bounds"> inclusive bin bounds, expected in ascending order </param>
    /// <returns>
    /// One bin more than there are bounds. The trailing bin collects everything above
    /// the last bound, it is not upper bounded and its <c>Bound</c> carries no meaning.
    /// </returns>
    public static (N Bound, NInt Count)[] GetInclusive<N, NInt>(IEnumerable<N> numbers, IList<N> bounds)
        where N : INumberBase<N>, IComparisonOperators<N, N, bool>
        where NInt : IBinaryInteger<NInt>
    {
        var bins = new (N Bound, NInt Count)[bounds.Count + 1];
        for (int i = 0; i < bounds.Count; i++)
            bins[i] = (bounds[i], NInt.Zero);

        foreach (var number in numbers)
        {
            // the last bin has no upper bound, everything above the last bound falls into it
            for (int i = 0; i < bounds.Count; i++)
            {
                if (number <= bins[i].Bound)
                {
                    bins[i].Count += NInt.One;
                    goto outerLoopEnd;
                }
            }
            bins[^1].Count += NInt.One;
        outerLoopEnd:;
        }

        return bins;
    }

    /// <summary>
    /// Get histogram. A number falls into the first bin whose bound it is strictly below.
    /// bin bounds indices: ---&lt;0--&lt;1----&lt;2---
    /// </summary>
    /// <param name="numbers"> data </param>
    /// <param name="bounds"> exclusive bin bounds, expected in ascending order </param>
    /// <returns>
    /// One bin more than there are bounds. The trailing bin collects everything at or above
    /// the last bound, it is not upper bounded and its <c>Bound</c> carries no meaning.
    /// </returns>
    public static (N Bound, NInt Count)[] GetExclusive<N, NInt>(ReadOnlySpan<N> numbers, ReadOnlySpan<N> bounds)
        where N : INumberBase<N>, IComparisonOperators<N, N, bool>
        where NInt : IBinaryInteger<NInt>
    {
        var bins = new (N Bound, NInt Count)[bounds.Length + 1];
        var binSpan = bins.AsSpan();
        for (int i = 0; i < bounds.Length; i++)
            binSpan[i] = (bounds[i], NInt.Zero);

        for (int n = 0; n < numbers.Length; n++)
        {
            var number = numbers[n];
            // the last bin has no upper bound, everything above the last bound falls into it
            var placed = false;
            for (int i = 0; i < bounds.Length; i++)
            {
                if (number < binSpan[i].Bound)
                {
                    binSpan[i].Count += NInt.One;
                    placed = true;
                    break;
                }
            }

            if (!placed)
                binSpan[^1].Count += NInt.One;
        }

        return bins;
    }

    /// <summary>
    /// Get histogram. A number falls into the first bin whose bound it does not exceed.
    /// bin bounds indices: ---0]--1]----2]---
    /// </summary>
    /// <param name="numbers"> data </param>
    /// <param name="bounds"> inclusive bin bounds, expected in ascending order </param>
    /// <returns>
    /// One bin more than there are bounds. The trailing bin collects everything above
    /// the last bound, it is not upper bounded and its <c>Bound</c> carries no meaning.
    /// </returns>
    public static (N Bound, NInt Count)[] GetInclusive<N, NInt>(ReadOnlySpan<N> numbers, ReadOnlySpan<N> bounds)
        where N : INumberBase<N>, IComparisonOperators<N, N, bool>
        where NInt : IBinaryInteger<NInt>
    {
        var bins = new (N Bound, NInt Count)[bounds.Length + 1];
        var binSpan = bins.AsSpan();
        for (int i = 0; i < bounds.Length; i++)
            binSpan[i] = (bounds[i], NInt.Zero);

        for (int n = 0; n < numbers.Length; n++)
        {
            var number = numbers[n];
            // the last bin has no upper bound, everything above the last bound falls into it
            var placed = false;
            for (int i = 0; i < bounds.Length; i++)
            {
                if (number <= binSpan[i].Bound)
                {
                    binSpan[i].Count += NInt.One;
                    placed = true;
                    break;
                }
            }

            if (!placed)
                binSpan[^1].Count += NInt.One;
        }

        return bins;
    }
}
