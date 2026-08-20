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
        // the length check is done within the single pass of Evaluate,
        // enumerating the sequences twice would re-evaluate lazy pipelines
        => Evaluate(aSequence, bSequence, out count);

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
        double sumS1 = 0;
        double sumS2 = 0;
        double sumS1S2 = 0;
        count = 0;

        using (var aEnumerator = aSequence.GetEnumerator())
        using (var bEnumerator = bSequence.GetEnumerator())
        {
            while (true)
            {
                var aMoved = aEnumerator.MoveNext();
                var bMoved = bEnumerator.MoveNext();

                // one sequence ran out sooner than the other
                if (aMoved != bMoved)
                    throw new ArgumentException("Inconsistent length of sequences.");

                if (!aMoved)
                    break;

                var a = double.CreateChecked(aEnumerator.Current);
                var b = double.CreateChecked(bEnumerator.Current);
                sumS1 += a;
                sumS2 += b;
                sumS1S2 += a * b;
                count++;
            }
        }

        return (sumS1S2 - sumS1 * sumS2 / count) / (count - 1);
    }

    public static double Eval<N>(ReadOnlySpan<N> aSequence, ReadOnlySpan<N> bSequence)
        where N : INumberBase<N>
    {
        if (aSequence.Length != bSequence.Length)
            throw new ArgumentException("Inconsistent length of sequences.");

        double sumS1 = 0;
        double sumS2 = 0;
        double sumS1S2 = 0;

        for (int i = 0; i < aSequence.Length; i++)
        {
            var a = double.CreateChecked(aSequence[i]);
            var b = double.CreateChecked(bSequence[i]);
            sumS1 += a;
            sumS2 += b;
            sumS1S2 += a * b;
        }

        return (sumS1S2 - sumS1 * sumS2 / aSequence.Length) / (aSequence.Length - 1);
    }
}
