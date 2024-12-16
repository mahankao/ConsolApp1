using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ConsoleApp1.Database;
using ConsoleApp1.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ConsoleApp1
{
    public class Actions : IAction
    {
        private readonly ApplicationDbContext _context; //dbcontextfactory чтобы обновлялось

        public Actions(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddTransactionAsync(ITransaction transaction)
        {
            await _context.Transactions.AddAsync((Transaction)transaction);
            await _context.SaveChangesAsync();
        }

        public async Task<decimal> GetTotalIncomeAsync()
        {
            return await _context.Transactions
                .Where(t => t.Type == OperationType.Income)
                .SumAsync(t => t.Amount);
        }

        public async Task<decimal> GetTotalExpenseAsync()
        {
            return await _context.Transactions
                .Where(t => t.Type == OperationType.Expense)
                .SumAsync(t => t.Amount);
        }

        public async Task<decimal> BalanceAsync()
        {
            return await GetTotalIncomeAsync() - await GetTotalExpenseAsync();
        }

        public async Task<List<ITransaction>> GetTransactionsByDateRangeAsync(DateTime startDay, DateTime endDay)
        {
            var transactions = await _context.Transactions
                .Where(t => t.Date >= startDay && t.Date <= endDay)
                .ToListAsync();

            return transactions.Cast<ITransaction>().ToList();
        }
        
    }
}