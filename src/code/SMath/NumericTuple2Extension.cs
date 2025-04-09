using SMath.Geometry2D;
using System.Numerics;
using System.Runtime.CompilerServices;

namespace SMath;

/// <summary>
/// Numeric tuple extensions
/// </summary>
public static class NumericTuple2Extension
{
    /// <summary>
    /// Add two vectors.
    /// </summary>
    public static (N X, N Y) Add<N>(this (N X, N Y) a, (N X, N Y) b)
        where N : IAdditionOperators<N,N,N>
        =>
        (a.X + b.X, a.Y + b.Y);

    /// <summary>
    /// Multiply vector by int scalar.
    /// </summary>
    public static (N X, N Y) Multiply<N>(this (N X, N Y) a, N scalar)
        where N : IMultiplyOperators<N, N, N>
        =>
        (a.X * scalar, a.Y * scalar);

    /// <summary>
    /// Direction from one to the other vector. It is not normalized.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static (N X, N Y) Direction<N>((N X, N Y) a, (N X, N Y) b)
        where N : ISubtractionOperators<N, N, N>
        =>
        GeometricVector2.Direction.FromCartesian(a, b);

    /// <summary>
    /// Magnitude/length/size/scalar of vector.
    /// </summary>
    public static N Magnitude<N>((N X, N Y) vector)
        where N : IRootFunctions<N>
        =>
        GeometricVector2.Magnitude.FromCartesian(vector);

    /// <summary>
    /// Dot product or scalar product.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static N Dot<N>(this (N X, N Y) a, (N X, N Y) b)
        where N : IAdditionOperators<N, N, N>, IMultiplyOperators<N, N, N>
        =>
        GeometricVector2.DotProduct.FromCartesian(a, b);

    /// <summary>
    /// Cross product or vector product.
    /// In 2D the result is magnitude in 3rd dimension.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static N CrossProduct<N>(this (N X, N Y) a, (N X, N Y) b)
        where N : ISubtractionOperators<N, N, N>, IMultiplyOperators<N, N, N>
        =>
        GeometricVector2.CrossProduct.FromCartesian(a, b);
}
