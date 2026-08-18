namespace SMath.Statistics;

using System;
using System.Collections.Generic;
using System.Numerics;

/// <summary>
/// Spearman's rank correlation coefficient.
/// </summary>
/// <remarks>
/// <a href="https://en.wikipedia.org/wiki/Spearman%27s_rank_correlation_coefficient">wikipedia</a>
/// </remarks>
public static class SpearmanRankCorrelation
{
    public static double Eval<N>(IEnumerable<N> aSequence, IEnumerable<N> bSequence)
        where N : INumberBase<N>
        => Evaluate(ToDoubles(aSequence), ToDoubles(bSequence));

    public static double Eval<N>(ICollection<N> aSequence, ICollection<N> bSequence)
        where N : INumberBase<N>
    {
        if (aSequence.Count != bSequence.Count)
            throw new ArgumentException("Inconsistent length of sequences.");

        return Evaluate(ToDoubles(aSequence), ToDoubles(bSequence));
    }

    public static double Eval<N>(ReadOnlySpan<N> aSequence, ReadOnlySpan<N> bSequence)
        where N : INumberBase<N>
    {
        if (aSequence.Length != bSequence.Length)
            throw new ArgumentException("Inconsistent length of sequences.");

        return Evaluate(ToDoubles(aSequence), ToDoubles(bSequence));
    }

    // Spearman's coefficient is Pearson's coefficient applied to the ranks of the data.
    internal static double Evaluate(double[] aValues, double[] bValues)
    {
        if (aValues.Length != bValues.Length)
            throw new ArgumentException("Inconsistent length of sequences.");

        var aRanks = Ranks(aValues);
        var bRanks = Ranks(bValues);

        return PearsonCorrelation.Eval<double>(new ReadOnlySpan<double>(aRanks), new ReadOnlySpan<double>(bRanks));
    }

    /// <summary>
    /// Fractional ranks of the values, tied values share the average of the positions they span.
    /// </summary>
    private static double[] Ranks(double[] values)
    {
        var n = values.Length;
        var order = new int[n];
        for (int i = 0; i < n; i++)
            order[i] = i;

        Array.Sort(order, (x, y) => values[x].CompareTo(values[y]));

        var ranks = new double[n];
        var i2 = 0;
        while (i2 < n)
        {
            var j = i2;
            while (j + 1 < n && values[order[j + 1]] == values[order[i2]])
                j++;

            // tied elements share the average of their 1-based positions
            var averageRank = (i2 + j) / 2.0 + 1.0;
            for (int k = i2; k <= j; k++)
                ranks[order[k]] = averageRank;

            i2 = j + 1;
        }

        return ranks;
    }

    private static double[] ToDoubles<N>(IEnumerable<N> sequence)
        where N : INumberBase<N>
    {
        if (sequence is ICollection<N> collection)
        {
            var buffer = new double[collection.Count];
            var index = 0;
            foreach (var value in sequence)
                buffer[index++] = double.CreateChecked(value);

            return buffer;
        }

        var list = new List<double>();
        foreach (var value in sequence)
            list.Add(double.CreateChecked(value));

        return list.ToArray();
    }

    private static double[] ToDoubles<N>(ReadOnlySpan<N> sequence)
        where N : INumberBase<N>
    {
        var buffer = new double[sequence.Length];
        for (int i = 0; i < sequence.Length; i++)
            buffer[i] = double.CreateChecked(sequence[i]);

        return buffer;
    }
}
