using System.Numerics;

namespace SMath.Functions1;

/// <summary>
/// Eighth root function.
/// </summary>
/// <remarks>
/// The function is defined for non negative numbers only.
/// <a href="https://en.wikipedia.org/wiki/Nth_root">Wikipedia</a>
/// </remarks>
public class Root8 : IMathFunction
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
        => "x^(1/8)";

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
        => (N.Zero, N.PositiveInfinity);

    /// <inheritdoc />
    public static (N Min, N Max) NumberImage<N>()
        where N : INumberBase<N>, IMinMaxValue<N>
        => (N.Zero, N.MaxValue);

    public static N GlobalMaximum<N>()
        where N : IFloatingPointIeee754<N>
        => N.PositiveInfinity;

    public static N GlobalMinimum<N>()
        where N : INumberBase<N>
        => N.Zero;

    /// <inheritdoc />
    public static N Eval<N>(N x)
        where N : IRootFunctions<N>
        => N.RootN(x, 8);

    public static N DerivativeEval<N>(N x)
        where N : IRootFunctions<N>
        => N.One / (N.CreateChecked(8) * Power7.Eval(N.RootN(x, 8)));
}
