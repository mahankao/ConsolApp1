using ConsoleApp1.Interfaces;

namespace ConsoleApp1.Data;

public record Transaction : ITransaction
{
    public DateTime Date { get; }
    public OperationType Type { get; }
    public string Category { get; }
    public decimal Amount { get; }

    public Transaction(DateTime date, OperationType type, string category, decimal amount)
    {
        Date = date;
        Type = type;
        Category = category;
        Amount = amount;
    }
}