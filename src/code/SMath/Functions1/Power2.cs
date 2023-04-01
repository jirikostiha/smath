using System.Numerics;

namespace SMath.Functions1
{
    /// <summary>
    /// Power 2 (square) function.
    /// </summary>
    /// <remarks>
    /// <a href="https://en.wikipedia.org/wiki/Square_(algebra)">wikipedia</a>
    /// <a href="https://en.wikipedia.org/wiki/Exponentiation#Power_functions">wikipedia</a>
    /// </remarks>
    public class Power2 : IMathFunction
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
            => "x^2";

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
            where N : IFloatingPointIeee754<N>
            => N.PositiveInfinity;

        public static N GlobalMinimum<N>()
            where N : INumberBase<N>
            => N.Zero;

        /// <inheritdoc />
        public static N Eval<N>(N x)
            where N : IMultiplyOperators<N, N, N>
            => x * x;

        public static N DerivativeEval<N>(N x)
            where N : INumberBase<N>
            => N.CreateChecked(2) * x;

        public static class TangentLine
        {
            public static (N A, N B, N C) FromX<N>(N x)
                where N : INumberBase<N>
            {
                var slope = Slope.FromX(x);
                return (-slope, N.One, slope * x - Eval(x));
            }

            public static class Slope
            {
                public static N FromX<N>(N x)
                    where N : INumberBase<N>
                    => DerivativeEval(x);
            }
        }
    }
}
