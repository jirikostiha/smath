namespace Wayout.Mathematics.Functions.P1.Trigonometricf
{
    public class Cosine
    {
        public const double DomainMin = double.NegativeInfinity;
        public const double DomainMax = double.PositiveInfinity;

        public const double ImageMin = -1;
        public const double ImageMax = 1;

        public const bool IsEven = true;
        public const bool IsOdd = false;

        public const double Period = 2 * PI;
        public const double InterceptsX1At = 1;

        public static double f(double x1) => Cos(x1);

        public const string Formula = "cos(x1)";
    }
}
