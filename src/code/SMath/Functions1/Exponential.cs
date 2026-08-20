using System.Numerics;

namespace SMath.Functions1;

/// <summary>
/// General exponential function.
/// The variable is the exponent, the base is a parameter.
/// </summary>
/// <remarks>
/// Properties of the function hold for a positive base other than one.
/// <a href="https://en.wikipedia.org/wiki/Exponential_function">Wikipedia</a>
/// <a href="https://en.wikipedia.org/wiki/Exponentiation">Wikipedia</a>
/// </remarks>
public class Exponential : IMathFunction
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
        => "b^x";

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
        => (N.Zero, N.PositiveInfinity);

    /// <inheritdoc />
    public static (N Min, N Max) NumberImage<N>()
        where N : INumberBase<N>, IMinMaxValue<N>
        => (N.Zero, N.MaxValue);

    public static N GlobalMaximum<N>()
        where N : IFloatingPointIeee754<N>
        => N.PositiveInfinity;

    /// <summary>
    /// Infimum of the function. It is not attained for any real number.
    /// </summary>
    public static N GlobalMinimum<N>()
        where N : INumberBase<N>
        => N.Zero;

    /// <inheritdoc />
    public static N Eval<N>(N x, N @base)
        where N : IPowerFunctions<N>
        => N.Pow(@base, x);

    public static N DerivativeEval<N>(N x, N @base)
        where N : IPowerFunctions<N>, ILogarithmicFunctions<N>
        => N.Pow(@base, x) * N.Log(@base);
}
