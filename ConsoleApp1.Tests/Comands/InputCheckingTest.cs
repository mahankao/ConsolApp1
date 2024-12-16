using Microsoft.VisualStudio.TestTools.UnitTesting;
using ConsoleApp1.Comands;
using System;
using System.IO;
using System.Threading.Tasks;

namespace ConsoleApp1.Tests
{
    [TestClass]
    public class InputCheckingTests
    {
        private InputChecking _inputChecking;

        [TestInitialize]
        public void SetUp()
        {
            _inputChecking = new InputChecking();
        }
        

        [TestMethod]
        public async Task GetValidAmountAsync_ShouldRetryUntilValidInput()
        {
            // Arrange
            var input = "abc\n-50\n0\n200\n"; // Несколько неверных вводов перед правильным
            using var inputReader = new StringReader(input);
            Console.SetIn(inputReader);

            // Act
            var result = await _inputChecking.GetValidAmountAsync("Введите сумму:");

            // Assert
            Assert.AreEqual(200m, result, "Метод должен вернуть значение после нескольких попыток.");
        }

        [TestMethod]
        public async Task GetValidDateAsync_ShouldReturnValidDate_WhenInputIsCorrect()
        {
            // Arrange
            var input = "2024-01-01\n"; // Ввод пользователя
            using var inputReader = new StringReader(input);
            Console.SetIn(inputReader);

            // Act
            var result = await _inputChecking.GetValidDateAsync("Введите дату:");

            // Assert
            Assert.AreEqual(new DateTime(2024, 1, 1), result, "Метод должен вернуть корректную дату.");
        }
        
    }
}