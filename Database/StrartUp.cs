using Microsoft.Extensions.DependencyInjection;
using ConsoleApp1.Database;
using ConsoleApp1.Interfaces;
using ConsoleApp1.Comands;
using ConsoleApp1.Data;

namespace ConsoleApp1
{
    public class Startup
    {
        public static ServiceProvider ConfigureServices()
        {
            // Создаём DI контейнер
            var services = new ServiceCollection();

            // Регистрация контекста базы данных
            services.AddDbContext<ApplicationDbContext>();

            // Регистрация сервисов и зависимостей
            services.AddScoped<ITransactionManager, TransactionManager>();
            services.AddScoped<IAction, Actions>();
            services.AddScoped<IInputChecking, InputChecking>();
            services.AddScoped<ITrends, Trends>();
            services.AddScoped<ITrendAnalyzer, TrendAnalyzer>();
            services.AddScoped<Menu>();

            return services.BuildServiceProvider(); // Создаём и возвращаем сервис-поставщика
        }
    }
}