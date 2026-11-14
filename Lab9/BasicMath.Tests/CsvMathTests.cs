using BasicMath;
using CsvHelper;
using System.Globalization;
using Xunit;

namespace BasicMathTests;

public class CsvMathTests
{
    private readonly BasicMaths _math = new();

    public static IEnumerable<object[]> AddData => ReadRows("add_testdata.csv");
    public static IEnumerable<object[]> SubtractData => ReadRows("subtract_testdata.csv");
    public static IEnumerable<object[]> MultiplyData => ReadRows("multiply_testdata.csv");
    public static IEnumerable<object[]> DivideData => ReadRows("divide_testdata.csv");

    [Theory]
    [MemberData(nameof(AddData))]
    public void Add_CsvData_ReturnsExpected(double a, double b, double expected)
        => Assert.Equal(expected, _math.Add(a, b));

    [Theory]
    [MemberData(nameof(SubtractData))]
    public void Subtract_CsvData_ReturnsExpected(double a, double b, double expected)
        => Assert.Equal(expected, _math.Subtract(a, b));

    [Theory]
    [MemberData(nameof(MultiplyData))]
    public void Multiply_CsvData_ReturnsExpected(double a, double b, double expected)
        => Assert.Equal(expected, _math.Multiply(a, b));

    [Theory]
    [MemberData(nameof(DivideData))]
    public void Divide_CsvData_ReturnsExpected(double a, double b, double expected)
        => Assert.Equal(expected, _math.Divide(a, b));

    private static IEnumerable<object[]> ReadRows(string fileName)
    {
        var path = Path.Combine(AppContext.BaseDirectory, "TestData", fileName);
        using var reader = new StreamReader(path);
        using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);

        foreach (var row in csv.GetRecords<MathTestData>())
        {
            yield return new object[] { row.A, row.B, row.Expected };
        }
    }

    public sealed class MathTestData
    {
        public double A { get; set; }
        public double B { get; set; }
        public double Expected { get; set; }
    }
}
