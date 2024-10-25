namespace ConsoleApp1.Interfaces;

public interface ITrendAnalyzer
{
    void AnalyzeTrends(ITransactionManager transactionManager); // Измените тип параметра на интерфейс
}