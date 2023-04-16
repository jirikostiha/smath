namespace Wayout.Mathematics.Functions
{
    /// <summary>
    /// Reciprocial function
    /// </summary>
    /// <remarks>
    /// </remarks>
    public static class Reciprocial
    {
        //public const double DomainMin = double.NegativeInfinity;
        //public const double DomainMax = double.PositiveInfinity;

        //public const double ImageMin = 0;
        //public const double ImageMax = double.PositiveInfinity;

        //public const bool IsEven = true;
        //public const bool IsOdd = false;

        //public const double InterceptsX1At = 0;

        //public const double MinimumX1 = 0;
        //public const double MinimumX2 = 0;

        public static double f(double x1) => 1 / x1;

        public const string Formula = "1/x1"; //plain test formula
    }

    public static class ReciprocialPower2
    {
        public static double f(double x1) => 1 / (x1 * x1);

        public const string Formula = "1/x1^2"; //plain text formula
    }

    public static class ReciprocialRoot2
    {
        public static double f(double x1) => 1 / (x1 * x1);

        public const string Formula = "1/√x1"; //plain text formula
    }
}
