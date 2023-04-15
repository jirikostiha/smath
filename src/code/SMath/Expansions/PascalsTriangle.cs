namespace SMath.Expansions
{
    /// <summary>
    /// Pascal's triangle
    /// </summary>
    /// <remarks>
    /// <a href="https://en.wikipedia.org/wiki/Pascal%27s_triangle">wikipedia</a>
    /// </remarks>
    public static class PascalsTriangle
    {
        ////presunout jinam - mozna spise combinatorika
        ////mozna z toho udelat i tridu
        //public static IEnumerable<int[]> PascalsTriangle(int rowCount, int baseNumber = 1)
        //{
        //    var row = new[] { baseNumber };
        //    var previousRow = row.ToArray(); //need a copy
        //    yield return row;

        //    for (int rowIndex = 1; rowIndex < rowCount; rowIndex++)
        //    {
        //        row = new int[rowIndex + 1];
        //        row[0] = baseNumber;
        //        for (int index = 1; index <= row.Length; index++)
        //        {
        //            row[rowIndex] = previousRow[index] * (rowIndex - x) / (x + 1);
        //        }
        //        yield return row;
        //    }

        //    yield break;
        //}

        //presunout jinam - mozna spise combinatorika
        //mozna z toho udelat i tridu
        /// <summary>
        /// 
        /// </summary>
        /// <param name="rowNumber"> starts from 0. </param>
        /// <param name="baseNumber"> triangle top number </param>
        /// <returns></returns>
        public static int[] PascalsTriangleRow(int rowNumber, int baseNumber = 1)
        {
            var row = new int[rowNumber + 1];
            row[0] = baseNumber;
            for (int index = 0; index < row.Length - 1; index++)
            {
                row[index+1] = row[index] * (rowNumber - index) / (index + 1);
            }
            return row;
        }
    }
}
