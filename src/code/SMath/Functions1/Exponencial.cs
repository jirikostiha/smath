namespace Wayout.Mathematics.Functions
{
    using System;

    /// <summary>
    /// General exponential function.
    /// X is exponent.
    /// </summary>
    /// <remarks>
    /// </remarks>
    public static class Exponencial //spliva s power funkci, v math nedava smysl definovat oboji
    {
        public static double f(double @base, double x1) => Math.Pow(@base, x1);
    }

}
