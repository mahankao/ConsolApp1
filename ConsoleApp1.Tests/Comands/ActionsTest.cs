using System;
using System.Linq;
using System.Threading.Tasks;
using ConsoleApp1;
using ConsoleApp1.Database;
using ConsoleApp1.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ConsoleApp1.Tests
{
    [TestClass]
    public class ActionsTest
    {
        private DbContextOptions<ApplicationDbContext> _options;
        private ApplicationDbContext _context;
        private Actions _actions;
        [TestInitialize]
        public void SetUp()
        {
            _options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase("TestDb")  
                .Options;

            _context = new ApplicationDbContext(_options); 
            _actions = new Actions(_context);
        }

        [TestMethod]
        public async Task AddTransactionAsync_ShouldAddTransaction()
        {
            // Arrange
            var transaction = new Transaction
            {
                Amount = 100,
                Type = OperationType.Income,
                Date = DateTime.Now
            };

            // Act
            await _actions.AddTransactionAsync(transaction);

            // Assert
            var addedTransaction = await _context.Transactions.FirstOrDefaultAsync();
            Assert.IsNotNull(addedTransaction); 
            Assert.AreEqual(transaction.Amount, addedTransaction.Amount);
            Assert.AreEqual(transaction.Type, addedTransaction.Type);
        }

        [TestMethod]
        public async Task GetTotalIncomeAsync_ShouldReturnCorrectTotalIncome()
        {
            // Arrange
            var transactions = new[]
            {
                new Transaction { Amount = 100, Type = OperationType.Income, Date = DateTime.Now },
                new Transaction { Amount = 200, Type = OperationType.Income, Date = DateTime.Now },
                new Transaction { Amount = 50, Type = OperationType.Expense, Date = DateTime.Now },
            };
            
            await _context.Transactions.AddRangeAsync(transactions);
            await _context.SaveChangesAsync();

            // Act
            var result = await _actions.GetTotalIncomeAsync();

            // Assert
            Assert.AreEqual(300, result); 
        }

        [TestMethod]
        public async Task GetTotalExpenseAsync_ShouldReturnCorrectTotalExpense()
        {
            // Arrange
            var transactions = new[]
            {
                new Transaction { Amount = 100, Type = OperationType.Income, Date = DateTime.Now },
                new Transaction { Amount = 200, Type = OperationType.Expense, Date = DateTime.Now },
                new Transaction { Amount = 50, Type = OperationType.Expense, Date = DateTime.Now },
            };

            await _context.Transactions.AddRangeAsync(transactions);
            await _context.SaveChangesAsync();

            // Act
            var result = await _actions.GetTotalExpenseAsync();

            // Assert
            Assert.AreEqual(250, result); 
        }

        [TestMethod]
        public async Task BalanceAsync_ShouldReturnCorrectBalance()
        {
            // Arrange
            var transactions = new[]
            {
                new Transaction { Amount = 100, Type = OperationType.Income, Date = DateTime.Now },
                new Transaction { Amount = 200, Type = OperationType.Expense, Date = DateTime.Now },
                new Transaction { Amount = 50, Type = OperationType.Expense, Date = DateTime.Now },
            };
            
            await _context.Transactions.AddRangeAsync(transactions);
            await _context.SaveChangesAsync();

            // Act
            var result = await _actions.BalanceAsync();

            // Assert
            Assert.AreEqual(-150, result);
        }

        [TestMethod]
        public async Task GetTransactionsByDateRangeAsync_ShouldReturnCorrectTransactions()
        {
            // Arrange
            var startDate = new DateTime(2024, 1, 1);
            var endDate = new DateTime(2024, 12, 31);
            var transactions = new[]
            {
                new Transaction { Date = new DateTime(2024, 5, 1), Amount = 100, Type = OperationType.Income },
                new Transaction { Date = new DateTime(2024, 6, 15), Amount = 200, Type = OperationType.Expense },
                new Transaction { Date = new DateTime(2024, 7, 20), Amount = 50, Type = OperationType.Income },
            };

            await _context.Transactions.AddRangeAsync(transactions);
            await _context.SaveChangesAsync();

            // Act
            var result = await _actions.GetTransactionsByDateRangeAsync(startDate, endDate);

            // Assert
            Assert.AreEqual(3, result.Count); 
        }
        [TestCleanup]
        public void Cleanup()
        {
            _context.Database.EnsureDeleted();
        }
    }
}
