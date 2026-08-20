using System.Numerics;
using System.Runtime.CompilerServices;

namespace SMath.Statistics;

/// <summary>
/// Variance.
/// </summary>
/// <remarks>
/// <a href="https://en.wikipedia.org/wiki/Variance">Wikipedia</a>
/// <a href="https://mathworld.wolfram.com/Variance.html">Wolfram Mathworld</a>
/// </remarks>
public static class Variance
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static double PreEvaluate<N>(IEnumerable<N> sequence, out int count)
        where N : INumberBase<N>
    {
        double mean = 0;
        double moment = 0;
        count = 0;

        foreach (var n in sequence)
        {
            count++;
            double x = double.CreateChecked(n);
            double delta = x - mean;
            mean += delta / count;
            moment += delta * (x - mean);
        }

        return moment;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static double PreEvaluate<N>(ReadOnlySpan<N> sequence)
        where N : INumberBase<N>
    {
        double mean = 0;
        double moment = 0;

        for (int i = 0; i < sequence.Length; i++)
        {
            double x = double.CreateChecked(sequence[i]);
            double delta = x - mean;
            mean += delta / (i + 1);
            moment += delta * (x - mean);
        }

        return moment;
    }

    /// <summary>
    /// Sample variance.
    /// </summary>
    public static class Sample
    {
        public static double Eval<N>(IEnumerable<N> sequence)
            where N : INumberBase<N>
            =>
            PreEvaluate(sequence, out int count) / (double.CreateChecked(count) - 1d);

        public static double Eval<N>(ReadOnlySpan<N> sequence)
            where N : INumberBase<N>
            =>
            PreEvaluate(sequence) / (double.CreateChecked(sequence.Length) - 1d);
    }

    /// <summary>
    /// Population variance.
    /// </summary>
    public static class Population
    {
        public static double Eval<N>(IEnumerable<N> sequence)
            where N : INumberBase<N>
            =>
            PreEvaluate(sequence, out int count) / double.CreateChecked(count);

        public static double Eval<N>(ReadOnlySpan<N> sequence)
            where N : INumberBase<N>
            =>
            PreEvaluate(sequence) / double.CreateChecked(sequence.Length);
    }
}
