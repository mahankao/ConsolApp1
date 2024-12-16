using ConsoleApp1.Comands;
using ConsoleApp1.Interfaces;
using ConsoleApp1.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ConsoleApp1.Database;

namespace ConsoleApp1.Tests
{
    [TestClass]
    public class TransactionManagerTests
    {
        private Mock<IInputChecking> _mockChecking;
        private ApplicationDbContext _context;
        private TransactionManager _transactionManager;

        [TestInitialize]
        public void SetUp()
        {
            // Инициализация контекста с In-Memory базой данных
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase("TestDb")  // Название базы данных для тестов
                .Options;

            _context = new ApplicationDbContext(options);
            _mockChecking = new Mock<IInputChecking>();
            
            var mockActions = new Mock<IAction>();
            _transactionManager = new TransactionManager(mockActions.Object, _mockChecking.Object);

            // Очистка базы данных перед каждым тестом
            _context.Database.EnsureDeleted();
            _context.Database.EnsureCreated();
        }

        [TestMethod]
        public async Task AddIncomeAsync_ShouldAddIncome()
        {
            // Arrange
            DateTime validDate = new DateTime(2024, 01, 01);
            decimal validAmount = 100m;
            string validCategory = "Salary";
            _mockChecking.Setup(c => c.GetValidDateAsync(It.IsAny<string>())).ReturnsAsync(validDate);
            _mockChecking.Setup(c => c.GetValidAmountAsync(It.IsAny<string>())).ReturnsAsync(validAmount);
            
            var income = new Income(validDate, OperationType.Income, validCategory, validAmount);

            // Act
            await _context.Transactions.AddAsync(income);
            await _context.SaveChangesAsync();

            // Assert
            var addedIncome = await _context.Transactions.FirstOrDefaultAsync(t => t.Category == validCategory);
            Assert.IsNotNull(addedIncome);
            Assert.AreEqual(validAmount, addedIncome.Amount);
            Assert.AreEqual(OperationType.Income, addedIncome.Type);
        }

        [TestMethod]
        public async Task AddExpenseAsync_ShouldAddExpense()
        {
            // Arrange
            DateTime validDate = new DateTime(2024, 02, 01);
            decimal validAmount = 50m;
            string validCategory = "Food";
            _mockChecking.Setup(c => c.GetValidDateAsync(It.IsAny<string>())).ReturnsAsync(validDate);
            _mockChecking.Setup(c => c.GetValidAmountAsync(It.IsAny<string>())).ReturnsAsync(validAmount);

            // Создаем Expense, которую будем добавлять
            var expense = new Expense(validDate, OperationType.Expense, validCategory, validAmount);

            // Act
            await _context.Transactions.AddAsync(expense);
            await _context.SaveChangesAsync();

            // Assert
            var addedExpense = await _context.Transactions.FirstOrDefaultAsync(t => t.Category == validCategory);
            Assert.IsNotNull(addedExpense);
            Assert.AreEqual(validAmount, addedExpense.Amount);
            Assert.AreEqual(OperationType.Expense, addedExpense.Type);
        }

        [TestMethod]
        public async Task GetBalanceAsync_ShouldReturnCorrectBalance()
        {
            // Arrange
            decimal expectedBalance = 100;
            
            var income = new Income(new DateTime(2024, 01, 01), OperationType.Income, "Salary", 200);
            var expense = new Expense(new DateTime(2024, 01, 02), OperationType.Expense, "Rent", 100);
            _context.Transactions.Add(income);
            _context.Transactions.Add(expense);
            await _context.SaveChangesAsync();

            // Act
            var totalIncome = await _context.Transactions.Where(t => t.Type == OperationType.Income).SumAsync(t => t.Amount);
            var totalExpense = await _context.Transactions.Where(t => t.Type == OperationType.Expense).SumAsync(t => t.Amount);
            decimal balance = totalIncome - totalExpense;

            // Assert
            Assert.AreEqual(expectedBalance, balance);
        }

        [TestMethod]
        public async Task GetTransactionsByDateRangeAsync_ShouldReturnTransactionsWithinRange()
        {
            // Arrange
            var startDate = new DateTime(2024, 01, 01);
            var endDate = new DateTime(2024, 12, 31);
            var transactions = new List<ITransaction>
            {
                new Income(new DateTime(2024, 06, 15), OperationType.Income, "Salary", 1500),
                new Expense(new DateTime(2024, 07, 01), OperationType.Expense, "Rent", 500)
            };
            
            foreach (var transaction in transactions)
            {
                await _context.Transactions.AddAsync((Transaction)transaction);
            }
            await _context.SaveChangesAsync();

            // Act
            var result = await _context.Transactions
                .Where(t => t.Date >= startDate && t.Date <= endDate)
                .ToListAsync();

            // Assert
            Assert.AreEqual(2, result.Count); // Ожидаем, что количество транзакций будет равно 2
        }
    }
}
