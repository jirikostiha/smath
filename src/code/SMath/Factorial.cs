using SMath.Sequences;
using System.Numerics;

namespace SMath
{
    /// <summary>
    /// Factorial.
    /// </summary>
    /// <remarks>
    /// <a href="https://en.wikipedia.org/wiki/Factorial">wikipedia</a>
    /// </remarks>
    public static class Factorial
    {
        public static NInt Eval<NInt>(NInt n)
            where NInt : IBinaryInteger<NInt>, IComparisonOperators<NInt, NInt, bool>
        {
            return n == NInt.Zero
                ? NInt.One
                : Product.Eval(ArithmeticSequence.Terms(NInt.One, NInt.One, n));
        }
    }

    /// <summary>
    /// Exponential factorial
    /// </summary>
    /// <remarks>
    /// <a href="https://en.wikipedia.org/wiki/Exponential_factorial">wikipedia</a>
    /// </remarks>
    //public static class ExponentialFactorial
    //{
    //    //todo
    //}
}
