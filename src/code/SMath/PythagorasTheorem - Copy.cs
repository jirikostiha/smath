using System.Numerics;

namespace SMath
{
    /// <summary>
    /// Geometric distance.
    /// <a href="">wikipedia</a>
    /// </summary>
    public static class Distance  //or point - distance
    {
        public static N Hypotenuse<N>(N leg1, N leg2)
            where N : IRootFunctions<N>
            => N.Sqrt((leg1 * leg1) + (leg2 * leg2));
    }
}
