using LibraryAPI.Application.Services;
using LibraryAPI.Domain.Entities;
using LibraryAPI.Domain.Interfaces;
using Moq;

namespace LibraryAPI.Tests.Application.Services;

public class GenreServiceTests
{
    private readonly Mock<IBaseRepository<Genre>> _genreRepositoryMock;
    private readonly GenreService _genreService;

    public GenreServiceTests()
    {
        _genreRepositoryMock = new Mock<IBaseRepository<Genre>>();
        _genreService = new GenreService(_genreRepositoryMock.Object);
    }

    [Fact]
    public async Task GetAllAsync_ShouldReturnListOfGenreViewModels()
    {
        var genresList = new List<Genre>
        {
            new Genre("Ficção Científica"),
            new Genre("Romance")
        };

        _genreRepositoryMock.Setup(repo => repo.GetAllAsync())
            .ReturnsAsync(genresList);

        var result = await _genreService.GetAllAsync();

        Assert.NotNull(result);
        Assert.Equal(2, result.Count());
        Assert.Contains(result, g => g.Name == "Ficção Científica");
    }

    [Fact]
    public async Task GetByIdAsync_ShouldReturnViewModel_WhenGenreExists()
    {
        var genreId = Guid.NewGuid();
        var genre = new Genre("Terror");

        _genreRepositoryMock.Setup(repo => repo.GetByIdAsync(genreId))
            .ReturnsAsync(genre);

        var result = await _genreService.GetByIdAsync(genreId);

        Assert.NotNull(result);
        Assert.Equal("Terror", result.Name);
    }

    [Fact]
    public async Task GetByIdAsync_ShouldReturnNull_WhenGenreDoesNotExist()
    {
        var genreId = Guid.NewGuid();

        _genreRepositoryMock.Setup(repo => repo.GetByIdAsync(genreId))
            .ReturnsAsync((Genre)null);

        var result = await _genreService.GetByIdAsync(genreId);

        Assert.Null(result);
    }
}