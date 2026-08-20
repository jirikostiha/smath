using System.Numerics;

namespace SMath.Functions1;

/// <summary>
/// Sign or signum function.
/// It is a special case of the step function.
/// </summary>
/// <remarks>
/// Function is not continuous. It has a jump discontinuity at zero.
/// <a href="https://en.wikipedia.org/wiki/Sign_function">Wikipedia</a>
/// </remarks>
public class SignFunction : IMathFunction
{
    /// <inheritdoc />
    public static bool IsEven
        => false;

    /// <inheritdoc />
    public static bool IsOdd
        => true;

    /// <inheritdoc />
    public static bool IsContinuous
        => false;

    /// <inheritdoc />
    public static string PlainTextFormula
        => "-1, x < 0; 0, x = 0; 1, x > 0";

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
        => (-N.One, N.One);

    /// <inheritdoc />
    public static (N Min, N Max) NumberImage<N>()
        where N : INumberBase<N>, IMinMaxValue<N>
        => (-N.One, N.One);

    public static N GlobalMaximum<N>()
        where N : INumberBase<N>
        => N.One;

    public static N GlobalMinimum<N>()
        where N : INumberBase<N>
        => -N.One;

    /// <inheritdoc />
    public static N Eval<N>(N x)
        where N : INumberBase<N>, IComparisonOperators<N, N, bool>
        => StepFunction.Eval(x, N.Zero, N.Zero, -N.One, N.One);
}
