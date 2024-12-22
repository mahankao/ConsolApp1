using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace WpfApp.Services
{
    public class DeleteTransactionService : BaseApiService
    {
        public DeleteTransactionService(string baseAddress) : base(baseAddress) { }

        public async Task ExecuteAsync(int id)
        {
            try
            {
                var response = await HttpClient.DeleteAsync($"api/transactions/{id}");
                response.EnsureSuccessStatusCode();
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"Ошибка при удалении транзакции с ID {id}: {ex.Message}");
                throw new Exception($"Не удалось удалить транзакцию с ID {id}. Проверьте подключение к серверу.", ex);
            }
        }
    }
}