namespace SMath.Statistics;

using System.Collections.Generic;
using System.Numerics;

/// <summary>
/// Arithmetic mean.
/// </summary>
/// <remarks>
/// <a href="https://en.wikipedia.org/wiki/Arithmetic_mean">wikipedia</a>
/// </remarks>
public static class ArithmeticMean
{
    // the values are summed as doubles, a sum kept in N wraps around for integer types
    // (the mean of two int.MaxValue values would come out as -1)
    public static double Eval<N>(IEnumerable<N> sequence)
        where N : INumberBase<N>
    {
        double sum = 0;
        long count = 0;
        foreach (var value in sequence)
        {
            sum += double.CreateChecked(value);
            count++;
        }

        return count > 0 ? sum / count : double.NaN;
    }

    public static double Eval<N>(ReadOnlySpan<N> sequence)
        where N : INumberBase<N>
    {
        if (sequence.Length == 0)
            return double.NaN;

        double sum = 0;
        for (int i = 0; i < sequence.Length; i++)
            sum += double.CreateChecked(sequence[i]);

        return sum / sequence.Length;
    }
}
