using Xunit;
using static System.Math;

namespace SMath.Geometry2D
{
    public class CircleChordTests
    {
        [Fact]
        public void Length()
        {
            Assert.Equal(2d * Sin(0.5d), Circle.Chord.Length.FromAngle(1d, 1d));
        }

        [Fact]
        public void Sagitta()
        {
            Assert.Equal(1d - Cos(0.5d), Circle.Chord.Sagitta.FromAngle(1d, 1d));
        }

        [Fact]
        public void Apothem()
        {
            Assert.Equal(Cos(0.5d), Circle.Chord.Apothem.FromAngle(1d, 1d));
        }
    }
}
