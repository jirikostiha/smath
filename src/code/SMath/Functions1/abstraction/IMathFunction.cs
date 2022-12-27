using System.Numerics;

namespace SMath.Functions1
{
    /// <summary>
    /// Mathematical function.
    /// </summary>
    public interface IMathFunction
    {
        /// <summary>
        /// Determines whether the specified function is even function.
        /// </summary>
        public static abstract bool IsEven { get; }

        /// <summary>
        /// Determines whether the specified function is odd function.
        /// </summary>
        public static abstract bool IsOdd { get; }

        /// <summary>
        /// Determines whether the specified function is continuous function.
        /// </summary>
        public static abstract bool IsContinuous { get; }

        /// <summary>
        /// String representation of the function.
        /// </summary>
        public static abstract string PlainTextFormula { get; }

        /// <summary>
        /// Represents domain of the function. 
        /// </summary>
        /// <typeparam name="N"> A number type. </typeparam>
        /// <returns> Domain. </returns>
        //public static abstract IEnumerable<(N Min, N Max, bool MinIn, bool MaxIn)> Domain<N>()
        //    where N : IFloatingPointIeee754<N>;

        /// <summary>
        /// Represents domain of the function in specified number type.
        /// </summary>
        /// <typeparam name="N"> A number type. </typeparam>
        /// <returns> Domain in number type. </returns>
        //public static abstract IEnumerable<(N Min, N Max, bool MinIn, bool MaxIn)> NumberDomain<N>()
        //    where N : INumberBase<N>, IMinMaxValue<N>;

        /// <summary>
        /// Represents image of the function. 
        /// </summary>
        /// <typeparam name="N"> A number type. </typeparam>
        /// <returns> Image. </returns>
        //public static abstract IEnumerable<(N Min, N Max, bool MinIn, bool MaxIn)> Image<N>()
        //    where N : IFloatingPointIeee754<N>;

        /// <summary>
        /// Represents image of the function in specified number type.
        /// </summary>
        /// <typeparam name="N"> A number type. </typeparam>
        /// <returns> Image in number type. </returns>
        //public static abstract IEnumerable<(N Min, N Max, bool MinIn, bool MaxIn)> NumberImage<N>()
        //    where N : INumberBase<N>, IMinMaxValue<N>;

        /// <summary> Evaluates function value. </summary>
        /// <typeparam name="N"> A number type. </typeparam>
        /// <param name="x"> Function parameter. </param>
        /// <returns> Evaluated output. </returns>
        public static abstract N Eval<N>(N x)
            where N : INumberBase<N>;
    }
}
