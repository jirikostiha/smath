using Xunit;

namespace SMath.Functions1
{
    public class SineTests
    {
        [Theory]
        [InlineData(0, 1)]
        [InlineData(double.Pi / 2, 0)]
        [InlineData(double.Pi, -1)]
        [InlineData(double.Pi * 1.5, 0)]
        public void SlopeInPoint(double x, double slope)
        {
            Assert.Equal(slope, Sine.TangentLine.Slope.FromX(x), 6);
        }

        [Theory]
        [InlineData(0, -1, 1, 0)]
        [InlineData(double.Pi / 2, 0, 1, -1)]
        [InlineData(double.Pi, 1, 1, -double.Pi)]
        public void TangentLineInPoint(double x, double a, double b, double c)
        {
            var tl = Sine.TangentLine.FromX(x);
            Assert.Equal(a, tl.A, 6);
            Assert.Equal(b, tl.B, 6);
            Assert.Equal(c, tl.C, 6);
        }
    }
}
