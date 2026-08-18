using System.Numerics;

namespace SMath.Statistics;

/// <summary>
/// Geometric mean.
/// </summary>
/// <remarks>
/// <a href="https://en.wikipedia.org/wiki/Geometric_mean">Wikipedia</a>
/// </remarks>
public static class GeometricMean
{
    public static double Eval<N>(IEnumerable<N> sequence)
        where N : INumberBase<N>
    {
        double sumLog = 0;
        int count = 0;
        bool hasZero = false;

        foreach (var val in sequence)
        {
            var d = double.CreateChecked(val);
            if (d < 0) return double.NaN;
            if (d == 0) hasZero = true;
            else sumLog += double.Log(d);
            count++;
        }

        if (count == 0) return double.NaN;
        if (hasZero) return 0d;

        return double.Exp(sumLog / count);
    }

    public static double Eval<N>(ReadOnlySpan<N> sequence)
        where N : INumberBase<N>
    {
        if (sequence.Length == 0) return double.NaN;

        double sumLog = 0;
        bool hasZero = false;

        for (int i = 0; i < sequence.Length; i++)
        {
            var d = double.CreateChecked(sequence[i]);
            if (d < 0) return double.NaN;
            if (d == 0) hasZero = true;
            else sumLog += double.Log(d);
        }

        if (hasZero) return 0d;

        return double.Exp(sumLog / sequence.Length);
    }
}
