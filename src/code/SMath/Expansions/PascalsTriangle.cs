using System.Numerics;

namespace SMath.Expansions;

/// <summary>
/// Pascal's triangle.
/// </summary>
/// <remarks>
/// <a href="https://en.wikipedia.org/wiki/Pascal%27s_triangle">Wikipedia</a>
/// <a href="https://mathworld.wolfram.com/PascalsTriangle.html">Wolfram Mathworld</a>
/// </remarks>
public static class PascalsTriangle
{
    /// <summary>
    /// Count of numbers in a row.
    /// </summary>
    /// <param name="rowNumber"> Row index, starting from 0. </param>
    public static int RowLength(int rowNumber)
        => rowNumber < 0
            ? 0
            : rowNumber + 1;

    /// <summary>
    /// Build a single row of Pascal's triangle.
    /// </summary>
    /// <param name="rowNumber"> Row index, starting from 0. </param>
    public static NInt[] Row<NInt>(int rowNumber)
        where NInt : IBinaryInteger<NInt>
        => Row(rowNumber, NInt.One);

    /// <summary>
    /// Build a single row of Pascal's triangle.
    /// </summary>
    /// <param name="rowNumber"> Row index, starting from 0. </param>
    /// <param name="baseNumber"> Triangle top number. </param>
    public static NInt[] Row<NInt>(int rowNumber, NInt baseNumber)
        where NInt : IBinaryInteger<NInt>
    {
        var row = new NInt[RowLength(rowNumber)];
        Row(rowNumber, baseNumber, row);

        return row;
    }

    /// <summary>
    /// Write a single row of Pascal's triangle into a destination buffer.
    /// Allocation free alternative to the array returning overload.
    /// Size the buffer with <c>RowLength(rowNumber)</c>.
    /// </summary>
    /// <param name="rowNumber"> Row index, starting from 0. </param>
    /// <param name="baseNumber"> Triangle top number. </param>
    /// <param name="destination"> Buffer to write the row into. </param>
    /// <returns> Count of written numbers. </returns>
    /// <exception cref="ArgumentException"> Destination is too short. </exception>
    public static int Row<NInt>(int rowNumber, NInt baseNumber, Span<NInt> destination)
        where NInt : IBinaryInteger<NInt>
    {
        var length = RowLength(rowNumber);
        if (length == 0)
            return 0;

        // the count is known up front, so the capacity is checked once instead of on every write
        if (destination.Length < length)
            throw new ArgumentException("Destination is too short.", nameof(destination));

        destination[0] = baseNumber;
        for (int index = 0; index < length - 1; index++)
        {
            // every number follows from the previous one, C(n,k+1) == C(n,k) * (n - k) / (k + 1),
            // reduced before it is multiplied out so that no intermediate value grows over the row
            var previous = destination[index];
            var factor = NInt.CreateChecked(rowNumber - index);
            var divisor = NInt.CreateChecked(index + 1);

            var common = BinaryIntegerExtension.GreatestCommonDivisor(previous, divisor);

            destination[index + 1] = previous / common * (factor / (divisor / common));
        }

        return length;
    }

    /// <summary>
    /// Enumerate the first <paramref name="rowCount"/> rows, starting from the top one.
    /// </summary>
    public static IEnumerable<NInt[]> Rows<NInt>(int rowCount)
        where NInt : IBinaryInteger<NInt>
        => Rows(rowCount, NInt.One);

    /// <summary>
    /// Enumerate the first <paramref name="rowCount"/> rows, starting from the top one.
    /// </summary>
    /// <param name="rowCount"> Count of rows. </param>
    /// <param name="baseNumber"> Triangle top number. </param>
    public static IEnumerable<NInt[]> Rows<NInt>(int rowCount, NInt baseNumber)
        where NInt : IBinaryInteger<NInt>
    {
        for (int rowNumber = 0; rowNumber < rowCount; rowNumber++)
            yield return Row(rowNumber, baseNumber);
    }
}
