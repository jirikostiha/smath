using System.Numerics;

namespace SMath.Functions1;

/// <summary>
/// General logarithm function.
/// </summary>
/// <remarks>
/// It is the inverse of the exponential function.
/// The function is defined for positive numbers only.
/// <a href="https://en.wikipedia.org/wiki/Logarithm">Wikipedia</a>
/// </remarks>
public class Logarithm : IMathFunction
{
    /// <inheritdoc />
    public static bool IsEven
        => false;

    /// <inheritdoc />
    public static bool IsOdd
        => false;

    /// <inheritdoc />
    public static bool IsContinuous
        => true;

    /// <inheritdoc />
    public static string PlainTextFormula
        => "log(x, b)";

    /// <inheritdoc />
    public static (N Min, N Max) Domain<N>()
        where N : IFloatingPointIeee754<N>
        => (N.Zero, N.PositiveInfinity);

    /// <inheritdoc />
    public static (N Min, N Max) NumberDomain<N>()
        where N : INumberBase<N>, IMinMaxValue<N>
        => (N.Zero, N.MaxValue);

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
    public static N Eval<N>(N x, N @base)
        where N : ILogarithmicFunctions<N>
        => N.Log(x, @base);

    public static N DerivativeEval<N>(N x, N @base)
        where N : ILogarithmicFunctions<N>
        => N.One / (x * N.Log(@base));
}
