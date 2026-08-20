using System.Numerics;
using SMath.Functions1;

namespace SMath.Functions2;

/// <summary>
/// Booth function.
/// </summary>
/// <remarks>
/// A convex unimodal function used as a performance test problem for optimization algorithms.
/// <a href="https://en.wikipedia.org/wiki/Test_functions_for_optimization">Wikipedia</a>
/// <a href="https://www.sfu.ca/~ssurjano/booth.html">Simon Fraser University</a>
/// </remarks>
public static class BoothFunction
{
    /// <summary>
    /// String representation of the function.
    /// </summary>
    public static string PlainTextFormula
        => "(x1 + 2*x2 - 7)^2 + (2*x1 + x2 - 5)^2";

    /// <summary>
    /// Point where the function attains its global minimum.
    /// </summary>
    public static (N X1, N X2) GlobalMinimumPoint<N>()
        where N : INumberBase<N>
        => (N.One, N.CreateChecked(3));

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
        where N : INumberBase<N>
    {
        var two = N.CreateChecked(2);

        return Power2.Eval(x1 + (two * x2) - N.CreateChecked(7))
            + Power2.Eval((two * x1) + x2 - N.CreateChecked(5));
    }
}
