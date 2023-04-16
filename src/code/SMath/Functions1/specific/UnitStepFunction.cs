namespace Wayout.Mathematics.Functions
{
    /// <summary>
    /// Unit step function
    /// Is special case of step function.
    /// </summary>
    /// <remarks>
    /// <a href="https://en.wikipedia.org/wiki/Heaviside_step_function">wikipedia</a>
    /// </remarks>
    public static class UnitStepFunction
    {
        public static double f(double x1, double p) => (x1 < 0) ? 0 : (x1 > 0) ? 1 : p;

        public const double DomainX1Min = double.NegativeInfinity;
        public const double DomainX1Max = double.PositiveInfinity;

        public const string Formula = "0, x1 < 0; 1, x1 > 0; P, x1 = 0";
    }
}
