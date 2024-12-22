using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace WpfApp.Services
{
    public class UpdateTransactionService : BaseApiService
    {
        public UpdateTransactionService(string baseAddress) : base(baseAddress) { }

        public async Task ExecuteAsync(int id, Transaction transaction)
        {
            try
            {
                var response = await HttpClient.PutAsJsonAsync($"api/transactions/{id}", transaction);
                response.EnsureSuccessStatusCode();
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"Ошибка при обновлении транзакции с ID {id}: {ex.Message}");
                throw new Exception($"Не удалось обновить транзакцию с ID {id}. Проверьте подключение к серверу.", ex);
            }
        }
    }
}