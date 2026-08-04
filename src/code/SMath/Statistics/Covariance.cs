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
        var sumS1 = N.Zero;
        var sumS2 = N.Zero;
        var sumS1S2 = N.Zero;
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

                var a = aEnumerator.Current;
                var b = bEnumerator.Current;
                sumS1 += a;
                sumS2 += b;
                sumS1S2 += a * b;
                count++;
            }
        }

        // the sums are widened before multiplying, the product of two N sums can overflow N
        return (double.CreateChecked(sumS1S2) - double.CreateChecked(sumS1) * double.CreateChecked(sumS2)
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
            var a = aSequence[i];
            var b = bSequence[i];
            sumS1 += a;
            sumS2 += b;
            sumS1S2 += a * b;
        }

        // the sums are widened before multiplying, the product of two N sums can overflow N
        return (double.CreateChecked(sumS1S2) - double.CreateChecked(sumS1) * double.CreateChecked(sumS2)
            / double.CreateChecked(aSequence.Length))
            / double.CreateChecked(aSequence.Length - 1);
    }
}
