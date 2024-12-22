using System.Windows;
using Microsoft.Extensions.DependencyInjection;
using WpfApp.ViewModels;

namespace WpfApp.Views
{

    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = App.ServiceProvider.GetRequiredService<MainWindowViewModel>();
        }

    }
}