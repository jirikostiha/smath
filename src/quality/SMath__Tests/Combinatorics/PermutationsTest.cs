namespace SMath.Combinatorics
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Xunit;
    using Xunit.Abstractions;

    public class PermutationsTest
    {
        private readonly ITestOutputHelper _output;

        public PermutationsTest(ITestOutputHelper output)
        {
            _output = output;
        }

        [Theory]
        [MemberData(nameof(CountForN))]
        public void Count(int n, int expected)
        {
            Assert.Equal(expected, Permutations.Count(n));
        }

        [Theory]
        [MemberData(nameof(CountForTuple1FromN))]
        [MemberData(nameof(CountForTuple2FromN))]
        [MemberData(nameof(CountForTuple3FromN))]
        [MemberData(nameof(CountForTuple4FromN))]
        public void CountTuples(int n, int k, int expected)
        {
            var count = Permutations.Count(n, k);
            WriteResult(n, k, count);

            Assert.Equal(expected, count);
        }

        [Theory]
        [MemberData(nameof(CountForTuple2FromN))]
        public void Tuple2(int n, int k, int expected)
        {
            var tuples = Permutations.Tuple2(n).ToArray();
            WriteRow(n, k, tuples.Select(x => $"{x.Index1}{x.Index2}"));

            Assert.Equal(expected, tuples.Distinct().Count());
        }

        [Theory]
        [MemberData(nameof(CountForTuple3FromN))]
        public void Tuple3(int n, int k, int expected)
        {
            var tuples = Permutations.Tuple3(n).ToArray();
            WriteRow(n, k, tuples.Select(x => $"{x.Index1}{x.Index2}{x.Index3}"));

            Assert.Equal(expected, tuples.Distinct().Count());
        }

        [Theory]
        [MemberData(nameof(CountForTuple4FromN))]
        public void Tuple4(int n, int k, int expected)
        {
            var tuples = Permutations.Tuple4(n).ToArray();
            WriteRow(n, k, tuples.Select(x => $"{x.Index1}{x.Index2}{x.Index3}{x.Index4}"));

            Assert.Equal(expected, tuples.Distinct().Count());
        }

        //[TestMethod]
        //public void Tuples()
        //{
        //    uint[] values = new uint[] { 0, 1, 2, 3 };

        //    Permutations.ForAllPermutation(values, (vals) =>
        //    {
        //        Debug.Pruint(String.Join("", vals));
        //        return false;
        //    });
        //}

        #region data
        public static IEnumerable<object[]> CountForN()
        {
            // n; expected
            yield return new object[] { -1, 0 };
            yield return new object[] { 0, 0 };
            yield return new object[] { 1, 1 };
            yield return new object[] { 2, 4 };
            yield return new object[] { 3, 15 };
            yield return new object[] { 4, 64 };
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
            yield return new object[] { 2, 2, 2 };
            yield return new object[] { 3, 2, 6 };
            yield return new object[] { 4, 2, 12 };
            yield return new object[] { 5, 2, 20 };
        }

        public static IEnumerable<object[]> CountForTuple3FromN()
        {
            // n; k; expected
            yield return new object[] { -1, 3, 0 };
            yield return new object[] { 0, 3, 0 };
            yield return new object[] { 1, 3, 0 };
            yield return new object[] { 2, 3, 0 };
            yield return new object[] { 3, 3, 6 };
            yield return new object[] { 4, 3, 24 };
            yield return new object[] { 5, 3, 60 };
            yield return new object[] { 6, 3, 120 };
        }

        public static IEnumerable<object[]> CountForTuple4FromN()
        {
            // n; k; expected
            yield return new object[] { -1, 4, 0 };
            yield return new object[] { 0, 4, 0 };
            yield return new object[] { 1, 4, 0 };
            yield return new object[] { 2, 4, 0 };
            yield return new object[] { 3, 4, 0 };
            yield return new object[] { 4, 4, 24 };
            yield return new object[] { 5, 4, 120 };
            yield return new object[] { 6, 4, 360 };
        }
        #endregion

        private void WriteResult(int n, int k, int count)
            => _output.WriteLine("n={0}, k={1}, count:{2}", n, k, count);

        private void WriteRow(int n, int k, IEnumerable<string> tuples)
            => _output.WriteLine("n={0}, k={1}, tuples:({2})", n, k, string.Join(", ", tuples));
    }
}
