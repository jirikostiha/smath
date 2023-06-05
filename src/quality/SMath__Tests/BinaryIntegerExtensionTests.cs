using SMath;
using Xunit;

namespace SMath.Geometry2D
{
    public class BinaryIntegerExtensionTests
    {
        [Theory]
        [InlineData(0, 0, 0)]
        [InlineData(int.MaxValue, int.MaxValue, 0)]
        [InlineData(0, 1, 1)]
        [InlineData(0, -1, 1)]
        [InlineData(0, 2, 1)]
        [InlineData(0, 3, 2)]
        [InlineData(1, -1, 0)]
        [InlineData(-1, -1, 0)]
        [InlineData(0, int.MaxValue, 31)]
        public void HammingDistance_Int(int n1, int n2, int distance)
        {
            Assert.Equal(distance, n1.HammingDistanceTo(n2));
        }

        [Theory]
        [InlineData(0, 0, 0)]
        [InlineData(0, 1, 1)]
        [InlineData(0, 2, 1)]
        [InlineData(0, 3, 2)]
        [InlineData(0, byte.MaxValue, 8)]
        [InlineData(byte.MaxValue, byte.MaxValue, 0)]
        public void HammingDistance_Byte(byte n1, byte n2, byte distance)
        {
            Assert.Equal(distance, n1.HammingDistanceTo(n2));
        }

        [Theory]
        [InlineData(1, 1)]
        [InlineData(2, 3)]
        [InlineData(3, 2)]
        [InlineData(7, 4)]
        [InlineData(15, 8)]
        public void ToGrayCode_Byte(byte number, byte gray)
        {
            Assert.Equal(gray, number.ToGrayCode());
        }

        [Theory]
        [InlineData(1, 1)]
        [InlineData(2, 3)]
        [InlineData(3, 2)]
        [InlineData(7, 4)]
        [InlineData(15, 8)]
        public void ToGrayCode_Int(uint number, uint gray)
        {
            Assert.Equal(gray, number.ToGrayCode());
        }

        [Theory]
        [InlineData(1, 1)]
        [InlineData(3, 2)]
        [InlineData(2, 3)]
        [InlineData(4, 7)]
        [InlineData(8, 15)]
        public void FromGrayCode_Byte(byte gray, byte number)
        {
            Assert.Equal(number, gray.FromGrayCode());
        }

        [Theory]
        [InlineData(1, 1)]
        [InlineData(3, 2)]
        [InlineData(2, 3)]
        [InlineData(4, 7)]
        [InlineData(8, 15)]
        public void FromGrayCode_Int(uint gray, uint number)
        {
            Assert.Equal(number, gray.FromGrayCode());
        }
    }
}
