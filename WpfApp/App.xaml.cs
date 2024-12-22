using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using ConsoleApp1.Database; // Путь к ApplicationDbContext
using System;
using System.IO;
using System.Windows;
using WpfApp.Services;
using WpfApp.ViewModels;
using WpfApp.Views;
using WpfApp1.Services.WpfApp.Services;

namespace WpfApp
{
    public partial class App : Application
    {
        public static ServiceProvider ServiceProvider { get; private set; }

        public App()
        {
            var services = new ServiceCollection();

            // Настройка конфигурации
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            // Регистрация DbContext
            ConfigureDbContext(services, configuration);

            // Получаем адрес API из конфигурации или устанавливаем значение по умолчанию
            string apiBaseAddress = configuration["ApiBaseAddress"] ?? "http://localhost:5275";

            // Регистрация сервисов
            services.AddScoped<GetAllTransactionsService>(provider =>
                new GetAllTransactionsService(apiBaseAddress));
            services.AddScoped<GetTransactionService>(provider =>
                new GetTransactionService(apiBaseAddress));
            services.AddScoped<CreateTransactionService>(provider =>
                new CreateTransactionService(apiBaseAddress));
            services.AddScoped<UpdateTransactionService>(provider =>
                new UpdateTransactionService(apiBaseAddress));
            services.AddScoped<DeleteTransactionService>(provider =>
                new DeleteTransactionService(apiBaseAddress));

            // Регистрация ViewModels
            services.AddScoped<MainWindowViewModel>();  // MainWindowViewModel
            services.AddScoped<AddTransactionViewModel>();  // AddTransactionViewModel

            // Регистрация окон
            services.AddScoped<MainWindow>();  // MainWindow
            services.AddScoped<AddTransactionWindow>();  // AddTransactionWindow

            // Построение ServiceProvider
            ServiceProvider = services.BuildServiceProvider();
        }

        private void ConfigureDbContext(ServiceCollection services, IConfiguration configuration)
        {
            // Получение строки подключения
            var connectionString = configuration.GetConnectionString("DefaultConnection");

            // Проверка, что строка подключения присутствует
            if (string.IsNullOrEmpty(connectionString))
            {
                throw new InvalidOperationException("Connection string is missing or invalid.");
            }

            // Регистрация DbContext
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseMySql(connectionString, new MySqlServerVersion(new Version(9, 1, 0))));
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            // Открытие главного окна
            var mainWindow = ServiceProvider.GetRequiredService<MainWindow>();
            mainWindow.Show();
        }
    }
}
