namespace SMath.Statistics
{
    using System;
    using System.Linq;
    using Xunit;

    public class PearsonCorrelationTest
    {
        [Theory]
        [MemberData(nameof(CorrelationData.FullyCorrelatedSequences), MemberType = typeof(CorrelationData))]
        public void FullCorrelation(int[] aSequence, int[] bSequence)
        {
            Assert.Equal(1d, PearsonCorrelation.Eval(aSequence, bSequence), 6);
        }

        [Theory]
        [MemberData(nameof(CorrelationData.FullyNegativellyCorrelatedSequences), MemberType = typeof(CorrelationData))]
        public void FullNegativeCorrelation(int[] aSequence, int[] bSequence)
        {
            Assert.Equal(-1, PearsonCorrelation.Eval(aSequence, bSequence), 6);
        }

        [Fact]
        public void Eval_FirstSequenceIsEmpty_NoException()
        {
            PearsonCorrelation.Eval(Array.Empty<double>(), new double[] { 1.0, 2.0 });
        }

        [Fact]
        public void Eval_SecondSequenceIsEmpty_NoException()
        {
            PearsonCorrelation.Eval(new double[] { 1.0, 2.0 }, Array.Empty<double>());
        }

        [Fact]
        public void Eval_BothSequencesAreEmpty_NoException()
        {
            PearsonCorrelation.Eval(Array.Empty<double>(), Array.Empty<double>());
        }
    }
}