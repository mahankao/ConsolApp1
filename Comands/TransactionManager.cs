using ConsoleApp1.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ConsoleApp1.Data;

namespace ConsoleApp1.Comands
{
    public class TransactionManager : ITransactionManager
    {
        private readonly IAction _actions;
        private readonly IInputChecking _checking;

        public TransactionManager(IAction actions, IInputChecking checking)
        {
            _actions = actions;
            _checking = checking;
        }
        public async Task AddIncomeAsync()
        {
            try
            {
                DateTime date = await _checking.GetValidDateAsync("Введите дату дохода (гггг-мм-дд): ");
                decimal amount = await _checking.GetValidAmountAsync("Введите сумму дохода (положительное число): ");

                Console.WriteLine("Введите категорию дохода: ");
                string category = Console.ReadLine();

                var income = new Income(date, OperationType.Income, category, amount);
                await _actions.AddTransactionAsync(income); 
                Console.WriteLine("Доход добавлен успешно.");
            }
            catch (InvalidUserInputException ex)
            {
                Console.WriteLine($"Ошибка: {ex.Message}. Повторите попытку.");
            }
        }
        
        public async Task AddExpenseAsync()
        {
            try
            {
                DateTime date = await _checking.GetValidDateAsync("Введите дату расхода (гггг-мм-дд): ");
                decimal amount = await _checking.GetValidAmountAsync("Введите сумму расхода (положительное число): ");

                Console.WriteLine("Введите категорию расхода: ");
                string category = Console.ReadLine();

                var expense = new Expense(date, OperationType.Expense, category, amount);
                await _actions.AddTransactionAsync(expense);
                Console.WriteLine("Расход добавлен успешно.");
            }
            catch (InvalidUserInputException ex)
            {
                Console.WriteLine($"Ошибка: {ex.Message}. Повторите попытку.");
            }
        }
        
        public async Task GetBalanceAsync()
        {
            decimal balance = await _actions.BalanceAsync();
            Console.WriteLine($"Ваш баланс: {balance:F2}");
        }
        
        public async Task<List<ITransaction>> GetTransactionsByDateRangeAsync(DateTime startDay, DateTime endDay)
        {
            return await _actions.GetTransactionsByDateRangeAsync(startDay, endDay);
        }
    }
} 