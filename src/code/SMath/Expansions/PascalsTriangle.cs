namespace SMath.Expansions;

/// <summary>
/// Pascal's triangle.
/// </summary>
/// <remarks>
/// <a href="https://en.wikipedia.org/wiki/Pascal%27s_triangle">Wikipedia</a>
/// </remarks>
public static class PascalsTriangle
{
    /// <summary>
    /// Build a single row of Pascal's triangle.
    /// </summary>
    /// <param name="rowNumber"> Row index, starting from 0. </param>
    /// <param name="baseNumber"> Triangle top number. </param>
    public static int[] Row(int rowNumber, int baseNumber = 1)
    {
        var row = new int[rowNumber + 1];
        row[0] = baseNumber;
        for (int index = 0; index < row.Length - 1; index++)
            row[index + 1] = row[index] * (rowNumber - index) / (index + 1);

        return row;
    }
}
