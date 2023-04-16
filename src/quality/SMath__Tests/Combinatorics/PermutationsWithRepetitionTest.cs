namespace SMath.Combinatorics
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Xunit;
    using Xunit.Abstractions;

    public class PermutationsWithRepetitionTest
    {
        private readonly ITestOutputHelper _output;

        public PermutationsWithRepetitionTest(ITestOutputHelper output)
        {
            _output = output;
        }

        //DES: NE - nema smysl pze muze byt k > n
        //[DataTestMethod]
        //[MemberData(nameof(CountForN))]
        //public void Count(uint n, uint expected)
        //{
        //    Assert.Equal(expected, PermutationsWithRepetition.Count(n));
        //}

        [Theory]
        [MemberData(nameof(CountForTuple1FromN))]
        [MemberData(nameof(CountForTuple2FromN))]
        [MemberData(nameof(CountForTuple3FromN))]
        [MemberData(nameof(CountForTuple4FromN))]
        public void CountTuples(uint n, uint k, uint expected)
        {
            var count = PermutationsWithRepetition.Count(n, k);
            WriteResult(n, k, count);

            Assert.Equal(expected, count);
        }

        [Theory]
        [MemberData(nameof(CountForTuple2FromN))]
        public void Tuple2(uint n, uint k, uint expected)
        {
            var tuples = PermutationsWithRepetition.Tuple2(n).ToArray();
            WriteRow(n, k, tuples.Select(x => $"{x.Index1}{x.Index2}"));

            Assert.Equal(expected, tuples.Length);
            //CollectionAssert.AllItemsAreUnique(tuples);
        }

        [Theory]
        [MemberData(nameof(CountForTuple3FromN))]
        public void Tuple3(uint n, uint k, uint expected)
        {
            var tuples = PermutationsWithRepetition.Tuple3(n).ToArray();
            WriteRow(n, k, tuples.Select(x => $"{x.Index1}{x.Index2}{x.Index3}"));

            Assert.Equal(expected, tuples.Length);
            //CollectionAssert.AllItemsAreUnique(tuples);
        }

        [Theory]
        [MemberData(nameof(CountForTuple4FromN))]
        public void Tuple4(uint n, uint k, uint expected)
        {
            var tuples = PermutationsWithRepetition.Tuple4(n).ToArray();
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
        //    yield return new object[] { 2, 6 };
        //    yield return new object[] { 3, 39 };
        //    yield return new object[] { 4, 340 };
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
            yield return new object[] { 2, 2, 4 };
            yield return new object[] { 3, 2, 9 };
            yield return new object[] { 4, 2, 16 };
            yield return new object[] { 5, 2, 25 };
        }

        public static IEnumerable<object[]> CountForTuple3FromN()
        {
            // n; k; expected
            yield return new object[] { -1, 3, 0 };
            yield return new object[] { 0, 3, 0 };
            yield return new object[] { 1, 3, 1 };
            yield return new object[] { 2, 3, 8 };
            yield return new object[] { 3, 3, 27 };
            yield return new object[] { 4, 3, 64 };
            yield return new object[] { 5, 3, 125 };
            yield return new object[] { 6, 3, 216 };
        }

        public static IEnumerable<object[]> CountForTuple4FromN()
        {
            // n; k; expected
            yield return new object[] { -1, 4, 0 };
            yield return new object[] { 0, 4, 0 };
            yield return new object[] { 1, 4, 1 };
            yield return new object[] { 2, 4, 16 };
            yield return new object[] { 3, 4, 81 };
            yield return new object[] { 4, 4, 256 };
            yield return new object[] { 5, 4, 625 };
            yield return new object[] { 6, 4, 1296 };
        }
        #endregion

        private void WriteResult(uint n, uint k, uint count)
            => _output.WriteLine("n={0}, k={1}, count:{2}", n, k, count);

        private void WriteRow(uint n, uint k, IEnumerable<string> tuples)
            => _output.WriteLine("n={0}, k={1}, tuples:({2})", n, k, string.Join(", ", tuples));
    }
}
