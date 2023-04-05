using System;
using Xunit;

namespace SMath.Geometry2D
{
    public class CircleSectorTests
    {
        [Fact]
        public void Circumference()
        {
            Assert.Equal(3d, Circle.Sector.Perimeter.Length.FromArcLength(1d, 1d));
            Assert.Equal(3d, Circle.Sector.Perimeter.Length.FromAngle(1d, 1d));
        }

        [Fact]
        public void Area()
        {
            Assert.Equal(0.5d, Circle.Sector.Region.Area.FromArcLength(1d, 1d));
            Assert.Equal(0.5d, Circle.Sector.Region.Area.FromArcAngle(1d, 1d));
        }
    }
}
