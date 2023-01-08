namespace SMath.Geometry2D
{
    using System;
    using System.Linq;
    using Xunit;

    public class CircleTests
    {
        [Fact]
        public void IndicesCoords()
        {
            var coords = Circle.Indexes(radius: 1, count: 3).ToArray();

            Assert.Equal((1, 0), coords[0]);
            Assert.Equal(-0.5, coords[1].X1, 0.01);
            Assert.Equal(0.866, coords[1].X2, 0.01);
            Assert.Equal(-0.5, coords[2].X1, 0.01);
            Assert.Equal(-0.866, coords[2].X2, 0.01);
        }

        [Fact(DisplayName = "Tangent points for point outside a circle")]
        public void TangentPointsForExternalPoint()
        {
            var hasTangents = Circle.TangentPoints(1, 1, radius: 1, 2, 2, out var tan1, out var tan2);

            Assert.True(hasTangents);
            Assert.Equal(1, tan1.X1, 0.001);
            Assert.Equal(2, tan1.X2, 0.001);
            Assert.Equal(2, tan2.X1, 0.001);
            Assert.Equal(1, tan2.X2, 0.001);
        }

        [Fact(DisplayName = "Tangent points for point inside a circle")]
        public void TangentPointsForInternalPoint()
        {
            var hasTangents = Circle.TangentPoints(1, 1, radius: 1, 1.3, 1.2, out var tan1, out var tan2);

            Assert.False(hasTangents);
            Assert.Equal(double.NaN, tan1.X1);
            Assert.Equal(double.NaN, tan1.X2);
            Assert.Equal(double.NaN, tan2.X1);
            Assert.Equal(double.NaN, tan2.X2);
        }

        [Fact( DisplayName = "Tangent points for point on a circle")]

        public void TangentPointsForTouchingPoint()
        {
            var hasTangents = Circle.TangentPoints(1, 1, radius: 1, 1, 2, out var tan1, out var tan2);

            Assert.True(hasTangents);
            Assert.Equal(1, tan1.X1, 0.001);
            Assert.Equal(2, tan1.X2, 0.001);
            Assert.Equal(1, tan2.X1, 0.001);
            Assert.Equal(2, tan2.X2, 0.001);
        }
    }
}
