using System.Text.Json;
using ConsoleApp1.Data;
using ConsoleApp1.Interfaces;

namespace ConsoleApp1.Comands
{
    public class TransactionFileManager : ITransactionFileManager
    {
        private readonly TransactionList _transactionList;

        public TransactionFileManager(TransactionList transactionList)
        {
            _transactionList = transactionList;
        }

        public async Task SaveTransactionsToFileAsync(string filePath)
        {
            var transactions = _transactionList.GetAllTransactions();
            var json = JsonSerializer.Serialize(transactions, new JsonSerializerOptions { WriteIndented = true });
            await File.WriteAllTextAsync(filePath, json);
            Console.WriteLine("Транзакции сохранены в файл.");
        }

        public async Task LoadTransactionsFromFileAsync(string filePath)
        {
            if (File.Exists(filePath))
            {
                try
                {
                    var json = await File.ReadAllTextAsync(filePath);
                    var transactions = JsonSerializer.Deserialize<List<Transaction>>(json);
                    if (transactions != null)
                    {
                        _transactionList.AddTransactions(transactions);
                        Console.WriteLine("Транзакции загружены из файла.");
                    }
                }
                catch (JsonException ex)
                {
                    Console.WriteLine($"Ошибка при чтении данных из файла: {ex.Message}");
                }
            }
            else
            {
                Console.WriteLine("Файл с транзакциями не найден.");
            }
        }
    }
}