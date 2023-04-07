using System;
using Xunit;

namespace SMath.Geometry2D
{
    public class GeometricVector2Tests
    {
        [Fact]
        public void Magnitude()
        {
            Assert.Equal(1, GeometricVector2.Magnitude(1d, 0d));
            Assert.Equal(1, GeometricVector2.Magnitude(0d, 1d));
            Assert.Equal(Math.Sqrt(2), GeometricVector2.Magnitude(1d, 1d));
        }

        [Fact]
        public void Normalized()
        {
            Assert.Equal((1d, 0d), GeometricVector2.Normalized(1d, 0d));
            Assert.Equal((0d, 1d), GeometricVector2.Normalized(0d, 1d));
            Assert.Equal(1 / Math.Sqrt(2d), GeometricVector2.Normalized(1d, 1d).X, 0.00000001);
            Assert.Equal(1 / Math.Sqrt(2d), GeometricVector2.Normalized(1d, 1d).Y, 0.00000001);
            Assert.Equal(2 / Math.Sqrt(8d), GeometricVector2.Normalized(2d, 2d).Y, 0.00000001);
        }

        [Fact]
        public void DirectionVector()
        {
            Assert.Equal((1, 0), GeometricVector2.Direction((0, 0), (1d, 0)));
            Assert.Equal((0, 1), GeometricVector2.Direction((0, 0), (0, 1d)));
            Assert.Equal((1, 1), GeometricVector2.Direction((0, 0), (1d, 1d)));
        }

        [Fact]
        public void Distance()
        {
            Assert.Equal(1, GeometricVector2.Distance((0, 0), (1d, 0)));
            Assert.Equal(1, GeometricVector2.Distance((0, 0), (0, 1d)));
            Assert.Equal(Math.Sqrt(2), GeometricVector2.Distance((0, 0), (1d, 1d)));
        }

        [Fact]
        public void NormalVector()
        {
            Assert.Equal((0d, 1d), GeometricVector2.Normal1(1d, 0d));
            Assert.Equal((-1d, 1d), GeometricVector2.Normal1(1d, 1d));

            Assert.Equal((0d, -1d), GeometricVector2.Normal2(1d, 0d));
            Assert.Equal((1d, -1d), GeometricVector2.Normal2(1d, 1d));
        }
    }
}
