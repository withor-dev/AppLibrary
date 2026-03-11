namespace LibraryAPI.Application.ViewModels;

public class BookViewModel
{
    public Guid Id { get; set; }
    public required string Title { get; set; }
    public required string AuthorName { get; set; }
    public required string GenreName { get; set; }
}