namespace SMath.Combinatorics
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Xunit;
    using Xunit.Abstractions;

    public class CombinationsWithRepetitionTest
    {
        private readonly ITestOutputHelper _output;

        public CombinationsWithRepetitionTest(ITestOutputHelper output)
        {
            _output = output;
        }

        //DES: NE - nema smysl pze muze byt k > n
        //[DataTestMethod]
        //[MemberData(nameof(CountForN))]
        //public void Count(uint n, uint expected)
        //{
        //    Assert.Equal(expected, CombinationsWithRepetition.Count(n));
        //}

        [Theory]
        [MemberData(nameof(CountForTuple1FromN))]
        [MemberData(nameof(CountForTuple2FromN))]
        [MemberData(nameof(CountForTuple3FromN))]
        [MemberData(nameof(CountForTuple4FromN))]
        public void CountTuples(uint n, uint k, uint expected)
        {
            var count = CombinationsWithRepetition.Count(n, k);
            WriteResult(n, k, count);

            Assert.Equal(expected, count);
        }

        [Theory]
        [MemberData(nameof(CountForTuple2FromN))]
        public void Tuple2(uint n, uint k, uint expected)
        {
            var tuples = CombinationsWithRepetition.Tuple2(n).ToArray();
            WriteRow(n, k, tuples.Select(x => $"{x.Index1}{x.Index2}"));

            Assert.Equal(expected, tuples.Length);
            //CollectionAssert.AllItemsAreUnique(tuples);
        }

        [Theory]
        [MemberData(nameof(CountForTuple3FromN))]
        public void Tuple3(uint n, uint k, uint expected)
        {
            var tuples = CombinationsWithRepetition.Tuple3(n).ToArray();
            WriteRow(n, k, tuples.Select(x => $"{x.Index1}{x.Index2}{x.Index3}"));

            Assert.Equal(expected, tuples.Length);
            //CollectionAssert.AllItemsAreUnique(tuples);
        }

        [Theory]
        [MemberData(nameof(CountForTuple4FromN))]
        public void Tuple4(uint n, uint k, uint expected)
        {
            var tuples = CombinationsWithRepetition.Tuple4(n).ToArray();
            WriteRow(n, k, tuples.Select(x => $"{x.Index1}{x.Index2}{x.Index3}{x.Index4}"));

            Assert.Equal(expected, tuples.Length);
            //CollectionAssert.AllItemsAreUnique(tuples);
        }

        #region data
        //DES: NE - nema smysl pze muze byt k > n
        //private static IEnumerable<object[]> CountForN()
        //{
        //    // n; expected
        //    yield return new object[] { -1, 0 };
        //    yield return new object[] { 0, 0 };
        //    yield return new object[] { 1, 1 };
        //    yield return new object[] { 2, 5 };
        //    yield return new object[] { 3, 19 };
        //    yield return new object[] { 4, 69 };
        //}

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
            yield return new object[] { 1, 2, 1 };
            yield return new object[] { 2, 2, 3 };
            yield return new object[] { 3, 2, 6 };
            yield return new object[] { 4, 2, 10 };
            yield return new object[] { 5, 2, 15 };
        }

        public static IEnumerable<object[]> CountForTuple3FromN()
        {
            // n; k; expected
            yield return new object[] { -1, 3, 0 };
            yield return new object[] { 0, 3, 0 };
            yield return new object[] { 1, 3, 1 };
            yield return new object[] { 2, 3, 4 };
            yield return new object[] { 3, 3, 10 };
            yield return new object[] { 4, 3, 20 };
            yield return new object[] { 5, 3, 35 };
            yield return new object[] { 6, 3, 56 };
        }

        public static IEnumerable<object[]> CountForTuple4FromN()
        {
            // n; k; expected
            yield return new object[] { -1, 4, 0 };
            yield return new object[] { 0, 4, 0 };
            yield return new object[] { 1, 4, 1 };
            yield return new object[] { 2, 4, 5 };
            yield return new object[] { 3, 4, 15 };
            yield return new object[] { 4, 4, 35 };
            yield return new object[] { 5, 4, 70 };
            yield return new object[] { 6, 4, 126 };
        }
        #endregion

        private void WriteResult(uint n, uint k, uint count)
            => _output.WriteLine("n={0}, k={1}, count:{2}", n, k, count);

        private void WriteRow(uint n, uint k, IEnumerable<string> tuples)
            => _output.WriteLine("n={0}, k={1}, tuples:({2})", n, k, string.Join(", ", tuples));
    }
}
