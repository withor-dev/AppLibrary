using LibraryAPI.Application.DTOs;
using LibraryAPI.Application.Services;
using LibraryAPI.Domain.Entities;
using LibraryAPI.Domain.Interfaces;
using Moq;

namespace LibraryAPI.Tests.Application.Services;

public class BookServiceTests
{
    private readonly Mock<IBookRepository> _bookRepositoryMock;
    private readonly Mock<IBaseRepository<Author>> _authorRepositoryMock;
    private readonly Mock<IBaseRepository<Genre>> _genreRepositoryMock;
    private readonly BookService _bookService;

    public BookServiceTests()
    {
        _bookRepositoryMock = new Mock<IBookRepository>();
        _authorRepositoryMock = new Mock<IBaseRepository<Author>>();
        _genreRepositoryMock = new Mock<IBaseRepository<Genre>>();

        _bookService = new BookService(
            _bookRepositoryMock.Object,
            _authorRepositoryMock.Object,
            _genreRepositoryMock.Object);
    }

    [Fact]
    public async Task AddAsync_ShouldAddBook_WhenAuthorAndGenreExist()
    {
        var dto = new BookCreateDTO("O Senhor dos Anéis", Guid.NewGuid(), Guid.NewGuid());

        var author = new Author("J.R.R. Tolkien");
        var genre = new Genre("Fantasia");

        _authorRepositoryMock.Setup(repo => repo.GetByIdAsync(dto.AuthorId))
            .ReturnsAsync(author);

        _genreRepositoryMock.Setup(repo => repo.GetByIdAsync(dto.GenreId))
            .ReturnsAsync(genre);

        var result = await _bookService.AddAsync(dto);

        Assert.NotNull(result);
        Assert.Equal("O Senhor dos Anéis", result.Title);
        Assert.Equal("J.R.R. Tolkien", result.AuthorName);
        Assert.Equal("Fantasia", result.GenreName);

        _bookRepositoryMock.Verify(repo => repo.AddAsync(It.IsAny<Book>()), Times.Once);

        _bookRepositoryMock.Verify(repo => repo.SaveChangesAsync(), Times.Once);
    }

    [Fact]
    public async Task AddAsync_ShouldThrowException_WhenAuthorDoesNotExist()
    {
        var dto = new BookCreateDTO("Livro Sem Autor", Guid.NewGuid(), Guid.NewGuid());

        _authorRepositoryMock.Setup(repo => repo.GetByIdAsync(dto.AuthorId))
            .ReturnsAsync((Author)null);

        var exception = await Assert.ThrowsAsync<ArgumentException>(() => _bookService.AddAsync(dto));

        Assert.Equal("Autor não encontrado.", exception.Message);

        _bookRepositoryMock.Verify(repo => repo.AddAsync(It.IsAny<Book>()), Times.Never);
    }
}