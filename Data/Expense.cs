using ConsoleApp1.Interfaces;

namespace ConsoleApp1.Data;

public record Expense : Transaction
{
    public Expense(DateTime date, OperationType type, string category, decimal amount) : base(date, OperationType.Expense, category, amount)
    {
    }
}