using System.Numerics;

namespace SMath
{
    /// <summary>
    /// Function image.
    /// </summary>
    public interface ISingleImage
    {
        /// <summary>
        /// Represents image of the function. 
        /// </summary>
        /// <typeparam name="N"> A number type. </typeparam>
        /// <returns> Image. </returns>
        public static abstract (N Min, N Max) Image<N>()
            where N : IFloatingPointIeee754<N>;

        /// <summary>
        /// Represents image of the function in specified number type.
        /// </summary>
        /// <typeparam name="N"> A number type. </typeparam>
        /// <returns> Image in number type. </returns>
        public static abstract (N Min, N Max) NumberImage<N>()
            where N : INumberBase<N>, IMinMaxValue<N>;
    }
}
