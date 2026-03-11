namespace LibraryAPI.Domain.Entities;

public class Book : Entity
{
    public string Title { get; private set; }
    public Guid AuthorId { get; private set; }
    public Author Author { get; private set; }
    public Guid GenreId { get; private set; }
    public Genre Genre { get; private set; }

    public Book(string title, Guid authorId, Guid genreId)
    {
        UpdateDetails(title, authorId, genreId);
    }

    public void UpdateDetails(string title, Guid authorId, Guid genreId)
    {
        if (string.IsNullOrWhiteSpace(title))
            throw new ArgumentException("O título do livro é obrigatório.");
        if (authorId == Guid.Empty)
            throw new ArgumentException("O ID do autor é inválido.");
        if (genreId == Guid.Empty)
            throw new ArgumentException("O ID do gênero é inválido.");

        Title = title;
        AuthorId = authorId;
        GenreId = genreId;
    }
}