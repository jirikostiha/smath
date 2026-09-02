using SMath;
using Xunit;

namespace SMath;

public class BinaryIntegerExtensionTests
{
    [Theory]
    [InlineData(0, 0, 0)]
    [InlineData(int.MaxValue, int.MaxValue, 0)]
    [InlineData(-1, -1, 0)]
    [InlineData(0, 1, 1)]
    [InlineData(0, 2, 1)]
    [InlineData(0, 3, 2)]
    [InlineData(0, -1, 32)]
    [InlineData(1, -1, 31)]
    [InlineData(0, int.MaxValue, 31)]
    [InlineData(int.MinValue, 0, 1)]
    [InlineData(int.MinValue, int.MaxValue, 32)]
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
    [InlineData((sbyte)0, (sbyte)0, (sbyte)0)]
    [InlineData((sbyte)0, (sbyte)1, (sbyte)1)]
    [InlineData((sbyte)0, (sbyte)-1, (sbyte)8)]
    [InlineData((sbyte)1, (sbyte)-1, (sbyte)7)]
    [InlineData(sbyte.MinValue, (sbyte)0, (sbyte)1)]
    [InlineData(sbyte.MinValue, sbyte.MaxValue, (sbyte)8)]
    public void HammingDistance_SByte(sbyte n1, sbyte n2, sbyte distance)
    {
        Assert.Equal(distance, n1.HammingDistanceTo(n2));
    }

    [Theory]
    [InlineData(0L, 0L, 0L)]
    [InlineData(0L, 1L, 1L)]
    [InlineData(0L, -1L, 64L)]
    [InlineData(1L, -1L, 63L)]
    [InlineData(long.MinValue, 0L, 1L)]
    [InlineData(long.MinValue, long.MaxValue, 64L)]
    public void HammingDistance_Long(long n1, long n2, long distance)
    {
        Assert.Equal(distance, n1.HammingDistanceTo(n2));
    }

    [Theory]
    [InlineData(0u, 0u, 0u)]
    [InlineData(0u, 1u, 1u)]
    [InlineData(0u, uint.MaxValue, 32u)]
    [InlineData(0xAAAAAAAAu, 0x55555555u, 32u)]
    public void HammingDistance_UInt(uint n1, uint n2, uint distance)
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

    [Theory]
    [InlineData(2, 0, 1)]
    [InlineData(2, 1, 2)]
    [InlineData(2, 10, 1024)]
    [InlineData(3, 3, 27)]
    [InlineData(5, 0, 1)]
    [InlineData(0, 0, 1)]
    [InlineData(0, 5, 0)]
    [InlineData(1, 30, 1)]
    [InlineData(-2, 3, -8)]
    [InlineData(-2, 4, 16)]
    [InlineData(7, 9, 40353607)]
    public void Pow_Int(int number, int exp, int expected)
    {
        Assert.Equal(expected, number.Pow(exp));
    }

    [Fact]
    public void Pow_Long()
    {
        Assert.Equal(1152921504606846976L, 2L.Pow(60L));
    }

    [Fact]
    public void Pow_NegativeExponentThrows()
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => 2.Pow(-1));
    }
}
