namespace SMath.FunctionsN
{
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Euclidean distance.
    /// </summary>
    /// <remarks>
    /// <a href="https://en.wikipedia.org/wiki/Euclidean_distance">wikipedia</a>
    /// <a href="https://xlinux.nist.gov/dads/HTML/euclidndstnc.html">NIST</a>
    /// </remarks>
    public static class EuclideanDistance
    {
        public static double f(IEnumerable<double> xs)
            => Root2.f(xs.Sum(x => Power2.f(x)));

        public static double Eval(double x1, double x2)
            => PythagorasTheorem.Hypotenuse(x1, x2);

        public static double f(double x1, double x2, double x3)
            => PT.Hypotenuse(x1, x2, x3);

        public const string Formula = "sqrt(sum(Xi^2))";
    }
}
