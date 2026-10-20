using System.Numerics;

namespace SMath.Geometry3D;

/// <summary>
/// Geometric vector in 3D space.
/// </summary>
/// <remarks>
/// <a href="https://en.wikipedia.org/wiki/Euclidean_vector">Wikipedia</a>
/// </remarks>
public static class GeometricVector3
{
    /// <summary>
    /// Magnitude (length) of a vector.
    /// </summary>
    public static class Magnitude
    {
        public static N FromCartesian<N>((N X, N Y, N Z) vector)
            where N : IRootFunctions<N>
            => PT.Hypotenuse(vector);
    }

    /// <summary>
    /// Cross product or vector product.
    /// </summary>
    /// <remarks>
    /// <a href="https://en.wikipedia.org/wiki/Cross_product">Wikipedia</a>
    /// </remarks>
    public static class CrossProduct
    {
        public static (N X, N Y, N Z) FromCartesian<N>((N X, N Y, N Z) vector1, (N X, N Y, N Z) vector2)
            where N : ISubtractionOperators<N, N, N>, IMultiplyOperators<N, N, N>
            => ((vector1.Y * vector2.Z) - (vector1.Z * vector2.Y),
                (vector1.Z * vector2.X) - (vector1.X * vector2.Z),
                (vector1.X * vector2.Y) - (vector1.Y * vector2.X));
    }

    /// <summary>
    /// Normal vector of an input vector: perpendicular to it and of the same length.
    /// Space has a whole circle of such vectors; this one is also perpendicular to the coordinate axis
    /// the input leans on least, which keeps it far from degenerate. A zero vector has the zero vector.
    /// </summary>
    /// <remarks>
    /// <a href="https://en.wikipedia.org/wiki/Normal_(geometry)">Wikipedia</a>
    /// </remarks>
    public static class Normal
    {
        public static (N X, N Y, N Z) FromCartesian<N>((N X, N Y, N Z) vector)
            where N : INumber<N>, IRootFunctions<N>
        {
            N absX = N.Abs(vector.X);
            N absY = N.Abs(vector.Y);
            N absZ = N.Abs(vector.Z);
            (N X, N Y, N Z) axis = absX <= absY && absX <= absZ
                ? (N.One, N.Zero, N.Zero)
                : absY <= absZ ? (N.Zero, N.One, N.Zero) : (N.Zero, N.Zero, N.One);

            var cross = CrossProduct.FromCartesian(vector, axis);
            N length = Magnitude.FromCartesian(cross);
            if (!(length > N.Zero))
            {
                return (N.Zero, N.Zero, N.Zero);
            }

            N scale = Magnitude.FromCartesian(vector) / length;
            return (cross.X * scale, cross.Y * scale, cross.Z * scale);
        }
    }
}
