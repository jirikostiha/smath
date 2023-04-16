namespace Wayout.Mathematics.Functions
{
    /// <summary>
    /// Booth's function
    /// </summary>
    /// <remarks>
    /// <a href="https://en.wikipedia.org/wiki/Test_functions_for_optimization">wikipedia</a>
    /// </remarks>
    public static class BoothFunction
    {
        public static double f(double x1, double x2) => Power2.f(x1 + 2 * x2 - 7) + Power2.f(2 * x1 + x2 - 5);

        public const double GlobalMinX1 = 1;
        public const double GlobalMinX2 = 3;
        public const double GlobalMin = 0;

        public const string Formula = "";
    }
}
