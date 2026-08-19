using System.Numerics;

namespace SMath.Functions1;

/// <summary>
/// Polynomial function of a single variable.
/// </summary>
/// <remarks>
/// Coefficients are ordered from the lowest degree to the highest,
/// so <c>[c0, c1, c2]</c> stands for <c>c0 + c1*x + c2*x^2</c>.
/// Values are evaluated by Horner's method.
/// <a href="https://en.wikipedia.org/wiki/Polynomial">Wikipedia</a>
/// <a href="https://en.wikipedia.org/wiki/Horner%27s_method">Wikipedia</a>
/// </remarks>
public static class Polynomial
{
    /// <summary>
    /// String representation of the function.
    /// </summary>
    public static string PlainTextFormula
        => "c0 + c1*x + c2*x^2 + ... + cn*x^n";

    /// <summary>
    /// Degree of the polynomial. It is -1 for the zero polynomial.
    /// </summary>
    public static int Degree<N>(ReadOnlySpan<N> coefficients)
        where N : INumberBase<N>
    {
        for (int i = coefficients.Length - 1; i >= 0; i--)
        {
            if (!N.IsZero(coefficients[i]))
                return i;
        }

        return -1;
    }

    /// <summary>
    /// Evaluates the polynomial value.
    /// </summary>
    public static N Eval<N>(N x, ReadOnlySpan<N> coefficients)
        where N : INumberBase<N>
    {
        var value = N.Zero;
        for (int i = coefficients.Length - 1; i >= 0; i--)
            value = (value * x) + coefficients[i];

        return value;
    }

    /// <summary>
    /// Evaluates the polynomial value.
    /// </summary>
    public static N Eval<N>(N x, IEnumerable<N> coefficients)
        where N : INumberBase<N>
    {
        var value = N.Zero;
        var power = N.One;
        foreach (var coefficient in coefficients)
        {
            value += coefficient * power;
            power *= x;
        }

        return value;
    }

    /// <summary>
    /// Evaluates the value of the first derivative of the polynomial.
    /// </summary>
    public static N DerivativeEval<N>(N x, ReadOnlySpan<N> coefficients)
        where N : INumberBase<N>
    {
        var value = N.Zero;
        for (int i = coefficients.Length - 1; i >= 1; i--)
            value = (value * x) + (N.CreateChecked(i) * coefficients[i]);

        return value;
    }

    /// <summary>
    /// Evaluates the value of the first derivative of the polynomial.
    /// </summary>
    public static N DerivativeEval<N>(N x, IEnumerable<N> coefficients)
        where N : INumberBase<N>
    {
        var value = N.Zero;
        var power = N.One;
        int degree = 0;
        foreach (var coefficient in coefficients)
        {
            if (degree > 0)
            {
                value += N.CreateChecked(degree) * coefficient * power;
                power *= x;
            }

            degree++;
        }

        return value;
    }

    /// <summary>
    /// Coefficients of the first derivative of the polynomial.
    /// </summary>
    public static N[] DerivativeCoefficients<N>(ReadOnlySpan<N> coefficients)
        where N : INumberBase<N>
    {
        if (coefficients.Length < 2)
            return [];

        var derivative = new N[coefficients.Length - 1];
        for (int i = 1; i < coefficients.Length; i++)
            derivative[i - 1] = N.CreateChecked(i) * coefficients[i];

        return derivative;
    }
}
