using ConsoleApp1.Comands;
using ConsoleApp1.Database;
using ConsoleApp1.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.IO;
using System.Threading.Tasks;
using ConsoleApp1.Data;

namespace ConsoleApp1
{
    class Program
    {
        static async Task Main(string[] args)
        {
            try
            {
                // Настройка Dependency Injection 
                var serviceProvider = Startup.ConfigureServices();

                // Получаем экземпляр Menu из DI контейнера
                var menu = serviceProvider.GetRequiredService<Menu>();

                // Основной цикл работы приложения
                bool continueRunning = true;
                while (continueRunning)
                {
                    menu.WriteMenu();
                    continueRunning = await menu.ChoseActionAsync();
                }

                Console.WriteLine("Выход из программы.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка: {ex.Message}\n{ex.StackTrace}");  // Вывод стека ошибки
            }
        }
    }
}