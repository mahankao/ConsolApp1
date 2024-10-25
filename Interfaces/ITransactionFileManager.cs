namespace ConsoleApp1.Interfaces;

public interface ITransactionFileManager
{
    Task SaveTransactionsToFileAsync(string filePath);
    Task LoadTransactionsFromFileAsync(string filePath);
}