using System.Numerics;

namespace SMath
{
    /// <summary>
    /// Product.
    /// </summary>
    /// <remarks>
    /// <a href="https://en.wikipedia.org/wiki/Product_(mathematics)">wikipedia</a>
    /// <a href="https://en.wikipedia.org/wiki/Empty_product">wikipedia</a>
    /// </remarks>
    public static class Product
    {
        public static N Eval<N>(IEnumerable<N> factors)
            where N : INumberBase<N>
        {
            N product = N.One;
            foreach (var number in factors)
                product *= number;

            return product;
        }

        public static N Eval<N, NInt>(IEnumerable<N> factors, out NInt count)
            where N : INumberBase<N>
            where NInt : IBinaryInteger<NInt>
        {
            N product = N.One;
            count = NInt.Zero;
            foreach (var number in factors)
            {
                product *= number;
                count++;
            }

            return product;
        }
    }
}
