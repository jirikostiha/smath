using System.Numerics;

namespace SMath
{
    public static class BinaryIntegerExtension
    {
        public static NInt Pow<NInt>(this NInt number, NInt exp)
            where NInt : IBinaryInteger<NInt>
        {
            var product = NInt.One;
            for (var i = NInt.Zero; i < exp; i++)
                product *= number;

            return product;
        }
    }
}
