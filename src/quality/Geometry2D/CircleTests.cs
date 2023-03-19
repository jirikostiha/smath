using System;
using Xunit;
using static System.Math;

namespace SMath.Geometry2D
{
    public class CircleTests
    {
        [Fact]
        public void Circumference()
        {
            Assert.Equal(2 * PI, Circle.Perimeter.Length.FromRadius(1d));
        }

        [Fact]
        public void Radius()
        {
            Assert.Equal(1d, Circle.Radius.FromCircumference(2 * PI));
            Assert.Equal(1d, Circle.Radius.FromArea(PI));
        }
    }
}
