namespace ConsoleApp1.Interfaces;

public interface ITrends
{
    void SetAnalysisPeriod(DateTime startDate, DateTime endDate);
    void Analysis(List<ITransaction> transactions);
}
