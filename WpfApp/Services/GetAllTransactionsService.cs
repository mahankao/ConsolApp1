using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;

namespace WpfApp.Services
{
    public class GetAllTransactionsService : BaseApiService
    {
        public GetAllTransactionsService(string baseAddress) : base(baseAddress) { }

        public async Task<List<Transaction>> ExecuteAsync()
        {
            try
            {
                var response = await HttpClient.GetAsync("api/transactions");

                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception($"API returned error: {response.StatusCode}");
                }

                var json = await response.Content.ReadAsStringAsync();

                return JsonSerializer.Deserialize<List<Transaction>>(json, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                }) ?? new List<Transaction>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in ExecuteAsync: {ex.Message}");
                throw; // Или обработайте ошибку
            }
        }

    }
}