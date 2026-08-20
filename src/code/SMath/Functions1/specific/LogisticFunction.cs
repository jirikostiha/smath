using System.Numerics;

namespace SMath.Functions1;

/// <summary>
/// Logistic function.
/// </summary>
/// <remarks>
/// The image of the function depends on the supremum parameter, therefore it is not declared here.
/// <a href="https://en.wikipedia.org/wiki/Logistic_function">Wikipedia</a>
/// </remarks>
public class LogisticFunction : IMathFunction
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
        => "l / (1 + e^(-k*(x - x0)))";

    /// <inheritdoc />
    public static (N Min, N Max) Domain<N>()
        where N : IFloatingPointIeee754<N>
        => (N.NegativeInfinity, N.PositiveInfinity);

    /// <inheritdoc />
    public static (N Min, N Max) NumberDomain<N>()
        where N : INumberBase<N>, IMinMaxValue<N>
        => (N.MinValue, N.MaxValue);

    /// <summary>
    /// Evaluates the function value.
    /// </summary>
    /// <param name="x"> Variable. </param>
    /// <param name="l"> Supremum of the function. </param>
    /// <param name="k"> Logistic growth rate or steepness of the curve. </param>
    /// <param name="x0"> Value of the variable at the midpoint of the curve. </param>
    public static N Eval<N>(N x, N l, N k, N x0)
        where N : IExponentialFunctions<N>
        => l / (N.One + N.Exp(-k * (x - x0)));

    /// <summary>
    /// Evaluates the function value of a curve with the midpoint at zero.
    /// </summary>
    /// <param name="x"> Variable. </param>
    /// <param name="l"> Supremum of the function. </param>
    /// <param name="k"> Logistic growth rate or steepness of the curve. </param>
    public static N Eval<N>(N x, N l, N k)
        where N : IExponentialFunctions<N>
        => Eval(x, l, k, N.Zero);

    public static N DerivativeEval<N>(N x, N l, N k, N x0)
        where N : IExponentialFunctions<N>
    {
        var value = Eval(x, l, k, x0);
        return k * value * (N.One - (value / l));
    }

    public static N DerivativeEval<N>(N x, N l, N k)
        where N : IExponentialFunctions<N>
        => DerivativeEval(x, l, k, N.Zero);
}
