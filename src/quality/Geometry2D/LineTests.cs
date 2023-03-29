using Xunit;

namespace SMath.Geometry2D
{
    public class LineTests
    {
        [Fact]
        public void FromTwoPoints()
        {
            Assert.Equal(Line.X<double>(), Line.FromTwoPoints((0d, 0d), (1d, 0d)));
            Assert.Equal(Line.Y<double>(), Line.FromTwoPoints((0d, 0d), (0d, 1d)));
            Assert.Equal((0, -1, 0), Line.FromTwoPoints((0d, 0d), (-1d, 0d)));
            Assert.Equal((1, 0, 0), Line.FromTwoPoints((0d, 0d), (0d, -1d)));

            var (A, B, _) = Line.FromTwoPoints((0d, 0d), (1d, 1d));
            Assert.Equal(A, -B);
        }
    }
}
