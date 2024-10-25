namespace ConsoleApp1.Interfaces
{
    public interface IAction
    {
        void AddTransaction(ITransaction transaction);
        decimal GetTotalIncome();
        decimal GetTotalExpense();
        decimal Balance();
        List<ITransaction> GetTransactionsByDateRange(DateTime startDay, DateTime endDay);
    }
}
