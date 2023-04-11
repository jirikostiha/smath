using System.Numerics;

namespace SMath.Geometry2D
{
    /// <summary>
    /// Parabola.
    /// </summary>
    /// <remarks>
    /// <a href="https://en.wikipedia.org/wiki/Parabola">wikipedia</a>
    /// <a href="https://mathworld.wolfram.com/Parabola.html">mathworld</a>
    /// </remarks>
    public static class Parabola
    {
        public static class Focus
        {

        }

        public static class TangentLine
        {
            public static (N A, N B, N C) FromX<N>(N x)
                where N : INumberBase<N>
            {
                var slope = Slope.FromX(x);
                return (-slope, N.One, slope * x - Eval(x));
            }

            public static class Slope
            {
                public static N FromX<N>(N x)
                    where N : INumberBase<N>
                    => DerivativeEval(x);
            }
        }
        public static class NormalLine
        {
            public static (N A, N B, N C) FromX<N>(N x)
                where N : INumberBase<N>
            {
                if (x != N.Zero)
                {
                    var slope = Slope.FromX(x);
                    return (-slope, N.One, slope * x - Eval(x));
                }
                else
                    return (N.One, N.Zero, N.Zero);
            }

            public static class Slope
            {
                public static N FromX<N>(N x)
                    where N : INumberBase<N>
                    => -N.One / DerivativeEval(x);
            }
        }
    }
}
