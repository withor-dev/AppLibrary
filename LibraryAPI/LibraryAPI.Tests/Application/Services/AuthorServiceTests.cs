using LibraryAPI.Application.DTOs;
using LibraryAPI.Application.Services;
using LibraryAPI.Domain.Entities;
using LibraryAPI.Domain.Interfaces;
using Moq;

namespace LibraryAPI.Tests.Application.Services;

public class AuthorServiceTests
{
    private readonly Mock<IBaseRepository<Author>> _authorRepositoryMock;
    private readonly AuthorService _authorService;

    public AuthorServiceTests()
    {
        _authorRepositoryMock = new Mock<IBaseRepository<Author>>();
        _authorService = new AuthorService(_authorRepositoryMock.Object);
    }

    [Fact]
    public async Task AddAsync_ShouldAddAuthor_AndReturnViewModel()
    {
        var dto = new AuthorCreateDTO("George R. R. Martin");

        var result = await _authorService.AddAsync(dto);

        Assert.NotNull(result);
        Assert.Equal("George R. R. Martin", result.Name);

        _authorRepositoryMock.Verify(repo => repo.AddAsync(It.IsAny<Author>()), Times.Once);
        _authorRepositoryMock.Verify(repo => repo.SaveChangesAsync(), Times.Once);
    }

    [Fact]
    public async Task UpdateAsync_ShouldThrowException_WhenAuthorNotFound()
    {
        var dto = new AuthorUpdateDTO(Guid.NewGuid(), "Novo Nome");

        _authorRepositoryMock.Setup(repo => repo.GetByIdAsync(dto.Id))
            .ReturnsAsync((Author)null);

        var exception = await Assert.ThrowsAsync<ArgumentException>(() => _authorService.UpdateAsync(dto));

        Assert.Equal("Autor não encontrado.", exception.Message);

        _authorRepositoryMock.Verify(repo => repo.Update(It.IsAny<Author>()), Times.Never);
    }

    [Fact]
    public async Task DeleteAsync_ShouldCallDelete_WhenAuthorExists()
    {
        var authorId = Guid.NewGuid();
        var author = new Author("J.K. Rowling");

        _authorRepositoryMock.Setup(repo => repo.GetByIdAsync(authorId))
            .ReturnsAsync(author);

        await _authorService.DeleteAsync(authorId);

        _authorRepositoryMock.Verify(repo => repo.Delete(author), Times.Once);
        _authorRepositoryMock.Verify(repo => repo.SaveChangesAsync(), Times.Once);
    }
}