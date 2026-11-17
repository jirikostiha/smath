using System.Numerics;

namespace SMath.Functions1;

/// <summary>Fourth root function.</summary>
/// <remarks>
/// The function is defined for non negative numbers only.
/// <a href="https://en.wikipedia.org/wiki/Nth_root">Wikipedia</a>
/// </remarks>
public class Root4 : IMathFunction, ISingleDomain, ISingleImage
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
        => "x^(1/4)";

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
    // For performance: two square roots are about ten times faster than RootN for double and float,
    // at the cost of an error of up to 1 ulp instead of a correctly rounded result.
    public static N Eval<N>(N x)
        where N : IRootFunctions<N>
        => N.Sqrt(N.Sqrt(x));

    public static N DerivativeEval<N>(N x)
        where N : IRootFunctions<N>
        => N.One / (N.CreateChecked(4) * Power3.Eval(Eval(x)));
}
