using System.Numerics;

namespace SMath.Geometry2D;

public static class Rectangle
{
    public static (N X, N Y) Vertex1<N>()
        where N : IFloatingPoint<N>
        => (N.Zero, N.Zero);

    public static (N X, N Y) Vertex2<N>()
        where N : IFloatingPoint<N>
        => (N.One, N.Zero);

    public static (N X, N Y) Vertex3<N>()
        where N : IFloatingPoint<N>
        => (N.One, N.One);

    public static (N X, N Y) Vertex4<N>()
        where N : IFloatingPoint<N>
        => (N.Zero, N.One);

    public static (N X, N Y) Center<N>()
        where N : IFloatingPoint<N>
        => (N.CreateTruncating(0.5), N.CreateTruncating(0.5));

    public static (N X, N Y) Quadrant11Center<N>()
        where N : IFloatingPoint<N>
        => (N.CreateTruncating(0.25), N.CreateTruncating(0.25));

    public static (N X, N Y) Quadrant21Center<N>()
        where N : IFloatingPoint<N>
        => (N.CreateTruncating(0.75), N.CreateTruncating(0.25));

    public static (N X, N Y) Quadrant12Center<N>()
        where N : IFloatingPoint<N>
        => (N.CreateTruncating(0.25), N.CreateTruncating(0.75));

    public static (N X, N Y) Quadrant22Center<N>()
        where N : IFloatingPoint<N>
        => (N.CreateTruncating(0.75), N.CreateTruncating(0.75));

}
