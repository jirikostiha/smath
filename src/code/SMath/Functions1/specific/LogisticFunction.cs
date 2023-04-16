namespace Wayout.Mathematics.Functions
{
    using System;

    /// <summary>
    /// Logistic function
    /// </summary>
    /// <remarks>
    /// <a href="https://en.wikipedia.org/wiki/Logistic_function">wikipedia</a>
    /// </remarks>
    public static class LogisticFunction
    {
        public static double f(double x1, double l, double k) => l / (1 + Math.Pow(Math.E, -k * x1));

        public const string Formula = "L / (1 + e^(-k * x1))";
    }
}
