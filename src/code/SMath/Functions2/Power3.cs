using System.Numerics;

namespace SMath.Functions2;

/// <summary>
/// Power 3 function.
/// </summary>
/// <remarks>
/// <a href="https://en.wikipedia.org/wiki/Exponentiation#Power_functions">Wikipedia</a>
/// </remarks>
public class Power3 //: IMathFunction
{
    /// <inheritdoc />
    public static string PlainTextFormula
        => "x^3";

    /// <inheritdoc />
    public static N Eval<N>(N x)
        where N : IMultiplyOperators<N, N, N>
        => x * x * x;
}
