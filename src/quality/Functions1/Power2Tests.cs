﻿using Xunit;

namespace SMath.Functions1
{
    public class Power2Tests
    {
        [Theory]
        [InlineData(0, 0)]
        [InlineData(1, 2)]
        [InlineData(-1, -2)]
        public void SlopeInPoint(double x, double slope)
        {
            Assert.Equal(slope, Power2.TangentLine.Slope.FromX(x), 6);
        }

        [Theory]
        [InlineData(0, 0, 1, 0)]
        [InlineData(1, -2, 1, 1)]
        [InlineData(-1, 2, 1, 1)]
        public void TangentLineInPoint(double x, double a, double b, double c)
        {
            var tl = Power2.TangentLine.FromX(x);
            Assert.Equal(a, tl.A, 6);
            Assert.Equal(b, tl.B, 6);
            Assert.Equal(c, tl.C, 6);
        }
    }
}