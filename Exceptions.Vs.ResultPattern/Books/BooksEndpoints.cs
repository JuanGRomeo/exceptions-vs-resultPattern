namespace Exceptions.Vs.ResultPattern.Books
{
    internal static class BooksEndpoints
    {
        internal static void Map(IEndpointRouteBuilder app)
        {
            #region V1 Endpoints
            app.MapPost("v1/books", (AddBookRequest request, BooksService booksService) =>
            {
                var bookId = booksService.AddBookOrThrow(request);

                return Microsoft.AspNetCore.Http.Results.CreatedAtRoute("GetBookV1", new { id = bookId }, bookId);

            })
            .WithName("PostBookV1")
            .WithOpenApi();

            app.MapGet("v1/books/{id}", (int id, BooksService booksService) =>
            {
                var book = booksService.GetBookOrThrow(id);
                return Microsoft.AspNetCore.Http.Results.Ok(book);
            })
            .WithName("GetBookV1")
            .WithOpenApi();
            #endregion

            #region V2 Endpoints
            app.MapPost("v2/books", (AddBookRequest request, BooksService booksService) =>
            {
                var result = booksService.AddBookOrFail(request);

                return result.IsSuccess
                    ? Microsoft.AspNetCore.Http.Results.CreatedAtRoute("GetBookV2", new { id = result.Value.Id }, result.Value)
                    : Results.ApiResults.Problem(result);

            })
            .WithName("PostBookV2")
            .WithOpenApi();

            app.MapGet("v2/books/{id}", (int id, BooksService booksService) =>
            {
                var result = booksService.GetBookOrFail(id);

                return result.IsSuccess
                    ? Microsoft.AspNetCore.Http.Results.Ok(result.Value)
                    : Results.ApiResults.Problem(result);
            })
            .WithName("GetBookV2")
            .WithOpenApi();
            #endregion
        }
    }

    internal record Book(int Id, string Title, string Author);
}
