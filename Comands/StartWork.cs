using ConsoleApp1.Comands;
using ConsoleApp1.Data;
using ConsoleApp1.Interfaces;
using System;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public class StartWork
    {
        private readonly string _filePath;
        private readonly TransactionManager _transactionManager;
        private readonly Menu _menu;

        public StartWork(string filePath)
        {
            _filePath = filePath;
    
            // Инициализация всех необходимых компонентов
            var transactionList = new TransactionList();
            IAction actions = new Actions(transactionList);
            var checking = new InputChecking();
            ITransactionFileManager transactionFileManager = new TransactionFileManager(transactionList);

            // Создание TransactionManager и Menu
            _transactionManager = new TransactionManager(actions, checking, transactionFileManager);
            var trendAnalyzer = new TrendAnalyzer();
            _menu = new Menu(_transactionManager, trendAnalyzer);
        }


        public async Task RunAsync()
        {
            // Загрузка транзакций
            await LoadTransactionsAsync();

            // Основной цикл работы с меню
            bool continueRunning = true;
            while (continueRunning)
            {
                _menu.WriteMenu();
                continueRunning = _menu.ChoseAction();  // Ожидается, что метод вернет false при выходе
            }

            // Сохранение транзакций
            await SaveTransactionsAsync();
        }

        private async Task LoadTransactionsAsync()
        {
            try
            {
                await _transactionManager.LoadTransactionsAsync(_filePath);
                Console.WriteLine("Транзакции успешно загружены.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка загрузки транзакций: {ex.Message}");
            }
        }

        private async Task SaveTransactionsAsync()
        {
            try
            {
                await _transactionManager.SaveTransactionsAsync(_filePath);
                Console.WriteLine("Транзакции успешно сохранены.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка сохранения транзакций: {ex.Message}");
            }
        }
    }
}
