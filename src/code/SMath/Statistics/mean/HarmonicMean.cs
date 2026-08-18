using System.Numerics;

namespace SMath.Statistics;

/// <summary>
/// Harmonic mean.
/// </summary>
/// <remarks>
/// <a href="https://en.wikipedia.org/wiki/Harmonic_mean">Wikipedia</a>
/// </remarks>
public static class HarmonicMean
{
    public static double Eval<N>(IEnumerable<N> sequence)
        where N : INumberBase<N>
    {
        double sumReciprocal = 0;
        int count = 0;

        foreach (var val in sequence)
        {
            var d = double.CreateChecked(val);
            if (d == 0) return 0d;
            sumReciprocal += 1d / d;
            count++;
        }

        if (count == 0) return double.NaN;

        return count / sumReciprocal;
    }

    public static double Eval<N>(ReadOnlySpan<N> sequence)
        where N : INumberBase<N>
    {
        if (sequence.Length == 0) return double.NaN;

        double sumReciprocal = 0;

        for (int i = 0; i < sequence.Length; i++)
        {
            var d = double.CreateChecked(sequence[i]);
            if (d == 0) return 0d;
            sumReciprocal += 1d / d;
        }

        return sequence.Length / sumReciprocal;
    }
}
