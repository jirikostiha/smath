using SMath.Functions1;
using System;
using Xunit;
using static System.Math;

namespace SMath.Geometry2D
{
    public class SineTests
    {
        [Fact]
        public void SlopeInPoint()
        {
            Assert.Equal(0, Sine.Slope.FromPoint(0d));
        }

        [Fact]
        public void TangentAndNormalLine()
        {
            var tl = Sine.TangentLine.FromPoint(0d);
            Assert.Equal(2 * PI, Circle.Perimeter.Length.FromRadius(1d));
        }
    }
}
