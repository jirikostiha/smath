using System.Numerics;

namespace SMath.Functions1
{
    /// <summary>
    /// Step function.
    /// </summary>
    /// <remarks>
    /// <a href="https://en.wikipedia.org/wiki/Step_function">wikipedia</a>
    /// </remarks>
    public class StepFunction : IMathFunction
    {
        /// <inheritdoc />
        public static bool IsEven
            => default;

        /// <inheritdoc />
        public static bool IsOdd
            => default;

        /// <inheritdoc />
        public static bool IsContinuous
            => default;

        public const double DomainX1Min = double.NegativeInfinity;
        public const double DomainX1Max = double.PositiveInfinity;

        /// <inheritdoc />
        public const string PlainTextFormula = "Ya, x1 < C; Yb, x1 > C; P, x1 = C";

        public static N Eval<N>(N x1, N c, N p, N ya, N yb)
            where N : INumberBase<N>, IComparisonOperators<N, N, bool>
            => (x1 < c) ? ya : (x1 > c) ? yb : p;
    }
}
