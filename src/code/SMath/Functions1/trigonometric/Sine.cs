namespace Wayout.Mathematics.Functions.P1.Trigonometricf
{
    /// <summary>
    /// Bipolar sigmoid function
    /// Is specific case of the sigmoid function.
    /// </summary>
    /// <remarks>
    ///https://en.wikipedia.org/wiki/Sine
    /// </remarks>
    public static class Sine
    {
        public const double DomainMin = double.NegativeInfinity;
        public const double DomainMax = double.PositiveInfinity;

        public const double ImageMin = -1;
        public const double ImageMax = 1;

        public const bool IsEven = false;
        public const bool IsOdd = true;

        public const double Period = 2 * PI;
        public const double InterceptsX1At = 0;

        public static double Maximums(int k) => 2 * k * PI + PI / 2;
        public static double Minimums(int k) => 2 * k * PI - PI / 2;
        //public const double Root = 0; //Root	kπ
        //Critical point	kπ + π/2
        //Inflection point	kπ
        public const double FixedPoint = 0;

        public static double f(double x1) => Sin(x1);

        public const string Formula = "sin(x1)";
    }
}
