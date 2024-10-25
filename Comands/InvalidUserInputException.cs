namespace ConsoleApp1.Comands;

public class InvalidUserInputException : Exception
{
        public InvalidUserInputException(string message) : base(message)
        {
        }
        
}