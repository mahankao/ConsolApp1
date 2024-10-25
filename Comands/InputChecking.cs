namespace ConsoleApp1.Comands
{
    public class InputChecking
    {
        public int GetValidAmount( string userInput)
        {
            int amount;
            while (true)
            {
                Console.WriteLine(userInput);
                string input = Console.ReadLine();

                if (!int.TryParse(input, out amount) || amount <= 0)
                {
                    Console.WriteLine("Пожалуйста, введите корректную сумму (больше 0).");
                }
                else
                {
                    return amount;
                }
            }
        }

        public DateTime GetValidDate(string words)
        {
            while (true)
            {
                Console.WriteLine(words);
                string input = Console.ReadLine();

                if (DateTime.TryParse(input, out DateTime date))
                {
                    return date;
                }
                Console.WriteLine("Ошибка: Неверный формат даты. Повторите ввод.");
            }
        }
    }
}