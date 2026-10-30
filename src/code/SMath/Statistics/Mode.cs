using System.Numerics;
using System.Runtime.InteropServices;

namespace SMath.Statistics;

/// <summary>Mode.</summary>
/// <remarks><a href="https://en.wikipedia.org/wiki/Mode_(statistics)">Wikipedia</a></remarks>
public static class Mode
{
    public static IEnumerable<N> Eval<N>(IEnumerable<N> numbers)
        where N : INumberBase<N>
    {
        var counts = CountOccurrences(numbers, out int maxCount);

        if (maxCount == 0) yield break;

        foreach (var pair in counts)
        {
            if (pair.Value == maxCount)
                yield return pair.Key;
        }
    }

    // A ref local is not allowed in an iterator before C# 13, so the counting lives outside of Eval.
    private static Dictionary<N, int> CountOccurrences<N>(IEnumerable<N> numbers, out int maxCount)
        where N : INumberBase<N>
    {
        var counts = new Dictionary<N, int>();
        maxCount = 0;

        foreach (var val in numbers)
        {
            // one hash lookup instead of the read and the write of a TryGetValue pair
            ref var count = ref CollectionsMarshal.GetValueRefOrAddDefault(counts, val, out _);
            count++;
            if (count > maxCount)
                maxCount = count;
        }

        return counts;
    }

    public static IEnumerable<N> Eval<N>(ReadOnlySpan<N> numbers)
        where N : INumberBase<N>
    {
        if (numbers.Length == 0) return Array.Empty<N>();

        // TODO: duplicates the counting of CountOccurrences; a span cannot be passed as IEnumerable<N>, so it is not shared yet.
        var counts = new Dictionary<N, int>();
        int maxCount = 0;

        for (int i = 0; i < numbers.Length; i++)
        {
            ref var count = ref CollectionsMarshal.GetValueRefOrAddDefault(counts, numbers[i], out _);
            count++;
            if (count > maxCount)
                maxCount = count;
        }

        var result = new List<N>();
        foreach (var pair in counts)
        {
            if (pair.Value == maxCount)
                result.Add(pair.Key);
        }

        return result;
    }
}
