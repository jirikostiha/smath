using System.Numerics;

namespace SMath.Statistics;

/// <summary>
/// Covariance.
/// </summary>
/// <remarks>
/// <a href="https://en.wikipedia.org/wiki/Covariance">Wikipedia</a>
/// <a href="https://mathworld.wolfram.com/Covariance.html">Wolfram Mathworld</a>
/// </remarks>
public static class Covariance
{
    public static double Eval<N>(IEnumerable<N> aSequence, IEnumerable<N> bSequence, out long count)
        where N : INumberBase<N>
    {
        if (aSequence.Count() != bSequence.Count())
            throw new ArgumentException("Inconsistent length of sequences.");

        return Evaluate(aSequence, bSequence, out count);
    }

    public static double Eval<N>(ICollection<N> aSequence, ICollection<N> bSequence)
        where N : INumberBase<N>
    {
        if (aSequence.Count != bSequence.Count)
            throw new ArgumentException("Inconsistent length of sequences.");

        return Evaluate(aSequence, bSequence, out _);
    }

    internal static double Evaluate<N>(IEnumerable<N> aSequence, IEnumerable<N> bSequence, out long count)
        where N : INumberBase<N>
    {
        var sumS1 = N.Zero;
        var sumS2 = N.Zero;
        var sumS1S2 = N.Zero;
        count = 0;

        using (var aEnumerator = aSequence.GetEnumerator())
        using (var bEnumerator = bSequence.GetEnumerator())
        {
            while (aEnumerator.MoveNext() && bEnumerator.MoveNext())
            {
                sumS1 += aEnumerator.Current;
                sumS2 += bEnumerator.Current;
                sumS1S2 += aEnumerator.Current * bEnumerator.Current;
                count++;
            }
        }

        return (double.CreateChecked(sumS1S2) - double.CreateChecked(sumS1 * sumS2)
            / double.CreateChecked(count))
            / double.CreateChecked(count - 1);
    }

    public static double Eval<N>(ReadOnlySpan<N> aSequence, ReadOnlySpan<N> bSequence)
        where N : INumberBase<N>
    {
        if (aSequence.Length != bSequence.Length)
            throw new ArgumentException("Inconsistent length of sequences.");

        var sumS1 = N.Zero;
        var sumS2 = N.Zero;
        var sumS1S2 = N.Zero;

        for (int i = 0; i < aSequence.Length; i++)
        {
            sumS1 += aSequence[i];
            sumS2 += bSequence[i];
            sumS1S2 += aSequence[i] * bSequence[i];
        }

        return (double.CreateChecked(sumS1S2) - double.CreateChecked(sumS1 * sumS2)
            / double.CreateChecked(aSequence.Length))
            / double.CreateChecked(aSequence.Length - 1);
    }
}
