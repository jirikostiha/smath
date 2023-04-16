namespace SMath.Combinatorics
{
    using System.Collections.Generic;
    using System.Linq;
    using Xunit;
    using Xunit.Abstractions;

    public class CombinationsTest
    {
        private readonly ITestOutputHelper _output;

        public CombinationsTest(ITestOutputHelper output)
        {
            _output = output;
        }

        [Theory]
        [MemberData(nameof(CountForN))]
        public void Count(int n, int expected)
        {
            Assert.Equal(expected, Combinations.Count(n));
        }

        [Theory]
        [MemberData(nameof(CountForTuple1FromN))]
        [MemberData(nameof(CountForTuple2FromN))]
        [MemberData(nameof(CountForTuple3FromN))]
        [MemberData(nameof(CountForTuple4FromN))]
        public void CountTuples(int n, int k, int expected)
        {
            Assert.Equal(expected, Combinations.Count(n, k));
        }

        [Theory]
        [MemberData(nameof(CountForTuple2FromN))]
        public void Tuple2(int n, int k, int expected)
        {
            var tuples = Combinations.Tuple2(n).ToArray();
            WriteRow(n, k, tuples.Select(x => $"{x.Index1}{x.Index2}"));

            Assert.Equal(expected, tuples.Distinct().Count());
        }

        [Theory]
        [MemberData(nameof(CountForTuple3FromN))]
        public void Tuple3(int n, int k, int expected)
        {
            var tuples = Combinations.Tuple3(n).ToArray();
            WriteRow(n, k, tuples.Select(x => $"{x.Index1}{x.Index2}{x.Index3}"));

            Assert.Equal(expected, tuples.Distinct().Count());
        }

        [Theory]
        [MemberData(nameof(CountForTuple4FromN))]
        public void Tuple4(int n, int k, int expected)
        {
            var tuples = Combinations.Tuple4(n).ToArray();
            WriteRow(n, k, tuples.Select(x => $"{x.Index1}{x.Index2}{x.Index3}{x.Index4}"));

            Assert.Equal(expected, tuples.Distinct().Count());
        }

        #region data
        public static IEnumerable<object[]> CountForN()
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

        public static IEnumerable<object[]> CountForTuple1FromN()
        {
            // n; k; expected
            yield return new object[] { -1, 1, 0 };
            yield return new object[] { 0, 1, 0 };
            yield return new object[] { 1, 1, 1 };
            yield return new object[] { 2, 1, 2 };
            yield return new object[] { 3, 1, 3 };
        }

        public static IEnumerable<object[]> CountForTuple2FromN()
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

        public static IEnumerable<object[]> CountForTuple3FromN()
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

        public static IEnumerable<object[]> CountForTuple4FromN()
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
            => _output.WriteLine("n={0}, k={1}, count:{2}", n, k, count);

        private void WriteRow(int n, int k, IEnumerable<string> tuples)
            => _output.WriteLine("n={0}, k={1}, tuples:({2})", n, k, string.Join(", ", tuples));
    }
}
