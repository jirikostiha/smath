using System.Numerics;

namespace SMath.Expansions;

/// <summary>
/// Binomial coefficient "n choose k".
/// </summary>
/// <remarks>
/// <a href="https://en.wikipedia.org/wiki/Binomial_coefficient">Wikipedia</a>
/// </remarks>
public static class BinomialCoefficient
{
    public static NInt Eval<NInt>(NInt n, NInt k)
        where NInt : IBinaryInteger<NInt>, IComparisonOperators<NInt, NInt, bool>
        => Factorial.Eval(n) / (Factorial.Eval(k) * Factorial.Eval(n - k));
}
