using ConsoleApp1.Data;
using ConsoleApp1.Interfaces;
using System;
using System.Threading.Tasks;
using ConsoleApp1.Database; // Подключаем пространство имен для работы с асинхронностью

namespace ConsoleApp1.Comands
{
    public class TrendAnalyzer : ITrendAnalyzer
    {
        private readonly ITrends _trends;
        private readonly IInputChecking _checking;
        public TrendAnalyzer(IInputChecking checking, ITrends trends)
        {
            _trends = trends;
            _checking = checking;
        }
        
        public async Task AnalyzeTrendsAsync(ITransactionManager transactionManager)
        {
            try
            {
                var startDay = await _checking.GetValidDateAsync("Введите начальную дату для анализа (гггг-мм-дд): ");
                var endDay = await _checking.GetValidDateAsync("Введите конечную дату для анализа (гггг-мм-дд): ");

                if (startDay > endDay)
                {
                    throw new InvalidDateRangeException("Начальная дата не может быть больше конечной даты.");
                }
                
                _trends.SetAnalysisPeriod(startDay, endDay);
                
                var transactions = await transactionManager.GetTransactionsByDateRangeAsync(startDay, endDay);
                
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