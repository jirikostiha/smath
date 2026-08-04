using System.Numerics;

namespace SMath.Functions1;

/// <summary>
/// Cotangent function.
/// </summary>
/// <remarks>
/// Function is not continuous. It has vertical asymptotes at k*pi, where k is an integer.
/// <a href="https://en.wikipedia.org/wiki/Trigonometric_functions#Cotangent">Wikipedia</a>
/// </remarks>
public class Cotangent : IMathFunction
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
        => "cot(x)";

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

    /// <summary>
    /// Period of the function.
    /// </summary>
    public static N Period<N>()
        where N : IFloatingPointConstants<N>
        => N.Pi;

    /// <inheritdoc />
    public static N Eval<N>(N x)
        where N : ITrigonometricFunctions<N>, IDivisionOperators<N, N, N>
        => N.Cos(x) / N.Sin(x);

    public static N DerivativeEval<N>(N x)
        where N : ITrigonometricFunctions<N>, IDivisionOperators<N, N, N>
        => -N.One / (N.Sin(x) * N.Sin(x));
}
