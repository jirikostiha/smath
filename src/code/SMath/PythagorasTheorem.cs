using System.Numerics;

namespace SMath
{
    /// <summary>
    /// Pythagoras Theorem
    /// <a href="https://en.wikipedia.org/wiki/Pythagorean_theorem">Wikipedia</a>
    /// </summary>
    public static class PT
    {
        /// <summary> Calculate length of hypotenuse of a right-angled triangle. </summary>
        public static N Hypotenuse<N>(N leg1, N leg2)
            where N : IRootFunctions<N>
            => N.Sqrt((leg1 * leg1) + (leg2 * leg2));

        public static N Hypotenuse<N>((N First, N Second) legs)
            where N : IRootFunctions<N>
            => N.Sqrt((legs.First * legs.First) + (legs.Second * legs.Second));

        /// <summary> Calculate length of leg of a right-angled triangle. </summary>
        public static N Leg<N>(N hypotenuse, N otherLeg)
            where N : IRootFunctions<N>
            => N.Sqrt((hypotenuse * hypotenuse) - (otherLeg * otherLeg));

        /// <summary> Calculate length of hypotenuse of a right-angled triangle like object. </summary>
        public static N Hypotenuse<N>(N leg1, N leg2, N leg3)
            where N : IRootFunctions<N>
            => N.Sqrt((leg1 * leg1) + (leg2 * leg2) + (leg3 * leg3));

        /// <summary> Calculate length of hypotenuse of a right-angled triangle like object. </summary>
        public static N Hypotenuse<N>((N First, N Second, N Third) legs)
            where N : IRootFunctions<N>
            => N.Sqrt((legs.First * legs.First) + (legs.Second * legs.Second) + (legs.Third * legs.Third));


        public static N Leg<N>(N hypotenuse, N otherLeg1, N otherLeg2)
            where N : IRootFunctions<N>
            => N.Sqrt((hypotenuse * hypotenuse) - (otherLeg1 * otherLeg1) - (otherLeg2 * otherLeg2));

        public static N Hypotenuse<N>(N leg1, N leg2, N leg3, N leg4)
            where N : IRootFunctions<N>
            => N.Sqrt((leg1 * leg1) + (leg2 * leg2) + (leg3 * leg3) + (leg4 * leg4));

        public static N Leg<N>(N hypotenuse, N otherLeg1, N otherLeg2, N otherLeg3)
            where N : IRootFunctions<N>
            => N.Sqrt((hypotenuse * hypotenuse) - (otherLeg1 * otherLeg1) - (otherLeg2 * otherLeg2) - (otherLeg3 * otherLeg3));

        /// <summary>
        /// Law of cosines
        /// <a href="https://en.wikipedia.org/wiki/Law_of_cosines">Wikipedia</a>
        /// </summary>
        public static N Cosine<N>(N a1, N a2, N angle)
            where N : IRootFunctions<N>, ITrigonometricFunctions<N>
            => N.Sqrt((a1 * a1) + (a2 * a2) - (N.CreateChecked(2) * a1 * a2 * N.Cos(angle)));
    }
}
