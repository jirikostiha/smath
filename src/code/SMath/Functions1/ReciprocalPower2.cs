using System.Numerics;

namespace SMath.Functions1;

/// <summary>
/// Reciprocal of the power 2 (square) function.
/// </summary>
/// <remarks>
/// Function is not continuous. It has a vertical asymptote at zero, which is not in the domain.
/// <a href="https://en.wikipedia.org/wiki/Multiplicative_inverse">Wikipedia</a>
/// <a href="https://en.wikipedia.org/wiki/Inverse-square_law">Wikipedia</a>
/// </remarks>
public class ReciprocalPower2 : IMathFunction
{
    /// <inheritdoc />
    public static bool IsEven
        => true;

    /// <inheritdoc />
    public static bool IsOdd
        => false;

    /// <inheritdoc />
    public static bool IsContinuous
        => false;

    /// <inheritdoc />
    public static string PlainTextFormula
        => "1/x^2";

    /// <inheritdoc />
    public static (N Min, N Max) Domain<N>()
        where N : IFloatingPointIeee754<N>
        => (N.NegativeInfinity, N.PositiveInfinity);

    /// <inheritdoc />
    public static (N Min, N Max) NumberDomain<N>()
        where N : INumberBase<N>, IMinMaxValue<N>
        => (N.MinValue, N.MaxValue);

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

    /// <summary>
    /// Infimum of the function. It is not attained for any real number.
    /// </summary>
    public static N GlobalMinimum<N>()
        where N : INumberBase<N>
        => N.Zero;

    /// <inheritdoc />
    public static N Eval<N>(N x)
        where N : INumberBase<N>
        => N.One / Power2.Eval(x);

    public static N DerivativeEval<N>(N x)
        where N : INumberBase<N>
        => -N.CreateChecked(2) / Power3.Eval(x);
}
