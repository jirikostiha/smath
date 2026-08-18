namespace SMath.Statistics;

using System;
using System.Collections.Generic;
using System.Numerics;

/// <summary>
/// Kendall's tau-b rank correlation coefficient.
/// </summary>
/// <remarks>
/// The tau-b variant corrects for ties in either sequence.
/// <a href="https://en.wikipedia.org/wiki/Kendall_rank_correlation_coefficient">wikipedia</a>
/// </remarks>
public static class KendallCorrelation
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

    internal static double Evaluate(double[] aValues, double[] bValues)
    {
        if (aValues.Length != bValues.Length)
            throw new ArgumentException("Inconsistent length of sequences.");

        var n = aValues.Length;
        long concordant = 0;
        long discordant = 0;
        // pairs that are tied in a resp. b (n1 resp. n2 in the tau-b formula)
        long tiesA = 0;
        long tiesB = 0;

        for (int i = 0; i < n; i++)
            for (int j = i + 1; j < n; j++)
            {
                var da = aValues[i].CompareTo(aValues[j]);
                var db = bValues[i].CompareTo(bValues[j]);

                if (da == 0)
                    tiesA++;
                if (db == 0)
                    tiesB++;

                if (da != 0 && db != 0)
                {
                    if (Math.Sign(da) == Math.Sign(db))
                        concordant++;
                    else
                        discordant++;
                }
            }

        // total number of pairs
        var n0 = (double)n * (n - 1) / 2.0;

        // the denominator collapses when a sequence has no distinct pairs left, the coefficient is undefined then
        var denominator = double.Sqrt((n0 - tiesA) * (n0 - tiesB));
        return denominator != 0 ? (concordant - discordant) / denominator : double.NaN;
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
