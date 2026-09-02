using System.Numerics;

namespace SMath.Statistics;

/// <summary>
/// Central moment of a data set.
/// </summary>
/// <remarks>
/// <a href="https://en.wikipedia.org/wiki/Central_moment">Wikipedia</a>
/// </remarks>
public static class Moment
{
    public static double Eval<N>(IEnumerable<N> sequence, int degree)
        where N : INumberBase<N>
    {
        if (degree < 1) throw new ArgumentOutOfRangeException(nameof(degree));

        var mean = ArithmeticMean.Eval(sequence);
        double sum = 0;
        int count = 0;

        foreach (var n in sequence)
        {
            var diff = double.CreateChecked(n) - mean;
            sum += double.Pow(diff, degree);
            count++;
        }

        if (count == 0) return double.NaN;

        return sum / count;
    }

    public static double Eval<N>(ReadOnlySpan<N> sequence, int degree)
        where N : INumberBase<N>
    {
        if (degree < 1) throw new ArgumentOutOfRangeException(nameof(degree));
        if (sequence.Length == 0) return double.NaN;

        var mean = ArithmeticMean.Eval(sequence);
        double sum = 0;

        for (int i = 0; i < sequence.Length; i++)
        {
            var diff = double.CreateChecked(sequence[i]) - mean;
            sum += double.Pow(diff, degree);
        }

        return sum / sequence.Length;
    }
}

/// <summary>
/// Standardized moment of a data set.
/// </summary>
/// <remarks>
/// <a href="https://en.wikipedia.org/wiki/Standardized_moment">Wikipedia</a>
/// </remarks>
public static class StandardizedMoment
{
    public static double Eval<N>(IEnumerable<N> sequence, int degree)
        where N : INumberBase<N>
    {
        if (degree < 1) throw new ArgumentOutOfRangeException(nameof(degree));
        if (degree == 1) return 0d;
        if (degree == 2) return 1d;

        var list = sequence as IReadOnlyList<N> ?? sequence.ToArray();
        if (list.Count == 0) return double.NaN;

        var mean = ArithmeticMean.Eval(list);
        double sumK = 0;
        double sum2 = 0;

        for (int i = 0; i < list.Count; i++)
        {
            var diff = double.CreateChecked(list[i]) - mean;
            sum2 += diff * diff;
            sumK += double.Pow(diff, degree);
        }

        var m2 = sum2 / list.Count;
        var mk = sumK / list.Count;

        var stdev = double.Sqrt(m2);
        if (stdev == 0) return double.NaN;

        return mk / double.Pow(stdev, degree);
    }

    public static double Eval<N>(ReadOnlySpan<N> sequence, int degree)
        where N : INumberBase<N>
    {
        if (degree < 1) throw new ArgumentOutOfRangeException(nameof(degree));
        if (degree == 1) return 0d;
        if (degree == 2) return 1d;
        if (sequence.Length == 0) return double.NaN;

        var mean = ArithmeticMean.Eval(sequence);
        double sumK = 0;
        double sum2 = 0;

        for (int i = 0; i < sequence.Length; i++)
        {
            var diff = double.CreateChecked(sequence[i]) - mean;
            sum2 += diff * diff;
            sumK += double.Pow(diff, degree);
        }

        var m2 = sum2 / sequence.Length;
        var mk = sumK / sequence.Length;

        var stdev = double.Sqrt(m2);
        if (stdev == 0) return double.NaN;

        return mk / double.Pow(stdev, degree);
    }
}
