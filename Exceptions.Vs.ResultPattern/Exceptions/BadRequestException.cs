namespace Exceptions.Vs.ResultPattern.Exceptions
{
    public class BadRequestException : Exception
    {
        public BadRequestException(string message, Error[]? errors = null)
            : base(message)
        {
            Errors = errors ?? [];
        }

        public Error[] Errors { get; }
    }

    public class Error
    {
        public Error(string message)
        {
            Message = message;
        }
        public string Message { get; }
    }
}
