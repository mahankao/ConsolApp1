using ConsoleApp1.Interfaces;

public class Transaction : ITransaction
{
    public int Id { get; set; }
    public DateTime Date { get; set; }
    public OperationType Type { get; set; }
    public string Category { get; set; }
    public decimal Amount { get; set; }

    // Параметрless конструктор, необходимый для EF
    public Transaction() {}

    // Конструктор с параметрами
    public Transaction(DateTime date, OperationType type, string category, decimal amount)
    {
        Date = date;
        Type = type;
        Category = category;
        Amount = amount;
    }
}