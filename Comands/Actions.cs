using ConsoleApp1.Data;
using ConsoleApp1.Interfaces;

namespace ConsoleApp1.Comands
{
    public class Actions : IAction
    {
        private readonly TransactionList _transactionList;

        public Actions(TransactionList transactionList)
        {
            _transactionList = transactionList;
        }

        public void AddTransaction(ITransaction transaction)
        {
            _transactionList.AddTransaction(transaction);
        }

        public decimal GetTotalIncome()
        {
            return _transactionList.GetAllTransactions()
                .Where(t => t.Type == OperationType.Income)
                .Sum(t => t.Amount);
        }

        public decimal GetTotalExpense()
        {
            return _transactionList.GetAllTransactions()
                .Where(t => t.Type == OperationType.Expense)
                .Sum(t => t.Amount);
        }

        public decimal Balance()
        {
            return GetTotalIncome() - GetTotalExpense();
        }

        public List<ITransaction> GetTransactionsByDateRange(DateTime startDay, DateTime endDay)
        {
            return _transactionList.GetAllTransactions()
                .Where(t => t.Date >= startDay && t.Date <= endDay)
                .ToList();
        }
        
    }
}