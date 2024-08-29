using Ardalis.Result;
using Exceptions.Vs.ResultPattern.Exceptions;

namespace Exceptions.Vs.ResultPattern.Books
{
    public class BooksService
    {
        List<Book> _books = new List<Book>();

        public BooksService()
        {
            _books.Add(new Book(1, "The Fellowship of the Ring", "J.R.R. Tolkien"));
            _books.Add(new Book(2, "The Two Towers", "J.R.R. Tolkien"));
            _books.Add(new Book(3, "The Return of the King", "J.R.R. Tolkien"));
        }

        public Book AddBookOrThrow(AddBookRequest request)
        {
            var errors = ValidateBook(request);

            if (errors.Any())
            {
                throw new BookValidationException(errors);
            }

            var bookId = _books.Select(book => book.Id).Max() + 1;
            var book = new Book(bookId, request.Title, request.Author);
            _books.Add(book);

            return Result<Book>.Created(book);
        }

        public Result<Book> AddBookOrFail(AddBookRequest request)
        {
            var errors = ValidateBook(request);

            if (errors.Any())
            {
                var validationErrors = errors.Select(error => new ValidationError(error.Message)).ToList();
                return Result<Book>.Invalid(validationErrors);
            }

            var bookId = _books.Select(book => book.Id).Max() + 1;
            var book = new Book(bookId, request.Title, request.Author);
            _books.Add(book);

            return Result<Book>.Created(book);
        }

        public Book GetBookOrThrow(int bookId)
        {
            var book = _books.FirstOrDefault(book => book.Id == bookId);

            if (book is null)
            {
                throw new BookNotFoundException(bookId);
            }

            return book;
        }

        public Result<Book> GetBookOrFail(int bookId)
        {
            var book = _books.FirstOrDefault(book => book.Id == bookId);

            if (book is null)
            {
                return Result<Book>.NotFound();
            }

            return book;
        }

        private static Error[] ValidateBook(AddBookRequest request)
        {
            List<Error> errors = new List<Error>();

            if (string.IsNullOrWhiteSpace(request.Title))
            {
                errors.Add(new Error("Title is required"));
            }

            if (string.IsNullOrWhiteSpace(request.Author))
            {
                errors.Add(new Error("Author is required"));
            }

            return [.. errors];
        }

        private static Result<AddBookRequest> ValidateBookResult(AddBookRequest request)
        {
            List<ValidationError> errors = new List<ValidationError>();

            if (string.IsNullOrWhiteSpace(request.Title))
            {
                errors.Add(new ValidationError("Title is required"));
            }

            if (string.IsNullOrWhiteSpace(request.Author))
            {
                errors.Add(new ValidationError("Author is required"));
            }

            return errors.Any()
                ? Result<AddBookRequest>.Invalid(errors)
                : Result<AddBookRequest>.Success(request);
        }

    }
}
