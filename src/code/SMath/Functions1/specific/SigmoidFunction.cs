namespace Wayout.Mathematics.Functions
{
    using System;

    /// <summary>
    /// Sigmoid function
    /// Is special case of logistic function.
    /// </summary>
    /// <remarks>
    /// <a href="http://mathworld.wolfram.com/SigmoidFunction.html">wikipedia</a>
    /// </remarks>
    public static class SigmoidFunction
    {
        public static double f(double x1) => 1 / (1 + Math.Pow(Math.E, -x1));

        public const string Formula = "1 / (1 + e^(-x1))";
    }
}
