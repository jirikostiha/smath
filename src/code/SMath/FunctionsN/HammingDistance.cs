namespace SMath.FunctionsN;

/// <summary>
/// Hamming distance of two sequences of equal length.
/// </summary>
/// <remarks>
/// It is the number of positions at which the corresponding items differ.
/// For the bitwise distance of two numbers see
/// <see cref="BinaryIntegerExtension.HammingDistanceTo{N}(N, N)"/>.
/// <a href="https://en.wikipedia.org/wiki/Hamming_distance">Wikipedia</a>
/// <a href="https://xlinux.nist.gov/dads/HTML/HammingDistance.html">NIST</a>
/// </remarks>
public static class HammingDistance
{
    /// <summary>
    /// String representation of the function.
    /// </summary>
    public static string PlainTextFormula
        => "count(xi != yi)";

    /// <summary>
    /// Hamming distance of two sequences.
    /// </summary>
    /// <param name="sequence1"> First sequence. </param>
    /// <param name="sequence2"> Second sequence. </param>
    /// <param name="comparer"> Comparer of the items, the default comparer is used when it is null. </param>
    public static int Eval<T>(IEnumerable<T> sequence1, IEnumerable<T> sequence2,
        IEqualityComparer<T>? comparer = null)
    {
        comparer ??= EqualityComparer<T>.Default;
        int distance = 0;

        using (var enumerator1 = sequence1.GetEnumerator())
        using (var enumerator2 = sequence2.GetEnumerator())
        {
            while (true)
            {
                var moved1 = enumerator1.MoveNext();
                var moved2 = enumerator2.MoveNext();

                // one sequence ran out sooner than the other
                if (moved1 != moved2)
                    throw new ArgumentException("Inconsistent length of sequences.");

                if (!moved1)
                    break;

                if (!comparer.Equals(enumerator1.Current, enumerator2.Current))
                    distance++;
            }
        }

        return distance;
    }

    /// <summary>
    /// Hamming distance of two sequences.
    /// </summary>
    /// <param name="sequence1"> First sequence. </param>
    /// <param name="sequence2"> Second sequence. </param>
    /// <param name="comparer"> Comparer of the items, the default comparer is used when it is null. </param>
    public static int Eval<T>(ReadOnlySpan<T> sequence1, ReadOnlySpan<T> sequence2,
        IEqualityComparer<T>? comparer = null)
    {
        if (sequence1.Length != sequence2.Length)
            throw new ArgumentException("Inconsistent length of sequences.");

        comparer ??= EqualityComparer<T>.Default;
        int distance = 0;

        for (int i = 0; i < sequence1.Length; i++)
        {
            if (!comparer.Equals(sequence1[i], sequence2[i]))
                distance++;
        }

        return distance;
    }
}
