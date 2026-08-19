using System.Numerics;

namespace SMath.Statistics;

/// <summary>
/// Median.
/// </summary>
/// <remarks>
/// <a href="https://en.wikipedia.org/wiki/Median">Wikipedia</a>
/// </remarks>
public static class Median
{
    public static double Eval<N>(IEnumerable<N> numbers)
        where N : INumberBase<N>
    {
        var array = numbers.Select(n => double.CreateChecked(n)).ToArray();
        if (array.Length == 0) return double.NaN;

        Array.Sort(array);
        int mid = array.Length / 2;

        if (array.Length % 2 != 0)
            return array[mid];
        else
            return (array[mid - 1] + array[mid]) / 2d;
    }

    public static double Eval<N>(ReadOnlySpan<N> numbers)
        where N : INumberBase<N>
    {
        if (numbers.Length == 0) return double.NaN;

        var array = new double[numbers.Length];
        for (int i = 0; i < numbers.Length; i++)
            array[i] = double.CreateChecked(numbers[i]);

        Array.Sort(array);
        int mid = array.Length / 2;

        if (array.Length % 2 != 0)
            return array[mid];
        else
            return (array[mid - 1] + array[mid]) / 2d;
    }
}
