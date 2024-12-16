using System.Collections.Generic;
using ConsoleApp1.Interfaces;

namespace ConsoleApp1.Data
{
    public class TransactionList
    {
        private readonly List<ITransaction> _transactions = new List<ITransaction>();

        public void AddTransaction(ITransaction transaction)
        {
            _transactions.Add(transaction);
        }

        public List<ITransaction> GetAllTransactions()
        {
            return _transactions;
        }

        public void AddTransactions(IEnumerable<ITransaction> transactions)
        {
            _transactions.AddRange(transactions);
        }
    }
}