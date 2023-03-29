using System.Numerics;

namespace SMath.Functions1
{
    /// <summary>
    /// Sine function.
    /// </summary>
    /// <remarks>
    /// <a href="https://en.wikipedia.org/wiki/Sine">wikipedia</a>
    /// </remarks>
    public class Sine
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

        /// <inheritdoc />
        public static N Eval<N>(N x)
            where N : ITrigonometricFunctions<N>
            => N.Sin(x);

        public static class Points
        {
            public static IEnumerable<(N X, N Y)> Get<N>(N step)
                where N : IFloatingPointIeee754<N>
                => Get<N>(N.Zero, N.CreateChecked(2) * N.Pi, step);

            public static IEnumerable<(N X, N Y)> Get<N>(N from, N to, N step)
                where N : IFloatingPointIeee754<N>
            {
                for (N x = from; x < to; x += step)
                    yield return (x, N.Sin(x));
            }
        }
    }
}
