using System.Numerics;

namespace SMath.Statistics;

/// <summary>
/// Generalized mean or power mean or Hölder mean.
/// </summary>
/// <remarks>
/// <a href="https://en.wikipedia.org/wiki/Generalized_mean">Wikipedia</a>
/// </remarks>
public static class GeneralizedMean
{
    public static double Eval<N>(IEnumerable<N> sequence, double p)
        where N : INumberBase<N>
    {
        if (p == 0) return GeometricMean.Eval(sequence);
        if (p == 1) return ArithmeticMean.Eval(sequence);
        if (p == 2) return QuadraticMean.Eval(sequence);
        if (p == -1) return HarmonicMean.Eval(sequence);
        if (p == 3) return CubicMean.Eval(sequence);

        double sumPow = 0;
        int count = 0;

        foreach (var val in sequence)
        {
            var d = double.CreateChecked(val);
            sumPow += double.Pow(d, p);
            count++;
        }

        if (count == 0) return double.NaN;

        return double.Pow(sumPow / count, 1d / p);
    }

    public static double Eval<N>(ReadOnlySpan<N> sequence, double p)
        where N : INumberBase<N>
    {
        if (sequence.Length == 0) return double.NaN;
        if (p == 0) return GeometricMean.Eval(sequence);
        if (p == 1) return ArithmeticMean.Eval(sequence);
        if (p == 2) return QuadraticMean.Eval(sequence);
        if (p == -1) return HarmonicMean.Eval(sequence);
        if (p == 3) return CubicMean.Eval(sequence);

        double sumPow = 0;

        for (int i = 0; i < sequence.Length; i++)
        {
            var d = double.CreateChecked(sequence[i]);
            sumPow += double.Pow(d, p);
        }

        return double.Pow(sumPow / sequence.Length, 1d / p);
    }
}
