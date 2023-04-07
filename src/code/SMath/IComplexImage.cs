using System.Numerics;

namespace SMath.Functions1
{
    /// <summary>
    /// Function image.
    /// </summary>
    public interface IComplexImage
    {
        /// <summary>
        /// Represents image of the function. 
        /// </summary>
        /// <typeparam name="N"> A number type. </typeparam>
        /// <returns> Image. </returns>
        public static abstract IEnumerable<(N Min, N Max, bool MinIn, bool MaxIn)> Image<N>()
            where N : INumberBase<N>, IMinMaxValue<N>;

        /// <summary>
        /// Represents image of the function in specified number type.
        /// </summary>
        /// <typeparam name="N"> A number type. </typeparam>
        /// <returns> Image in number type. </returns>
        public static abstract IEnumerable<(N Min, N Max, bool MinIn, bool MaxIn)> NumberImage<N>()
            where N : INumberBase<N>, IMinMaxValue<N>;
    }
}
