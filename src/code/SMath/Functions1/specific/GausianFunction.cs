namespace Wayout.Mathematics.Functions
{
    using System;
    using SMath.Functions1;

    /// <summary>
    /// Gausian function
    /// </summary>
    /// <remarks>
    /// <a href="https://en.wikipedia.org/wiki/Gaussian_function">wikipedia</a>
    /// </remarks>
    public static class GausianFunction
    {
        public static double f(double x1, double a, double b, double c)
            => a * Math.Pow(Math.E, -(Power2.Eval(x1 - b) / (2 * Power2.Eval(c))));

        public const string Formula = "A * e^-(((x1 - B)^2) / (2 * C^2))";
    }
}
