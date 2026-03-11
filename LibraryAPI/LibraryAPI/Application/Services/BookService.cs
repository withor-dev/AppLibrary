using LibraryAPI.Application.DTOs;
using LibraryAPI.Application.ViewModels;
using LibraryAPI.Domain.Entities;
using LibraryAPI.Domain.Interfaces;

namespace LibraryAPI.Application.Services;

public class BookService : IBookService
{
    private readonly IBookRepository _bookRepository;
    private readonly IBaseRepository<Author> _authorRepository;
    private readonly IBaseRepository<Genre> _genreRepository;

    public BookService(
        IBookRepository bookRepository,
        IBaseRepository<Author> authorRepository,
        IBaseRepository<Genre> genreRepository)
    {
        _bookRepository = bookRepository;
        _authorRepository = authorRepository;
        _genreRepository = genreRepository;
    }

    public async Task<IEnumerable<BookViewModel>> GetAllAsync()
    {
        var books = await _bookRepository.GetBooksWithDetailsAsync();

        return books.Select(b => new BookViewModel
        {
            Id = b.Id,
            Title = b.Title,
            AuthorName = b.Author.Name,
            GenreName = b.Genre.Name
        });
    }

    public async Task<BookViewModel?> GetByIdAsync(Guid id)
    {
        var book = await _bookRepository.GetByIdAsync(id);
        if (book == null) return null;

        var author = await _authorRepository.GetByIdAsync(book.AuthorId);
        var genre = await _genreRepository.GetByIdAsync(book.GenreId);

        return new BookViewModel
        {
            Id = book.Id,
            Title = book.Title,
            AuthorName = author?.Name ?? "Sem autor",
            GenreName = genre?.Name ?? "Sem gênero"
        };
    }

    public async Task<BookViewModel> AddAsync(BookCreateDTO dto)
    {
        var author = await _authorRepository.GetByIdAsync(dto.AuthorId);
        if (author == null) throw new ArgumentException("Autor não encontrado.");

        var genre = await _genreRepository.GetByIdAsync(dto.GenreId);
        if (genre == null) throw new ArgumentException("Gênero não encontrado.");

        var book = new Book(dto.Title, dto.AuthorId, dto.GenreId);

        await _bookRepository.AddAsync(book);
        await _bookRepository.SaveChangesAsync();

        return new BookViewModel
        {
            Id = book.Id,
            Title = book.Title,
            AuthorName = author.Name,
            GenreName = genre.Name
        };
    }

    public async Task UpdateAsync(BookUpdateDTO dto)
    {
        var book = await _bookRepository.GetByIdAsync(dto.Id);
        if (book == null) throw new ArgumentException("Livro não encontrado.");

        var author = await _authorRepository.GetByIdAsync(dto.AuthorId);
        if (author == null) throw new ArgumentException("Autor não encontrado.");

        var genre = await _genreRepository.GetByIdAsync(dto.GenreId);
        if (genre == null) throw new ArgumentException("Gênero não encontrado.");

        book.UpdateDetails(dto.Title, dto.AuthorId, dto.GenreId);

        _bookRepository.Update(book);
        await _bookRepository.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid id)
    {
        var book = await _bookRepository.GetByIdAsync(id);
        if (book == null) throw new ArgumentException("Livro não encontrado.");

        _bookRepository.Delete(book);
        await _bookRepository.SaveChangesAsync();
    }
}