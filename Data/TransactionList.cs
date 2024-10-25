using ConsoleApp1.Interfaces;

namespace ConsoleApp1.Data;

public class TransactionList
{
    private List<ITransaction> transactions = new List<ITransaction>();

    public void AddTransaction(ITransaction transaction)
    {
        transactions.Add(transaction);
    }

    public void AddTransactions(IEnumerable<ITransaction> transactions)
    {
        this.transactions.AddRange(transactions);
    }

    public IEnumerable<ITransaction> GetAllTransactions() => transactions;

    public void PrintTransactions()
    {
        foreach (var el in transactions)
        {
            Console.WriteLine(el);
        }
    }
}
