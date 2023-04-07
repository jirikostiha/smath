using System.Numerics;

namespace SMath
{
    /// <summary>
    /// Function domain.
    /// </summary>
    public interface IComplexDomain
    {
        /// <summary>
        /// Represents domain of the function. 
        /// </summary>
        /// <typeparam name="N"> A number type. </typeparam>
        /// <returns> Domain. </returns>
        public static abstract IEnumerable<(N Min, N Max, bool MinIn, bool MaxIn)> Domain<N>()
            where N : INumberBase<N>, IMinMaxValue<N>;

        /// <summary>
        /// Represents domain of the function in specified number type.
        /// </summary>
        /// <typeparam name="N"> A number type. </typeparam>
        /// <returns> Domain in number type. </returns>
        public static abstract IEnumerable<(N Min, N Max, bool MinIn, bool MaxIn)> NumberDomain<N>()
            where N : INumberBase<N>, IMinMaxValue<N>;
    }
}
