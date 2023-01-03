using System.Numerics;

namespace SMath.Functions1
{
    /// <summary>
    /// Identity function.
    /// </summary>
    /// <remarks>
    /// <a href="https://en.wikipedia.org/wiki/Identity_function">wikipedia</a>
    /// </remarks>
    public class Identity
    {        
        /// <inheritdoc />
        public static bool IsEven 
            => false;

        /// <inheritdoc />
        public static bool IsOdd 
            => true;

        /// <inheritdoc />
        public static bool IsContinuous
            => true;

        /// <inheritdoc />
        public static string PlainTextFormula
            => "x";

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
            => (N.NegativeInfinity, N.PositiveInfinity);

        /// <inheritdoc />
        public static (N Min, N Max) NumberImage<N>()
            where N : INumberBase<N>, IMinMaxValue<N>
            => (N.MinValue, N.MaxValue);

        /// <inheritdoc />
        public static N Eval<N>(N x) 
            where N : INumberBase<N>
            => x;
    }
}
