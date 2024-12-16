using System.ComponentModel.DataAnnotations.Schema;
using ConsoleApp1.Interfaces;

namespace ConsoleApp1.Data;
public class Expense : Transaction
{
    
    public Expense(DateTime date, OperationType expense, string category, decimal amount)
        : base(date, OperationType.Expense, category, amount)
    {
    }
    public Expense(){}
    
}
