using Ardalis.Result;

namespace Exceptions.Vs.ResultPattern.Results
{
    internal static class ApiResults
    {
        internal static Microsoft.AspNetCore.Http.IResult Problem<T>(Result<T> result)
        {
            if (result.IsSuccess)
            {
                throw new InvalidOperationException();
            }

            return Microsoft.AspNetCore.Http.Results.Problem(
                detail: GetDetail(result),
                title: GetTitle(result),
                statusCode: GetStatusCode(result),
                extensions: GetErrors(result));

            static string GetTitle(Result<T> result)
            {
                return result.Status switch
                {
                    ResultStatus.NotFound => "Not Found",
                    ResultStatus.Invalid => "Bad Request",
                    _ => "Internal Server Error"
                };
            }

            static string GetDetail(Result<T> result)
            {
                return result.Status switch
                {
                    ResultStatus.NotFound => "The requested resource was not found.",
                    ResultStatus.Invalid => "The request was invalid.",
                    _ => "An error occurred while processing the request."
                };
            }

            static int GetStatusCode(Result<T> result)
            {
                return result.Status switch
                {
                    ResultStatus.NotFound => 404,
                    ResultStatus.Invalid => 400,
                    _ => 500
                };
            }

            static Dictionary<string, object?>? GetErrors(Result<T> result)
            {
                if (!result.ValidationErrors.Any())
                {
                    return null;
                }

                return new Dictionary<string, object?>
                {
                    ["validationErrors"] = result.ValidationErrors
                };
            }
        }
    }
}
