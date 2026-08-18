using System.Numerics;
using SMath.Sequences;

namespace SMath;

/// <summary>
/// Factorial.
/// </summary>
/// <remarks>
/// <a href="https://en.wikipedia.org/wiki/Factorial">Wikipedia</a>
/// </remarks>
public static class Factorial
{
    public static NInt Eval<NInt>(NInt n)
        where NInt : IBinaryInteger<NInt>, IComparisonOperators<NInt, NInt, bool>
        => n == NInt.Zero
            ? NInt.One
            : Product.Eval(ArithmeticSequence.Terms(NInt.One, NInt.One, n));
}
