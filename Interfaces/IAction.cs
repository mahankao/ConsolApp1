using System.Collections.Generic;
using System.Threading.Tasks;

namespace ConsoleApp1.Interfaces
{
    public interface IAction
    {
        Task AddTransactionAsync(ITransaction transaction);
        Task<decimal> BalanceAsync();
        Task<List<ITransaction>> GetTransactionsByDateRangeAsync(DateTime startDay, DateTime endDay);
    }
}