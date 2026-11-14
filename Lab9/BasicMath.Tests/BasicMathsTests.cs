using BasicMath;
using Xunit;

namespace BasicMathTests;

public class BasicMathsTests
{
    private readonly BasicMaths _math = new();

    // Equivalence Partitioning: positive, negative and zero inputs.
    [Theory]
    [InlineData(1, 1, 2)]
    [InlineData(-1, -1, -2)]
    [InlineData(0, 0, 0)]
    [InlineData(12.5, 7.5, 20)]
    public void Add_EquivalencePartitions_ReturnsExpected(double a, double b, double expected)
        => Assert.Equal(expected, _math.Add(a, b));

    // Boundary Value Analysis around the 32-bit integer limits.
    [Theory]
    [InlineData(2147483647d, 1d, 2147483648d)]
    [InlineData(-2147483648d, -1d, -2147483649d)]
    public void Add_BoundaryValues_ReturnsExpected(double a, double b, double expected)
        => Assert.Equal(expected, _math.Add(a, b));

    [Theory]
    [InlineData(5, 3, 2)]
    [InlineData(-5, -3, -2)]
    [InlineData(0, 0, 0)]
    public void Subtract_ValidInputs_ReturnsExpected(double a, double b, double expected)
        => Assert.Equal(expected, _math.Subtract(a, b));

    [Theory]
    [InlineData(2, 3, 6)]
    [InlineData(-2, -3, 6)]
    [InlineData(0, 5, 0)]
    public void Multiply_ValidInputs_ReturnsExpected(double a, double b, double expected)
        => Assert.Equal(expected, _math.Multiply(a, b));

    [Theory]
    [InlineData(6, 3, 2)]
    [InlineData(-6, -3, 2)]
    [InlineData(0, 1, 0)]
    public void Divide_ValidInputs_ReturnsExpected(double a, double b, double expected)
        => Assert.Equal(expected, _math.Divide(a, b));

    [Fact]
    public void Divide_ByZero_ThrowsDivideByZeroException()
        => Assert.Throws<DivideByZeroException>(() => _math.Divide(10, 0));
}
