namespace Exceptions.Vs.ResultPattern.Exceptions
{
    public class BookNotFoundException : NotFoundException
    {
        public BookNotFoundException(int bookId)
            : base($"Book with with id = {bookId} was not found")
        {
        }
    }
}
