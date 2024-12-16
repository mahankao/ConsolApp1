namespace ConsoleApp1.Interfaces
{
    public interface IInputChecking
    {
        Task<decimal> GetValidAmountAsync(string userInput);  // Асинхронный метод для получения суммы
        Task<DateTime> GetValidDateAsync(string words);  // Асинхронный метод для получения даты
    }
}