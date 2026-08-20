using System.Numerics;

namespace SMath.Functions1;

/// <summary>
/// Cube root function.
/// </summary>
/// <remarks>
/// The function is defined for all real numbers, the root of a negative number is negative.
/// <a href="https://en.wikipedia.org/wiki/Cube_root">Wikipedia</a>
/// <a href="https://en.wikipedia.org/wiki/Nth_root">Wikipedia</a>
/// </remarks>
public class Root3 : IMathFunction
{
    /// <inheritdoc />
    public static bool IsEven
        => false;

    /// <inheritdoc />
    public static bool IsOdd
        => true;

    /// <inheritdoc />
    public static bool IsContinuous
        => true;

    /// <inheritdoc />
    public static string PlainTextFormula
        => "x^(1/3)";

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
        => (N.NegativeInfinity, N.PositiveInfinity);

    /// <inheritdoc />
    public static (N Min, N Max) NumberImage<N>()
        where N : INumberBase<N>, IMinMaxValue<N>
        => (N.MinValue, N.MaxValue);

    public static N GlobalMaximum<N>()
        where N : IFloatingPointIeee754<N>
        => N.PositiveInfinity;

    public static N GlobalMinimum<N>()
        where N : IFloatingPointIeee754<N>
        => N.NegativeInfinity;

    /// <inheritdoc />
    public static N Eval<N>(N x)
        where N : IRootFunctions<N>
        => N.Cbrt(x);

    public static N DerivativeEval<N>(N x)
        where N : IRootFunctions<N>
        => N.One / (N.CreateChecked(3) * Power2.Eval(N.Cbrt(x)));
}
