using System.Numerics;

namespace SMath.Statistics;

/// <summary>
/// Standard deviation.
/// </summary>
/// <remarks>
/// <a href="https://en.wikipedia.org/wiki/Standard_deviation">Wikipedia</a>
/// <a href="https://mathworld.wolfram.com/StandardDeviation.html">Wolfram Mathworld</a>
/// </remarks>
public static class StandardDeviation
{
    public static double FromVariance<N>(N variance)
        where N : INumberBase<N>
        => double.Sqrt(double.CreateChecked(variance));

    /// <summary>
    /// Sample standard deviation.
    /// </summary>
    public static class Sample
    {
        public static double Eval<N>(IEnumerable<N> sequence)
            where N : INumberBase<N>
            => FromVariance(Variance.Sample.Eval(sequence));

        public static double Eval<N>(ReadOnlySpan<N> sequence)
            where N : INumberBase<N>
            => FromVariance(Variance.Sample.Eval(sequence));
    }

    /// <summary>
    /// Population standard deviation.
    /// </summary>
    public static class Population
    {
        public static double Eval<N>(IEnumerable<N> sequence)
            where N : INumberBase<N>
            => FromVariance(Variance.Population.Eval(sequence));

        public static double Eval<N>(ReadOnlySpan<N> sequence)
            where N : INumberBase<N>
            => FromVariance(Variance.Population.Eval(sequence));
    }
}
