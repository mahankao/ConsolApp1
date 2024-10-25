namespace ConsoleApp1
{
    class Program
    {
        static async Task Main()
        {
            const string filePath = "transactions.json";
            
            var startWork = new StartWork(filePath);
            await startWork.RunAsync();
            
            Console.WriteLine("Программа завершена.");
        }
    }
}