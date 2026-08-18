namespace SMath.Statistics;

using System;
using System.Collections.Generic;
using System.Numerics;

/// <summary>
/// Cramér's V, a measure of association between two nominal (categorical) sequences.
/// </summary>
/// <remarks>
/// Derived from the Pearson chi-squared statistic of the contingency table.
/// The result lies in the range [0, 1] and is unsigned, unlike the product-moment correlations.
/// <a href="https://en.wikipedia.org/wiki/Cram%C3%A9r%27s_V">wikipedia</a>
/// </remarks>
public static class CramerCorrelation
{
    public static double Eval<N>(IEnumerable<N> aSequence, IEnumerable<N> bSequence)
        where N : INumberBase<N>
        => Evaluate(ToArray(aSequence), ToArray(bSequence));

    public static double Eval<N>(ICollection<N> aSequence, ICollection<N> bSequence)
        where N : INumberBase<N>
    {
        if (aSequence.Count != bSequence.Count)
            throw new ArgumentException("Inconsistent length of sequences.");

        return Evaluate(ToArray(aSequence), ToArray(bSequence));
    }

    public static double Eval<N>(ReadOnlySpan<N> aSequence, ReadOnlySpan<N> bSequence)
        where N : INumberBase<N>
    {
        if (aSequence.Length != bSequence.Length)
            throw new ArgumentException("Inconsistent length of sequences.");

        return Evaluate(ToArray(aSequence), ToArray(bSequence));
    }

    internal static double Evaluate<N>(N[] aValues, N[] bValues)
        where N : INumberBase<N>
    {
        if (aValues.Length != bValues.Length)
            throw new ArgumentException("Inconsistent length of sequences.");

        var n = aValues.Length;

        // map each distinct category to a contingency table axis
        var rowIndex = new Dictionary<N, int>();
        var colIndex = new Dictionary<N, int>();
        for (int i = 0; i < n; i++)
        {
            if (!rowIndex.ContainsKey(aValues[i]))
                rowIndex[aValues[i]] = rowIndex.Count;
            if (!colIndex.ContainsKey(bValues[i]))
                colIndex[bValues[i]] = colIndex.Count;
        }

        var rows = rowIndex.Count;
        var cols = colIndex.Count;
        var table = new int[rows, cols];
        var rowTotals = new int[rows];
        var colTotals = new int[cols];
        for (int i = 0; i < n; i++)
        {
            var r = rowIndex[aValues[i]];
            var c = colIndex[bValues[i]];
            table[r, c]++;
            rowTotals[r]++;
            colTotals[c]++;
        }

        // Pearson chi-squared statistic, every marginal total is positive by construction
        var chiSquared = 0d;
        for (int r = 0; r < rows; r++)
            for (int c = 0; c < cols; c++)
            {
                var expected = (double)rowTotals[r] * colTotals[c] / n;
                var diff = table[r, c] - expected;
                chiSquared += diff * diff / expected;
            }

        // a single category variable carries no association to measure, the coefficient is undefined then
        var minDimension = Math.Min(rows - 1, cols - 1);
        return minDimension > 0 ? double.Sqrt(chiSquared / (n * minDimension)) : double.NaN;
    }

    private static N[] ToArray<N>(IEnumerable<N> sequence)
        where N : INumberBase<N>
    {
        if (sequence is N[] array)
            return array;

        if (sequence is ICollection<N> collection)
        {
            var buffer = new N[collection.Count];
            collection.CopyTo(buffer, 0);
            return buffer;
        }

        var list = new List<N>(sequence);
        return list.ToArray();
    }

    private static N[] ToArray<N>(ReadOnlySpan<N> sequence)
        where N : INumberBase<N>
        => sequence.ToArray();
}
