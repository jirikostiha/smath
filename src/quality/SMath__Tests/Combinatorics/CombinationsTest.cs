namespace Wayout.Mathematics.Combinatorics
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Xunit;

    public class CombinationsTest
    {
        public TestContext TestContext { get; set; }

        [Theory]
        [DynamicData(nameof(CountForN), DynamicDataSourceType.Method)]
        public void Count(int n, int expected)
        {
            Assert.AreEqual(expected, Combinations.Count(n));
        }

        [Theory]
        [DynamicData(nameof(CountForTuple1FromN), DynamicDataSourceType.Method)]
        [DynamicData(nameof(CountForTuple2FromN), DynamicDataSourceType.Method)]
        [DynamicData(nameof(CountForTuple3FromN), DynamicDataSourceType.Method)]
        [DynamicData(nameof(CountForTuple4FromN), DynamicDataSourceType.Method)]
        public void CountTuples(int n, int k, int expected)
        {
            var count = Combinations.Count(n, k);
            WriteResult(n, k, count);

            Assert.AreEqual(expected, count);
        }

        [Theory]
        [DynamicData(nameof(CountForTuple2FromN), DynamicDataSourceType.Method)]
        public void Tuple2(int n, int k, int expected)
        {
            var tuples = Combinations.Tuple2(n).ToArray();
            WriteRow(n, k, tuples.Select(x => $"{x.Index1}{x.Index2}"));

            Assert.AreEqual(expected, tuples.Length);
            CollectionAssert.AllItemsAreUnique(tuples);
        }

        [Theory]
        [DynamicData(nameof(CountForTuple3FromN), DynamicDataSourceType.Method)]
        public void Tuple3(int n, int k, int expected)
        {
            var tuples = Combinations.Tuple3(n).ToArray();
            WriteRow(n, k, tuples.Select(x => $"{x.Index1}{x.Index2}{x.Index3}"));

            Assert.AreEqual(expected, tuples.Length);
            CollectionAssert.AllItemsAreUnique(tuples);
        }

        [Theory]
        [DynamicData(nameof(CountForTuple4FromN), DynamicDataSourceType.Method)]
        public void Tuple4(int n, int k, int expected)
        {
            var tuples = Combinations.Tuple4(n).ToArray();
            WriteRow(n, k, tuples.Select(x => $"{x.Index1}{x.Index2}{x.Index3}{x.Index4}"));

            Assert.AreEqual(expected, tuples.Length);
            CollectionAssert.AllItemsAreUnique(tuples);
        }

        #region data
        private static IEnumerable<object[]> CountForN()
        {
            // n; expected
            yield return new object[] { -1, 0 };
            yield return new object[] { 0, 0 };
            yield return new object[] { 1, 1 };
            yield return new object[] { 2, 3 };
            yield return new object[] { 3, 7 };
            yield return new object[] { 4, 15 };
            //todo upper limit
        }
        
        private static IEnumerable<object[]> CountForTuple1FromN()
        {
            // n; k; expected
            yield return new object[] { -1, 1, 0 };
            yield return new object[] { 0, 1, 0 };
            yield return new object[] { 1, 1, 1 };
            yield return new object[] { 2, 1, 2 };
            yield return new object[] { 3, 1, 3 };
        }

        private static IEnumerable<object[]> CountForTuple2FromN()
        {
            // n; k; expected
            yield return new object[] { -1, 2, 0 };
            yield return new object[] { 0, 2, 0 };
            yield return new object[] { 1, 2, 0 };
            yield return new object[] { 2, 2, 1 };
            yield return new object[] { 3, 2, 3 };
            yield return new object[] { 4, 2, 6 };
            yield return new object[] { 5, 2, 10 };
        }

        private static IEnumerable<object[]> CountForTuple3FromN()
        {
            // n; k; expected
            yield return new object[] { -1, 3, 0 };
            yield return new object[] { 0, 3, 0 };
            yield return new object[] { 1, 3, 0 };
            yield return new object[] { 2, 3, 0 };
            yield return new object[] { 3, 3, 1 };
            yield return new object[] { 4, 3, 4 };
            yield return new object[] { 5, 3, 10 };
            yield return new object[] { 6, 3, 20 };
        }

        private static IEnumerable<object[]> CountForTuple4FromN()
        {
            // n; k; expected
            yield return new object[] { -1, 4, 0 };
            yield return new object[] { 0, 4, 0 };
            yield return new object[] { 1, 4, 0 };
            yield return new object[] { 2, 4, 0 };
            yield return new object[] { 3, 4, 0 };
            yield return new object[] { 4, 4, 1 };
            yield return new object[] { 5, 4, 5 };
            yield return new object[] { 6, 4, 15 };
        }
        #endregion

        private void WriteResult(int n, int k, int count)
            => TestContext.WriteLine("n={0}, k={1}, count:{2}", n, k, count);

        private void WriteRow(int n, int k, IEnumerable<string> tuples)
            => TestContext.WriteLine("n={0}, k={1}, tuples:({2})", n, k, string.Join(", ", tuples));
    }
}
