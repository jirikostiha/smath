using Xunit;

namespace SMath.Geometry2D
{
    public class LineRayTests
    {
        [Fact]
        public void Points()
        {
            Assert.Empty(Line.Ray.Points.Get(0d, 1, 0));

            Assert.Collection(Line.Ray.Points.Get(0d, 1, 1),
                item =>
                {
                    Assert.Equal(1, item.X, 6);
                    Assert.Equal(0, item.Y, 6);
                });

            Assert.Collection(Line.Ray.Points.Get(double.Pi / 2d, 1, 1),
               item =>
               {
                   Assert.Equal(0, item.X, 6);
                   Assert.Equal(1, item.Y, 6);
               });
        }
    }
}
