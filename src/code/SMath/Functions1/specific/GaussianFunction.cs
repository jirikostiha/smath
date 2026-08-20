using System.Numerics;

namespace SMath.Functions1;

/// <summary>
/// Gaussian function.
/// </summary>
/// <remarks>
/// The image of the function depends on the height parameter, therefore it is not declared here.
/// <a href="https://en.wikipedia.org/wiki/Gaussian_function">Wikipedia</a>
/// </remarks>
public class GaussianFunction : IMathFunction
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
        => "a * e^(-((x - b)^2 / (2*c^2)))";

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
    /// <param name="a"> Height of the curve peak. </param>
    /// <param name="b"> Position of the center of the peak. </param>
    /// <param name="c"> Standard deviation, it controls the width of the bell. </param>
    public static N Eval<N>(N x, N a, N b, N c)
        where N : IExponentialFunctions<N>
        => a * N.Exp(-(Power2.Eval(x - b) / (N.CreateChecked(2) * Power2.Eval(c))));

    public static N DerivativeEval<N>(N x, N a, N b, N c)
        where N : IExponentialFunctions<N>
        => -((x - b) / Power2.Eval(c)) * Eval(x, a, b, c);
}
