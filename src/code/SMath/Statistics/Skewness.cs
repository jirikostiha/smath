using System.Numerics;

namespace SMath.Statistics;

/// <summary>
/// Skewness.
/// </summary>
/// <remarks>
/// <a href="https://en.wikipedia.org/wiki/Skewness">Wikipedia</a>
/// </remarks>
public static class Skewness
{
    public static double Eval<N>(IEnumerable<N> sequence)
        where N : INumberBase<N>
        => StandardizedMoment.Eval(sequence, 3);

    public static double Eval<N>(ReadOnlySpan<N> sequence)
        where N : INumberBase<N>
        => StandardizedMoment.Eval(sequence, 3);
}
