using System.Numerics;

namespace SMath.Statistics
{
    /// <summary>
    /// Median
    /// </summary>
    /// <remarks>
    /// <a href="https://en.wikipedia.org/wiki/Median">wikipedia</a>
    /// </remarks>
    public static class Median
    {
        public static N Eval<N>(IEnumerable<N> numbers)
            where N : INumberBase<N>
        {
            var orderedNumber = numbers.OrderBy(x => x).ToArray();
            if (orderedNumber.Length % 2 == 0)
                return orderedNumber[orderedNumber.Length/2];
            else
                //aritmeticky prumer 2 strednich hodnot
                return orderedNumber[orderedNumber.Length / 2 + (orderedNumber.Length / 2 + 1) / 2];
        }
    }
}
