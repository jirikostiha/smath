using System.Numerics;

namespace SMath.Functions2;

/// <summary>
/// Ackley function.
/// </summary>
/// <remarks>
/// A non convex multimodal function used as a performance test problem for optimization
/// algorithms. The many local minima of the nearly flat outer region make gradient based
/// methods get trapped before they reach the single global minimum at the origin.
/// <a href="https://en.wikipedia.org/wiki/Ackley_function">Wikipedia</a>
/// <a href="https://www.sfu.ca/~ssurjano/ackley.html">Simon Fraser University</a>
/// </remarks>
public static class AckleyFunction
{
    /// <summary>
    /// String representation of the function.
    /// </summary>
    public static string PlainTextFormula
        => "-20*e^(-0.2*sqrt(0.5*(x1^2 + x2^2))) - e^(0.5*(cos(2*pi*x1) + cos(2*pi*x2))) + e + 20";

    /// <summary>
    /// Point where the function attains its global minimum.
    /// </summary>
    public static (N X1, N X2) GlobalMinimumPoint<N>()
        where N : INumberBase<N>
        => (N.Zero, N.Zero);

    /// <summary>
    /// Value of the global minimum.
    /// </summary>
    public static N GlobalMinimum<N>()
        where N : INumberBase<N>
        => N.Zero;

    /// <summary>
    /// Evaluates the function value.
    /// </summary>
    public static N Eval<N>(N x1, N x2)
        where N : IRootFunctions<N>, IExponentialFunctions<N>, ITrigonometricFunctions<N>
    {
        var two = N.CreateChecked(2);
        var twenty = N.CreateChecked(20);
        var half = N.One / two;
        var fifth = N.One / N.CreateChecked(5);

        var sumOfSquares = (x1 * x1) + (x2 * x2);
        var sumOfCosines = N.Cos(two * N.Pi * x1) + N.Cos(two * N.Pi * x2);

        return (-twenty * N.Exp(-fifth * N.Sqrt(half * sumOfSquares)))
            - N.Exp(half * sumOfCosines)
            + N.E
            + twenty;
    }
}
