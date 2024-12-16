using ConsoleApp1.Interfaces;

namespace ConsoleApp1.Data
{
    public class Trends : ITrends
    {
        public DateTime startDay { get; set; }
        public DateTime endDay { get; set; }

        public Trends()
        {
        }
        
        public void SetAnalysisPeriod(DateTime startDay, DateTime endDay)
        {
            this.startDay = startDay;
            this.endDay = endDay;
        }
        
        public void Analysis(List<ITransaction> transactions)
        {
            var incomes = transactions.Where(t => t.Type == OperationType.Income).ToList();
            var expenses = transactions.Where(t => t.Type == OperationType.Expense).ToList();
            
            decimal totalIncome = incomes.Sum(t => t.Amount);
            decimal totalExpense = expenses.Sum(t => t.Amount);
            
            Console.WriteLine($"Анализ за период с {startDay.ToShortDateString()} по {endDay.ToShortDateString()}:");
            Console.WriteLine($"Общий доход: {totalIncome:F2}");
            Console.WriteLine($"Общий расход: {totalExpense:F2}");
            Console.WriteLine($"Чистый баланс: {(totalIncome - totalExpense):F2}");
        }
    }
}