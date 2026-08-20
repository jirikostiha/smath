using System.Numerics;

namespace SMath.Statistics;

/// <summary>
/// Mode.
/// </summary>
/// <remarks>
/// <a href="https://en.wikipedia.org/wiki/Mode_(statistics)">Wikipedia</a>
/// </remarks>
public static class Mode
{
    public static IEnumerable<N> Eval<N>(IEnumerable<N> numbers)
        where N : INumberBase<N>
    {
        var counts = new Dictionary<N, int>();
        int maxCount = 0;

        foreach (var val in numbers)
        {
            counts.TryGetValue(val, out int count);
            count++;
            counts[val] = count;
            if (count > maxCount)
                maxCount = count;
        }

        if (maxCount == 0) yield break;

        foreach (var pair in counts)
        {
            if (pair.Value == maxCount)
                yield return pair.Key;
        }
    }

    public static IEnumerable<N> Eval<N>(ReadOnlySpan<N> numbers)
        where N : INumberBase<N>
    {
        if (numbers.Length == 0) return Array.Empty<N>();

        var counts = new Dictionary<N, int>();
        int maxCount = 0;

        for (int i = 0; i < numbers.Length; i++)
        {
            var val = numbers[i];
            counts.TryGetValue(val, out int count);
            count++;
            counts[val] = count;
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
