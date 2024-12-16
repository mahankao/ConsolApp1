using System.ComponentModel.DataAnnotations.Schema;
using ConsoleApp1.Interfaces;

namespace ConsoleApp1.Data;
public class Income : Transaction
{
    public Income(DateTime date, OperationType income, string category, decimal amount) : base(date, OperationType.Income, category, amount)
    {
    }
    public Income(){}
}
