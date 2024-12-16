namespace ConsoleApp1.Interfaces
{
    public interface ITrendAnalyzer
    {
        Task AnalyzeTrendsAsync(ITransactionManager transactionManager); // Изменен на асинхронный
    }
}