using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using ConsoleApp1.Interfaces;

namespace WpfApp.ViewModels
{
    public class AddTransactionViewModel : BaseViewModel
    {
        private Transaction _newTransaction;
        private string _category;
        private decimal? _amount;
        private DateTime? _date;
        private OperationType? _type;

        public event Action OnSaveCompleted;

        public Transaction NewTransaction
        {
            get => _newTransaction;
            set
            {
                _newTransaction = value;
                OnPropertyChanged();
            }
        }

        public string Category
        {
            get => _category;
            set
            {
                _category = value;
                OnPropertyChanged();
            }
        }

        public decimal? Amount
        {
            get => _amount;
            set
            {
                _amount = value;
                OnPropertyChanged();
            }
        }

        public DateTime? Date
        {
            get => _date;
            set
            {
                _date = value;
                OnPropertyChanged();
            }
        }

        public OperationType? Type
        {
            get => _type;
            set
            {
                _type = value;
                OnPropertyChanged();
            }
        }

        public IEnumerable<OperationType> OperationTypes => Enum.GetValues(typeof(OperationType)).Cast<OperationType>();

        public ICommand SaveCommand { get; }

        public AddTransactionViewModel(Transaction transaction = null)
        {
            if (transaction != null)
            {
                NewTransaction = transaction;
                Category = transaction.Category;
                Amount = transaction.Amount;
                Date = transaction.Date;
                Type = transaction.Type;
            }
            else
            {
                NewTransaction = new Transaction();
                Date = DateTime.Now; // Default to current date
            }

            SaveCommand = new RelayCommand(SaveTransaction);
        }

        private void SaveTransaction(object parameter)
        {
            try
            {
                if (!Amount.HasValue || Amount.Value <= 0)
                {
                    MessageBox.Show("Invalid amount. Please enter a valid number greater than 0.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                if (!Date.HasValue)
                {
                    MessageBox.Show("Please select a valid date.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                if (Type == null)
                {
                    MessageBox.Show("Please select a transaction type.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                NewTransaction.Date = Date.Value;
                NewTransaction.Type = Type.Value;
                NewTransaction.Category = string.IsNullOrWhiteSpace(Category) ? "Uncategorized" : Category;
                NewTransaction.Amount = Amount.Value;

                OnSaveCompleted?.Invoke();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An unexpected error occurred: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
