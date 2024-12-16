using ConsoleApp1.Interfaces;
using System;
using System.Threading.Tasks;

namespace ConsoleApp1.Comands
{
        public class InputChecking : IInputChecking
        {
            public async Task<decimal> GetValidAmountAsync(string prompt)
            {
                decimal amount;
                while (true)
                {
                    Console.WriteLine(prompt);
                    string input = await Task.Run(() => Console.ReadLine());
                    if (decimal.TryParse(input, out amount) && amount > 0)
                    {
                        return amount; // Возвращаем корректную сумму
                    }
                    Console.WriteLine("Пожалуйста, введите корректную сумму (число больше 0).");
                }
            }

            public async Task<DateTime> GetValidDateAsync(string prompt)
            {
                while (true)
                {
                    Console.WriteLine(prompt);
                    string input = await Task.Run(() => Console.ReadLine());

                    if (DateTime.TryParse(input, out DateTime date))
                    {
                        return date;
                    }
                    Console.WriteLine("Ошибка: Неверный формат даты. Повторите ввод.");
                }
            }
        }
}

