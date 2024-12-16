namespace ConsoleApp1.Interfaces
{
    public interface ITransactionManager
    {
        Task AddIncomeAsync(); // Добавить доход
        Task AddExpenseAsync(); // Добавить расход
        Task GetBalanceAsync(); // Получить текущий баланс
        Task<List<ITransaction>> GetTransactionsByDateRangeAsync(DateTime startDay, DateTime endDay); // Асинхронно получить транзакции по диапазону дат
    }
}