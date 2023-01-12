using System;
using Xunit;
using static System.Math;

namespace SMath.Geometry2D
{
    public class CircleArcTests
    {
        [Fact]
        public void Length()
        {
            Assert.Equal(1d, Circle.Arc.Length.FromAngle(1d, 1d));
        }
    }
}
