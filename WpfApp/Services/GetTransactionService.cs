using System.Net.Http;
using System.Net.Http.Json;
using WpfApp.Services;

namespace WpfApp1.Services
{


    namespace WpfApp.Services
    {
        public class GetTransactionService :  BaseApiService
        {
            public GetTransactionService(string baseAddress) : base(baseAddress)
            {
            }

            public async Task<Transaction?> ExecuteAsync(int id)
            {
                try
                {
                    return await HttpClient.GetFromJsonAsync<Transaction>($"api/transactions/{id}");
                }
                catch (HttpRequestException ex)
                {
                    Console.WriteLine($"Ошибка при получении транзакции с ID {id}: {ex.Message}");
                    throw new Exception($"Не удалось получить транзакцию с ID {id}. Проверьте подключение к серверу.",
                        ex);
                }
            }
        }
    }
}