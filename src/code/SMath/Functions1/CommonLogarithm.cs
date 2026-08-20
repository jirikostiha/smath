using System.Numerics;

namespace SMath.Functions1;

/// <summary>
/// Common logarithm function.
/// </summary>
/// <remarks>
/// It is a special case of the logarithm function with the base equal to ten.
/// The function is defined for positive numbers only.
/// <a href="https://en.wikipedia.org/wiki/Common_logarithm">Wikipedia</a>
/// </remarks>
public class CommonLogarithm : IMathFunction
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
        => "log10(x)";

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
    public static N Eval<N>(N x)
        where N : ILogarithmicFunctions<N>
        => N.Log10(x);

    public static N DerivativeEval<N>(N x)
        where N : ILogarithmicFunctions<N>
        => N.One / (x * N.Log(N.CreateChecked(10)));
}
