using System;
using Xunit;

namespace SMath.Geometry2D
{
    public class CircleTests
    {
        [Fact]
        public void Circumference()
        {
            Assert.Equal(2 * Math.PI, Circle.Perimeter.Lenght.FromRadius(1d));
        }
    }
}
