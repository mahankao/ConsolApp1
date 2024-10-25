using ConsoleApp1.Data;
using ConsoleApp1.Interfaces;

namespace ConsoleApp1.Comands
{
    public class TransactionManager : ITransactionManager
    {
        private readonly IAction _actions; // Экземпляр для работы с транзакциями
        private readonly InputChecking _checking; // Проверка ввода
        private readonly ITransactionFileManager _transactionFileManager;

        public TransactionManager(IAction actions, InputChecking checking, ITransactionFileManager transactionFileManager)
        {
            _actions = actions;
            _checking = checking;
            _transactionFileManager = transactionFileManager;
        }
        

        public void AddIncome()
        {
            try
            {
                DateTime date = _checking.GetValidDate("Введите дату дохода (гггг-мм-дд): ");
                decimal amount = _checking.GetValidAmount("Введите сумму дохода (положительное число): ");
                
                Console.WriteLine("Введите категорию дохода: ");
                string category = Console.ReadLine();

                _actions.AddTransaction(new Transaction(date, OperationType.Income, category, amount));
                Console.WriteLine("Доход добавлен успешно.");
            }
            catch (InvalidUserInputException ex)
            {
                Console.WriteLine($"Ошибка: {ex.Message}. Повторите попытку.");
            }
        }

        public void AddExpense()
        {
            try
            {
                DateTime date = _checking.GetValidDate("Введите дату расхода (гггг-мм-дд): ");
                decimal amount = _checking.GetValidAmount("Введите сумму расхода (положительное число): ");

                Console.WriteLine("Введите категорию расхода: ");
                string category = Console.ReadLine();

                _actions.AddTransaction(new Transaction(date, OperationType.Expense, category, amount));
                Console.WriteLine("Расход добавлен успешно.");
            }
            catch (InvalidUserInputException ex)
            {
                Console.WriteLine($"Ошибка: {ex.Message}. Повторите попытку.");
            }
        }

        public void GetBalance()
        {
            Console.WriteLine($"Ваш баланс: {_actions.Balance()}");
        }

        public List<ITransaction> GetTransactionsByDateRange(DateTime startDay, DateTime endDay)
        {
            return _actions.GetTransactionsByDateRange(startDay, endDay);
        }

        public async Task LoadTransactionsAsync(string filePath)
        {
            await _transactionFileManager.LoadTransactionsFromFileAsync(filePath);
        }

        public async Task SaveTransactionsAsync(string filePath)
        {
            await _transactionFileManager.SaveTransactionsToFileAsync(filePath);
        }
    }
}
