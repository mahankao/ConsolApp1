namespace ConsoleApp1.Interfaces;

public interface ITransaction
{
    DateTime Date { get; }
    OperationType Type { get; }
    string Category { get; }
    decimal Amount { get; }
    int Id { get; set; }
}

public enum OperationType
{
    Income,
    Expense,
}