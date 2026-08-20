using System.Numerics;

namespace SMath.Functions1;

/// <summary>
/// Sigmoid or standard logistic function.
/// It is a special case of the logistic function with the maximum equal to one,
/// the growth rate equal to one and the midpoint at zero.
/// </summary>
/// <remarks>
/// <a href="https://en.wikipedia.org/wiki/Sigmoid_function">Wikipedia</a>
/// <a href="https://mathworld.wolfram.com/SigmoidFunction.html">Wolfram Mathworld</a>
/// </remarks>
public class SigmoidFunction : IMathFunction
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
        => "1 / (1 + e^(-x))";

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
        => (N.Zero, N.One);

    /// <inheritdoc />
    public static (N Min, N Max) NumberImage<N>()
        where N : INumberBase<N>, IMinMaxValue<N>
        => (N.Zero, N.One);

    /// <summary>
    /// Supremum of the function. It is not attained for any real number.
    /// </summary>
    public static N GlobalMaximum<N>()
        where N : INumberBase<N>
        => N.One;

    /// <summary>
    /// Infimum of the function. It is not attained for any real number.
    /// </summary>
    public static N GlobalMinimum<N>()
        where N : INumberBase<N>
        => N.Zero;

    /// <inheritdoc />
    public static N Eval<N>(N x)
        where N : IExponentialFunctions<N>
        => N.One / (N.One + N.Exp(-x));

    public static N DerivativeEval<N>(N x)
        where N : IExponentialFunctions<N>
    {
        var value = Eval(x);
        return value * (N.One - value);
    }
}
