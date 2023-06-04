using SMath.GeometryD2;
using Xunit;

namespace SMath.Geometry2D
{
    public class Point3Tests
    {
        [Theory]
        [InlineData(0, 0, 0, 1, 0, 0, 1)]
        [InlineData(0, 0, 0, 1, 1, 1, 3)]
        [InlineData(1, 1, 1, -1, -1, -1, 6)]
        public void ManhattanDistance(double x1, double y1, double z1, double x2, double y2, double z2, double distance)
        {
            Assert.Equal(distance, Point3.ManhattanDistance((x1, y1, z1), (x2, y2, z2)));
            Assert.Equal(distance, Point3.ManhattanDistance((x2 - x1, y2 - y1, z2 - z1)));
        }

        [Theory]
        [InlineData(0, 0, 0, 1, 0, 0, 1)]
        [InlineData(0, 0, 0, 1, 1, 1, 1)]
        [InlineData(1, 1, 1, -1, -1, -1, 2)]
        public void ChebyshevDistance(double x1, double y1, double z1, double x2, double y2, double z2, double distance)
        {
            Assert.Equal(distance, Point3.ChebyshevDistance((x1, y1, z1), (x2, y2, z2)));
            Assert.Equal(distance, Point3.ChebyshevDistance((x2 - x1, y2 - y1, z2 - z1)));
        }

        [Theory]
        [InlineData(0, 0, 0, 1, 0, 0, 1, 1)]
        [InlineData(0, 0, 0, 1, 1, 1, 1, 3)]
        [InlineData(1, 1, 1, -1, -1, -1, 1, 6)]
        public void MinkowskiDistance(double x1, double y1, double z1, double x2, double y2, double z2, double r, double distance)
        {
            Assert.Equal(distance, Point3.MinkowskiDistance((x1, y1, z1), (x2, y2, z2), r));
            Assert.Equal(distance, Point3.MinkowskiDistance((x2 - x1, y2 - y1, z2 - z1), r));
        }

        //[Theory]
        //[InlineData(0, 0, 1, 0, 1)] //todo
        //public void CanberraDistance(double x1, double y1, double x2, double y2, double distance)
        //{
        //    Assert.Equal(distance, Point2.CanberraDistance((x1, y1), (x2, y2)));
        //}

        //[Theory]
        //[InlineData(0, 0, 1, 0, 1)] //todo
        //public void BrayCurtisDissimilarity(double x1, double y1, double x2, double y2, double distance)
        //{
        //    Assert.Equal(distance, Point2.BrayCurtisDissimilarity((x1, y1), (x2, y2)));
        //}
    }
}
