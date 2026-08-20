using System.Numerics;

namespace SMath.Statistics;

/// <summary>
/// Weighted arithmetic mean.
/// </summary>
/// <remarks>
/// <a href="https://en.wikipedia.org/wiki/Weighted_arithmetic_mean">Wikipedia</a>
/// </remarks>
public static class WeightedArithmeticMean
{
    public static double Eval<N>(IEnumerable<KeyValuePair<N, N>> sequence)
        where N : INumberBase<N>
    {
        double sumWeighted = 0;
        double sumWeights = 0;
        int count = 0;

        foreach (var pair in sequence)
        {
            var val = double.CreateChecked(pair.Key);
            var weight = double.CreateChecked(pair.Value);
            sumWeighted += val * weight;
            sumWeights += weight;
            count++;
        }

        if (count == 0 || sumWeights == 0) return double.NaN;

        return sumWeighted / sumWeights;
    }

    public static double Eval<N>(IEnumerable<N> sequence, IEnumerable<N> weights)
        where N : INumberBase<N>
    {
        using var valEnum = sequence.GetEnumerator();
        using var weightEnum = weights.GetEnumerator();

        double sumWeighted = 0;
        double sumWeights = 0;
        int count = 0;

        while (valEnum.MoveNext())
        {
            if (!weightEnum.MoveNext())
                throw new ArgumentException("Sequence lengths do not match.");

            var val = double.CreateChecked(valEnum.Current);
            var weight = double.CreateChecked(weightEnum.Current);
            sumWeighted += val * weight;
            sumWeights += weight;
            count++;
        }

        if (weightEnum.MoveNext())
            throw new ArgumentException("Sequence lengths do not match.");

        if (count == 0 || sumWeights == 0) return double.NaN;

        return sumWeighted / sumWeights;
    }

    public static double Eval<N>(ReadOnlySpan<N> sequence, ReadOnlySpan<N> weights)
        where N : INumberBase<N>
    {
        if (sequence.Length != weights.Length)
            throw new ArgumentException("Sequence lengths do not match.");

        if (sequence.Length == 0) return double.NaN;

        double sumWeighted = 0;
        double sumWeights = 0;

        for (int i = 0; i < sequence.Length; i++)
        {
            var val = double.CreateChecked(sequence[i]);
            var weight = double.CreateChecked(weights[i]);
            sumWeighted += val * weight;
            sumWeights += weight;
        }

        if (sumWeights == 0) return double.NaN;

        return sumWeighted / sumWeights;
    }
}
