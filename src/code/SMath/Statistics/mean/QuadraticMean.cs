using System.Numerics;

namespace SMath.Statistics;

/// <summary>
/// Quadratic mean or Root Mean Square (RMS).
/// </summary>
/// <remarks>
/// <a href="https://en.wikipedia.org/wiki/Root_mean_square">Wikipedia</a>
/// </remarks>
public static class QuadraticMean
{
    public static double Eval<N>(IEnumerable<N> sequence)
        where N : INumberBase<N>
    {
        double sumSq = 0;
        int count = 0;

        foreach (var val in sequence)
        {
            var d = double.CreateChecked(val);
            sumSq += d * d;
            count++;
        }

        if (count == 0) return double.NaN;

        return double.Sqrt(sumSq / count);
    }

    public static double Eval<N>(ReadOnlySpan<N> sequence)
        where N : INumberBase<N>
    {
        if (sequence.Length == 0) return double.NaN;

        double sumSq = 0;

        for (int i = 0; i < sequence.Length; i++)
        {
            var d = double.CreateChecked(sequence[i]);
            sumSq += d * d;
        }

        return double.Sqrt(sumSq / sequence.Length);
    }
}
