using System.Numerics;
using System.Runtime.CompilerServices;

namespace SMath.Functions1;

/// <summary>
/// Power 4 (quartic) function.
/// </summary>
/// <remarks>
/// <a href="https://en.wikipedia.org/wiki/Exponentiation#Power_functions">Wikipedia</a>
/// </remarks>
public class Power4 : IMathFunction
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
        => "x^4";

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

    public static N GlobalMinimum<N>()
        where N : INumberBase<N>
        => N.Zero;

    /// <inheritdoc />
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static N Eval<N>(N x)
        where N : IMultiplyOperators<N, N, N>
    {
        var x2 = x * x;
        return x2 * x2;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static N DerivativeEval<N>(N x)
        where N : INumberBase<N>
        => N.CreateChecked(4) * (x * x * x);
}
