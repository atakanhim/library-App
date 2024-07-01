
namespace libraryApp.Application.Exceptions
{
    public class IdParseErrorException : Exception
    {
        public IdParseErrorException() : base("String To Guid Parse Error")
        {
        }

        public IdParseErrorException(string? message) : base(message)
        {
        }
    }
 
}
