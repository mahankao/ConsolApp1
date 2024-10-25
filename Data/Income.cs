using ConsoleApp1.Interfaces;

namespace ConsoleApp1.Data;
public record Income : Transaction
{
    public Income(DateTime date, OperationType type, string category, int amount) : base(date, OperationType.Income, category, amount)
    {
    }

}