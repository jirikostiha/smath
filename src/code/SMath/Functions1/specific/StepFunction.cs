using System.Numerics;

namespace SMath.Functions1;

/// <summary>
/// Step function with a single jump.
/// </summary>
/// <remarks>
/// Function is not continuous. It has a jump discontinuity at the threshold.
/// The image of the function depends on the value parameters, therefore it is not declared here.
/// <a href="https://en.wikipedia.org/wiki/Step_function">Wikipedia</a>
/// </remarks>
public class StepFunction : IMathFunction
{
    /// <inheritdoc />
    public static bool IsEven
        => false;

    /// <inheritdoc />
    public static bool IsOdd
        => false;

    /// <inheritdoc />
    public static bool IsContinuous
        => false;

    /// <inheritdoc />
    public static string PlainTextFormula
        => "ya, x < c; p, x = c; yb, x > c";

    /// <inheritdoc />
    public static (N Min, N Max) Domain<N>()
        where N : IFloatingPointIeee754<N>
        => (N.NegativeInfinity, N.PositiveInfinity);

    /// <inheritdoc />
    public static (N Min, N Max) NumberDomain<N>()
        where N : INumberBase<N>, IMinMaxValue<N>
        => (N.MinValue, N.MaxValue);

    /// <summary>
    /// Evaluates the function value.
    /// </summary>
    /// <param name="x"> Variable. </param>
    /// <param name="c"> Threshold where the function jumps. </param>
    /// <param name="p"> Value at the threshold. </param>
    /// <param name="ya"> Value below the threshold. </param>
    /// <param name="yb"> Value above the threshold. </param>
    public static N Eval<N>(N x, N c, N p, N ya, N yb)
        where N : INumberBase<N>, IComparisonOperators<N, N, bool>
        => x < c ? ya : x > c ? yb : p;
}
