using System.Numerics;
using System.Runtime.CompilerServices;

namespace SMath;

/// <summary>
/// Summation.
/// </summary>
/// <remarks>
/// <a href="https://en.wikipedia.org/wiki/Summation">Wikipedia</a>
/// </remarks>
public static class Summation
{
    public static N Eval<N>(IEnumerable<N> summands)
         where N : INumberBase<N>
    {
        var sum = N.Zero;
        foreach (var summand in summands)
            sum += summand;

        return sum;
    }

    public static N Eval<N, NInt>(IEnumerable<N> summands, out NInt count)
        where N : INumberBase<N>
        where NInt : IBinaryInteger<NInt>
    {
        var sum = N.Zero;
        count = NInt.Zero;
        foreach (var summand in summands)
        {
            sum += summand;
            count++;
        }

        return sum;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static N Eval<N>(ReadOnlySpan<N> summands)
         where N : INumberBase<N>
    {
        var sum = N.Zero;
        for (int i = 0; i < summands.Length; i++)
            sum += summands[i];

        return sum;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static N Eval<N, NInt>(ReadOnlySpan<N> summands, out NInt count)
        where N : INumberBase<N>
        where NInt : IBinaryInteger<NInt>
    {
        count = NInt.CreateChecked(summands.Length);
        var sum = N.Zero;
        for (int i = 0; i < summands.Length; i++)
            sum += summands[i];

        return sum;
    }
}
