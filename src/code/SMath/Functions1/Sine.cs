using System.Numerics;

namespace SMath.Functions1
{
    /// <summary>
    /// Sine function.
    /// </summary>
    /// <remarks>
    /// <a href="https://en.wikipedia.org/wiki/Sine">wikipedia</a>
    /// </remarks>
    public class Sine : IMathFunction
    {
        /// <inheritdoc />
        public static bool IsEven
            => true;

        /// <inheritdoc />
        public static bool IsOdd
            => false;

        /// <inheritdoc />
        public static bool IsContinuous
            => true;

        /// <inheritdoc />
        public static string PlainTextFormula
            => "sin(x)";

        /// <inheritdoc />
        public static (N Min, N Max) Domain<N>()
            where N : IFloatingPointIeee754<N>
            => (N.NegativeInfinity, N.PositiveInfinity);

        /// <inheritdoc />
        public static (N Min, N Max) NumberDomain<N>()
            where N : INumberBase<N>, IMinMaxValue<N>
            => (N.MaxValue, N.MaxValue);

        /// <inheritdoc />
        public static (N Min, N Max) Image<N>()
            where N : IFloatingPointIeee754<N>
            => (N.Zero, N.PositiveInfinity);

        /// <inheritdoc />
        public static (N Min, N Max) NumberImage<N>()
            where N : INumberBase<N>, IMinMaxValue<N>
            => (N.Zero, N.MaxValue);

        public static N GlobalMaximum<N>()
            where N : INumberBase<N>
            => N.One;

        public static N GlobalMinimum<N>()
            where N : INumberBase<N>
            => -N.One;

        /// <inheritdoc />
        public static N Eval<N>(N x)
            where N : ITrigonometricFunctions<N>
            => N.Sin(x);

        public static N DerivativeEval<N>(N x)
            where N : ITrigonometricFunctions<N>
            => N.Cos(x);

        public static class TangentLine
        {
            public static (N A, N B, N C) FromX<N>(N x)
                where N : ITrigonometricFunctions<N>
            {
                var slope = Slope.FromX(x);
                return (-slope, N.One, slope * x - Eval(x));
            }

            public static class Slope
            {
                public static N FromX<N>(N x)
                    where N : ITrigonometricFunctions<N>
                    => DerivativeEval(x);
            }
        }

        public static class NormalLine
        {
            public static (N A, N B, N C) FromX<N>(N x)
                where N : ITrigonometricFunctions<N>
            {
                var fx = Eval(x);
                var slope = -N.One / DerivativeEval(x);
                return (slope, N.One, fx - slope * x);
            }
        }

        public static class Points
        {
            public static IEnumerable<(N X, N Y)> Get<N>(int count)
                where N : ITrigonometricFunctions<N>, IComparisonOperators<N, N, bool>
                => Get(N.Zero, N.CreateChecked(2) * N.Pi, count);

            public static IEnumerable<(N X, N Y)> Get<N>(N from, N to, int count)
                where N : ITrigonometricFunctions<N>, IComparisonOperators<N, N, bool>
                => Get(from, to, N.CreateChecked(2) * N.Pi / N.CreateChecked(count));

            public static IEnumerable<(N X, N Y)> Get<N>(N xstep)
                where N : ITrigonometricFunctions<N>, IComparisonOperators<N, N, bool>
                => Get(N.Zero, N.CreateChecked(2) * N.Pi, xstep);

            public static IEnumerable<(N X, N Y)> Get<N>(N from, N to, N xstep)
                where N : ITrigonometricFunctions<N>, IComparisonOperators<N, N, bool>
            {
                for (N x = from; x < to; x += xstep)
                    yield return (x, N.Sin(x));
            }
        }
    }
}
