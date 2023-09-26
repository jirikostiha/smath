using System.Numerics;

namespace SMath;

/// <summary>
/// Function domain.
/// </summary>
public interface ISingleDomain
{
    /// <summary>
    /// Represents domain of the function. 
    /// </summary>
    /// <typeparam name="N"> A number type. </typeparam>
    /// <returns> Domain. </returns>
    public static abstract (N Min, N Max) Domain<N>()
        where N : IFloatingPointIeee754<N>;

    /// <summary>
    /// Represents domain of the function in specified number type.
    /// </summary>
    /// <typeparam name="N"> A number type. </typeparam>
    /// <returns> Domain in number type. </returns>
    public static abstract (N Min, N Max) NumberDomain<N>()
        where N : INumberBase<N>, IMinMaxValue<N>;
}
