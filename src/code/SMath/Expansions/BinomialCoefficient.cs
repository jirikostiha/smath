using System.Numerics;

namespace SMath.Expansions;

/// <summary>
/// Binomial coefficient, "n choose k".
/// </summary>
/// <remarks>
/// <a href="https://en.wikipedia.org/wiki/Binomial_coefficient">Wikipedia</a>
/// <a href="https://mathworld.wolfram.com/BinomialCoefficient.html">Wolfram Mathworld</a>
/// </remarks>
public static class BinomialCoefficient
{
    public static string PlainTextFormula => "C(n,k) = n! / (k! * (n - k)!)";

    /// <summary>
    /// Count of k element subsets of an n element set.
    /// Zero for a negative argument or for k greater than n.
    /// </summary>
    /// <remarks>
    /// Evaluated by the reduced multiplicative formula, so only the result itself has to fit
    /// into the number type. The factorial form overflows a 64 bit integer already for n = 21
    /// and the plain multiplicative one overflows for a result over MaxValue / k.
    /// </remarks>
    public static NInt Eval<NInt>(NInt n, NInt k)
        where NInt : IBinaryInteger<NInt>
    {
        if (NInt.IsNegative(n) || NInt.IsNegative(k) || k > n)
            return NInt.Zero;

        // C(n,k) == C(n,n-k), the smaller one takes fewer steps
        if (k > n - k)
            k = n - k;

        var coefficient = NInt.One;
        for (var i = NInt.One; i <= k; i++)
        {
            var factor = n - k + i;
            var divisor = i;

            // the fraction is reduced before it is multiplied out, so no intermediate value
            // grows over the result, and what is left of the divisor divides the factor exactly
            var common = coefficient.GreatestCommonDivisor(divisor);
            coefficient /= common;
            divisor /= common;

            coefficient *= factor / divisor;
        }

        return coefficient;
    }
}
