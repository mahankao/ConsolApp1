using ConsoleApp1.Interfaces;
using System;
using System.Threading.Tasks;

namespace ConsoleApp1.Data
{
    public class Menu
    {
        private readonly ITransactionManager _transactionManager;
        private readonly ITrendAnalyzer _trendAnalyzer;

        public Menu(ITransactionManager transactionManager, ITrendAnalyzer trendAnalyzer)
        {
            _transactionManager = transactionManager;
            _trendAnalyzer = trendAnalyzer;
        }

        public void WriteMenu()
        {
            Console.WriteLine("Выберите операцию из списка:");
            Console.WriteLine("\t1 - Добавить доход");
            Console.WriteLine("\t2 - Добавить расход");
            Console.WriteLine("\t3 - Посмотреть текущий баланс");
            Console.WriteLine("\t4 - Анализ трендов");
            Console.WriteLine("\t5 - Выход из программы");
            Console.Write("Ваш выбор операции: ");
        }

        // Сделаем метод асинхронным, потому что вызываем асинхронные методы
        public async Task<bool> ChoseActionAsync()
        {
            var choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    await _transactionManager.AddIncomeAsync();  // Сделали асинхронным
                    return true;
                case "2":
                    await _transactionManager.AddExpenseAsync();  // Сделали асинхронным
                    return true;
                case "3":
                    await _transactionManager.GetBalanceAsync();  // Сделали асинхронным
                    return true;
                case "4":
                    await _trendAnalyzer.AnalyzeTrendsAsync(_transactionManager);  // Сделали асинхронным
                    return true;
                case "5":
                    return false; // Выход из программы
                default:
                    Console.WriteLine("Неверный выбор. Попробуйте снова.");
                    return true;
            }
        }
    }
}
