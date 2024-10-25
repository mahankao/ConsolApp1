namespace ConsoleApp1.Comands
{
    public class InvalidDateRangeException : Exception
    {
        public InvalidDateRangeException(string message) : base(message) 
        { 
        }
    }
}