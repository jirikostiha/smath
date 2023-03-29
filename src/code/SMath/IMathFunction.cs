using System.Numerics;

namespace SMath
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

        // Error CS0425  
        /*/// <summary> Evaluates function value. </summary>
        /// <typeparam name="N"> A number type. </typeparam>
        /// <param name="x"> Input number. </param>
        /// <returns> Output number. </returns>
        public static abstract N Eval<N>(N x);*/
            where N : INumberBase<N>;
    }
}
