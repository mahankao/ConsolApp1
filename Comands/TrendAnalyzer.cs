using ConsoleApp1.Data;
using ConsoleApp1.Interfaces;

namespace ConsoleApp1.Comands
{
    public class TrendAnalyzer : ITrendAnalyzer
    {
        private readonly Trends _trends = new Trends();
        private readonly InputChecking _checking = new InputChecking();

        public void AnalyzeTrends(ITransactionManager transactionManager)
        {
            try
            {
                var startDay = _checking.GetValidDate("Введите начальную дату для анализа (гггг-мм-дд): ");
                var endDay = _checking.GetValidDate("Введите конечную дату для анализа (гггг-мм-дд): ");

                if (startDay > endDay)
                {
                    throw new InvalidDateRangeException("Начальная дата не может быть больше конечной даты.");
                }

                _trends.SetAnalysisPeriod(startDay, endDay);

                // Вызов метода через интерфейс
                var transactions = transactionManager.GetTransactionsByDateRange(startDay, endDay);

                _trends.Analysis(transactions);
            }
            catch (InvalidUserInputException ex)
            {
                Console.WriteLine($"Ошибка: {ex.Message}. Повторите попытку.");
            }
            catch (InvalidDateRangeException ex)
            {
                Console.WriteLine($"Ошибка диапазона дат: {ex.Message}. Повторите попытку.");
            }
        }
    }
}