using SMath.Functions1;
using Xunit;

namespace SMath.Geometry2D
{
    public class Function1GeometryTests
    {
        [Theory]
        [InlineData(0, 1)]
        [InlineData(double.Pi / 2, 0)]
        [InlineData(double.Pi, -1)]
        [InlineData(double.Pi * 1.5, 0)]
        public void Sine_SlopeInPoint(double x, double slope)
        {
            Assert.Equal(slope, Function1Geometry.TangentLine.Slope.FromX(Sine.DerivativeEval(x)), 6);
        }

        [Theory]
        [InlineData(0, -1, 1, 0)]
        [InlineData(double.Pi / 2, 0, 1, -1)]
        [InlineData(double.Pi, 1, 1, -double.Pi)]
        public void Sine_TangentLineInPoint(double x, double a, double b, double c)
        {
            var tl = Function1Geometry.TangentLine.FromX(x, Sine.Eval(x), Sine.DerivativeEval(x));
            Assert.Equal(a, tl.A, 6);
            Assert.Equal(b, tl.B, 6);
            Assert.Equal(c, tl.C, 6);
        }

        [Theory]
        [InlineData(0d, 1, 0, 0)]  // y-axis
        [InlineData(1, 0.5, 1, -1.5)]
        [InlineData(-1, -0.5, 1, -1.5)]
        public void Power2_NormalLineInPoint(double x, double a, double b, double c)
        {
            var tl = Function1Geometry.NormalLine.FromX(x, Power2.Eval(x), 
                Function1Geometry.NormalLine.Slope.FromX(Power2.DerivativeEval(x)));
            
            Assert.Equal(a, tl.A, 6);
            Assert.Equal(b, tl.B, 6);
            Assert.Equal(c, tl.C, 6);
        }
    }
}
