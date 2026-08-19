using System.Numerics;

namespace SMath.Functions1;

/// <summary>
/// Power 5 (quintic) function.
/// </summary>
/// <remarks>
/// <a href="https://en.wikipedia.org/wiki/Exponentiation#Power_functions">Wikipedia</a>
/// </remarks>
public class Power5 : IMathFunction
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
        => "x^5";

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
        where N : IMultiplyOperators<N, N, N>
        => x * x * x * x * x;

    public static N DerivativeEval<N>(N x)
        where N : INumberBase<N>
        => N.CreateChecked(5) * x * x * x * x;
}
