using System.Numerics;

namespace SMath
{
    /// <summary>
    /// Determinant of a square matrix.
    /// </summary>
    /// <remarks>
    /// <a href="https://en.wikipedia.org/wiki/Determinant">Wikipedia</a>
    /// </remarks>
    public static class Determinant
    {
        public static N FromCells<N>(N r1c1, N r1c2,
                                     N r2c1, N r2c2)
            where N : INumberBase<N>
            => r1c1 * r2c2 - r1c2 * r2c1;

        public static N FromRows<N>((N C1, N C2) row1,
                                    (N C1, N C2) row2)
           where N : INumberBase<N>
           => row1.C1 * row2.C2 - row1.C2 * row2.C1;

        public static N FromColumns<N>((N R1, N R2) col1,
                                       (N R1, N R2) col2)
           where N : INumberBase<N>
           => col1.R1 * col2.R2 - col2.R1 * col1.R2;

        public static N FromCells<N>(N r1c1, N r1c2, N r1c3,
                                     N r2c1, N r2c2, N r2c3,
                                     N r3c1, N r3c2, N r3c3)
            where N : INumberBase<N>
            =>
              r1c1 * r2c2 * r3c3 + r1c1 * r2c3 * r3c1 + r1c3 * r2c1 * r3c2
            - r1c3 * r2c2 * r3c1 - r2c3 * r3c2 * r1c1 - r3c3 * r1c2 * r2c1;
    }
}
