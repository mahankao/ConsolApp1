using ConsoleApp1.Comands;
using ConsoleApp1.Data;
using ConsoleApp1.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ConsoleApp1.Tests
{
    [TestClass]
    public class TrendAnalyzerTests
    {
        private Mock<IInputChecking> _mockChecking;
        private Mock<ITrends> _mockTrends;
        private Mock<ITransactionManager> _mockTransactionManager;
        private ITrendAnalyzer _trendAnalyzer;

        [TestInitialize]
        public void SetUp()
        {
            // Создаем моки для зависимостей
            _mockChecking = new Mock<IInputChecking>();
            _mockTrends = new Mock<ITrends>();
            _mockTransactionManager = new Mock<ITransactionManager>();
            _trendAnalyzer = new TrendAnalyzer(_mockChecking.Object, _mockTrends.Object);
        }
        
        [TestMethod]
        public async Task AnalyzeTrendsAsync_ShouldCallAnalysisWithValidTransactions()
        {
            // Arrange
            var startDate = new DateTime(2024, 01, 01);
            var endDate = new DateTime(2024, 12, 31);
            var transactions = new List<ITransaction>
            {
                new Income(new DateTime(2024, 06, 15), OperationType.Income, "Salary", 1500),
                new Expense(new DateTime(2024, 07, 01), OperationType.Expense, "Rent", 500)
            };

            // Настраиваем последовательное поведение моков
            _mockChecking.SetupSequence(c => c.GetValidDateAsync(It.IsAny<string>()))
                .ReturnsAsync(startDate)
                .ReturnsAsync(endDate);

            _mockTransactionManager.Setup(t => t.GetTransactionsByDateRangeAsync(startDate, endDate))
                .ReturnsAsync(transactions);

            // Act
            await _trendAnalyzer.AnalyzeTrendsAsync(_mockTransactionManager.Object);

            // Assert
            _mockTrends.Verify(t => t.SetAnalysisPeriod(startDate, endDate), Times.Once, "SetAnalysisPeriod не был вызван.");
            _mockTrends.Verify(t => t.Analysis(transactions), Times.Once, "Analysis не был вызван.");
        }
        

        [TestMethod]
        public async Task AnalyzeTrendsAsync_ShouldHandleInvalidUserInputException()
        {
            // Arrange
            _mockChecking.Setup(c => c.GetValidDateAsync(It.IsAny<string>()))
                .ThrowsAsync(new InvalidUserInputException("Некорректная дата"));

            // Act
            await _trendAnalyzer.AnalyzeTrendsAsync(_mockTransactionManager.Object);

            // Assert
            _mockTrends.Verify(t => t.SetAnalysisPeriod(It.IsAny<DateTime>(), It.IsAny<DateTime>()), Times.Never,
                "SetAnalysisPeriod был вызван, несмотря на исключение InvalidUserInputException.");
            _mockTrends.Verify(t => t.Analysis(It.IsAny<List<ITransaction>>()), Times.Never,
                "Analysis был вызван, несмотря на исключение InvalidUserInputException.");
        }

    }
}
