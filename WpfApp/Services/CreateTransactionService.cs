using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace WpfApp.Services
{
    public class CreateTransactionService : BaseApiService
    {
        public CreateTransactionService(string baseAddress) : base(baseAddress) { }

        public async Task ExecuteAsync(Transaction transaction)
        {
            try
            {
                var response = await HttpClient.PostAsJsonAsync("api/transactions", transaction);
                response.EnsureSuccessStatusCode();
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"Ошибка при создании транзакции: {ex.Message}");
                throw new Exception("Не удалось создать транзакцию. Проверьте подключение к серверу.", ex);
            }
        }
    }
}