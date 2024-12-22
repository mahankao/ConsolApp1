using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using WpfApp.Services;
using WpfApp1.Services;
using WpfApp1.Services.WpfApp;
using WpfApp.Views;

namespace WpfApp.ViewModels
{
    public class MainWindowViewModel : BaseViewModel
    {
        private readonly GetAllTransactionsService _getAllService;
        private readonly CreateTransactionService _createService;
        private readonly UpdateTransactionService _updateService;
        private readonly DeleteTransactionService _deleteService;
        
        public ObservableCollection<Transaction> Transactions { get; set; }

        public ICommand LoadTransactionsCommand { get; }
        public ICommand AddTransactionCommand { get; }
        public ICommand UpdateTransactionCommand { get; }
        public ICommand DeleteTransactionCommand { get; }

        private Transaction _selectedTransaction;

        public Transaction SelectedTransaction
        {
            get => _selectedTransaction;
            set
            {
                if (_selectedTransaction != value)
                {
                    _selectedTransaction = value;
                    // Уведомляем об изменении свойства
                    OnPropertyChanged();
                }
            }
        }

        public MainWindowViewModel(
            GetAllTransactionsService getAllService,
            CreateTransactionService createService,
            UpdateTransactionService updateService,
            DeleteTransactionService deleteService)
        {
            _getAllService = getAllService;
            _createService = createService;
            _updateService = updateService;
            _deleteService = deleteService;

            Transactions = new ObservableCollection<Transaction>();

            LoadTransactionsCommand = new RelayCommandAsync(LoadTransactionsAsync);
            AddTransactionCommand = new RelayCommandAsync(AddTransactionAsync);
            UpdateTransactionCommand = new RelayCommandAsync(UpdateTransactionAsync);
            DeleteTransactionCommand = new RelayCommandAsync(DeleteTransactionAsync);

            // Автоматическая загрузка данных
            Task.Run(async () => await LoadTransactionsAsync());
        }

        private async Task LoadTransactionsAsync()
        {
            try
            {
                var transactions = await _getAllService.ExecuteAsync();
                // Обновление коллекции в UI-потоке
                Application.Current.Dispatcher.Invoke(() =>
                {
                    Transactions.Clear();
                    foreach (var transaction in transactions)
                    {
                        Transactions.Add(transaction);
                    }
                });
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading transactions: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private async Task AddTransactionAsync()
        {
            // Создаем ViewModel для окна добавления транзакции
            var addViewModel = new AddTransactionViewModel();

            // Создаем окно, передаем ViewModel в качестве параметра
            var addWindow = new AddTransactionWindow(addViewModel);

            // Подписываемся на событие завершения сохранения
            addViewModel.OnSaveCompleted += async () =>
            {
                // Когда транзакция успешно сохранена, выполняем сохранение через сервис
                await _createService.ExecuteAsync(addViewModel.NewTransaction);
                await LoadTransactionsAsync();  // Перезагружаем данные
            };

            // Показываем окно и ждем его закрытия
            addWindow.ShowDialog();
        }

        private async Task UpdateTransactionAsync()
        {
            if (SelectedTransaction != null)
            {
                // Создаем ViewModel с текущей выбранной транзакцией для редактирования
                var updateViewModel = new AddTransactionViewModel(SelectedTransaction);

                // Создаем окно для редактирования транзакции
                var updateWindow = new AddTransactionWindow(updateViewModel);

                // Подписываемся на событие завершения сохранения
                updateViewModel.OnSaveCompleted += async () =>
                {
                    // Когда транзакция успешно обновлена, выполняем обновление через сервис
                    await _updateService.ExecuteAsync(SelectedTransaction.Id, updateViewModel.NewTransaction);
                    await LoadTransactionsAsync();  // Перезагружаем данные
                };

                // Показываем окно и ждем его закрытия
                updateWindow.ShowDialog();
            }
        }



        private async Task DeleteTransactionAsync()
        {
            if (SelectedTransaction != null)
            {
                await _deleteService.ExecuteAsync(SelectedTransaction.Id);
                await LoadTransactionsAsync(); // Перезагрузить данные после удаления
            }
        }
    }
}
