namespace Wayout.Mathematics.Functions.P1.Trigonometricf
{
    //https://www.analyzemath.com/trigonometry/properties.html
    public static class Tangent
    {
        //public const double DomainMin = double.NegativeInfinity;
        //public const double DomainMax = double.PositiveInfinity;

        public const double ImageMin = double.NegativeInfinity;
        public const double ImageMax = double.PositiveInfinity;

        public const bool IsEven = false;
        public const bool IsOdd = true;

        public const double Period = PI;
        public const double InterceptsX1At = 0;

        public static double f(double x1) => Tan(x1);

        public const string Formula = "tan(x1)";
    }
}
