using Xunit;

namespace SMath.Functions1
{
    public class IdentityTests
    {
        [Fact]
        public void SlopeInPoint()
        {
            // always 1
            Assert.Equal(1d, Identity.TangentLine.Slope.FromX<double>(), 6);
        }

        [Theory]
        [InlineData(0, -1, 1, 0)]
        [InlineData(1, -1, 1, 0)]
        [InlineData(-1, -1, 1, 0)]
        public void TangentLineInPoint(double x, double a, double b, double c)
        {
            var line = Identity.TangentLine.FromX(x);
            Assert.Equal(a, line.A, 6);
            Assert.Equal(b, line.B, 6);
            Assert.Equal(c, line.C, 6);
        }

        [Fact]
        public void NormalLineSlopeInPoint()
        {
            // always 1
            Assert.Equal(1d, Identity.TangentLine.Slope.FromX<double>(), 6);
        }

        [Theory]
        [InlineData(0, 1, 1, 0)]
        [InlineData(1, 1, 1, -1)]
        [InlineData(-1, 1, 1, 1)]
        public void NormalLineInPoint(double x, double a, double b, double c)
        {
            var line = Identity.NormalLine.FromX(x);
            Assert.Equal(a, line.A, 6);
            Assert.Equal(b, line.B, 6);
            Assert.Equal(c, line.C, 6);
        }
    }
}
