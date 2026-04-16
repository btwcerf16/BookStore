namespace BookStore.Contracts
{
    public record BooksResponse(
        Guid Id,
        string Title,
        string Description,
        decimal Price
        );
    public record BooksRequest(
        string Title,
        string Description,
        decimal Price
        );
}
