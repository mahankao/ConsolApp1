using System;
using System.Windows;
using WpfApp.ViewModels;

namespace WpfApp.Views
{
    public partial class AddTransactionWindow : Window
    {
        public AddTransactionViewModel ViewModel { get; }

        public AddTransactionWindow(AddTransactionViewModel viewModel)
        {
            InitializeComponent();
            ViewModel = viewModel ?? throw new ArgumentNullException(nameof(viewModel));
            DataContext = ViewModel;

            // Подписка на событие завершения сохранения
            ViewModel.OnSaveCompleted += OnSaveCompleted;
        }

        private void OnSaveCompleted()
        {
            // Закрываем окно при успешном сохранении
            DialogResult = true;
            Close();
        }

        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);
            // Убираем подписку на событие, чтобы избежать утечек памяти
            ViewModel.OnSaveCompleted -= OnSaveCompleted;
        }
    }
}