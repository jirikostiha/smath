using System.Numerics;

namespace SMath.Functions1;

/// <summary>
/// Unit step or Heaviside step function.
/// It is a special case of the step function.
/// </summary>
/// <remarks>
/// Function is not continuous. It has a jump discontinuity at zero.
/// <a href="https://en.wikipedia.org/wiki/Heaviside_step_function">Wikipedia</a>
/// </remarks>
public class UnitStepFunction : IMathFunction
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
        => "0, x < 0; p, x = 0; 1, x > 0";

    /// <inheritdoc />
    public static (N Min, N Max) Domain<N>()
        where N : IFloatingPointIeee754<N>
        => (N.NegativeInfinity, N.PositiveInfinity);

    /// <inheritdoc />
    public static (N Min, N Max) NumberDomain<N>()
        where N : INumberBase<N>, IMinMaxValue<N>
        => (N.MinValue, N.MaxValue);

    /// <inheritdoc />
    public static (N Min, N Max) Image<N>()
        where N : IFloatingPointIeee754<N>
        => (N.Zero, N.One);

    /// <inheritdoc />
    public static (N Min, N Max) NumberImage<N>()
        where N : INumberBase<N>, IMinMaxValue<N>
        => (N.Zero, N.One);

    public static N GlobalMaximum<N>()
        where N : INumberBase<N>
        => N.One;

    public static N GlobalMinimum<N>()
        where N : INumberBase<N>
        => N.Zero;

    /// <summary>
    /// Evaluates the function value.
    /// </summary>
    /// <param name="x"> Variable. </param>
    /// <param name="p"> Value at zero. </param>
    public static N Eval<N>(N x, N p)
        where N : INumberBase<N>, IComparisonOperators<N, N, bool>
        => StepFunction.Eval(x, N.Zero, p, N.Zero, N.One);

    /// <summary>
    /// Evaluates the function value using the half maximum convention,
    /// where the value at zero is one half.
    /// </summary>
    /// <param name="x"> Variable. </param>
    public static N Eval<N>(N x)
        where N : INumberBase<N>, IComparisonOperators<N, N, bool>
        => Eval(x, N.One / N.CreateChecked(2));
}
