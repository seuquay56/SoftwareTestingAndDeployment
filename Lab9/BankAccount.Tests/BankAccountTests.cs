using BankAccountApp;
using Xunit;

namespace BankAccountTests;

public class BankAccountTests
{
    [Theory]
    [InlineData("John Doe", 1000, 200, 800)]
    [InlineData("Jane Smith", 500, 100, 400)]
    [InlineData("Alice Johnson", 300, 50, 250)]
    public void Debit_ValidAmount_UpdatesBalance(string name, decimal opening, decimal amount, decimal expected)
    {
        var account = new BankAccount(name, opening);
        account.Debit(amount);
        Assert.Equal(expected, account.Balance);
    }

    [Fact]
    public void Debit_InsufficientFunds_ThrowsException()
    {
        var account = new BankAccount("Bob Brown", 100);
        var error = Assert.Throws<InvalidOperationException>(() => account.Debit(150));
        Assert.Equal("Insufficient funds", error.Message);
        Assert.Equal(100, account.Balance);
    }

    [Theory]
    [InlineData("John Doe", 1000, 200, 1200)]
    [InlineData("Jane Smith", 500, 100, 600)]
    [InlineData("Alice Johnson", 300, 50, 350)]
    public void Credit_ValidAmount_UpdatesBalance(string name, decimal opening, decimal amount, decimal expected)
    {
        var account = new BankAccount(name, opening);
        account.Credit(amount);
        Assert.Equal(expected, account.Balance);
    }

    [Theory]
    [InlineData("John Doe", 1000, 200, 800)]
    [InlineData("Jane Smith", 500, 100, 400)]
    [InlineData("Alice Johnson", 300, 50, 250)]
    public void Withdraw_ValidAmount_UpdatesBalance(string name, decimal opening, decimal amount, decimal expected)
    {
        var account = new BankAccount(name, opening);
        account.Withdraw(amount);
        Assert.Equal(expected, account.Balance);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public void Debit_NonPositiveAmount_ThrowsArgumentOutOfRangeException(decimal amount)
    {
        var account = new BankAccount("Boundary Case", 100);
        Assert.Throws<ArgumentOutOfRangeException>(() => account.Debit(amount));
        Assert.Equal(100, account.Balance);
    }
}
