namespace SMath.Statistics
{
    using System;
    using Xunit;

    public class PearsonCorrelationTest
    {
        #region Eval
        [Theory]
        [MemberData(nameof(PearsonCorrelationData.FullyCorrelatedSequences), MemberType = typeof(PearsonCorrelationData))]
        public void EvalArray_FullCorrelation(int[] aSequence, int[] bSequence)
        {
            Assert.Equal(1d, PearsonCorrelation.Eval(aSequence, bSequence), 6);
        }

        [Theory]
        [MemberData(nameof(PearsonCorrelationData.FullyCorrelatedSequences), MemberType = typeof(PearsonCorrelationData))]
        public void EvalSpan_FullCorrelation(int[] aSequence, int[] bSequence)
        {
            Assert.Equal(1d, PearsonCorrelation.Eval(new ReadOnlySpan<int>(aSequence), new ReadOnlySpan<int>(bSequence)), 6);
        }

        [Theory]
        [MemberData(nameof(PearsonCorrelationData.FullyNegativellyCorrelatedSequences), MemberType = typeof(PearsonCorrelationData))]
        public void EvalArray_FullNegativeCorrelation(int[] aSequence, int[] bSequence)
        {
            Assert.Equal(-1, PearsonCorrelation.Eval(aSequence, bSequence), 6);
        }

        [Theory]
        [MemberData(nameof(PearsonCorrelationData.FullyNegativellyCorrelatedSequences), MemberType = typeof(PearsonCorrelationData))]
        public void EvalSpan_FullNegativeCorrelation(int[] aSequence, int[] bSequence)
        {
            Assert.Equal(-1, PearsonCorrelation.Eval(new ReadOnlySpan<int>(aSequence), new ReadOnlySpan<int>(bSequence)), 6);
        }

        [Theory]
        [MemberData(nameof(PearsonCorrelationData.NotCorrelatedData), MemberType = typeof(PearsonCorrelationData))]
        public void EvalArray_NotCorrelated(int[] aSequence, int[] bSequence, double expected)
        {
            Assert.Equal(expected, PearsonCorrelation.Eval(aSequence, bSequence), 6);
        }

        [Theory]
        [MemberData(nameof(PearsonCorrelationData.NotCorrelatedData), MemberType = typeof(PearsonCorrelationData))]
        public void EvalSpan_NotCorrelated(int[] aSequence, int[] bSequence, double expected)
        {
            Assert.Equal(expected, PearsonCorrelation.Eval(new ReadOnlySpan<int>(aSequence), new ReadOnlySpan<int>(bSequence)), 6);
        }

        [Theory]
        [MemberData(nameof(PearsonCorrelationData.SameValues), MemberType = typeof(PearsonCorrelationData))]
        public void EvalArray_SameValues(int[] aSequence, int[] bSequence, double expected)
        {
            Assert.Equal(expected, PearsonCorrelation.Eval(aSequence, bSequence), 6);
        }

        [Theory]
        [MemberData(nameof(PearsonCorrelationData.SameValues), MemberType = typeof(PearsonCorrelationData))]
        public void EvalSpan_SameValues(int[] aSequence, int[] bSequence, double expected)
        {
            Assert.Equal(expected, PearsonCorrelation.Eval(new ReadOnlySpan<int>(aSequence), new ReadOnlySpan<int>(bSequence)), 6);
        }

        [Fact]
        public void EvalArray_BothAreEmpty_NoException() =>
            PearsonCorrelation.Eval(Array.Empty<double>(), Array.Empty<double>());

        [Fact]
        public void EvalSpan_BothAreEmpty_NoException() =>
            PearsonCorrelation.Eval(new ReadOnlySpan<double>(Array.Empty<double>()), new ReadOnlySpan<double>(Array.Empty<double>()));

        [Fact]
        public void EvalArray_DifferentLength_Exception() =>
            Assert.Throws<ArgumentException>(() => PearsonCorrelation.Eval(new int[] { 1 }, new int[] { 1, 2 }));

        [Fact]
        public void EvalSpan_DifferentLength_Exception() =>
            Assert.Throws<ArgumentException>(() => PearsonCorrelation.Eval(new ReadOnlySpan<int>(new int[] { 1 }), new ReadOnlySpan<int>(new int[] { 1, 2 })));
        #endregion

        #region EvalPerf
        [Theory]
        [MemberData(nameof(PearsonCorrelationData.FullyCorrelatedSequences), MemberType = typeof(PearsonCorrelationData))]
        public void EvalArrayPerf_FullCorrelation(int[] aSequence, int[] bSequence)
        {
            Assert.Equal(1d, PearsonCorrelation.EvalPerf(aSequence, bSequence, 0), 6);
        }

        [Theory]
        [MemberData(nameof(PearsonCorrelationData.FullyNegativellyCorrelatedSequences), MemberType = typeof(PearsonCorrelationData))]
        public void EvalArrayPerf_FullNegativeCorrelation(int[] aSequence, int[] bSequence)
        {
            Assert.Equal(-1, PearsonCorrelation.EvalPerf(aSequence, bSequence, 0), 6);
        }

        [Theory]
        [MemberData(nameof(PearsonCorrelationData.NotCorrelatedData), MemberType = typeof(PearsonCorrelationData))]
        public void EvalArrayPerf_NotCorrelated(int[] aSequence, int[] bSequence, double expected)
        {
            Assert.Equal(expected, PearsonCorrelation.EvalPerf(aSequence, bSequence, 0), 6);
        }

        [Fact]
        public void EvalArrayPerf_BothSequencesAreEmpty_NoException() =>
            PearsonCorrelation.EvalPerf(Array.Empty<double>(), Array.Empty<double>(), 0);
        #endregion
        [Theory]
        [MemberData(nameof(PearsonCorrelationData.NotCorrelatedData), MemberType = typeof(PearsonCorrelationData))]
        public void EvalArrayPerf_NotCorrelated(int[] aSequence, int[] bSequence, double expected)
        {
            Assert.Equal(expected, PearsonCorrelation.EvalPerf(aSequence, bSequence, 0), 6);
        }
        public void EvalArrayPerf_BothSequencesAreEmpty_NoException() =>
            PearsonCorrelation.EvalPerf(Array.Empty<double>(), Array.Empty<double>(), 0);
        #endregion
    }
}