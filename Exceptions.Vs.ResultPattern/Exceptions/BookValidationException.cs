namespace Exceptions.Vs.ResultPattern.Exceptions
{
    public class BookValidationException : BadRequestException
    {
        public BookValidationException(Error[] errors)
            : base("User validation failed", errors)
        {
        }
    }
}
