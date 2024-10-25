namespace ConsoleApp1.Interfaces;

public interface ITransactionManager
{
    void AddIncome(); // Добавить доход
    void AddExpense(); // Добавить расход
    void GetBalance(); // Получить текущий баланс
    List<ITransaction> GetTransactionsByDateRange(DateTime startDay, DateTime endDay);
}