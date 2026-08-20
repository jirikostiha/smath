using System.Numerics;

namespace SMath.Statistics;

/// <summary>
/// Kurtosis.
/// </summary>
/// <remarks>
/// <a href="https://en.wikipedia.org/wiki/Kurtosis">Wikipedia</a>
/// </remarks>
public static class Kurtosis
{
    public static double Eval<N>(IEnumerable<N> numbers)
        where N : INumberBase<N>
        => StandardizedMoment.Eval(numbers, 4);

    public static double Eval<N>(ReadOnlySpan<N> numbers)
        where N : INumberBase<N>
        => StandardizedMoment.Eval(numbers, 4);
}

/// <summary>
/// Excess kurtosis.
/// </summary>
/// <remarks>
/// <a href="https://en.wikipedia.org/wiki/Kurtosis#Excess_kurtosis">Wikipedia</a>
/// </remarks>
public static class ExcessKurtosis
{
    public static double Eval<N>(IEnumerable<N> sequence)
        where N : INumberBase<N>
        => Kurtosis.Eval(sequence) - 3d;

    public static double Eval<N>(ReadOnlySpan<N> sequence)
        where N : INumberBase<N>
        => Kurtosis.Eval(sequence) - 3d;
}
