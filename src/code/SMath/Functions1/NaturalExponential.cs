using System.Numerics;

namespace SMath.Functions1;

/// <summary>
/// Natural exponential function.
/// It is a special case of the exponential function with the base equal to Euler's number.
/// </summary>
/// <remarks>
/// <a href="https://en.wikipedia.org/wiki/Exponential_function">Wikipedia</a>
/// <a href="https://en.wikipedia.org/wiki/E_(mathematical_constant)">Wikipedia</a>
/// </remarks>
public class NaturalExponential : IMathFunction
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
        => "e^x";

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
    public static N Eval<N>(N x)
        where N : IExponentialFunctions<N>
        => N.Exp(x);

    public static N DerivativeEval<N>(N x)
        where N : IExponentialFunctions<N>
        => N.Exp(x);
}
