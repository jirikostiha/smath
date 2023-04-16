using System.Numerics;

namespace SMath.Expansions
{
    /// <summary>
    /// Binomial coefficient.
    /// </summary>
    /// <remarks>
    /// <a href="https://en.wikipedia.org/wiki/Binomial_coefficient">wikipedia</a>
    /// </remarks>
    public static class BinomialCoefficient
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="n"></param>
        /// <param name="k"></param>
        /// <returns></returns>
        public static NInt Eval<NInt>(NInt n, NInt k)
            where NInt : IBinaryInteger<NInt>, IComparisonOperators<NInt, NInt, bool>
            => Factorial.Eval(n) / (Factorial.Eval(k) * Factorial.Eval(n - k));
    }
}
