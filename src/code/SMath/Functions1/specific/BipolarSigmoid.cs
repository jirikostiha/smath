namespace Wayout.Mathematics.Functions
{
    using System;

    /// <summary>
    /// Bipolar sigmoid function
    /// Is specific case of the sigmoid function.
    /// </summary>
    public static class BipolarSigmoid
    {
        public static double f(double x1) => (2 / (1 + Math.Pow(Math.E, -x1))) - 1;

        public const double DomainX1Min = double.NegativeInfinity;
        public const double DomainX1Max = double.PositiveInfinity;

        public const double ImageMin = -1;
        public const double ImageMax =  1;

        public const string Formula = "2 / (1 + e^(-x1)) - 1";
    }
}
