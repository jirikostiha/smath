namespace SMath.FunctionsN
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Numerics;

    /// <summary>
    /// Manhatten distance
    /// </summary>
    /// <remarks>
    /// <a href="https://en.wikipedia.org/wiki/Taxicab_geometry">wikipedia</a>
    /// <a href="https://xlinux.nist.gov/dads/HTML/manhattanDistance.html">NIST</a>
    /// </remarks>
    public static class ManhattenDistance
    {
        //public const string Formula = "sum(Xi))";

        /// <summary> Distance from origin in 2D. </summary>
        public static N Eval<N>(N x1, N x2)
            where N : INumberBase<N>
            => N.Abs(x1) + N.Abs(x2);

        public static N Eval<N>(N x1, N x2, N x3)
            where N : INumberBase<N>
            => N.Abs(x1) + N.Abs(x2) + N.Abs(x3);

        public static N Eval<N>(N p1x1, N p1x2, N p2x1, N p2x2)
            where N : INumberBase<N>
            => N.Abs(p1x1 - p2x1) + N.Abs(p1x2 - p2x2);

        public static N f(IEnumerable<double> xs)
            where N : INumberBase<N>
            => Summation.f(xs.Select(x => Abs(x)));
    }
}
