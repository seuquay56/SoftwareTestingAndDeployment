namespace BankAccountApp;

/// <summary>A small account model used to demonstrate unit testing.</summary>
public class BankAccount
{
    public string CustomerName { get; }
    public decimal Balance { get; private set; }

    public BankAccount(string customerName, decimal initialBalance)
    {
        if (string.IsNullOrWhiteSpace(customerName))
            throw new ArgumentException("Customer name is required.", nameof(customerName));
        if (initialBalance < 0)
            throw new ArgumentOutOfRangeException(nameof(initialBalance), "Initial balance cannot be negative.");

        CustomerName = customerName;
        Balance = initialBalance;
    }

    public void Debit(decimal amount)
    {
        ValidateAmount(amount);
        if (amount > Balance)
            throw new InvalidOperationException("Insufficient funds");

        Balance -= amount;
    }

    public void Credit(decimal amount)
    {
        ValidateAmount(amount);
        Balance += amount;
    }

    public void Withdraw(decimal amount) => Debit(amount);

    private static void ValidateAmount(decimal amount)
    {
        if (amount <= 0)
            throw new ArgumentOutOfRangeException(nameof(amount), "Transaction amount must be greater than zero.");
    }
}
