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
            var tl = Identity.TangentLine.FromX(x);
            Assert.Equal(a, tl.A, 6);
            Assert.Equal(b, tl.B, 6);
            Assert.Equal(c, tl.C, 6);
        }

        [Fact]
        public void NormalLineSlopeInPoint()
        {
            // always -1
            Assert.Equal(-1d, Identity.TangentLine.Slope.FromX<double>(), 6);
        }

        [Theory]
        [InlineData(0, 1, 1, 0)]
        [InlineData(1, 1, 1, -1)]
        [InlineData(-1, 1, 1, 1)]
        public void NormalLineInPoint(double x, double a, double b, double c)
        {
            var tl = Identity.NormalLine.FromX(x);
            Assert.Equal(a, tl.A, 6);
            Assert.Equal(b, tl.B, 6);
            Assert.Equal(c, tl.C, 6);
        }
    }
}
