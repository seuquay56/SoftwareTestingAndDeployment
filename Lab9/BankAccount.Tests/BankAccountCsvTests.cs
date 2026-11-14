using BankAccountApp;
using CsvHelper;
using System.Globalization;
using Xunit;

namespace BankAccountTests;

public class BankAccountCsvTests
{
    public static IEnumerable<object[]> CsvCases => ReadCsv();

    [Theory]
    [MemberData(nameof(CsvCases))]
    public void Debit_CsvData_UpdatesBalanceOrRejectsInsufficientFunds(
        string customerName, decimal initialBalance, decimal debitAmount, string expectedBalance)
    {
        var account = new BankAccount(customerName, initialBalance);

        if (string.Equals(expectedBalance, "Insufficient funds", StringComparison.OrdinalIgnoreCase))
        {
            Assert.Throws<InvalidOperationException>(() => account.Debit(debitAmount));
            Assert.Equal(initialBalance, account.Balance);
            return;
        }

        account.Debit(debitAmount);
        Assert.Equal(decimal.Parse(expectedBalance, CultureInfo.InvariantCulture), account.Balance);
    }

    private static IEnumerable<object[]> ReadCsv()
    {
        var path = Path.Combine(AppContext.BaseDirectory, "TestData", "BankAccountTestData.csv");
        using var reader = new StreamReader(path);
        using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);
        foreach (var row in csv.GetRecords<BankAccountRow>())
            yield return new object[] { row.CustomerName, row.InitialBalance, row.DebitAmount, row.ExpectedBalance };
    }

    public sealed class BankAccountRow
    {
        public string CustomerName { get; set; } = string.Empty;
        public decimal InitialBalance { get; set; }
        public decimal DebitAmount { get; set; }
        public string ExpectedBalance { get; set; } = string.Empty;
    }
}
