namespace SMath.FunctionsN
{
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Hamming distance.
    /// </summary>
    /// <remarks>
    /// <a href="">wikipedia</a>
    /// <a href="">NIST</a>
    /// </remarks>
    public static class HammingDistance
    {
        public static int f<T>(IList<T> source1, IList<T> source2, IEqualityComparer<T>? comparer = null)
        {
            comparer ??= EqualityComparer<T>.Default;
            int distance = Abs(source1.Count - source2.Count);
            for (int i = 0; i < Min(source1.Count, source2.Count); i++)
            {
                if (!comparer.Equals(source1[i], source2[i]))
                    distance++;
            }

            return distance;
        }
    }
}
