using System.Numerics;

namespace SMath.Statistics;

/// <summary>
/// Cubic mean.
/// </summary>
/// <remarks>
/// <a href="https://en.wikipedia.org/wiki/Cubic_mean">Wikipedia</a>
/// </remarks>
public static class CubicMean
{
    public static double Eval<N>(IEnumerable<N> sequence)
        where N : INumberBase<N>
    {
        double sumCube = 0;
        int count = 0;

        foreach (var val in sequence)
        {
            var d = double.CreateChecked(val);
            sumCube += d * d * d;
            count++;
        }

        if (count == 0) return double.NaN;

        return double.Cbrt(sumCube / count);
    }

    public static double Eval<N>(ReadOnlySpan<N> sequence)
        where N : INumberBase<N>
    {
        if (sequence.Length == 0) return double.NaN;

        double sumCube = 0;

        for (int i = 0; i < sequence.Length; i++)
        {
            var d = double.CreateChecked(sequence[i]);
            sumCube += d * d * d;
        }

        return double.Cbrt(sumCube / sequence.Length);
    }
}
