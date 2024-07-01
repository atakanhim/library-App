
namespace libraryApp.Application.Exceptions
{
    public class UserCreateFailedException:Exception
    {
        public UserCreateFailedException() : base("Kullanıcı oluşturulurken beklenmeyen bir hatayla karşılaşıldı!") // default olarak mesaj bu
        {
        }

        public UserCreateFailedException(string? message) : base(message)
        {
        }

        public UserCreateFailedException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}
